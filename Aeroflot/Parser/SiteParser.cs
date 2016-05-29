using System;
using Aeroflot.Models;

namespace Aeroflot.Parser
{
    public class SiteParser
    {
        private ISiteRequests requests;
        private ISiteParser parser;
        private string rawResponse = string.Empty;
        
        public SiteParser(ISiteParser parser, ISiteRequests requests)
        {
            this.parser = parser;
            this.requests = requests;
        }

        public CalcRequest CalcRequestData { get; set; }

        public CalcResponse GetCalcResponse()
        {
            return parser.ParseCalc(rawResponse);
        }

        public void GetCalc()
        {
            rawResponse = requests.GetCalc(CalcRequestData);
        }
    }
}