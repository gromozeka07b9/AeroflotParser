using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using Aeroflot.Models;

namespace Aeroflot.Validation
{
    public class ValidateCalcRequest
    {
        string origin;
        string destination;
        string departureDate;
        string returnDate;
        string numAdults;

        string originValidated;
        string destinationValidated;
        DateTime departureDateValidated;
        DateTime returnDateValidated;
        int numAdultsValidated;

        public int NumAdults
        {
            get
            {
                return numAdultsValidated;
            }
            set
            {
                numAdultsValidated = value;
            }
        }

        public bool RequestValidated { get; private set; }

        public ValidateCalcRequest(string origin, string destination, string departureDate, string returnDate)
        {
            this.origin = origin;
            this.destination = destination;
            this.departureDate = departureDate;
            this.returnDate = returnDate;
            this.numAdults = string.Empty;
        }

        public ValidateCalcRequest(string origin, string destination, string departureDate, string returnDate, string numAdults)
        {
            this.origin = origin;
            this.destination = destination;
            this.departureDate = departureDate;
            this.returnDate = returnDate;
            this.numAdults = numAdults;
        }

        public string Validate()
        {
            StringBuilder errors = new StringBuilder();
            DateTime.TryParse(departureDate, CultureInfo.CreateSpecificCulture("ru-RU"), DateTimeStyles.None, out departureDateValidated);
            DateTime.TryParse(returnDate, CultureInfo.CreateSpecificCulture("ru-RU"), DateTimeStyles.None, out returnDateValidated);
            //DateTime.TryParse(returnDate, out returnDateValidated);
            if(!string.IsNullOrEmpty(numAdults))
                int.TryParse(numAdults, out numAdultsValidated);

            if (origin.Length == 3)
                originValidated = origin;
            else
                errors.AppendLine("Origin must be 3 symbols");

            if (destination.Length == 3)
                destinationValidated = destination;
            else
                errors.AppendLine("Destination must be 3 symbols");

            if (departureDateValidated < DateTime.Now)
            {
                errors.AppendLine("DepartureDate must be more than Now and have format 'dd-MM-YYYY'");
            }
            if (returnDateValidated < DateTime.Now)
            {
                errors.AppendLine("ReturnDate must be more than Now and have format 'dd-MM-YYYY'");
            }
            if (numAdultsValidated < 1)
            {
                errors.AppendLine("NumAdults > 0 required");
            }

            string errString = errors.ToString();
            RequestValidated = errString.Length == 0;
            return errString;
        }

        public CalcRequest GetCalcRequest()
        {
            CalcRequest request;
            if (RequestValidated)
                request = new CalcRequest(originValidated, destinationValidated, departureDateValidated, returnDateValidated, numAdultsValidated);
            else
                throw new HttpRequestValidationException("Request parameters incorrect");

            return request;

        }
    }
}