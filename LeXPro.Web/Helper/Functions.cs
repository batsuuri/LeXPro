using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeXPro;
using LeXPro.Core;
using System.Globalization;
using System.Net;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;

namespace LeXPro
{
   
    #region [ User enums ]
   
    public enum enumLang
    {
        eMongolian = 1,
        eEnglish = 2,
    }
    #endregion
    public struct Result
    {

        public bool Succeed;
        public object Data;
        public object Data2;
        public object Data3;
        public string Desc;
        public int Code;

        public Result(bool pSucceed)
        {
            Succeed = pSucceed;
            Data = null;
            Data2 = null;
            Data3 = null;
            Desc = "";
            Code = 0;
        }

        public Result(bool pSucceed, object pData)
        {
            Succeed = pSucceed;
            Data = pData;
            Data2 = null;
            Data3 = null;
            Desc = "";
            Code = 0;
        }

        public Result(bool pSucceed, string desc)
        {
            Succeed = pSucceed;
            Data = null;
            Data2 = null;
            Data3 = null;
            Desc = desc;
            Code = 0;
        }

        public Result(bool pSucceed, object pData, object pData2)
        {
            Succeed = pSucceed;
            Data = pData;
            Data2 = pData2;
            Data3 = null;
            Desc = "";
            Code = 0;
        }
        public Result(bool pSucceed, object pData, object pData2, string desc)
        {
            Succeed = pSucceed;
            Data = pData;
            Data2 = pData2;
            Data3 = null;
            Desc = desc;
            Code = 0;
        }
        public Result(bool pSucceed, object pData, object pData2, object pData3, string desc)
        {
            Succeed = pSucceed;
            Data = pData;
            Data2 = pData2;
            Data3 = pData3;
            Desc = desc;
            Code = 0;
        }
    }
        public class Func
    {
        public static byte[] stkkey = new byte[] { (byte)0x0A, (byte)0x07, (byte)0xE1, (byte)0x09, (byte)0x03, (byte)0xA3, (byte)0x11, (byte)0x0E };
        public static byte[] stkiv = new byte[] { (byte)0x04, (byte)0x05, (byte)0x06, (byte)0x07, (byte)0x08, (byte)0x09, (byte)0x0A, (byte)0x0B };

        public static string GetClientIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string strIP = "";
            try
            {
                string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(ipAddress))
                {
                    string[] addresses = ipAddress.Split(',');
                    if (addresses.Length != 0)
                    {
                        strIP = addresses[0].ToString();
                    }
                    else
                    {
                        strIP = "";
                    }
                }
                else
                {
                    strIP = context.Request.ServerVariables["REMOTE_ADDR"];
                }
            }
            catch (Exception ex)
            {
                Core.Main.ErrorLog("GetClientIPAddress", ex);
            }

