using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MotorSurveySystem.WebService
{
    /// <summary>
    /// Summary description for CurrencyConvertion
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class CurrencyConvertion : System.Web.Services.WebService
    {

        [WebMethod]
        
        public decimal ConvertCurrency(decimal amount, string currencyCode)
        {           
            CodesMasterManager objCodesMasterManager = new CodesMasterManager();
            decimal exchangeRate = objCodesMasterManager.ReturnVal(currencyCode);
            return amount * exchangeRate;
        }

        public decimal ConvertCurrency1(decimal amount, string currencyCode)
        {
            CodesMasterManager objCodesMasterManager = new CodesMasterManager();
            decimal exchangeRate = objCodesMasterManager.ReturnVal(currencyCode);
            return amount / exchangeRate;
        }
    }
}
