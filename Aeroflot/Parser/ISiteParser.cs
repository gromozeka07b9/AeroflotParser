using Aeroflot.Models;

namespace Aeroflot
{
    public interface ISiteParser
    {

        CalcResponse ParseCalc(string rawResponse);
    }
}