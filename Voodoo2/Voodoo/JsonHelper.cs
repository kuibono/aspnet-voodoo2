using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Reflection;
using System.Collections;
using System.Web.UI.WebControls;
namespace Voodoo
{
    public static class JsonHelper
    {
        #region  List转Json字符串
        /// <summary>
        /// List转Json字符串
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static string ListToJsonStr(this IEnumerable<object> lst)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(System.Web.HttpContext.Current.Request["jsoncallback"] + "([");
            foreach (object o in lst)
            {
                ArrayList list = new ArrayList();

                PropertyInfo[] fieldinfo = o.GetType().GetProperties();

                foreach (PropertyInfo info in fieldinfo)
                {
                    ListItem listitem = new ListItem(info.Name, info.GetValue(o, null).ToS());
                    list.Add(listitem);
                }
                sb.Append("{");

                for (int i = 0; i < list.Count; i++)
                {

                    ListItem li = (ListItem)list[i];

                    sb.Append("\"" + li.Text.Replace("\"", "\\\"").Replace("'", "\\'") + "\":");
                    sb.Append("\"" + li.Value.Replace("\"", "\\\"").Replace("'", "\\'") + "\"");

                    if (i != list.Count - 1)
                    {
                        sb.Append(",");
                    }

                }
                sb.Append("},");

            }
            sb = sb.TrimEnd(',');

            sb.Append("])");
            return sb.ToString();
        }
        #endregion

        #region 实体转换为JSON
        /// <summary>
        /// 实体转换为JSON,带jsoncallback参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string ToJsonStr(this object model)
        {
            return model.ToJson();
        }

        /// <summary>
        /// 实体转换为JSON,没有jsoncallback参数
        /// </summary>
        /// <returns></returns>
        public static string ToJson(this object model)
        {
            JavaScriptSerializer ss = new JavaScriptSerializer();
            return ss.Serialize(model);
        }

        public static void ResponseJson(this object model)
        {
            JavaScriptSerializer ss = new JavaScriptSerializer();
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentType = "text/json";
            System.Web.HttpContext.Current.Response.Write(ss.Serialize(model));
        }
        #endregion

        #region 将数据表转换成JSON类型串
        /// <summary>
        /// 将数据表转换成JSON类型串
        /// </summary>
        /// <param name="dt">要转换的数据表</param>
        /// <param name="dispose">数据表转换结束后是否dispose掉</param>
        /// <returns></returns>
        public static StringBuilder DataTableToJson(this System.Data.DataTable dt, bool dt_dispose)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("[\r\n");

            //数据表字段名和类型数组
            string[] dt_field = new string[dt.Columns.Count];
            int i = 0;
            string formatStr = "{{";
            string fieldtype = "";
            foreach (System.Data.DataColumn dc in dt.Columns)
            {
                dt_field[i] = dc.Caption.ToLower().Trim();
                formatStr += "'" + dc.Caption.ToLower().Trim() + "':";
                fieldtype = dc.DataType.ToString().Trim().ToLower();
                if (fieldtype.IndexOf("int") > 0 || fieldtype.IndexOf("deci") > 0 ||
                    fieldtype.IndexOf("floa") > 0 || fieldtype.IndexOf("doub") > 0 ||
                    fieldtype.IndexOf("bool") > 0)
                {
                    formatStr += "{" + i + "}";
                }
                else
                {
                    formatStr += "'{" + i + "}'";
                }
                formatStr += ",";
                i++;
            }

            if (formatStr.EndsWith(","))
                formatStr = formatStr.Substring(0, formatStr.Length - 1);//去掉尾部","号

            formatStr += "}},";

            i = 0;
            object[] objectArray = new object[dt_field.Length];
            foreach (System.Data.DataRow dr in dt.Rows)
            {

                foreach (string fieldname in dt_field)
                {   //对 \ , ' 符号进行转换 
                    objectArray[i] = dr[dt_field[i]].ToString().Trim().Replace("\\", "\\\\").Replace("'", "\\'");
                    switch (objectArray[i].ToString())
                    {
                        case "True":
                            {
                                objectArray[i] = "true"; break;
                            }
                        case "False":
                            {
                                objectArray[i] = "false"; break;
                            }
                        default: break;
                    }
                    i++;
                }
                i = 0;
                stringBuilder.Append(string.Format(formatStr, objectArray));
            }
            if (stringBuilder.ToString().EndsWith(","))
                stringBuilder.Remove(stringBuilder.Length - 1, 1);//去掉尾部","号

            if (dt_dispose)
                dt.Dispose();

            return stringBuilder.Append("\r\n]");
        }
        #endregion
    }
}
