//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Simulations;

//namespace UnitTests.Simulations
//{
//    [TestClass]
//    public class EraReportGeneratorTests
//    {
//        private class IEraInfoMock : IEraInfo
//        {
//            public int Era { get; set; }
//            public int NumberOfEras { get; set; }

//            public double BestFitness { get; set; }
//            public double ControlFitness { get; set; }
//        }

//        [TestMethod]
//        public void EraReportComposerTest()
//        {
//            // if
//            var eraInfo = new IEraInfoMock()
//            {
//                Era = 10,
//                NumberOfEras = 2000,
//                BestFitness = 348.21,
//                ControlFitness = 734.45
//            };

//            string expectedReport =
//                "Era: 10/2000\r\n" +
//                "Best fitness: 348.21\r\n" +
//                "Control fitness: 734.45\r\n";

//            // when
//            var report = EraReportGenerator.Report(eraInfo);

//            // then
//            Assert.IsTrue(report.StartsWith(expectedReport));
//        }
//    }
//}
