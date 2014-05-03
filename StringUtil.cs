using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using Microsoft.VisualBasic;
namespace jsb
{
    public class StringUtil
    {
        public static string DeleteUnVisibleChar(string sourceString)
        {
            StringBuilder builder = new StringBuilder(0x83);
            for (int i = 0; i < sourceString.Length; i++)
            {
                int num2 = sourceString[i];
                if (num2 >= 0x10)
                {
                    builder.Append(sourceString[i].ToString());
                }
            }
            return builder.ToString();
        }

        public static string DelLastComma(string origin)
        {
            if (origin.IndexOf(",") == -1)
            {
                return origin;
            }
            return origin.Substring(0, origin.LastIndexOf(","));
        }

        public static string GetArrayString(string[] stringArray)
        {
            string str = null;
            for (int i = 0; i < stringArray.Length; i++)
            {
                str = str + stringArray[i];
            }
            return str;
        }

        public static int GetByteCount(string strTmp)
        {
            int num = 0;
            for (int i = 0; i < strTmp.Length; i++)
            {
                if (Encoding.UTF8.GetByteCount(strTmp.Substring(i, 1)) == 3)
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
            }
            return num;
        }

        public static int GetByteIndex(int intIns, string strTmp)
        {
            int num = 0;
            if (strTmp.Trim() == "")
            {
                return intIns;
            }
            for (int i = 0; i < strTmp.Length; i++)
            {
                if (Encoding.UTF8.GetByteCount(strTmp.Substring(i, 1)) == 3)
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
                if (num >= intIns)
                {
                    return (i + 1);
                }
            }
            return num;
        }

        public static int GetStringCount(string[] stringArray, string findString)
        {
            int num = -1;
            string arrayString = GetArrayString(stringArray);
            string str2 = arrayString;
            while (str2.IndexOf(findString) >= 0)
            {
                str2 = arrayString.Substring(str2.IndexOf(findString));
                num++;
            }
            return num;
        }

        public static int GetStringCount(string sourceString, string findString)
        {
            int num = 0;
            int length = findString.Length;
            string str = sourceString;
            while (str.IndexOf(findString) >= 0)
            {
                str = str.Substring(str.IndexOf(findString) + length);
                num++;
            }
            return num;
        }

        public static string GetSubString(string sourceString, string startString)
        {
            try
            {
                int index = sourceString.ToUpper().IndexOf(startString.ToUpper());
                if (index > 0)
                {
                    return sourceString.Substring(index);
                }
                return sourceString;
            }
            catch
            {
                return "";
            }
        }

        public static string GetSubString(string sourceString, string beginRemovedString, string endRemovedString)
        {
            try
            {
                if (sourceString.IndexOf(beginRemovedString) != 0)
                {
                    beginRemovedString = "";
                }
                if (sourceString.LastIndexOf(endRemovedString, (int)(sourceString.Length - endRemovedString.Length)) < 0)
                {
                    endRemovedString = "";
                }
                int length = beginRemovedString.Length;
                int num2 = (sourceString.Length - beginRemovedString.Length) - endRemovedString.Length;
                if (num2 > 0)
                {
                    return sourceString.Substring(length, num2);
                }
                return sourceString;
            }
            catch
            {
                return sourceString;
            }
        }
        /// <summary>
        /// 第一段被分割的字符
        /// </summary>
        /// <param name="sourceString">任意字符串</param>
        /// <param name="splitChar">分隔符</param>
        /// <returns>第一段字符</returns>
        public static string LeftSplit(string sourceString, char splitChar)
        {
            string str = null;
            string[] strArray = sourceString.Split(new char[] { splitChar });
            if (strArray.Length > 0)
            {
                str = strArray[0].ToString();
            }
            return str;
        }
        /// <summary>
        /// 去除字符
        /// </summary>
        /// <param name="sourceString">任意字符串</param>
        /// <param name="removedString">想要去除的字符</param>
        /// <returns></returns>
        public static string Remove(string sourceString, string removedString)
        {
            try
            {
                if (sourceString.IndexOf(removedString) < 0)
                {
                    throw new Exception("原字符串中不包含移除字符串！");
                }
                string str = sourceString;
                int length = sourceString.Length;
                int count = removedString.Length;
                int startIndex = length - count;
                if (sourceString.Substring(startIndex).ToUpper() == removedString.ToUpper())
                {
                    str = sourceString.Remove(startIndex, count);
                }
                return str;
            }
            catch
            {
                return sourceString;
            }
        }
        /// <summary>
        /// 最后一段字符
        /// </summary>
        /// <param name="sourceString">任意字符串</param>
        /// <param name="splitChar">分隔符</param>
        /// <returns></returns>
        public static string RightSplit(string sourceString, char splitChar)
        {
            string str = null;
            string[] strArray = sourceString.Split(new char[] { splitChar });
            if (strArray.Length > 0)
            {
                str = strArray[strArray.Length - 1].ToString();
            }
            return str;
        }
        /// <summary>
        /// 为空或null判断
        /// </summary>
        /// <param name="str"></param>
        /// <returns>bool</returns>
        public static bool IsNullEmpty(string str)
        {
            return string.IsNullOrEmpty(str);
        }

