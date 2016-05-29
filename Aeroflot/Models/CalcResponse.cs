using System.Collections.Generic;

namespace Aeroflot.Models
{
    public class CalcResponse
    {
        public string Error = string.Empty;
        public List<Flight> Flights = new List<Flight>();
    }
}