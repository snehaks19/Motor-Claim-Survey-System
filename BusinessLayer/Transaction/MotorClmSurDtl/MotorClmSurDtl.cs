using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{ 
    public class MotorClmSurDtl
    {
        public long SurdUid { get; set; }
        public long SurdSurUid { get; set; }
        public string SurdItemCode { get; set; }
        public string SurdType { get; set; }
        public int SurdUnitPrice { get; set; }
        public int SurdQuantity { get; set; }
        public double SurdFcAmt { get; set; }
        public double SurdLcAmt { get; set; }
        public string SurdRemarks { get; set; }
        public string SurdCrBy { get; set; }
        public DateTime SurdCrDt { get; set; }
        public string SurdUpBy { get; set; }
        public DateTime SurdUpDt { get; set; }

    }
}