        static public string IsNullEmpty(string str, string defaultValue)
        {

            return IsNullEmpty(str) ? defaultValue : str;
        }
        /// <summary>
        /// 数据库字符过滤
        /// </summary>
        /// <param name="str">任意字符串</param>
        /// <returns></returns>
        static public string SafeSql(string str)
        {
            str = IsNullEmpty(str) ? "" : str.Replace("'", "''");
            str = new Regex("exec", RegexOptions.IgnoreCase).Replace(str, "&#101;xec");
            str = new Regex("xp_cmdshell", RegexOptions.IgnoreCase).Replace(str, "&#120;p_cmdshell");
            str = new Regex("select", RegexOptions.IgnoreCase).Replace(str, "&#115;elect");
            str = new Regex("insert", RegexOptions.IgnoreCase).Replace(str, "&#105;nsert");
            str = new Regex("update", RegexOptions.IgnoreCase).Replace(str, "&#117;pdate");
            str = new Regex("delete", RegexOptions.IgnoreCase).Replace(str, "&#100;elete");
            str = new Regex("drop", RegexOptions.IgnoreCase).Replace(str, "&#100;rop");
            str = new Regex("create", RegexOptions.IgnoreCase).Replace(str, "&#99;reate");
            str = new Regex("rename", RegexOptions.IgnoreCase).Replace(str, "&#114;ename");
            str = new Regex("truncate", RegexOptions.IgnoreCase).Replace(str, "&#116;runcate");
            str = new Regex("alter", RegexOptions.IgnoreCase).Replace(str, "&#97;lter");
            str = new Regex("exists", RegexOptions.IgnoreCase).Replace(str, "&#101;xists");
            str = new Regex("master.", RegexOptions.IgnoreCase).Replace(str, "&#109;aster.");
            str = new Regex("restore", RegexOptions.IgnoreCase).Replace(str, "&#114;estore");
            return str;
        }
        static public string SafeSqlSimple(string str)
        {
            str = IsNullEmpty(str) ? "" : str.Replace("'", "''");
            return str;
        }
        public static string NoHtml(string Htmlstring)
        {
            if (Htmlstring == null)
            {
                return "";
            }
            else
            {
                //删除脚本
                Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
                //删除HTML
                Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "xp_cmdshell", "", RegexOptions.IgnoreCase);

                //删除与数据库相关的词
                Htmlstring = Regex.Replace(Htmlstring, "select", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "insert", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "delete from", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "count''", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "drop table", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "truncate", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "asc", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "mid", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "char", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "xp_cmdshell", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "exec master", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "net localgroup administrators", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "and", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "net user", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "or", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "net", "", RegexOptions.IgnoreCase);
                //Htmlstring = Regex.Replace(Htmlstring, "*", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "-", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "delete", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "drop", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "script", "", RegexOptions.IgnoreCase);

                //特殊的字符
                Htmlstring = Htmlstring.Replace("<", "");
                Htmlstring = Htmlstring.Replace(">", "");
                Htmlstring = Htmlstring.Replace("*", "");
                Htmlstring = Htmlstring.Replace("-", "");
                Htmlstring = Htmlstring.Replace("?", "");
                Htmlstring = Htmlstring.Replace("'", "''");
                Htmlstring = Htmlstring.Replace(",", "");
                Htmlstring = Htmlstring.Replace("/", "");
                Htmlstring = Htmlstring.Replace(";", "");
                Htmlstring = Htmlstring.Replace("*/", "");
                Htmlstring = Htmlstring.Replace("\r\n", "");
                Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
                return Htmlstring;
            }
        }

