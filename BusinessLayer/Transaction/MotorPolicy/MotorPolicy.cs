using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public  class MotorPolicy
    {
        public long PolUid { get; set; }
        public string PolNo { get; set; }
        public DateTime PolIssDt { get; set; }
        public DateTime PolFmDt { get; set; }
        public DateTime PolToDt { get; set; }
        public string PolAssrName { get; set; }
        public string PolAssrMobile { get; set; }
        public string PolCurrCode { get; set; }
        public double PolGrossFcPrem { get; set; }
        public double PolGrossLcPrem { get; set; }
        public string PolVehMake { get; set; }
        public string PolVehModel { get; set; }
        public string PolVehChassisNo { get; set; }
        public string PolVehEngineNo { get; set; }
        public string PolVehRegnNo { get; set; }
        public double PolVehValue { get; set; }
        public string PolApprStatus { get; set; }
        public DateTime PolApprDt { get; set; }
        public string PolApprBy { get; set; }
        public string PolCrBy { get; set; }
        public DateTime PolCrDt { get; set; }
        public string PolUpBy { get; set; }
        public DateTime PolUpDt { get; set; }
    }
}
