using System;
using System.IO;
using System.Net;
using System.Text;
using Aeroflot.Models;

namespace Aeroflot.Controllers
{
    public class AeroflotRequests : ISiteRequests
    {
        public string GetCalc(CalcRequest CalcRequestData)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://reservation.aeroflot.ru");
            req.Method = "GET";
            req.Credentials = CredentialCache.DefaultCredentials;
            req.AllowAutoRedirect = false;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            var cookies = resp.Cookies;
            resp.Close();
            req = null;
            //ToDo:поправить на URL
            string url = string.Format("https://reservation.aeroflot.ru/SSW2010/7B47/webqtrip.html?searchType=NORMAL&journeySpan=RT&alternativeLandingPage=1&lang=ru&currency=RUB&cabinClass=ECONOMY&referrerCode=AFLBOOK&origin={0}&destination={1}&departureDate={2}&returnDate={3}&numAdults={4}&numChildren=0&numInfants=0&utm_source=&utm_campaign=&utm_medium=&utm_content=&utm_term=", CalcRequestData.origin, CalcRequestData.destination, CalcRequestData.departureDate.ToString("yyyy-MM-dd"), CalcRequestData.returnDate.ToString("yyyy-MM-dd"), CalcRequestData.numAdults.ToString());
            req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.CookieContainer = new CookieContainer();
            foreach(Cookie cookie in cookies)
            {
                req.CookieContainer.Add(cookie);
            }

            resp = (HttpWebResponse)req.GetResponse();
            string response = string.Empty;
            using (StreamReader stream = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
            {
                response = stream.ReadToEnd();
            }
            return response;
        }
    }
}