        /// <summary>
        /// html解码
        /// </summary>
        /// <param name="htmlStr"></param>
        /// <returns></returns>
        public static string UnHtml(string htmlStr)
        {
            if (IsNullEmpty(htmlStr)) return string.Empty;
            return ShowXmlHtml(htmlStr.Replace("\"", "\\\"")).Replace(" ", "&nbsp;").Replace("\n", "<br />");
        }
        public static string ShowXmlHtml(string htmlStr)
        {
            if (IsNullEmpty(htmlStr)) return string.Empty;
            string str = htmlStr.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;");
            return str;
        }
        public static string ShowHtml(string htmlStr)
        {
            if (IsNullEmpty(htmlStr)) return string.Empty;
            string str = htmlStr;
            str = Regex.Replace(str, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "_$1.$2", RegexOptions.IgnoreCase);
            str = new Regex("<script", RegexOptions.IgnoreCase).Replace(str, "<_script");
            str = new Regex("<object", RegexOptions.IgnoreCase).Replace(str, "<_object");
            str = new Regex("javascript:", RegexOptions.IgnoreCase).Replace(str, "_javascript:");
            str = new Regex("vbscript:", RegexOptions.IgnoreCase).Replace(str, "_vbscript:");
            str = new Regex("expression", RegexOptions.IgnoreCase).Replace(str, "_expression");
            str = new Regex("@import", RegexOptions.IgnoreCase).Replace(str, "_@import");
            str = new Regex("<iframe", RegexOptions.IgnoreCase).Replace(str, "<_iframe");
            str = new Regex("<frameset", RegexOptions.IgnoreCase).Replace(str, "<_frameset");
            str = Regex.Replace(str, @"(\<|\s+)o([a-z]+\s?=)", "$1_o$2", RegexOptions.IgnoreCase);
            str = new Regex(@" (on[a-zA-Z ]+)=", RegexOptions.IgnoreCase).Replace(str, " _$1=");
            return str;
        }
        /// <summary>
        /// 移除html代码
        /// </summary>
        /// <param name="HtmlCode">html代码</param>
        /// <returns></returns>
        public static string RemoveHTML(string HtmlCode)
        {
            string MatchVale = HtmlCode;
            MatchVale = new Regex("<br>", RegexOptions.IgnoreCase).Replace(MatchVale, "\n");
            foreach (Match s in Regex.Matches(HtmlCode, "<[^{><}]*>"))
            {
                MatchVale = MatchVale.Replace(s.Value, "");
            }
            //"(<[^{><}]*>)"//@"<[\s\S-! ]*?>"//"<.+?>"//<(.*)>.*<\/\1>|<(.*) \/>//<[^>]+>//<(.|\n)+?>     
            MatchVale = new Regex("\n", RegexOptions.IgnoreCase).Replace(MatchVale, "<br>");
            return MatchVale;
        }
        /// <summary>
        /// 移除所有html代码
        /// </summary>
        /// <param name="HtmlCode">html代码</param>
        /// <returns></returns>
        public static string RemoveAllHTML(string content)
        {
            string pattern = "<[^>]*>";
            return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
        }
        public static string UrlEncode(string str)
        { return HttpUtility.UrlEncode(str); }
        public static string UrlDecode(string str)
        { return HttpUtility.UrlDecode(str); }
        public static string HtmlEncode(string str)
        { return HttpUtility.HtmlEncode(str); }
        public static string HtmlDecode(string str)
        { return HttpUtility.HtmlDecode(str); }

