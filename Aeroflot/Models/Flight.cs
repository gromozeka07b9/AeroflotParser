using System;
using System.Collections.Generic;

namespace Aeroflot.Models
{
    public class Flight
    {
        public string DepartureAirport;
        public string ReturnAirport;
        public DateTime DepartureDate;
        public DateTime ReturnDate;
        public string OperatedBy;
        public string Duration;
        public List<Tariff> TariffList;

        public class Tariff
        {
            public string FareFamilyKey;
            public string Cost;
            public string Currency;
        }
    }
}