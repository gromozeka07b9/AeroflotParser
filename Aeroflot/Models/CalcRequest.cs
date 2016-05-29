using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Aeroflot.Models
{
    public class CalcRequest
    {
        public string origin { get; set; }
        public string destination { get; set; }
        public DateTime departureDate { get; set; }
        public DateTime returnDate { get; set; }
        public int numAdults { get; set; }
        public int? numChildren { get; set; }
        public int? numInfants { get; set; }

        public CalcRequest(string origin, string destination, DateTime departureDateValidated, DateTime returnDateValidated, int numAdults)
        {
            this.origin = origin;
            this.destination = destination;
            this.departureDate = departureDateValidated;
            this.returnDate = returnDateValidated;
            this.numAdults = numAdults;
            this.numChildren = 0;
            this.numInfants = 0;
        }
    }
}