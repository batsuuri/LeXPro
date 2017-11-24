using LeXPro.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LeXPro.Models
{
    public class TerminalContext
    {
        private static int lang = 1;
        private static Result res;
        public static Result SysConfigList()
        {
            SysConfigViewModel model = new SysConfigViewModel();
            SysConfig item;
            try
            {
                #region SQL
                string sql = @"Select c.* From sys_config c";
                #endregion

                #region SQL Params

                #endregion
                res = App.DataSetExecute(sql.ToString(), lang);

                if (res.Succeed)
                {
                    DataTable dt = ((DataSet)res.Data).Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        model.List = new List<SysConfig>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            item = new SysConfig();

                            item.config_key = Func.ToStr(dt.Rows[i]["config_key"]);
                            item.config_value = Func.ToStr(dt.Rows[i]["config_value"]);
                            item.config_value2 = Func.ToStr(dt.Rows[i]["config_value2"]);
                            item.config_type = Func.ToStr(dt.Rows[i]["config_type"]);
                            model.List.Add(item);
                        }
                    }
                    else
                    {
                        res = App.getMsgRes(1005, lang); // Invoice not found
                    }
                }
                else
                {
                    res = App.getMsgRes(1001, lang); // Error occured when get data
                }
            }
            catch (Exception ex)
            {
                Main.ErrorLog("GetGeneralInfo", ex);
                res = App.getMsgRes(1003, lang);
            }
            res.Data = model;
            return res;
        }

        public static Result SysConfigUpdate(SysConfig item)
        {
            try
            {
                #region SQL
                string sqlCust = @"
                    
                    UPDATE sys_config SET
                            [config_value] = @config_value
                    WHERE config_key = @config_key";

                #endregion

                #region SQL Parameter Initiliaze
                SqlParameter pConfig_key = new SqlParameter("@config_key", SqlDbType.NVarChar, 50);
                SqlParameter pConfig_value = new SqlParameter("@config_value", SqlDbType.NVarChar, 500);

                pConfig_key.Value = item.config_key;
                pConfig_value.Value = item.config_value;

                #endregion
                res = App.ExecuteNonQuery(sqlCust, new SqlParameter[] {
                                                                pConfig_key, pConfig_value
                                                                }, lang);
                res = SysConfigList();
            }
            catch (Exception ex)
            {
                Main.ErrorLog("SysConfigUpdate", ex);
                res = App.getMsgRes(1003, lang);
            }
            return res;
        }

        public static Result CustSearch(CustSearchViewModel model)
        {
            cust item;
            try
            {
                #region SQL
                string sql = @"Select c.* From cust c where status = 1 ";
                if (Func.ToStr(model.cif_name) != "")
                {
                    sql = sql + " and lower(cif_name) like '%'+@cif_name+'%'";
                }
                if (Func.ToStr(model.register_no) != "")
                {
                    sql = sql + " and lower(register_no) like '%'+@register_no+'%'";
                }
                if (Func.ToStr(model.phone) != "")
                {
                    sql = sql + " and phone like '%'+@phone+'%'";
                }
                #endregion

                #region SQL Params
                SqlParameter pCif_name = new SqlParameter("@cif_name", SqlDbType.NVarChar, 50);
                SqlParameter pRegister_no = new SqlParameter("@register_no", SqlDbType.NVarChar, 50);
                SqlParameter pPhone = new SqlParameter("@phone", SqlDbType.NVarChar, 50);

                pCif_name.Value = Func.ToStr(model.cif_name).ToLower();
                pRegister_no.Value = Func.ToStr(model.register_no).ToLower();
                pPhone.Value = model.phone;
                #endregion
                res = App.DataSetExecute(sql.ToString(), new SqlParameter[] { pCif_name, pRegister_no, pPhone }, lang);

                if (res.Succeed)
                {
                    DataTable dt = ((DataSet)res.Data).Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        model.List = new List<cust>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            item = new cust();

                            item.cif_id = Func.ToInt(dt.Rows[i]["cif_id"]);
                            item.cif_name = Func.ToStr(dt.Rows[i]["cif_name"]);
                            item.cif_middle_name = Func.ToStr(dt.Rows[i]["cif_middle_name"]);
                            item.cif_last_name = Func.ToStr(dt.Rows[i]["cif_last_name"]);
                            item.sex = Func.ToStr(dt.Rows[i]["sex"]);
                            item.cif_address = Func.ToStr(dt.Rows[i]["cif_address"]);
                            item.register_no = Func.ToStr(dt.Rows[i]["register_no"]);
                            item.birth_date = Func.ToStr(dt.Rows[i]["birth_date"]);
                            item.phone = Func.ToStr(dt.Rows[i]["phone"]);
                            item.home_phone = Func.ToStr(dt.Rows[i]["home_phone"]);
                            item.social_id = Func.ToStr(dt.Rows[i]["social_id"]);
                            item.email = Func.ToStr(dt.Rows[i]["email"]);
                            item.off_address = Func.ToStr(dt.Rows[i]["off_address"]);
                            item.work_place = Func.ToStr(dt.Rows[i]["work_place"]);
                            item.work_phone = Func.ToStr(dt.Rows[i]["work_phone"]);
                            item.work_address = Func.ToStr(dt.Rows[i]["work_address"]);
                            item.work_position = Func.ToStr(dt.Rows[i]["work_position"]);
                            item.profession = Func.ToStr(dt.Rows[i]["profession"]);
                            item.reg_date = Func.ToDateTime(dt.Rows[i]["reg_date"]);
                            item.reg_user = Func.ToInt(dt.Rows[i]["reg_user"]);
                            item.status = Func.ToStr(dt.Rows[i]["status"]);
                            item.last_edit_date = Func.ToDateTime(dt.Rows[i]["last_edit_date"]);
                            item.last_edit_user = Func.ToInt(dt.Rows[i]["last_edit_user"]);

                            model.List.Add(item);
                        }
                    }
                    else
                    {
                        res = App.getMsgRes(1005, lang); // Invoice not found
                    }
                }
                else
                {
                    res = App.getMsgRes(1001, lang); // Error occured when get data
                }
            }
            catch (Exception ex)
            {
                Main.ErrorLog("CustSearch", ex);
                res = App.getMsgRes(1003, lang);
            }
            res.Data = model;
            return res;
        }

        public static Result CustGet(string id)
        {
            CustViewModel model = new CustViewModel();
            model.DisplayMode = "EditOnly";
            try
            {
                #region SQL
                string sql = @"Select c.*, convert(nvarchar, birth_date, 102) birthdate From cust c where status = 1 and cif_id = @cif_id";

                #endregion

                #region SQL Params
                SqlParameter pCif_id = new SqlParameter("@cif_id", SqlDbType.NVarChar, 50);

                pCif_id.Value = id;
                #endregion
                res = App.DataSetExecute(sql.ToString(), new SqlParameter[] { pCif_id }, lang);

                if (res.Succeed)
                {
                    DataTable dt = ((DataSet)res.Data).Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        model.cif_id = Func.ToInt(dt.Rows[0]["cif_id"]);
                        model.cif_name = Func.ToStr(dt.Rows[0]["cif_name"]);
                        model.cif_middle_name = Func.ToStr(dt.Rows[0]["cif_middle_name"]);
                        model.cif_last_name = Func.ToStr(dt.Rows[0]["cif_last_name"]);
                        model.sex = Func.ToStr(dt.Rows[0]["sex"]);
                        model.cif_address = Func.ToStr(dt.Rows[0]["cif_address"]);
                        model.register_no = Func.ToStr(dt.Rows[0]["register_no"]);
                        model.birth_date = Func.ToStr(dt.Rows[0]["birthdate"]);
                        model.phone = Func.ToStr(dt.Rows[0]["phone"]);
                        model.home_phone = Func.ToStr(dt.Rows[0]["home_phone"]);
                        model.social_id = Func.ToStr(dt.Rows[0]["social_id"]);
                        model.email = Func.ToStr(dt.Rows[0]["email"]);
                        model.off_address = Func.ToStr(dt.Rows[0]["off_address"]);
                        model.work_place = Func.ToStr(dt.Rows[0]["work_place"]);
                        model.work_phone = Func.ToStr(dt.Rows[0]["work_phone"]);
                        model.work_address = Func.ToStr(dt.Rows[0]["work_address"]);
                        model.work_position = Func.ToStr(dt.Rows[0]["work_position"]);
                        model.profession = Func.ToStr(dt.Rows[0]["profession"]);
                        model.reg_date = Func.ToDateTime(dt.Rows[0]["reg_date"]);
                        model.reg_user = Func.ToInt(dt.Rows[0]["reg_user"]);
                        model.status = Func.ToStr(dt.Rows[0]["status"]);
                        model.last_edit_date = Func.ToDateTime(dt.Rows[0]["last_edit_date"]);
                        model.last_edit_user = Func.ToInt(dt.Rows[0]["last_edit_user"]);
                    }
                    else
                    {
                        res = App.getMsgRes(1005, lang); // Invoice not found
                    }
                }
                else
                {
                    res = App.getMsgRes(1001, lang); // Error occured when get data
                }
            }
            catch (Exception ex)
            {
                Main.ErrorLog("CustGet", ex);
                res = App.getMsgRes(1003, lang);
            }
            res.Data = model;
            return res;
        }

        public static Result CustInsert(CustViewModel model)
        {
            Result res = new Result();
            int cif_id = 0;
            try
            {
                #region 1. Insert Customer to Database

                #region SQL
                string sql = @"
                    BEGIN 
                    SELECT @cif_id= COALESCE(MAX(cif_id),0)+1 from cust;
                    SELECT @contract_id= COALESCE(MAX(contract_id),0)+1 from cust_contract;
                    INSERT INTO [dbo].[cust]
                               ([cif_id]
                               ,[cif_name]
                               ,[cif_middle_name]
                               ,[cif_last_name]
                               ,[sex]
                               ,[cif_address]
                               ,[register_no]
                               ,[birth_date]
                               ,[phone]
                               ,[home_phone]
                               ,[social_id]
                               ,[email]
                               ,[off_address]
                               ,[work_place]
                               ,[work_phone]
                               ,[work_address]
                               ,[work_position]
                               ,[profession]
                               ,[reg_date]
                               ,[reg_user]
                               ,[status])
                         VALUES
                               (@cif_id
                                ,@cif_name
                                ,@cif_middle_name
                                ,@cif_last_name
                                ,@sex
                                ,@cif_address
                                ,@register_no
                                ,@birth_date
                                ,@phone
                                ,@home_phone
                                ,@social_id
                                ,@email
                                ,@off_address
                                ,@work_place
                                ,@work_phone
                                ,@work_address
                                ,@work_position
                                ,@profession
                                ,@reg_date
                                ,@reg_user
                                ,@status
                                );
                insert into cust_contract(contract_id, cif_id, contract_date, status, create_user, create_date)
                        values (@contract_id,@cif_id,@reg_date, 1, @reg_user,@reg_date);
                END;";
                #endregion

                #region SQL Params

                SqlParameter pCif_Id = new SqlParameter("@cif_id", SqlDbType.Int);
                SqlParameter pCif_Name = new SqlParameter("@cif_name", SqlDbType.NVarChar, 50);
                SqlParameter pCif_Middle_Name = new SqlParameter("@cif_middle_name", SqlDbType.NVarChar, 50);
                SqlParameter pCif_Last_Name = new SqlParameter("@cif_last_name", SqlDbType.NVarChar, 50);
                SqlParameter pSex = new SqlParameter("@sex", SqlDbType.Char, 1);
                SqlParameter pCif_Address = new SqlParameter("@cif_address", SqlDbType.NVarChar, 250);
                SqlParameter pRegister_No = new SqlParameter("@register_no", SqlDbType.NVarChar, 10);
                SqlParameter pBirth_Date = new SqlParameter("@birth_date", SqlDbType.DateTime);
                SqlParameter pPhone = new SqlParameter("@phone", SqlDbType.NVarChar, 50);
                SqlParameter pHome_Phone = new SqlParameter("@home_phone", SqlDbType.NVarChar, 50);
                SqlParameter pSocial_Id = new SqlParameter("@social_id", SqlDbType.NVarChar, 150);
                SqlParameter pEmail = new SqlParameter("@email", SqlDbType.NVarChar, 50);
                SqlParameter pOff_Address = new SqlParameter("@off_address", SqlDbType.NVarChar, 250);
                SqlParameter pWork_Place = new SqlParameter("@work_place", SqlDbType.NVarChar, 100);
                SqlParameter pWork_Phone = new SqlParameter("@work_phone", SqlDbType.NVarChar, 50);
                SqlParameter pWork_Address = new SqlParameter("@work_address", SqlDbType.NVarChar, 250);
                SqlParameter pWork_Position = new SqlParameter("@work_position", SqlDbType.NVarChar, 50);
                SqlParameter pProfession = new SqlParameter("@profession", SqlDbType.NVarChar, 50);
                SqlParameter pReg_Date = new SqlParameter("@reg_date", SqlDbType.DateTime);
                SqlParameter pReg_User = new SqlParameter("@reg_user", SqlDbType.Int);
                SqlParameter pStatus = new SqlParameter("@status", SqlDbType.Char, 1);
                SqlParameter pContract_Id = new SqlParameter("@contract_id", SqlDbType.Int);

                pCif_Id.Value = model.cif_id;
                pCif_Name.Value = model.cif_name;
                pCif_Middle_Name.Value = model.cif_middle_name;
                pCif_Last_Name.Value = model.cif_last_name;
                pSex.Value = model.sex;
                pCif_Address.Value = model.cif_address;
                pRegister_No.Value = model.register_no;
                pBirth_Date.Value = model.birth_date;
                pPhone.Value = model.phone;
                pHome_Phone.Value = model.home_phone;
                pSocial_Id.Value = model.social_id;
                pEmail.Value = model.email;
                pOff_Address.Value = model.off_address;
                pWork_Place.Value = model.work_place;
                pWork_Phone.Value = model.work_phone;
                pWork_Address.Value = model.work_address;
                pWork_Position.Value = model.work_position;
                pProfession.Value = model.profession;
                pReg_Date.Value = DateTime.Now;
                pReg_User.Value = model.reg_user;
                pStatus.Value = 1;

                pCif_Id.Direction = ParameterDirection.InputOutput;
                pContract_Id.Direction = ParameterDirection.InputOutput;

                #endregion

                res = App.ExecuteNonQuery(sql.ToString(), new SqlParameter[] {
                                                                       pContract_Id
                                                                       ,pCif_Id
                                                                        ,pCif_Name
                                                                        ,pCif_Middle_Name
                                                                        ,pCif_Last_Name
                                                                        ,pSex
                                                                        ,pCif_Address
                                                                        ,pRegister_No
                                                                        ,pBirth_Date
                                                                        ,pPhone
                                                                        ,pHome_Phone
                                                                        ,pSocial_Id
                                                                        ,pEmail
                                                                        ,pOff_Address
                                                                        ,pWork_Place
                                                                        ,pWork_Phone
                                                                        ,pWork_Address
                                                                        ,pWork_Position
                                                                        ,pProfession
                                                                        ,pReg_Date
                                                                        ,pReg_User
                                                                        ,pStatus
                                                                        }
                                    , lang);

                if (!res.Succeed)
                {
                    res = App.getMsgRes(1002, lang);
                    return res;
                }

                cif_id = Func.ToInt(pCif_Id.Value);
                #endregion
                res.Data = cif_id;

            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("UNIQUE")
                {

                }
                Main.ErrorLog("CustInsert", ex);
                res = App.getMsgRes(1003, lang);
            }
            return res;
        }

        public static Result CustUpdate(CustViewModel model)
        {

            Result res = new Result();
            try
            {

                #region 1. Insert Customer to Database

                #region SQL
                string sql = @" update cust set
                                    cif_name = @cif_name,
                                    cif_middle_name = @cif_middle_name,
                                    cif_last_name = @cif_last_name,
                                    sex = @sex,
                                    cif_address = @cif_address,
                                    register_no = @register_no,
                                    birth_date = @birth_date,
                                    phone = @phone,
                                    home_phone = @home_phone,
                                    social_id = @social_id,
                                    email = @email,
                                    off_address = @off_address,
                                    work_place = @work_place,
                                    work_phone = @work_phone,
                                    work_address = @work_address,
                                    work_position = @work_position,
                                    profession = @profession,
                                    last_edit_date = @last_edit_date,
                                    last_edit_user = @last_edit_user
                                where cif_id = @cif_id;";
                #endregion

                #region SQL Params

                SqlParameter pCif_Id = new SqlParameter("@cif_id", SqlDbType.Int);
                SqlParameter pCif_Name = new SqlParameter("@cif_name", SqlDbType.NVarChar, 50);
                SqlParameter pCif_Middle_Name = new SqlParameter("@cif_middle_name", SqlDbType.NVarChar, 50);
                SqlParameter pCif_Last_Name = new SqlParameter("@cif_last_name", SqlDbType.NVarChar, 50);
                SqlParameter pSex = new SqlParameter("@sex", SqlDbType.Char, 1);
                SqlParameter pCif_Address = new SqlParameter("@cif_address", SqlDbType.NVarChar, 250);
                SqlParameter pRegister_No = new SqlParameter("@register_no", SqlDbType.NVarChar, 10);
                SqlParameter pBirth_Date = new SqlParameter("@birth_date", SqlDbType.DateTime);
                SqlParameter pPhone = new SqlParameter("@phone", SqlDbType.NVarChar, 50);
                SqlParameter pHome_Phone = new SqlParameter("@home_phone", SqlDbType.NVarChar, 50);
                SqlParameter pSocial_Id = new SqlParameter("@social_id", SqlDbType.NVarChar, 150);
                SqlParameter pEmail = new SqlParameter("@email", SqlDbType.NVarChar, 50);
                SqlParameter pOff_Address = new SqlParameter("@off_address", SqlDbType.NVarChar, 250);
                SqlParameter pWork_Place = new SqlParameter("@work_place", SqlDbType.NVarChar, 100);
                SqlParameter pWork_Phone = new SqlParameter("@work_phone", SqlDbType.NVarChar, 50);
                SqlParameter pWork_Address = new SqlParameter("@work_address", SqlDbType.NVarChar, 250);
                SqlParameter pWork_Position = new SqlParameter("@work_position", SqlDbType.NVarChar, 50);
                SqlParameter pProfession = new SqlParameter("@profession", SqlDbType.NVarChar, 50);
                SqlParameter pLast_Edit_Date = new SqlParameter("@last_edit_date", SqlDbType.DateTime);
                SqlParameter pLast_Edit_User = new SqlParameter("@last_edit_user", SqlDbType.Int);

                pCif_Id.Value = model.cif_id;
                pCif_Name.Value = model.cif_name;
                pCif_Middle_Name.Value = model.cif_middle_name;
                pCif_Last_Name.Value = model.cif_last_name;
                pSex.Value = model.sex;
                pCif_Address.Value = model.cif_address;
                pRegister_No.Value = model.register_no;
                pBirth_Date.Value = model.birth_date;
                pPhone.Value = model.phone;
                pHome_Phone.Value = model.home_phone;
                pSocial_Id.Value = model.social_id;
                pEmail.Value = model.email;
                pOff_Address.Value = model.off_address;
                pWork_Place.Value = model.work_place;
                pWork_Phone.Value = model.work_phone;
                pWork_Address.Value = model.work_address;
                pWork_Position.Value = model.work_position;
                pProfession.Value = model.profession;
                pLast_Edit_Date.Value = DateTime.Now;
                pLast_Edit_User.Value = model.last_edit_user;

                #endregion

                res = App.ExecuteNonQuery(sql.ToString(), new SqlParameter[] {
                                                                         pCif_Id
                                                                        ,pCif_Name
                                                                        ,pCif_Middle_Name
                                                                        ,pCif_Last_Name
                                                                        ,pSex
                                                                        ,pCif_Address
                                                                        ,pRegister_No
                                                                        ,pBirth_Date
                                                                        ,pPhone
                                                                        ,pHome_Phone
                                                                        ,pSocial_Id
                                                                        ,pEmail
                                                                        ,pOff_Address
                                                                        ,pWork_Place
                                                                        ,pWork_Phone
                                                                        ,pWork_Address
                                                                        ,pWork_Position
                                                                        ,pProfession
                                                                        ,pLast_Edit_Date
                                                                        ,pLast_Edit_User
                                                                        }
                                    , lang);

                if (!res.Succeed)
                {
                    res = App.getMsgRes(1002, lang);
                    return res;
                }

                #endregion
                res.Data = model;

            }
            catch (Exception ex)
            {
                Main.ErrorLog("CustUpdate", ex);
                res = App.getMsgRes(1003, lang);
            }
            return res;

        }
    }

}