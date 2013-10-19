using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Voodoo
{
    public static class XmlHelper
    {
        #region 序列化为xml字符串
        /// <summary>
        /// 序列化为xml字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeToXML(this object obj)
        {
            return Serialize(obj);
        }
        #endregion

        #region 反序列化字符串为对象
        /// <summary>
        /// 反序列化字符串为对象
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static object DeSerializeTo(this string str, Type type)
        {
            return DeSerialize(type, str);
        }
        #endregion

        private static Dictionary<int, XmlSerializer> serializer_dict = new Dictionary<int, XmlSerializer>();

        #region 反序列化读取XML实体类型文件内容
        /// <summary>
        /// 反序列化读取XML实体类型文件内容
        /// </summary>
        /// <param name="Types">实体类型，例如typeof(Ws_blockSet)</param>
        /// <param name="FilePath">相对地址，例如"~/files.xml"</param>
        /// <returns>返回的实体</returns>
        public static object Read(Type Types, string FilePath)
        {
            if (System.IO.File.Exists(FilePath))
            {
                FileStream stream = null;
                object objectValue = string.Empty;
                stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(Types);
                objectValue = serializer.Deserialize(stream);
                stream.Flush();
                stream.Close();
                return objectValue;
            }
            return null;
        }
        #endregion

        #region 读取XML文件内容,返回DataSet
        /// <summary>
        /// 读取XML文件内容,返回DataSet 
        /// </summary>
        /// <param name="filePath">XML文件的相对地址，例如"~/files.xml"</param>
        /// <returns>返回DataSet </returns>
        public static DataSet Read(string filePath)
        {
            filePath = System.Web.HttpContext.Current.Server.MapPath(filePath);
            DataSet set = new DataSet();
            if (System.IO.File.Exists(filePath))
            {
                set.ReadXml(filePath);
            }
            return set;
        }
        #endregion

        #region 读取Xml文件内容 返回 DataView
        /// <summary>
        /// 读取Xml文件内容 返回 DataView 
        /// </summary>
        /// <param name="Path">XML文件的相对地址，例如"~/files.xml"</param>
        /// <returns>返回 DataView</returns>
        public static DataView ReadView(string filePath)
        {
            filePath = System.Web.HttpContext.Current.Server.MapPath(filePath);
            DataSet set = new DataSet();
            DataView view = null;
            if (System.IO.File.Exists(filePath))
            {
                set.ReadXml(filePath);
                if (set.Tables.Count > 0)
                {
                    view = new DataView(set.Tables[0]);
                }
            }
            return view;
        }
        #endregion

        #region 序列化保存XML实体类型文件
        /// <summary>
        /// 序列化保存XML实体类型文件
        /// </summary>
        /// <param name="Info">要序列化的实体</param>
        /// <param name="FilePath">绝对地址，例如"c:\files.xml"</param>
        public static void SaveSerialize(object Info, string FilePath)
        {
            SaveSerialize(Info.GetType(), Info, FilePath);
        }
        #endregion

        #region 序列化保存XML实体类型文件，重写
        /// <summary>
        /// 序列化保存XML实体类型文件，重写
        /// </summary>
        /// <param name="Types">要序列化的实体类型，例如typeof(Ws_blockSet)</param>
        /// <param name="Info">要序列化的实体</param>
        /// <param name="FilePath">相对地址，例如"~/files.xml"</param>
        public static void SaveSerialize(Type Types, object Info, string FilePath)
        {
            FileInfo file = new FileInfo(FilePath);

            if (!Directory.Exists(file.DirectoryName))//目录是否存在
            {
                Directory.CreateDirectory(file.DirectoryName);//不存在则创建
            }
            FileStream stream = null;
            stream = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.Write);
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
            writer.Write(Serialize(Info));
            writer.Close();
            stream.Close();
        }
        #endregion

        public static XmlSerializer GetSerializer(Type t)
        {
            int type_hash = t.GetHashCode();

            if (!serializer_dict.ContainsKey(type_hash))
                serializer_dict.Add(type_hash, new XmlSerializer(t));

            return serializer_dict[type_hash];
        }

        /// <summary>
        /// xml序列化成字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>xml字符串</returns>
        public static string Serialize(object obj)
        {
            string returnStr = "";
            XmlSerializer serializer = GetSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            XmlTextWriter xtw = null;
            StreamReader sr = null;
            try
            {
                xtw = new System.Xml.XmlTextWriter(ms, Encoding.UTF8);
                xtw.Formatting = System.Xml.Formatting.Indented;
                serializer.Serialize(xtw, obj);
                ms.Seek(0, SeekOrigin.Begin);
                sr = new StreamReader(ms);
                returnStr = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (xtw != null)
                    xtw.Close();
                if (sr != null)
                    sr.Close();
                ms.Close();
            }
            return returnStr;

        }

        public static object DeSerialize(Type type, string s)
        {
            byte[] b = System.Text.Encoding.UTF8.GetBytes(s);
            try
            {
                XmlSerializer serializer = GetSerializer(type);
                return serializer.Deserialize(new MemoryStream(b));
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// DataTable转换为XML
        /// </summary>
        /// <param name="dt">要进行转换的DataTable</param>
        /// <returns>返回XML源文件</returns>
        public static string DataTableToXML(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<" + dt.TableName + ">");
            foreach (DataRow row in dt.Rows)
            {
                sb.Append("<item>");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sb.Append("<" + dt.Columns[i].ColumnName + ">" + row[i].ToString() + "</" + dt.Columns[i].ColumnName + ">");
                }
                sb.Append("</item>");
            }
            sb.Append("</" + dt.TableName + ">");

            return sb.ToS();
        }
    }
}