            return strIP;
        }

        [System.Runtime.InteropServices.DllImport("iphlpapi.dll")]
        private static extern int SendARP(int DestIP, int SrcIP, [System.Runtime.InteropServices.Out] byte[] pMacAddr, ref int PhyAddrLen);
        public static string GetMACAddress(string RemoteHostName)
        {
            try
            {
                byte[] pMacAddr = new byte[6];
                int length = pMacAddr.Length;
                Func.SendARP((int)Dns.GetHostEntry(RemoteHostName).AddressList[0].Address, 0, pMacAddr, ref length);
                return BitConverter.ToString(pMacAddr, 0, 6);
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        #region String Functions
        static public string Mid(string text, int start, int length)
        {
            string s = string.Empty;
            if (text != null)
            {
                int index = start - 1;
                int len = text.Length - index;

                if (len >= length) s = text.Substring(index, length);
                else if (len > 0) s = text.Substring(index, text.Length - index);
            }
            return s;
        }
        public static double DateDiff(string retType, DateTime pStartDate, DateTime pEndDate)
        {
            TimeSpan timeSpan = new TimeSpan(pEndDate.Ticks - pStartDate.Ticks);
            double num = 0.0;
            switch (retType)
            {
                case "Y":
                    num = Convert.ToDouble(timeSpan.TotalDays / 365.0);
                    break;
                case "Q":
                    num = Convert.ToDouble(timeSpan.TotalDays / 91.0);
                    break;
                case "M":
                    num = Convert.ToDouble(timeSpan.TotalDays / 30.0);
                    break;
                case "D":
                    num = Convert.ToDouble(timeSpan.TotalDays);
                    break;
                case "H":
                    num = Convert.ToDouble(timeSpan.TotalHours);
                    break;
                case "m":
                    num = Convert.ToDouble(timeSpan.TotalMinutes);
                    break;
                case "s":
                    num = Convert.ToDouble(timeSpan.TotalSeconds);
                    break;
                case "ms":
                    num = Convert.ToDouble(timeSpan.TotalMilliseconds);
                    break;
            }
            return num;
        }
        public static string SubStr(string text, int index, int length)
        {
            string str = string.Empty;
            int num = text.Length - index;
            if (num >= length)
                str = text.Substring(index, length);
            else if (num > 0)
                str = text.Substring(index, text.Length - index);
            return str;
        }

        public static string SubStr(string text, int index)
        {
            return Func.SubStr(text, index, 1);
        }
        public static string PadRight(string dat, int len, char padChar)
        {
            dat = Func.ToStr((object)dat);
            if (dat.Length >= len)
                return dat.Substring(0, len);
            return dat.PadRight(len, padChar);
        }

        public static string PadLeft(string dat, int len, char padChar)
        {
            dat = Func.ToStr((object)dat);
            if (dat.Length >= len)
                return dat.Substring(0, len);
            return dat.PadLeft(len, padChar);
        }
        public static string ToMoneyStr(object pObj)
        {
            if (!Convert.IsDBNull(pObj))
                return Convert.ToDecimal(pObj).ToString("#,###,###,###,##0.00");
            return "0.00";
        }

        public static string ToMoney5Str(object pObj)
        {
            if (!Convert.IsDBNull(pObj))
                return Convert.ToDecimal(pObj).ToString("#,###,###,###,##0.00000");
            return "0.00";
        }
        #endregion

        #region Conversation types
        static public string AnsiToUni(string strTmp)
        {
            if (!string.IsNullOrEmpty(strTmp))
            {

                StringBuilder sb = new StringBuilder();
                char[] chars = strTmp.ToCharArray();
                char c;

                for (int i = 0; i < chars.Length; i++)
                {
                    c = chars[i];
                    if ((c >= '\xc0' && c <= '\xff') || c == '\xa8' || c == '\xaa' || c == '\xaf' || c == '\xb8' || c == '\xba' || c == '\xbf')
                    {
                        switch (c)
                        {
                            case '\xa8':    // Ё
                                sb.Append('\u0401');
                                break;
                            case '\xb8':    // ё
                                sb.Append('\u0451');
                                break;
                            case '\xaa':    // Ө
                                sb.Append('\u04e8');
                                break;
                            case '\xba':    // ө
                                sb.Append('\u04e9');
                                break;
                            case '\xaf':    // Ү
                                sb.Append('\u04ae');
                                break;
                            case '\xbf':    // ү
                                sb.Append('\u04af');
                                break;
                            default:
                                sb.Append((char)(c + '\u0350'));
                                break;
                        }
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
                return sb.ToString();
            }
            else
            {
                return "";
            }
        }
        public static string UniToAnsi(string strTmp)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char ch in strTmp.ToCharArray())
            {
                if ((int)ch > (int)byte.MaxValue)
                {
                    switch (ch)
                    {
                        case 'Ү':
                            stringBuilder.Append('¯');
                            continue;
                        case 'ү':
                            stringBuilder.Append('¿');
                            continue;
                        case 'Ө':
                            stringBuilder.Append('ª');
                            continue;
                        case 'ө':
                            stringBuilder.Append('º');
                            continue;
                        case 'є':
                            stringBuilder.Append('º');
                            continue;
                        case 'ї':
                            stringBuilder.Append('¿');
                            continue;
                        case 'Ї':
                            stringBuilder.Append('¯');
                            continue;
                        case 'ё':
                            stringBuilder.Append('¸');
                            continue;
                        case 'Ё':
                            stringBuilder.Append('¨');
                            continue;
                        case 'Є':
                            stringBuilder.Append('ª');
                            continue;
                        default:
                            if ((int)ch >= 1040 && (int)ch <= 1103)
                            {
                                stringBuilder.Append((char)((uint)ch - 848U));
                                continue;
                            }
                            stringBuilder.Append((char)((uint)ch - (uint)byte.MaxValue));
                            continue;
                    }
                }
                else
                    stringBuilder.Append(ch);
            }
            return stringBuilder.ToString();
        }
      
        static public string ToBase64(string text)
        {
            byte[] textbytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textbytes);
        }

        static public string FromBase64(string text)
        {
            byte[] textbytes = Convert.FromBase64String(text);
            return Encoding.UTF8.GetString(textbytes);
        }

        public static char ToChar(object pobj)
        {
            if (Convert.IsDBNull(pobj) || pobj == null)
                return Convert.ToChar(" ");
            return Convert.ToChar(pobj);
        }
        public static byte ToByte(object pobj)
        {
            return (byte)Func.ToDouble(pobj);
        }
        public static int ToInt(object pObject)
        {
            if (Convert.IsDBNull(pObject))
            {
                return 0;
            }
            else if (pObject == null)
            {
                return 0;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(pObject)))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(pObject);
            }
        }
        public static short ToShort(object pobj)
        {
            if (pobj is short)
                return (short)pobj;
            if (pobj is bool)
                return !(bool)pobj ? (short)0 : (short)1;
            short result;
            short.TryParse(Convert.ToString(pobj), NumberStyles.Number, (IFormatProvider)null, out result);
            return result;
        }
        public static decimal ToNumber(object pObject)
        {
            decimal number = 0;
            if (Convert.IsDBNull(pObject))
            {
                return 0;
            }
            else if (pObject == null)
            {
                return 0;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(pObject)))
            {
                return 0;
            }
            else if (Decimal.TryParse(ToStr(pObject), out number))
            {
                return number;
            }
            else
            {
                return 0;
            }
        }
        public static float ToFloat(object pobj)
        {
            if (pobj is float)
                return (float)pobj;
            if (pobj is bool)
                return !(bool)pobj ? 0.0f : 1f;
            float result;
            float.TryParse(Convert.ToString(pobj), NumberStyles.Float, (IFormatProvider)null, out result);
            return result;
        }

        public static float ToSingle(object pobj)
        {
            if (pobj is float)
                return (float)pobj;
            if (pobj is bool)
                return !(bool)pobj ? 0.0f : 1f;
            float result;
            float.TryParse(Convert.ToString(pobj), NumberStyles.Float, (IFormatProvider)null, out result);
            return result;
        }

        public static double ToDouble(object pobj)
        {
            if (pobj is double)
                return (double)pobj;
            if (pobj is bool)
                return !(bool)pobj ? 0.0 : 1.0;
            double result;
            if (pobj is bool)
                result = Convert.ToDouble(pobj);
            else
                double.TryParse(Convert.ToString(pobj), NumberStyles.Float, (IFormatProvider)null, out result);
            return result;
        }

        public static long ToLong(object pobj)
        {
            return (long)Func.ToDecimal(pobj);
        }
        public static Decimal ToDecimal(object pobj)
        {
            if (pobj is Decimal)
                return (Decimal)pobj;
            if (pobj is bool)
                return (Decimal)((bool)pobj ? 1 : 0);
            Decimal result;
            Decimal.TryParse(Convert.ToString(pobj), NumberStyles.Any, (IFormatProvider)null, out result);
            return result;
        }
        public static string ToStr(object pObject)
        {
            if (pObject == null)
            {
                return "";
            }
            else if (Convert.IsDBNull(pObject))
            {
                return "";
            }
            else
            {
                return Convert.ToString(pObject);
            }
        }
        public static DateTime ToDate(object pobj)
        {
            if (pobj is DateTime)
                return (DateTime)pobj;
            DateTime result;
            DateTime.TryParseExact(Convert.ToString(pobj), new string[9]
            {
        "G",
        "yyyyMMdd HH:mm:ss",
        "yyyy/M/d HH:mm:ss",
        "yyyy-M-d HH:mm:ss",
        "yyyy.M.d HH:mm:ss",
        "yyyyMMdd",
        "yyyy/M/d",
        "yyyy-M-d",
        "yyyy.M.d"
            }, (IFormatProvider)null, DateTimeStyles.None, out result);
            return result;
        }

        public static DateTime ToDateTime(object pobj)
        {
            if (pobj is DateTime)
                return (DateTime)pobj;
            DateTime result;
            DateTime.TryParseExact(Convert.ToString(pobj), new string[9]
            {
        "G",
        "yyyyMMdd HH:mm:ss",
        "yyyy/M/d HH:mm:ss",
        "yyyy-M-d HH:mm:ss",
        "yyyy.M.d HH:mm:ss",
        "yyyyMMdd",
        "yyyy/M/d",
        "yyyy-M-d",
        "yyyy.M.d"
            }, (IFormatProvider)null, DateTimeStyles.None, out result);
            return result;
        }

        public static object ToDateDb(object pobj)
        {
            DateTime date = Func.ToDate(pobj);
            if (date.Equals(DateTime.MinValue))
                return (object)DBNull.Value;
            return (object)date;
        }

        public static string ToDateStr(object pobj)
        {
            return Func.ToDate(pobj).ToString("yyyy.MM.dd");
        }

        public static string ToDateTimeStr(object pobj)
        {
            return Func.ToDateTime(pobj).ToString("yyyy.MM.dd HH:mm:ss");
        }

        public static bool ToBool(object pObject)
        {
            if (pObject == null)
            {
                return false;
            }
            else if (Convert.IsDBNull(pObject))
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(pObject);
            }
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
        internal static string HexToString(string text)
        {
            byte[] bytes;
            string result = "";
            bytes = StringToByteArray(text);
            result = Convert.ToBase64String(bytes);
            return result;
        }
        public static string StrToBHex(string strUTFtext)
        {

            string retdata = "";
            try
            {
                retdata = Func.UniToAnsi(strUTFtext);
                byte[] b = StringToByteArray(retdata);
                retdata = "";
                for (int i = 0; i < b.Length; i++)
                {
                    retdata = retdata + Func.DecToHex((int)b[i]);
                }
            }
            catch (Exception ex)
            {
                Main.ErrorLog("StrToBHex ret = " + strUTFtext + " Hex = " + retdata + " err ", ex);
            }
            return retdata;
        }

        public static long HexToDec(string hex)
        {
            long.TryParse(Convert.ToString(hex), NumberStyles.HexNumber, (IFormatProvider)null, out long result);
            return result;
        }

        public static string DecToHex(long value)
        {
            return value.ToString("X").PadLeft(2, '0');
        }
        
        public static List<System.Collections.Hashtable> ToListHT(DataTable dt)
        {
            List<Hashtable> hashList = new List<Hashtable>();

            foreach (DataRow drIn in dt.Rows)
            {
                Hashtable ht = new Hashtable();
                foreach (DataColumn dc in dt.Columns)
                {
                    ht.Add(dc.ColumnName, drIn[dc.Ordinal].ToString());
                }
                hashList.Add(ht);
            }
            return hashList;
        }

        public static object JsonToObject(string json,object obj) {
            Result res = new Result(true);
            object tmp ;
            try
            {
                tmp = Newtonsoft.Json.JsonConvert.DeserializeObject(json,obj.GetType());
            }
            catch (Exception ex)
            {
                Main.ErrorLog("Json2Object", ex);
                tmp = null;
            }

            return tmp;
        }

        public static object JsonToObject(object json, object obj)
        {
            return JsonToObject(Func.ToStr(json),obj);
        }
        public static string ObjectToJson(object obj)
        {
            string ret = "";

            try
            {
                ret = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                Main.ErrorLog("ObjectToJson", ex);
                ret = "";
            }

            return ret;
        }
        #endregion

        #region DropDownList Functions

        public static void SelectItemToList(DropDownList ddl, string value)
        {
            bool selected = false;
            ddl.SelectedIndex = -1;
            foreach (System.Web.UI.WebControls.ListItem items in ddl.Items)
            {
                if (items.Value == value)
                {
                    selected = true;
                    items.Selected = true;
                    break;
                }
            }
            if (!selected && ddl.Items.Count > 0)
            {
                ddl.SelectedIndex = 0;
            }
        }

        public static void SortDropDownList(DropDownList ddl, bool byText)
        {
            ListItem[] sorted = new ListItem[ddl.Items.Count];
            for (int i = 0; i < sorted.Length; i++)
            {
                sorted[i] = ddl.Items[i];
            }
            if (byText)
            {
                Array.Sort(sorted, SortDDLByText);
            }
            else
            {
                Array.Sort(sorted, SortDDLByValue);
            }
            ddl.Items.Clear();
            ddl.Items.AddRange(sorted);
        }

        private static int SortDDLByText(ListItem a, ListItem b)
        {
            return a.Text.CompareTo(b.Text);
        }

        private static int SortDDLByValue(ListItem a, ListItem b)
        {
            int ret = 0;

            int aval = ToInt(a.Value.Split('^'.ToString().ToCharArray())[1]);
            int bval = ToInt(b.Value.Split('^'.ToString().ToCharArray())[1]);
            if (aval > bval)
            {
                ret = 1;
            }
            else if (aval < bval)
            {
                ret = -1;
            }
            return ret;
        }

        #endregion
    }

    #region [ Other class ]
    public class CItem
    {
        public string remoteclient;
        public long totalrequest;
        public long requestlast;
        public double totalexecutiontime;
    }
    public class RequestLog
    {
        public string remoteclient;
        public DateTime starttime;
        public DateTime endtime;
        public double ExecutionTime
        {
            get
            {
                TimeSpan ts = new TimeSpan();
                ts = endtime - starttime;
                return ts.TotalSeconds;
            }
        }
    }
    #endregion
}
