using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class MotorClmSurHdr
    {
        public long SurUid { get; set; }
        public long SurClmUid { get; set; }
        public string SurClmNo { get; set; }
        public string SurNo { get; set; }
        public DateTime SurDate { get; set; }
        public string SurLocation { get; set; }
        public string SurChassisNo { get; set; }
        public string SurRegnNo { get; set; }
        public string SurEngineNo { get; set; }
        public string SurCurr { get; set; }
        public double SurFcAmt { get; set; }
        public double SurLcAmt { get; set; }
        public string SurStatus { get; set; }
        public string SurApprSts { get; set; }
        public DateTime SurApprDt { get; set; }
        public string SurApprBy { get; set; }
        public string SurCrBy { get; set; }
        public DateTime SurCrDt { get; set; }
        public string SurUpBy { get; set; }
        public DateTime SurUpDt { get; set; }

    }
}
