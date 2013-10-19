using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

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

        #region 类型转换为SByte
        /// <summary>
        /// 类型转换为SByte
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static SByte ToSByte(this object o)
        {
            try
            {
                return Convert.ToSByte(o);
            }
            catch
            {
                return Convert.ToSByte(0);
            }

        }
        #endregion

        #region 类型转换为uint16
        /// <summary>
        /// 类型转换为uint16
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static ushort ToUInt16(this object o)
        {
            try
            {
                return Convert.ToUInt16(o);
            }
            catch
            {
                return ushort.MinValue;
            }
        }
        #endregion

        #region 类型转换为int16
        /// <summary>
        /// 类型转换为uint16
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static short ToInt16(this object o)
        {
            try
            {
                return Convert.ToInt16(o);
            }
            catch
            {
                return short.MinValue;
            }
        }
        #endregion

        #region 对象转换为string，失败返回空字符串
        /// <summary>
        /// 对象转换为string，失败返回空字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToS(this object str)
        {
            if (str == null)
            {
                return "";
            }
            try
            {
                return Convert.ToString(str);
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region DataTable 转换为List 集合
        /// <summary>
        /// DataTable 转换为List 集合
        /// </summary>
        /// <typeparam name="TResult">类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static List<TResult> ToList<TResult>(this DataTable dt) where TResult : class, new()
        {
            //创建一个属性的列表
            List<PropertyInfo> prlist = new List<PropertyInfo>();
            //获取TResult的类型实例  反射的入口
            Type t = typeof(TResult);
            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表 
            Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });
            //创建返回的集合
            List<TResult> oblist = new List<TResult>();

            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例
                TResult ob = new TResult();
                //找到对应的数据  并赋值
                prlist.ForEach(p => { if (row[p.Name] != DBNull.Value) p.SetValue(ob, row[p.Name], null); });
                //放入到返回的集合中.
                oblist.Add(ob);
            }
            return oblist;
        }
        #endregion

        #region 字符串数组转数字数组
        public static List<int> ToInt32(this List<string> list)
        {
            return list.Select(p => p.ToInt32()).ToList();
        }
        public static List<long> ToInt64(this List<string> list)
        {
            return list.Select(p => p.ToInt64()).ToList();
        }
        #endregion


        #region 将布尔类型的状态转换为“是”、“否”的字符串
        /// <summary>
        /// 将布尔类型的状态转换为“是”、“否”的字符串
        /// </summary>
        /// <param name="b">true或者false</param>
        /// <example>
        /// bool IsGood=false;<br/>
        /// string str_Good=IsGood.ToChinese();//结果为“否”
        ///  </example>
        public static string ToChinese(this bool b)
        {
            if (b)
            {
                return "<span style='color:green'>是</span>";
            }
            return "<span style='color:red'>否</span>";
        }
        #endregion

        public static string ToS(this bool b)
        {
            return b ? "1" : "0";
        }

        /// <summary>
        /// BOOL类型转换为short
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static short BoolToShort(this bool b)
        {
            if (b)
            {
                return 1;
            }
            return 0;
        }

        public static int ToInt32(this bool b)
        {
            return b ? 1 : 2;
        }


        #region 转全角(SBC case)
        /// <summary>
        /// 转全角(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        public static string ToSBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }
        #endregion

        #region 转半角(DBC case)
        /// <summary>
        /// 转半角(DBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        public static string ToDBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion

        #region 转换为简体中文
        /// <summary>
        /// 转换为简体中文
        /// </summary>
        public static string ToSChinese(this string str)
        {
            return Strings.StrConv(str, VbStrConv.SimplifiedChinese, 0);
        }
        #endregion

        #region 转换为繁体中文
        /// <summary>
        /// 转换为繁体中文
        /// </summary>
        public static string ToTChinese(this string str)
        {
            return Strings.StrConv(str, VbStrConv.TraditionalChinese, 0);
        }
        #endregion

        #region 获取文本首字母
        /// <summary>
        /// 获取文本首字母
        /// </summary>
        /// <param name="m_Text">要处理的文本</param>
        /// <returns></returns>
        public static string FirstChar(this string m_Text)
        {
            if (m_Text.Length > 0)
            {
                return IndexCode(m_Text[0].ToString()).ToUpper();
            }
            return "";
        }

        //返回给定字符串的首字母 
        private static string IndexCode(string IndexTxt)
        {

            return GetOneIndex(IndexTxt[0].ToString());
        }

        //得到单个字符的首字母 
        private static string GetOneIndex(string OneIndexTxt)
        {
            if (Convert.ToChar(OneIndexTxt) >= 0 && Convert.ToChar(OneIndexTxt) < 256)
                return OneIndexTxt;
            else
            {
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] unicodeBytes = Encoding.Unicode.GetBytes(OneIndexTxt);
                byte[] gb2312Bytes = Encoding.Convert(Encoding.Unicode, gb2312, unicodeBytes);
                return GetX(Convert.ToInt32(
                string.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[0]) - 160)
                + string.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[1]) - 160)
                ));
            }
        }

        //根据区位得到首字母 
        private static string GetX(int GBCode)
        {
            if (GBCode >= 1601 && GBCode < 1637) return "A";
            if (GBCode >= 1637 && GBCode < 1833) return "B";
            if (GBCode >= 1833 && GBCode < 2078) return "C";
            if (GBCode >= 2078 && GBCode < 2274) return "D";
            if (GBCode >= 2274 && GBCode < 2302) return "E";
            if (GBCode >= 2302 && GBCode < 2433) return "F";
            if (GBCode >= 2433 && GBCode < 2594) return "G";
            if (GBCode >= 2594 && GBCode < 2787) return "H";
            if (GBCode >= 2787 && GBCode < 3106) return "J";
            if (GBCode >= 3106 && GBCode < 3212) return "K";
            if (GBCode >= 3212 && GBCode < 3472) return "L";
            if (GBCode >= 3472 && GBCode < 3635) return "M";
            if (GBCode >= 3635 && GBCode < 3722) return "N";
            if (GBCode >= 3722 && GBCode < 3730) return "O";
            if (GBCode >= 3730 && GBCode < 3858) return "P";
            if (GBCode >= 3858 && GBCode < 4027) return "Q";
            if (GBCode >= 4027 && GBCode < 4086) return "R";
            if (GBCode >= 4086 && GBCode < 4390) return "S";
            if (GBCode >= 4390 && GBCode < 4558) return "T";
            if (GBCode >= 4558 && GBCode < 4684) return "W";
            if (GBCode >= 4684 && GBCode < 4925) return "X";
            if (GBCode >= 4925 && GBCode < 5249) return "Y";
            if (GBCode >= 5249 && GBCode <= 5589) return "Z";
            if (GBCode >= 5601 && GBCode <= 8794)
            {
                string CodeData = "cjwgnspgcenegypbtwxzdxykygtpjnmjqmbsgzscyjsyyfpggbzgydywjkgaljswkbjqhyjwpdzlsgmr"
                + "ybywwccgznkydgttngjeyekzydcjnmcylqlypyqbqrpzslwbdgkjfyxjwcltbncxjjjjcxdtqsqzycdxxhgckbphffss"
                + "pybgmxjbbyglbhlssmzmpjhsojnghdzcdklgjhsgqzhxqgkezzwymcscjnyetxadzpmdssmzjjqjyzcjjfwqjbdzbjgd"
                + "nzcbwhgxhqkmwfbpbqdtjjzkqhylcgxfptyjyyzpsjlfchmqshgmmxsxjpkdcmbbqbefsjwhwwgckpylqbgldlcctnma"
                + "eddksjngkcsgxlhzaybdbtsdkdylhgymylcxpycjndqjwxqxfyyfjlejbzrwccqhqcsbzkymgplbmcrqcflnymyqmsqt"
                + "rbcjthztqfrxchxmcjcjlxqgjmshzkbswxemdlckfsydsglycjjssjnqbjctyhbftdcyjdgwyghqfrxwckqkxebpdjpx"
                + "jqsrmebwgjlbjslyysmdxlclqkxlhtjrjjmbjhxhwywcbhtrxxglhjhfbmgykldyxzpplggpmtcbbajjzyljtyanjgbj"
                + "flqgdzyqcaxbkclecjsznslyzhlxlzcghbxzhznytdsbcjkdlzayffydlabbgqszkggldndnyskjshdlxxbcghxyggdj"
                + "mmzngmmccgwzszxsjbznmlzdthcqydbdllscddnlkjyhjsycjlkohqasdhnhcsgaehdaashtcplcpqybsdmpjlpcjaql"
                + "cdhjjasprchngjnlhlyyqyhwzpnccgwwmzffjqqqqxxaclbhkdjxdgmmydjxzllsygxgkjrywzwyclzmcsjzldbndcfc"
                + "xyhlschycjqppqagmnyxpfrkssbjlyxyjjglnscmhcwwmnzjjlhmhchsyppttxrycsxbyhcsmxjsxnbwgpxxtaybgajc"
                + "xlypdccwqocwkccsbnhcpdyznbcyytyckskybsqkkytqqxfcwchcwkelcqbsqyjqcclmthsywhmktlkjlychwheqjhtj"
                + "hppqpqscfymmcmgbmhglgsllysdllljpchmjhwljcyhzjxhdxjlhxrswlwzjcbxmhzqxsdzpmgfcsglsdymjshxpjxom"
                + "yqknmyblrthbcftpmgyxlchlhlzylxgsssscclsldclepbhshxyyfhbmgdfycnjqwlqhjjcywjztejjdhfblqxtqkwhd"
                + "chqxagtlxljxmsljhdzkzjecxjcjnmbbjcsfywkbjzghysdcpqyrsljpclpwxsdwejbjcbcnaytmgmbapclyqbclzxcb"
                + "nmsggfnzjjbzsfqyndxhpcqkzczwalsbccjxpozgwkybsgxfcfcdkhjbstlqfsgdslqwzkxtmhsbgzhjcrglyjbpmljs"
                + "xlcjqqhzmjczydjwbmjklddpmjegxyhylxhlqyqhkycwcjmyhxnatjhyccxzpcqlbzwwwtwbqcmlbmynjcccxbbsnzzl"
                + "jpljxyztzlgcldcklyrzzgqtgjhhgjljaxfgfjzslcfdqzlclgjdjcsnclljpjqdcclcjxmyzftsxgcgsbrzxjqqcczh"
                + "gyjdjqqlzxjyldlbcyamcstylbdjbyregklzdzhldszchznwczcllwjqjjjkdgjcolbbzppglghtgzcygezmycnqcycy"
                + "hbhgxkamtxyxnbskyzzgjzlqjdfcjxdygjqjjpmgwgjjjpkjsbgbmmcjssclpqpdxcdyykypcjddyygywchjrtgcnyql"
                + "dkljczzgzccjgdyksgpzmdlcphnjafyzdjcnmwescsglbtzcgmsdllyxqsxsbljsbbsgghfjlwpmzjnlyywdqshzxtyy"
                + "whmcyhywdbxbtlmswyyfsbjcbdxxlhjhfpsxzqhfzmqcztqcxzxrdkdjhnnyzqqfnqdmmgnydxmjgdhcdycbffallztd"
                + "ltfkmxqzdngeqdbdczjdxbzgsqqddjcmbkxffxmkdmcsychzcmljdjynhprsjmkmpcklgdbqtfzswtfgglyplljzhgjj"
                + "gypzltcsmcnbtjbhfkdhbyzgkpbbymtdlsxsbnpdkleycjnycdykzddhqgsdzsctarlltkzlgecllkjljjaqnbdggghf"
                + "jtzqjsecshalqfmmgjnlyjbbtmlycxdcjpldlpcqdhsycbzsckbzmsljflhrbjsnbrgjhxpdgdjybzgdlgcsezgxlblg"
                + "yxtwmabchecmwyjyzlljjshlgndjlslygkdzpzxjyyzlpcxszfgwyydlyhcljscmbjhblyjlycblydpdqysxktbytdkd"
                + "xjypcnrjmfdjgklccjbctbjddbblblcdqrppxjcglzcshltoljnmdddlngkaqakgjgyhheznmshrphqqjchgmfprxcjg"
                + "dychghlyrzqlcngjnzsqdkqjymszswlcfqjqxgbggxmdjwlmcrnfkkfsyyljbmqammmycctbshcptxxzzsmphfshmclm"
                + "ldjfyqxsdyjdjjzzhqpdszglssjbckbxyqzjsgpsxjzqznqtbdkwxjkhhgflbcsmdldgdzdblzkycqnncsybzbfglzzx"
                + "swmsccmqnjqsbdqsjtxxmbldxcclzshzcxrqjgjylxzfjphymzqqydfqjjlcznzjcdgzygcdxmzysctlkphtxhtlbjxj"
                + "lxscdqccbbqjfqzfsltjbtkqbsxjjljchczdbzjdczjccprnlqcgpfczlclcxzdmxmphgsgzgszzqjxlwtjpfsyaslcj"
                + "btckwcwmytcsjjljcqlwzmalbxyfbpnlschtgjwejjxxglljstgshjqlzfkcgnndszfdeqfhbsaqdgylbxmmygszldyd"
                + "jmjjrgbjgkgdhgkblgkbdmbylxwcxyttybkmrjjzxqjbhlmhmjjzmqasldcyxyqdlqcafywyxqhz";
                string _gbcode = GBCode.ToString();
                int pos = (Convert.ToInt16(_gbcode.Substring(0, 2)) - 56) * 94 + Convert.ToInt16(_gbcode.Substring(_gbcode.Length - 2, 2));
                return CodeData.Substring(pos - 1, 1);
            }
            return " ";
        }
        #endregion

        #region 识别Js中的Ascii 字符串为中文
        /// <summary>
        /// 识别Js中的Ascii 字符串为中文
        /// </summary>
        /// <param name="AsciiString"></param>
        /// <returns></returns>
        public static string AsciiToNative(this string AsciiString)
        {
            string result = AsciiString;
            Match m = new Regex("(\\\\u([\\w]{4}))").Match(AsciiString);
            while (m.Success)
            {
                string v = m.Value;
                string word = v.Substring(2);
                byte[] codes = new byte[2];
                int code = Convert.ToInt32(word.Substring(0, 2), 16);
                int code2 = Convert.ToInt32(word.Substring(2), 16);
                codes[0] = (byte)code2;
                codes[1] = (byte)code;
                result = result.Replace(v, Encoding.Unicode.GetString(codes));

                m = m.NextMatch();
            }
            return result;

            //MatchCollection mc = Regex.Matches(AsciiString, "(\\\\u([\\w]{4}))");
            //if (mc != null && mc.Count > 0)
            //{
            //    StringBuilder sb = new StringBuilder();
            //    foreach (Match m2 in mc)
            //    {
            //        string v = m2.Value;
            //        string word = v.Substring(2);
            //        byte[] codes = new byte[2];
            //        int code = Convert.ToInt32(word.Substring(0, 2), 16);
            //        int code2 = Convert.ToInt32(word.Substring(2), 16);
            //        codes[0] = (byte)code2;
            //        codes[1] = (byte)code;
            //        sb.Append(Encoding.Unicode.GetString(codes));
            //    }
            //    return sb.ToString();
            //}
            //else
            //{
            //    return AsciiString;
            //}
        }
        #endregion

        #region 转换汉字为js中使用的Ascii字符串
        /// <summary>
        /// 转换汉字为js中使用的Ascii字符串
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string NativeToAscii(this string text)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(text);
            string lowCode = "", temp = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                if (i % 2 == 0)
                {
                    temp = System.Convert.ToString(bytes[i], 16);//取出元素4编码内容（两位16进制）
                    if (temp.Length < 2) temp = "0" + temp;
                }
                else
                {
                    string mytemp = Convert.ToString(bytes[i], 16);
                    if (mytemp.Length < 2) mytemp = "0" + mytemp; lowCode = lowCode + @"\u" + mytemp + temp;//取出元素4编码内容（两位16进制）
                }
            }
            return lowCode;
        }
        #endregion
    }
}
