using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeXPro.Models
{
    public class SysConfig
    {
        [Key]
        public string config_key { get; set; }
        [Required]
        public string config_value { get; set; }
        [Required]
        public string config_type { get; set; }
        public string config_value2 { get; set; }
    }
    public class SysConfigViewModel
    {
        public SysConfig CurrentSysConfig { get; set; }
        public string config_key { get; set; }
        public List<SysConfig> List { get; set; }
        public string DisplayMode { get; set; }
        public void SetCurrent(string key) {
            
            for (int i = 0; i < List.Count; i++)
            {
                if (List[i].config_key ==key)
                {
                    CurrentSysConfig = List[i];
                    this.config_key = key;
                    break;
                }
            }
        }
    }
   
    public class CustViewModel : cust {
        public string DisplayMode { get; set; }
    }
    public class CustSearchViewModel
    {
        public string register_no { get; set; }
        public string cif_name { get; set; }
        public string phone { get; set; }
        public string isOverRun { get; set; }
        public List<cust> List { get; set; }
    }
}