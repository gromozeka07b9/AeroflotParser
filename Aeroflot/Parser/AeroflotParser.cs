using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Xml;
using Aeroflot.Models;
using HtmlAgilityPack;

namespace Aeroflot
{
    public class AeroflotParser : ISiteParser 
    {
        public AeroflotParser()
        {
        }

        public CalcResponse ParseCalc(string rawResponse)
        {
            var flights = new List<Flight>();
            string source = WebUtility.HtmlDecode(rawResponse);
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(source);

            List<HtmlNode> htmlMain = html.DocumentNode.Descendants().Where(x => (x.Name == "tr" && x.Attributes["class"] != null &&(x.Attributes["class"].Value.Contains("yui-dt-even")||x.Attributes["class"].Value.Contains("yui-dt-odd")))).ToList();
            foreach(var item in htmlMain)
            {
                HtmlDocument rowHtml = new HtmlDocument();
                rowHtml.LoadHtml(item.InnerHtml);
                var cellsView = rowHtml.DocumentNode.Descendants().Where(x => (x.Name == "th" && x.Attributes["scope"] != null && x.Attributes["scope"].Value.Contains("row")));
                List<HtmlNode> cells = cellsView.ToList();
                HtmlDocument rowFlightInfo = new HtmlDocument();
                rowFlightInfo.LoadHtml(cells[2].InnerHtml);
                List<HtmlNode> cellFlightInfo = rowFlightInfo.DocumentNode.Descendants().Where(y => (y.Name == "span" && y.Attributes["class"] != null && y.Attributes["class"].Value.Contains("translate wasTranslated"))).ToList();
                HtmlDocument rowCosts = new HtmlDocument();
                rowCosts.LoadHtml(item.InnerHtml);
                List<HtmlNode> cellsCost = rowCosts.DocumentNode.Descendants().Where(x => (x.Name == "td" && x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("price price-cell fare-family-"))).ToList();
                List<Flight.Tariff> tariffList = new List<Flight.Tariff>();
                foreach(var tariffItem in cellsCost)
                {
                    var tariff = new Flight.Tariff() { FareFamilyKey = tariffItem.Attributes[2].Value};
                    HtmlDocument tariffHtml = new HtmlDocument();
                    tariffHtml.LoadHtml(tariffItem.InnerHtml);
                    List<HtmlNode> cellsTariff = tariffHtml.DocumentNode.Descendants().Where(x => (x.Name == "span" && x.Attributes["class"] != null && (x.Attributes["class"].Value.Contains("prices-amount")|| x.Attributes["class"].Value.Contains("translate currency prices-currency")))).ToList();
                    if(cellsTariff.Count > 0)
                    {
                        tariff.Cost = cellsTariff[0].InnerText;
                        tariff.Currency = cellsTariff[1].InnerText;
                        tariffList.Add(tariff);
                    }
                }
                var arrDurationString = cellFlightInfo[1].Attributes[1].Value.Split('|');
                string duration = string.Format("{0}.{1}", arrDurationString[1], arrDurationString[2]);
                string operatedBy = cellFlightInfo[3].Attributes[1].Value;
                string departureAirport = cells[0].ChildNodes[1].InnerText;
                string returnAirport = cells[1].ChildNodes[1].InnerText;
                string departureDate = cells[0].ChildNodes[2].InnerText;
                string returnDate = cells[1].ChildNodes[2].InnerText;
                Flight flight = new Flight();
                flight.Duration = duration;
                flight.OperatedBy = operatedBy;
                flight.DepartureDate = getDateFromString(departureDate);
                flight.ReturnDate = getDateFromString(returnDate);
                flight.DepartureAirport = departureAirport;
                flight.ReturnAirport = returnAirport;
                flight.TariffList = tariffList;
                flights.Add(flight);
            }


            return new CalcResponse() {Flights = flights };
        }

        private DateTime getDateFromString(string stringDate)
        {
            string tempDate = stringDate.Replace(":", "").Replace(" ", "").Replace("(", "").Replace(")", "");
            int hour = 0;
            int min = 0;
            int day = 0;
            int month = 0;
            int.TryParse(tempDate.Substring(0, 2), out hour);
            int.TryParse(tempDate.Substring(2, 2), out min);
            int.TryParse(tempDate.Substring(4, 2), out day);
            month = getMonthFromView(tempDate.Substring(6, 3).ToLower());
            return new DateTime(DateTime.Now.Year, month, day, hour, min, 0, DateTimeKind.Local);
        }

        private int getMonthFromView(string Month3Symbols)
        {
            int numMonth = 0;
            switch(Month3Symbols)
            {
                case "янв":
                    numMonth = 1;
                    break;
                case "фев":
                    numMonth = 2;
                    break;
                case "мар":
                    numMonth = 3;
                    break;
                case "апр":
                    numMonth = 4;
                    break;
                case "май":
                    numMonth = 5;
                    break;
                case "июн":
                    numMonth = 6;
                    break;
                case "июл":
                    numMonth = 7;
                    break;
                case "авг":
                    numMonth = 8;
                    break;
                case "сен":
                    numMonth = 9;
                    break;
                case "окт":
                    numMonth = 10;
                    break;
                case "ноя":
                    numMonth = 11;
                    break;
                case "дек":
                    numMonth = 12;
                    break;
                default:
                    numMonth = 0;
                    break;
            }
            return numMonth;
        }
    }
}