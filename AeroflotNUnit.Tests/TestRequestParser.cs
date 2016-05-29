using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aeroflot;
using Aeroflot.Models;
using Aeroflot.Parser;
using Aeroflot.Validation;
using NUnit.Framework;

namespace AeroflotNUnit.Tests
{
    [TestFixture]
    public class TestRequestParser
    {
        [Test]
        public void TestMust_ParseCalcResponseSuccess()
        {
            //arrange
            ValidateCalcRequest validation = new ValidateCalcRequest("MOW", "LED", "01.08.2016", "01.09.2016", "1");
            validation.Validate();
            CalcRequest request = validation.GetCalcRequest();

            //act
            //ISiteParser aeroflotParser = new FakeParser();
            ISiteRequests aeroflotRequests = new FakeRequests();
            SiteParser parser = new SiteParser(new AeroflotParser(), aeroflotRequests);
            parser.CalcRequestData = request;
            parser.GetCalc();
            CalcResponse response = parser.GetCalcResponse();

            //assert
            Assert.IsTrue(response != null);

        }

        private class FakeRequests : ISiteRequests
        {
            public FakeRequests()
            {
            }

            public string GetCalc(CalcRequest CalcRequestData)
            {
                return FakeResponses.AeroflotCalc;
            }
        }
    }
}
