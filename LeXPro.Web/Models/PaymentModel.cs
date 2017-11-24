using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeXPro.Models
{
    public class cust_payment_schedule
    {
        public int contract_id { get; set; }
        public int schedule_id { get; set; }
        public string line_no { get; set; }
        public System.DateTime due_date { get; set; }
        public Nullable<decimal> amount { get; set; }
        public string cur { get; set; }
        public Nullable<decimal> finalty { get; set; }
        public Nullable<int> late_day { get; set; }
        public Nullable<decimal> remainder { get; set; }
        public Nullable<System.DateTime> last_late_pay_date { get; set; }
    }

    public class payment_tran
    {
        public int tran_id { get; set; }
        public string tran_type { get; set; }
        public System.DateTime tran_date { get; set; }
        public int cif_id { get; set; }
        public string contract_no { get; set; }
        public Nullable<int> schedule_id { get; set; }
        public Nullable<decimal> tran_amount { get; set; }
        public string tran_cur { get; set; }
        public string tran_status { get; set; }
        public string tran_desc { get; set; }
        public string inst_code { get; set; }
        public string jrn_no { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> created_user_id { get; set; }
        public string is_corrected { get; set; }
        public string account_no { get; set; }
        public Nullable<int> late_day { get; set; }
    }
}