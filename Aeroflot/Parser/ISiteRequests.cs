using Aeroflot.Models;

namespace Aeroflot
{
    public interface ISiteRequests
    {
        string GetCalc(CalcRequest CalcRequestData);
    }
}