using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Voodoo
{
    public static class Convertor
    {
        #region IP转换为数字
        /// <summary>
        /// IP转换为数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long IpToLong(this string str)
        {
            string[] strArray = str.Split('.');
            return Convert.ToInt64(strArray[0]) * 256 * 256 * 256 + Convert.ToInt64(strArray[1]) * 256 * 256 + Convert.ToInt64(strArray[2]) * 256 + Convert.ToInt64(strArray[3]);
        }
        #endregion

        #region 20100526类型的时间转换成 2010年05月26日这种格式
        /// <summary>
        /// 20100526类型的时间转换成 2010年05月26日这种格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToDateString(this string str)
        {
            try
            {
                if (str.Length == 8)
                {
                    str = str.Insert(4, "年");
                    str = str.Insert(7, "月");
                    str += "日";
                    return str;

                }
                else
                {
                    return str;
                }
            }
            catch
            {
                return str;
            }
        }
        #endregion

        #region 变量转换为Byte,不成功则转换为0
        /// <summary>
        /// 变量转换为Byte,不成功则转换为0
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Byte ToByte(this object self)
        {
            try
            {
                return Convert.ToByte(self);
            }
            catch
            {
                return Convert.ToByte("0");
            }
        }
        #endregion

        #region 类型转换为DateTime
        /// <summary>
        /// 类型转换为DateTime
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object self)
        {
            return self.ToDateTime(new DateTime(2000, 1, 1));
        }

        public static DateTime ToDateTime(this object self, DateTime Default)
        {
            try
            {
                return Convert.ToDateTime(self.ToString().Replace("年", "-").Replace("月", "-").Replace("日", ""));
            }
            catch
            {
                return Default;
            }
        }
        #endregion

        #region 类型转换为Decimal
        /// <summary>
        /// 类型转换为Decimal
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Decimal ToDecimal(this object self)
        {
            return ToDecimal(self, Decimal.MinValue);
        }

        /// <summary>
        /// 转换为Decimal类型 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="DefaultValue">转换失败的默认值</param>
        /// <returns></returns>
        public static Decimal ToDecimal(this object self, Decimal DefaultValue)
        {
            try
            {
                return Convert.ToDecimal(self.ToString());
            }
            catch
            {
                return DefaultValue;
            }
        }
        #endregion

        #region 转换为bool类型
        /// <summary>
        /// 转换为bool类型
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ToBoolean(this object self)
        {
            if (self == null || self.ToString().IsNullOrEmpty())
            {
                return false;
            }

            if (self.ToString().ToLower() == "false")
            {
                return false;
            }
            if (self.ToString().ToLower() == "true")
            {
                return true;
            }
            if (self.ToString().ToInt32() == 0)
            {
                return false;
            }
            if (self.ToString().ToInt32() != 0)
            {
                return true;

            }
            try
            {
                return Convert.ToBoolean(self.ToString());
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region 字符串转换为数字
        /// <summary>
        /// 字符串转换为 Int32格式
        /// </summary>
        /// <param name="self"></param>
        /// <returns>int类型字符串，出错返回int.MinValue</returns>
        public static int ToInt32(this object self)
        {
            try
            {
                return Convert.ToInt32(self);
            }
            catch
            {
                return int.MinValue;
            }
        }

        public static int ToInt32(this object self, int defaultvalue)
        {
            try
            {
                return Convert.ToInt32(self);
            }
            catch
            {
                return defaultvalue;
            }
        }
        /// <summary>
        /// 字符串转换为 Int64格式
        /// </summary>
        /// <param name="self"></param>
        /// <returns>long类型字符串，出错返回int.MinValue</returns>
        public static long ToInt64(this object self)
        {
            try
            {
                return Convert.ToInt64(self);
            }
            catch
            {
                return int.MinValue;
            }
        }

        public static long ToInt64(this object self, long DefaultValue)
        {
            try
            {
                return Convert.ToInt64(self);
            }
            catch
            {
                return DefaultValue;
            }
        }
        #endregion

        #region 字符串转换为Pascal格式
        /// <summary>
        /// 字符串转换为Pascal格式
        /// </summary>
        /// <param name="s">要转换的字符串</param>
        /// <returns>返回Pascal格式字符串</returns>
        /// <example>输入myString,返回MyString这种字符串</example>
        public static string ToPascal(this string s)
        {
            return s.Substring(0, 1).ToUpper() + s.Substring(1);
        }
        #endregion

        #region 字符串转换为camel格式
        /// <summary>
        /// 字符串转换为camel格式
        /// </summary>
        /// <param name="s">要转换的字符串</param>
        /// <returns></returns>
        public static string ToCamel(this string s)
        {
            return s.Substring(0, 1).ToLower() + s.Substring(1);
        }
        #endregion

       
    }
}
