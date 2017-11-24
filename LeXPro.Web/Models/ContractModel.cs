using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeXPro.Models
{
    public class Contract
    {
        public int contract_id { get; set; }
        public int cif_id { get; set; }
        public System.DateTime contract_date { get; set; }
        public string contract_no { get; set; }
        public Nullable<int> credit_inst_id { get; set; }
        public Nullable<System.DateTime> credit_contract_date { get; set; }
        public string credit_contract_no { get; set; }
        public string credit_contract_product { get; set; }
        public Nullable<decimal> credit_principal_balance { get; set; }
        public string contract_cur { get; set; }
        public string contract_term { get; set; }
        public Nullable<int> contract_period { get; set; }
        public Nullable<System.DateTime> contract_period_start { get; set; }
        public Nullable<System.DateTime> contract_period_end { get; set; }
        public int create_user_id { get; set; }
        public System.DateTime create_date { get; set; }
        public Nullable<int> edit_user_id { get; set; }
        public Nullable<System.DateTime> edit_date { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> last_payment_date { get; set; }
        public Nullable<decimal> last_payment_amount { get; set; }
        public Nullable<decimal> total_payment_amount { get; set; }
        public Nullable<decimal> total_finalty_amount { get; set; }
    }
}