using LeXPro.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using static LeXPro.App;

namespace LeXPro
{
    public class AppConfig
    {
        public static void Init()
        {
            InitAppConfig();
            InitDic();
            //InitDefaultMenu();
        }

        private static void InitAppConfig()
        {

            //App.tourList = new SortedList<string, Tour>();
            //App.HomeTours = new SortedList<string, Tour>();
            //App.dataBasic = new SortedList<string, DataBasic>();
            //Main.mConnString = Func.ToStr(ConfigurationManager.AppSettings["ConStr"]);
    }
        public static void InitDic()
        {
            DataSet ds = new DataSet();
            DataTable dt = null;
            Dictionary dic;
            SysConfig config;
            SysMsg sysmsg;
            string sqlData = @"select 
                                    'fin_inst' dic_type, 
                                    cast([inst_code] as nvarchar) id,
                                    inst_name name,
                                    inst_name2 name2,
                                    inst_param1 extra,
                                    inst_param2 extra2
                                from [dic_fin_inst]
                            SELECT * FROM sys_msg order by msg_no, msg_lang;
                            SELECT * FROM sys_config ORDER BY config_key;";


            Result res = DataSetExecute(sqlData, 1);

            try
            {
                if (res.Succeed)
                {
                    if (App.dicTable != null && App.dicTable.Count > 0)
                    {
                        App.dicTable.Clear();
                    }
                    ds = (DataSet)res.Data;
                    dt = ds.Tables[0];


                    foreach (DataRow row in dt.Rows)
                    {
                        dic = new Dictionary
                        {
                            dicType = Func.ToStr(row["dic_Type"]),
                            id = Func.ToStr(row["id"]),
                            name = Func.ToStr(row["name"]),
                            name2 = Func.ToStr(row["name2"]),
                            extra = Func.ToStr(row["extra"]),
                            extra2 = Func.ToStr(row["extra2"])
                        };
                        if (dic.dicType == "address")
                        {
                            dicAddress.Add(dic.id, dic);
                        }
                        else if (dic.dicType == "product_item")
                        {
                            dicTable.Add(dic.dicType + "*" +   dic.id+"~" +dic.extra2 , dic);
                        }
                        else
                            dicTable.Add(dic.dicType + "*" +dic.id, dic);
                    }

                    App.sysMsg = new System.Collections.Hashtable();
                    dt = ds.Tables[1];
                    foreach (DataRow row in dt.Rows)
                    {
                        sysmsg = new SysMsg
                        {
                            msg_no = Func.ToInt(row["msg_no"]),
                            msg_text = Func.ToStr(row["msg_text"]),
                            lang = Func.ToInt(row["msg_lang"])
                        };
                        sysMsg.Add(Func.ToStr(sysmsg.msg_no) + keydelm + Func.ToStr(sysmsg.lang), sysmsg.msg_text);
                    }

                    App.sysConfig = new System.Collections.Hashtable();
                    dt = ds.Tables[2];
                    foreach (DataRow row in dt.Rows)
                    {
                        config = new SysConfig
                        {
                            config_key = Func.ToStr(row["config_Key"]),
                            config_value = Func.ToStr(row["config_Value"]),
                            config_type = Func.ToStr(row["config_type"]),
                            config_value2 = Func.ToStr(row["config_value2"])
                        };
                        sysConfig.Add(config.config_key, config);
                    }

                    dt.Dispose();
                    dt = null;
                    ds.Dispose();
                    ds = null;
                    dic = null;
                    config = null;
                    sysmsg = null;
                }
                else
                {
                    Main.ErrorLog("AppConfig-DicInit", res.Desc);
                }
            }
            catch (Exception ex)
            {
                Main.ErrorLog("AppConfig-DicInit", ex);
            }
        }

        
    }
}