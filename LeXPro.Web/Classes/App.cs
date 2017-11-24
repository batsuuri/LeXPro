using LeXPro.Core;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace LeXPro
{
    public class App
    {
        #region Properties
        public static DataTable globTexts;
        public static Hashtable globTextsHT;
        public static Hashtable sysConfig = new Hashtable();
        public static Hashtable dicTable = new Hashtable();
        public static Hashtable sysMsg = new Hashtable();
        public static Hashtable dicAddress = new Hashtable();
        public static Hashtable tranTemp = new Hashtable();

        public static string keydelm = "_";
        public static string QPAY_Merchant_Code = Func.ToStr(ConfigurationManager.AppSettings["QPay_Merchant_Code"]);
        public static string QPay_Verification_Code = Func.ToStr(ConfigurationManager.AppSettings["QPay_Verification_Code"]);
        public static string QPay_Invoice_Code = Func.ToStr(ConfigurationManager.AppSettings["QPay_Invoice_Code"]);
        public const string REQUIRED = "Заавал оруулна уу!";
        #endregion

        #region Dictionary, Messages
        public static void InitGlobalizationTable(DataTable dt)
        {
            globTexts = dt;
            if (globTextsHT != null && globTextsHT.Count > 0)
            {
                globTextsHT.Clear();
            }
            else
                globTextsHT = new Hashtable();

            string key = "";

            foreach (DataRow row in dt.Rows)
            {
                key = Func.ToStr(row["TableName"]) + keydelm + Func.ToStr(row["ColumnName"]) + keydelm + Func.ToStr(row["KeyField"]) + keydelm + Func.ToStr(row["Lang"]);
                globTextsHT.Add(key.ToLower(), row["TextValue"]);
            }
        }

        public static Result getDicList(string type, string parent_id, int lang)
        {
            Dictionary dic;
            param p;
            List<param> list = new List<param>();
            
            foreach (string key in dicTable.Keys)
            {
                if (type == key.ToLower().Split("*".ToCharArray())[0])
                {
                    dic = (Dictionary)dicTable[key];
                    p = new param
                    {
                        key = dic.id,
                        extra = dic.extra,
                        extra2 = dic.extra2
                    };
                    if (lang == 1)
                    {
                        p.value = dic.name;
                    }
                    else
                    {
                        p.value = dic.name2;
                    }
                    if (Func.ToStr(parent_id) != "")
                    {
                        if (dic.extra2 == parent_id)
                        {
                            list.Add(p);
                        }
                    }
                    else
                    {
                        list.Add(p);
                    }
                }
            }

            Result res = new Result(true);
            res.Data = Func.ObjectToJson(list);
            return res;
        }

        public static Result getAddressList( int lang)
        {
            Dictionary dic;
            param p;
            List<param> list = new List<param>();

            foreach (string key in dicAddress.Keys)
            {
                dic = (Dictionary)dicAddress[key];
                p = new param
                {
                    key = dic.id,
                    extra = dic.extra,
                    extra2 = dic.extra2
                };
                if (lang == 1)
                {
                    p.value = dic.name;
                }
                else { 
                    p.value = dic.name2;
                }

                list.Add(p);
            }

            Result res = new Result(true);
            res.Data = Func.ObjectToJson(list);
            return res;
        }

        public static string getDicTextValue(string type, string id, int lang)
        {
            string ret = "";
            foreach (string key in dicTable.Keys)
            {
                if (key.StartsWith(type + "*" + id))
                {
                    if (lang == 1)
                    {
                        ret = ((Dictionary)dicTable[key]).name;
                    }
                    else
                        ret = ((Dictionary)dicTable[key]).name2;

                    break;
                }
            }
            //if (dicTable.ContainsKey(type + "*" + id))
            //{
            //    if (lang == 1)
            //    {
            //        ret = ((Dictionary)dicTable[type + "*" + id]).name;
            //    }
            //    else
            //        ret = ((Dictionary)dicTable[type + "*" + id]).name2;
            //}
            return ret;
        }

        public static Dictionary getDic(string type, string id)
        {
            Dictionary ret=new Dictionary();
            if (dicTable.ContainsKey(type + "*" + id))
            {
                ret = (Dictionary)dicTable[type + "*" + id];
            }
            return ret;
        }
        public static string getMsg(int id, int lang)
        {
            string ret = "";
            string key = Func.ToStr(id) + keydelm + Func.ToStr(lang);
            if (sysMsg.ContainsKey(key))
            {
                ret = Func.ToStr(sysMsg[key]);
            }
            return ret;
        }
        public static string getMsg(int id, enumLang lang)
        {
            return getMsg(id, (int)lang);
        }
        public static Result getMsgRes(int id, int lang)
        {
            Result ret = new Result(false);
            ret.Code = id;
            string key = Func.ToStr(id) + keydelm + Func.ToStr(lang);

            if (sysMsg.ContainsKey(key))
            {
                ret.Desc = Func.ToStr(sysMsg[key]);
            }
            return ret;
        }
        public static Result getMsgRes(int id, enumLang lang)
        {
            return getMsgRes(id, (int) lang);
        }
        public static SysConfig getConfig(string key)
        {
            SysConfig config = null;
            if (sysConfig.ContainsKey(key.ToLower()))
            {
                config = (SysConfig)sysConfig[key.ToLower()];
            }
            return config;
        }
        public static string getConfigValue(string key)
        {
            string ret = "";
            if (sysConfig.ContainsKey(key.ToLower()))
            {
                ret = ((SysConfig)sysConfig[key.ToLower()]).config_value;
            }
            return ret;
        }


        #endregion

        #region Database operations
        public static Result DataSetExecute(string sql, int lang)
        {
            Result res = new Result(true);

            try
            {
                res = Main.DataSetExecute(sql);
            }
            catch (Exception ex)
            {
                res = App.getMsgRes(1003, lang);
                Main.ErrorLog("DataSetExecute", ex.ToString());
                Main.TestLog("DataSetExecute", sql);
            }

            return res;
        }
        public static Result DataSetExecute(string sql, SqlParameter[] obj, int lang)
        {
            Result res = new Result(true);

            try
            {
                res = Main.DataSetExecute(sql, obj);
            }
            catch (Exception ex)
            {
                res = App.getMsgRes(1003, lang);
                Main.ErrorLog("DataSetExecute", ex.ToString());
                Main.TestLog("DataSetExecute", sql);
            }

            return res;
        }
        public static Result ExecuteNonQuery(string sql, SqlParameter[] obj, int lang)
        {
            Result res = new Result(true);

            try
            {
                res = Main.ExecuteNonQuery(sql, obj);
            }
            catch (Exception ex)
            {
                res = App.getMsgRes(1003, lang);
                Main.ErrorLog("ExecuteNonQuery", ex.ToString());
                Main.TestLog("ExecuteNonQuery", sql);
            }

            return res;
        }
        public static Result ExecuteNonQuery(string sql, int lang)
        {
            Result res = new Result(true);

            try
            {
                res = Main.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                res = App.getMsgRes(1003, lang);
                Main.ErrorLog("ExecuteNonQuery", ex.ToString());
                Main.TestLog("ExecuteNonQuery", sql);
            }

            return res;
        }
        public static Result ExecuteReader(string sql, SqlParameter[] obj, int lang)
        {
            Result res = new Result(true);

            try
            {
                res.Data = Main.ExecuteReader(sql, obj);
            }
            catch (Exception ex)
            {
                res = App.getMsgRes(1003, lang);
                Main.ErrorLog("ExecuteReader", ex.ToString());
                Main.TestLog("ExecuteReader", sql);
            }

            return res;
        }
        public static Result ExecuteReader(string sql, int lang)
        {

            Result res = new Result(true);

            try
            {
                res.Data = Main.ExecuteReader(sql);
            }
            catch (Exception ex)
            {
                res = App.getMsgRes(1003, lang);
                Main.ErrorLog("ExecuteReader", ex.ToString());
                Main.TestLog("ExecuteReader", sql);
            }

            return res;
        }
        #endregion

        #region Dictionary classes
        [Serializable]
        public class Dictionary
        {
            public string dicType { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string name2 { get; set; }
            public string extra { get; set; }
            public string extra2 { get; set; }
        }

        public class SysConfig
        {
            public string config_key { get; set; }
            public string config_value { get; set; }
            public string config_type { get; set; }
            public string config_value2 { get; set; }
        }
        public class SysMsg
        {
            public int msg_no { get; set; }
            public string msg_text { get; set; }
            public int lang { get; set; }
        }
        public class param
        {
            public string key { get; set; }
            public string value { get; set; }
            public string extra { get; set; }
            public string extra2 { get; set; }
        }
        public class Tran
        {
            public string type { get; set; }
            public string date { get; set; }
            public string invoice_id { get; set; }
            public string invoice_code { get; set; }
            public string status { get; set; }
            public string payment_method { get; set; }
            public string device_id { get; set; }
            public string tran_data { get; set; }
            public string tran_extra { get; set; }
            public string last_check_date { get; set; }
            public string cust { get; set; }
            public string payment_invoice_id { get; set; }
            public string product_no { get; set; }
        }

        public class FCMessage
        {
            public string to { get; set; }
            public FCMData data { get; set; }
           
        }
        public class FCMData {
            public string qpay_invoice_id { get; set; }
            public string contract_no { get; set; }
            public string tran_status { get; set; }
            public string tran_date { get; set; }
            public string product { get; set; }
        }
        #endregion
    }
}