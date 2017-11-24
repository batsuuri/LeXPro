using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeXPro.Models
{
    public class cust
    {
        public int cif_id { get; set; }
        [Required(ErrorMessage = App.REQUIRED)]
        [StringLength(50)]
        public string cif_name { get; set; }
        [Required(ErrorMessage = App.REQUIRED)]
        [StringLength(50)]
        public string cif_middle_name { get; set; }
        [Required(ErrorMessage = App.REQUIRED)]
        [StringLength(50)]
        public string cif_last_name { get; set; }
        public string sex { get; set; }
        [Required(ErrorMessage = App.REQUIRED)]
        [StringLength(250)]
        public string cif_address { get; set; }
        [Required(ErrorMessage = App.REQUIRED)]
        [StringLength(10, ErrorMessage = "Регистерийн дугаарыг зөв оруулна уу.", MinimumLength = 10)]
        public string register_no { get; set; }
        [Required(ErrorMessage = App.REQUIRED)]
        [StringLength(50)]
        public string phone { get; set; }
        public string home_phone { get; set; }
        public string social_id { get; set; }
        public string email { get; set; }
        public string off_address { get; set; }
        [Required(ErrorMessage = App.REQUIRED)]
        [StringLength(100)]
        public string work_place { get; set; }
        public string work_phone { get; set; }
        public string work_address { get; set; }
        public string work_position { get; set; }
        public string profession { get; set; }
        [Required(ErrorMessage = App.REQUIRED)]
        [StringLength(10, ErrorMessage = "Төрсөн онгоог зөв оруулна уу.", MinimumLength=10)]
        public string birth_date { get; set; }
        public Nullable<System.DateTime> reg_date { get; set; }
        public Nullable<int> reg_user { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> last_edit_date { get; set; }
        public Nullable<int> last_edit_user { get; set; }
    }
    public class cust_emergency_contact
    {
        public int ec_id { get; set; }
        public int cif_id { get; set; }
        public string ec_name { get; set; }
        public string ec_middle_name { get; set; }
        public string relative { get; set; }
        public string work_place { get; set; }
        public string work_position { get; set; }
        public string contact_phone { get; set; }
    }

    public  class cust_family_member
    {
        public int fm_id { get; set; }
        public int cif_id { get; set; }
        public string register_no { get; set; }
        public string fm_name { get; set; }
        public string fm_middle_name { get; set; }
        public string relative { get; set; }
        public Nullable<decimal> monthly_income { get; set; }
        public string work_position { get; set; }
        public string contact_phone { get; set; }
    }
}