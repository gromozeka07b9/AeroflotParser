using System;
using Aeroflot.Models;
using Aeroflot.Validation;
using NUnit.Framework;

namespace AeroflotNUnit.Tests
{
    [TestFixture]
    public class TestValidation
    {
        [Test]
        public void TestMust_ValidateRequestDefaultAdultsSuccess()
        {
            //arrange
            ValidateCalcRequest validation = new ValidateCalcRequest("MOW", "LED", "01.08.2016", "01.09.2016");
            validation.NumAdults = 1;

            //act
            string validateResult = validation.Validate();

            //assert
            Assert.IsTrue(string.IsNullOrEmpty(validateResult) && validation.RequestValidated);

            /*validation.Origin = "MOW";
            validation.Destination = "LED";
            validation.DepartureDate = "01.08.2016";
            validation.ReturnDate = "01.09.2016";

            CalcRequest request = new CalcRequest();
            request.Origin = "MOW";
            request.Destination = "LED";
            request.DepartureDate = DateTime.Parse("01.08.2016");
            request.ReturnDate = DateTime.Parse("01.09.2016");
            request.NumAdults = 1;
            request.NumChildren = 0;
            request.NumInfants = 0;

            //act
            string validateResult = request.Validate();

            //assert
            Assert.IsTrue(string.IsNullOrEmpty(validateResult));*/
        }

        [Test]
        public void TestMust_ValidateRequestWithoutAdults()
        {
            //arrange
            ValidateCalcRequest validation = new ValidateCalcRequest("MOW", "LED", "01.08.2016", "01.09.2016");

            //act
            string validateResult = validation.Validate();

            //assert
            Assert.IsTrue(validateResult.Contains("NumAdults > 0 required") && !validation.RequestValidated);
            //arrange
            /*CalcRequest request = new CalcRequest();
            request.Origin = "";
            request.Destination = "LED";
            request.DepartureDate = DateTime.Parse("01.08.2016");
            request.ReturnDate = DateTime.Parse("01.09.2016");
            request.NumAdults = 1;
            request.NumChildren = 0;
            request.NumInfants = 0;

            //act
            string validateResult = request.Validate();

            //assert
            Assert.IsTrue(validateResult.Contains("Origin must be 3 symbols"));*/
        }

        [Test]
        public void TestMust_ValidateRequestIncorrectOrigin()
        {
            //arrange
            ValidateCalcRequest validation = new ValidateCalcRequest("", "LED", "01.08.2016", "01.09.2016");

            //act
            string validateResult = validation.Validate();

            //assert
            Assert.IsTrue(validateResult.Contains("Origin must be 3 symbols") && !validation.RequestValidated);
        }

        [Test]
        public void TestMust_ValidateRequestAndCreateCalcRequest()
        {
            //arrange
            ValidateCalcRequest validation = new ValidateCalcRequest("MOW", "LED", "01.08.2016", "01.09.2016");
            validation.NumAdults = 1;

            //act
            validation.Validate();
            CalcRequest request = validation.GetCalcRequest();

            //assert
            Assert.IsTrue(request.origin.Length > 0);
        }

    }
}
