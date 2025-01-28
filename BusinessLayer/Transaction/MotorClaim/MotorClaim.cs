using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public  class MotorClaim
    {
        public long ClmUid { get; set; }
        public string ClmNo { get; set; }
        public string ClmPolNo { get; set; }
        public DateTime ClmPolFmDt { get; set; }
        public DateTime ClmPolToDt { get; set; }
        public string ClmPolAssrName { get; set; }
        public string ClmPolAssrMob { get; set; }
        public DateTime ClmLossDt { get; set; }
        public DateTime ClmIntmDt { get; set; }
        public DateTime ClmRegDt { get; set; }
        public string ClmPolRepNo { get; set; }
        public string ClmPolRepDtl { get; set; }
        public string ClmLossDesc { get; set; }
        public string ClmVehMake { get; set; }
        public string ClmVehModel { get; set; }
        public string ClmVehChassisNo { get; set; }
        public string ClmVehEngineNo { get; set; }
        public string ClmVehRegnNo { get; set; }
        public double ClmVehValue { get; set; }
        public string ClmSurCrYn { get; set; }
        public string ClmSurApprYn { get; set; }
        public string ClmApprStatus { get; set; }
        public string ClmSurNo { get; set; }
        public string ClmCrBy { get; set; }
        public DateTime ClmCrDt { get; set; }
        public string ClmUpBy { get; set; }
        public DateTime ClmUpDt { get; set; }
    }
}
