﻿// The project released under MS-PL license https://daxeditor.codeplex.com/license

using DaxEditor;
using DaxEditor.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DaxEditorSample.Test
{
    [TestClass]
    public class MeasuresContainerTests
    {
        [TestMethod]
        public void MeasuresFormats()
        {
            string input = Utils.ReadFileFromResources("MeasuresFormats.bim");
            var bim = MeasuresContainer.ParseText(input);
            Assert.IsNotNull(bim.Measures);
            Assert.AreEqual(8, bim.Measures.Count);
            var daxText = bim.GetDaxText();
            Assert.IsNotNull(daxText);

            var measuresFromDax = MeasuresContainer.ParseDaxScript(daxText);
            Assert.AreEqual(bim.Measures.Count, measuresFromDax.Measures.Count);

            var expected = input;
            var actual = measuresFromDax.UpdateMeasures(input);
            WindiffAssert.AreEqualNormalizedXmla(expected, actual);
        }

        [TestMethod]
        public void Bim1100()
        {
            string input = Utils.ReadFileFromResources("BIM1100.bim");
            var bim = MeasuresContainer.ParseText(input);
            Assert.IsNotNull(bim.Measures);
            Assert.AreEqual(3, bim.Measures.Count);
            var daxText = bim.GetDaxText();
            Assert.IsNotNull(daxText);

            var measuresFromDax = MeasuresContainer.ParseDaxScript(daxText);
            Assert.AreEqual(bim.Measures.Count, measuresFromDax.Measures.Count);

            var expected = input;
            var actual = measuresFromDax.UpdateMeasures(input);
            WindiffAssert.AreEqualNormalizedXmla(expected, actual);
        }

        [TestMethod]
        public void M1()
        {
            try
            {
                string input = Utils.ReadFileFromResources("M1.bim");
                var bim = MeasuresContainer.ParseText(input);
                Assert.IsNotNull(bim.Measures);
                Assert.AreEqual(88, bim.Measures.Count);
                var daxText = bim.GetDaxText();
                Assert.IsNotNull(daxText);

                var measuresFromDax = MeasuresContainer.ParseDaxScript(daxText);
                Assert.AreEqual(bim.Measures.Count, measuresFromDax.Measures.Count);

                var expected = input;
                var actual = measuresFromDax.UpdateMeasures(input);
                WindiffAssert.AreEqualNormalizedXmla(expected, actual);
                Assert.Fail("Exception expected");
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "KPI are not yet supported");
            }
        }

        [TestMethod]
        public void M3()
        {
            try
            {
                string input = Utils.ReadFileFromResources("M3.bim");
                var bim = MeasuresContainer.ParseText(input);
                Assert.IsNotNull(bim.Measures);
                Assert.AreEqual(88, bim.Measures.Count);
                var daxText = bim.GetDaxText();
                Assert.IsNotNull(daxText);

                var measuresFromDax = MeasuresContainer.ParseDaxScript(daxText);
                Assert.AreEqual(bim.Measures.Count, measuresFromDax.Measures.Count);

                var expected = input;
                var actual = measuresFromDax.UpdateMeasures(input);
                WindiffAssert.AreEqualNormalizedXmla(expected, actual);
                Assert.Fail("Exception expected");
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "KPI are not yet supported");
            }
        }

        [TestMethod]
        public void Bim1100_Json()
        {
            var text = Utils.ReadFileFromResources("BIM1100_JSON.bim");
            var bim = MeasuresContainer.ParseText(text);
            Assert.IsNotNull(bim.Measures);
            Assert.AreEqual(3, bim.Measures.Count);
            var daxText = bim.GetDaxText();
            Assert.IsNotNull(daxText);

            var measuresFromDax = MeasuresContainer.ParseDaxScript(daxText);
            Assert.AreEqual(bim.Measures.Count, measuresFromDax.Measures.Count);

            var expected = text;
            var actual = measuresFromDax.UpdateMeasures(text);
            WindiffAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void M1_Json()
        {
            string text = Utils.ReadFileFromResources("M1_JSON.bim");
            var bim = MeasuresContainer.ParseText(text);
            Assert.IsNotNull(bim.Measures);
            Assert.AreEqual(68, bim.Measures.Count);//MB FIX 88
            var daxText = bim.GetDaxText();
            Assert.IsNotNull(daxText);
            
            var measuresFromDax = MeasuresContainer.ParseDaxScript(daxText);
            Assert.AreEqual(bim.Measures.Count, measuresFromDax.Measures.Count);

            //kpi not supported
            var document = JsonUtilities.Deserialize(text);
            document?.Model?.Tables?.ForEach(table =>
            {
                table.Measures?.ForEach(measure =>
                {
                    measure.KPI = null;
                });
            });
            var expected = JsonUtilities.Serialize(document);
            var actual = measuresFromDax.UpdateMeasures(text);
            WindiffAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Storecard()
        {
            try
            {
                string input = Utils.ReadFileFromResources("Scorecard.bim");
                var bim = MeasuresContainer.ParseText(input);
                Assert.IsNotNull(bim.Measures);
                //Assert.AreEqual(88, bim.Measures.Count);
                var daxText = bim.GetDaxText();
                Assert.IsNotNull(daxText);

                var measuresFromDax = MeasuresContainer.ParseDaxScript(daxText);
                Assert.AreEqual(bim.Measures.Count, measuresFromDax.Measures.Count);

                var expected = input;
                var actual = measuresFromDax.UpdateMeasures(input);
                WindiffAssert.AreEqualNormalizedXmla(expected, actual);
                Assert.Fail("Exception expected");
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "KPI are not yet supported");
            }
        }

        [TestMethod]
        public void Storecard_Json()
        {
            var text = Utils.ReadFileFromResources("Scorecard_JSON.bim");
            var bim = MeasuresContainer.ParseText(text);
            Assert.IsNotNull(bim.Measures);
            var daxText = bim.GetDaxText();
            Assert.IsNotNull(daxText);

            var measuresFromDax = MeasuresContainer.ParseDaxScript(daxText);
            Assert.AreEqual(bim.Measures.Count, measuresFromDax.Measures.Count);

            var document = JsonUtilities.Deserialize(text);
            //kpi not supported
            document?.Model?.Tables?.ForEach(table =>
            {
                table.Measures?.ForEach(measure =>
                {
                    measure.KPI = null;
                });
            });
            var expected = JsonUtilities.Serialize(document);
            var actual = measuresFromDax.UpdateMeasures(text);
            WindiffAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NewDaxModel_Json()
        {
            var text = Utils.ReadFileFromResources("NewDaxModel_JSON.bim");
            var bim = MeasuresContainer.ParseText(text);
            Assert.IsNotNull(bim.Measures);
            var daxText = bim.GetDaxText();
            Assert.IsNotNull(daxText);

            var measuresFromDax = MeasuresContainer.ParseDaxScript(daxText);
            Assert.AreEqual(bim.Measures.Count, measuresFromDax.Measures.Count);

            var document = JsonUtilities.Deserialize(text);
            var expected = JsonUtilities.Serialize(document);
            var actual = measuresFromDax.UpdateMeasures(text);
            WindiffAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NewMeasuresFuncs_Json()
        {
            var text = Utils.ReadFileFromResources("NewMeasuresFuncs_JSON.bim");
            var bim = MeasuresContainer.ParseText(text);
            Assert.IsNotNull(bim.Measures);
            var daxText = bim.GetDaxText();
            Assert.IsNotNull(daxText);

            var measuresFromDax = MeasuresContainer.ParseDaxScript(daxText);
            Assert.AreEqual(bim.Measures.Count, measuresFromDax.Measures.Count);

            var document = JsonUtilities.Deserialize(text);
            var expected = JsonUtilities.Serialize(document);
            var actual = measuresFromDax.UpdateMeasures(text);
            WindiffAssert.AreEqual(expected, actual);
        }
    }
}