using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aeroflot.Models;
using Aeroflot.Parser;
using Aeroflot.Validation;

namespace Aeroflot.Controllers
{
    public class CalcController : ApiController
    {
        // GET api/calc/SEK/MOW/01.01.2018/02.02.2018        
        [Route("api/calc/{origin}/{destination}/{departureDate}/{returnDate}")]
        public CalcResponse Get(string origin, string destination, string departureDate, string returnDate)
        {
            CalcResponse calcResponse = new CalcResponse();
            ValidateCalcRequest validation = new ValidateCalcRequest(origin, destination, departureDate, returnDate);
            validation.NumAdults = 1;
            string validateResult = validation.Validate();
            if (validation.RequestValidated)
            {
                CalcRequest request = validation.GetCalcRequest();
                ISiteRequests aeroflotRequests = new AeroflotRequests();
                SiteParser parser = new SiteParser(new AeroflotParser(), aeroflotRequests);
                parser.CalcRequestData = request;
                parser.GetCalc();
                calcResponse = parser.GetCalcResponse();
            }
            else
                calcResponse.Error = validateResult;
            return calcResponse;
        }

        // GET api/calc/SEK/MOW/01.01.2018/02.02.2018/1        
        [Route("api/calc/{origin}/{destination}/{departureDate}/{returnDate}/{numAdults}")]
        public CalcResponse Get(string origin, string destination, string departureDate, string returnDate, string numAdults)
        {
            CalcResponse calcResponse = new CalcResponse();
            ValidateCalcRequest validation = new ValidateCalcRequest(origin, destination, departureDate, returnDate, numAdults);
            validation.NumAdults = 1;
            string validateResult = validation.Validate();
            if (validation.RequestValidated)
            {
                CalcRequest request = validation.GetCalcRequest();
                ISiteRequests aeroflotRequests = new AeroflotRequests();
                SiteParser parser = new SiteParser(new AeroflotParser(), aeroflotRequests);
                parser.CalcRequestData = request;
                parser.GetCalc();
                calcResponse = parser.GetCalcResponse();
            }
            else
                calcResponse.Error = validateResult;
            return calcResponse;
        }
    }
}