        /// <summary>
        /// 转换ubb格式
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        public static string UBB( string chr)
        { 
            if (IsNullEmpty(chr)) return string.Empty; 
            chr = HtmlEncode(chr); 
            chr = Regex.Replace(chr, @"<script(?<x>[^\>]*)>(?<y>[^\>]*)            \</script\>", @"&lt;script$1&gt;$2&lt;/script&gt;", RegexOptions.IgnoreCase); 
            chr = Regex.Replace(chr, @"\[url=(?<x>[^\]]*)\](?<y>[^\]]*)\[/url\]", @"<a href=$1  target=_blank>$2</a>", RegexOptions.IgnoreCase); 
            chr = Regex.Replace(chr, @"\[url\](?<x>[^\]]*)\[/url\]", @"<a href=$1 target=_blank>$1</a>", RegexOptions.IgnoreCase);
            chr = Regex.Replace(chr, @"\[email=(?<x>[^\]]*)\](?<y>[^\]]*)\[/email\]", @"<a href=$1>$2</a>", RegexOptions.IgnoreCase); 
            chr = Regex.Replace(chr, @"\[email\](?<x>[^\]]*)\[/email\]", @"<a href=$1>$1</a>", RegexOptions.IgnoreCase);
            chr = Regex.Replace(chr, @"\[flash](?<x>[^\]]*)\[/flash]", @"<OBJECT codeBase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=4,0,2,0 classid=clsid:D27CDB6E-AE6D-11cf-96B8-444553540000 width=500 height=400><PARAM NAME=movie VALUE=""$1""><PARAM NAME=quality VALUE=high><embed src=""$1"" quality=high pluginspage='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash' type='application/x-shockwave-flash' width=500 height=400>$1</embed></OBJECT>", RegexOptions.IgnoreCase);
            chr = Regex.Replace(chr, @"\", @"<IMG SRC=""$1"" border=0>", RegexOptions.IgnoreCase);
            chr = Regex.Replace(chr, @"\[color=(?<x>[^\]]*)\](?<y>[^\]]*)\[/color\]", @"<font color=$1>$2</font>", RegexOptions.IgnoreCase);
            chr = Regex.Replace(chr, @"\[face=(?<x>[^\]]*)\](?<y>[^\]]*)\[/face\]", @"<font face=$1>$2</font>", RegexOptions.IgnoreCase); 
            chr = Regex.Replace(chr, @"\[size=1\](?<x>[^\]]*)\[/size\]", @"<font size=1>$1</font>", RegexOptions.IgnoreCase); 
            chr = Regex.Replace(chr, @"\[size=2\](?<x>[^\]]*)\[/size\]", @"<font size=2>$1</font>", RegexOptions.IgnoreCase); 
            chr = Regex.Replace(chr, @"\[size=3\](?<x>[^\]]*)\[/size\]", @"<font size=3>$1</font>", RegexOptions.IgnoreCase);
            chr = Regex.Replace(chr, @"\[size=4\](?<x>[^\]]*)\[/size\]", @"<font size=4>$1</font>", RegexOptions.IgnoreCase);
            chr = Regex.Replace(chr, @"\[align=(?<x>[^\]]*)\](?<y>[^\]]*)\[/align\]", @"<align=$1>$2</align>", RegexOptions.IgnoreCase);
            chr = Regex.Replace(chr, @"\[fly](?<x>[^\]]*)\[/fly]", @"<marquee width=90% behavior=alternate scrollamount=3>$1</marquee>", RegexOptions.IgnoreCase); chr = Regex.Replace(chr, @"\[move](?<x>[^\]]*)\[/move]", @"<marquee scrollamount=3>$1</marquee>", RegexOptions.IgnoreCase);
            chr = Regex.Replace(chr, @"\[glow=(?<x>[^\]]*),(?<y>[^\]]*),(?<z>[^\]]*)\](?<w>[^\]]*)\[/glow\]", @"<table width=$1 style='filter:glow(color=$2, strength=$3)'>$4</table>", RegexOptions.IgnoreCase); 
            chr = Regex.Replace(chr, @"\[shadow=(?<x>[^\]]*),(?<y>[^\]]*),(?<z>[^\]]*)\](?<w>[^\]]*)\[/shadow\]", @"<table width=$1 style='filter:shadow(color=$2, strength=$3)'>$4</table>", RegexOptions.IgnoreCase);
            chr = Regex.Replace(chr, @"\[b\](?<x>[^\]]*)\[/b\]", @"<b>$1</b>", RegexOptions.IgnoreCase); 
            chr = Regex.Replace(chr, @"\[i\](?<x>[^\]]*)\[/i\]", @"<i>$1</i>", RegexOptions.IgnoreCase);
            chr = Regex.Replace(chr, @"\[u\](?<x>[^\]]*)\[/u\]", @"<u>$1</u>", RegexOptions.IgnoreCase);
            chr = Regex.Replace(chr, @"\[code\](?<x>[^\]]*)\[/code\]", @"<pre id=code><font size=1 face='Verdana, Arial' id=code>$1</font id=code></pre id=code>", RegexOptions.IgnoreCase);
            chr = Regex.Replace(chr, @"\[list\](?<x>[^\]]*)\[/list\]", @"<ul>$1</ul>", RegexOptions.IgnoreCase); 
            chr = Regex.Replace(chr, @"\[list=1\](?<x>[^\]]*)\[/list\]", @"<ol type=1>$1</ol id=1>", RegexOptions.IgnoreCase); 
            chr = Regex.Replace(chr, @"\[list=a\](?<x>[^\]]*)\[/list\]", @"<ol type=a>$1</ol id=a>", RegexOptions.IgnoreCase);
            chr = Regex.Replace(chr, @"\[\*\](?<x>[^\]]*)\[/\*\]", @"<li>$1</li>", RegexOptions.IgnoreCase); 
            chr = Regex.Replace(chr, @"\[quote](?<x>.*)\[/quote]", @"<center>—— 以下是引用 ——<table border='1' width='80%' cellpadding='10' cellspacing='0' ><tr><td>$1</td></tr></table></center>", RegexOptions.IgnoreCase);
            return (chr);
        }

        /// <summary>
        /// 颜色转换
        /// </summary>
        /// <param name="s">#fffff</param>
        /// <returns>rgb(1,1,1)</returns>
        public static Color ToColor(string s) 
        {
            s = s.Replace("#", string.Empty); 
            byte a = System.Convert.ToByte("ff", 16);
            byte pos = 0; if (s.Length == 8) 
            {
                a = System.Convert.ToByte(s.Substring(pos, 2), 16); 
                pos = 2;
            } 
            byte r = System.Convert.ToByte(s.Substring(pos, 2), 16);
            pos += 2;
            byte g = System.Convert.ToByte(s.Substring(pos, 2), 16);
            pos += 2; 
            byte b = System.Convert.ToByte(s.Substring(pos, 2), 16); 
            return Color.FromArgb(a, r, g, b); 
        }
    }
}

