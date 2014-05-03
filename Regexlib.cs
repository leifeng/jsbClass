using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace jsb
{
    /// <summary> 
    /// Regexlib 的摘要说明。 
    /// </summary> 
    public class Regexlib
    {
        public Regexlib()
        {
            // 
            // TODO: 在此处添加构造函数逻辑 
            // 
        }

        //搜索输入字符串并返回所有 href=“...”值 
        string DumpHrefs(String inputString)
        {
            Regex r;
            Match m;
            string str = string.Empty;

            r = new Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))",
             RegexOptions.IgnoreCase | RegexOptions.Compiled);
            for (m = r.Match(inputString); m.Success; m = m.NextMatch())
            {

                str += "Found href " + m.Groups[1];

            }
            return str;
        }

        //验证Email地址 
        bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format. 
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        //dd-mm-yy 的日期形式代替 mm/dd/yy 的日期形式。 
        string MDYToDMY(String input)
        {
            return Regex.Replace(input, "\\b(?\\d{1,2})/(?\\d{1,2})/(?\\d{2,4})\\b", "${day}-${month}-${year}");
        }

        //验证是否为小数 
        bool IsValidDecimal(string strIn)
        {

            return Regex.IsMatch(strIn, @"[0].\d{1,2}|[1]");
        }

        //验证是否为电话号码 
        bool IsValidTel(string strIn)
        {
            return Regex.IsMatch(strIn, @"(\d+-)?(\d{4}-?\d{7}|\d{3}-?\d{8}|^\d{7,8})(-\d+)?");
        }

        //验证年月日 
        bool IsValidDate(string strIn)
        {
            return Regex.IsMatch(strIn, @"^2\d{3}-(?:0?[1-9]|1[0-2])-(?:0?[1-9]|[1-2]\d|3[0-1])(?:0?[1-9]|1\d|2[0-3]):(?:0?[1-9]|[1-5]\d):(?:0?[1-9]|[1-5]\d)$");
        }

        //验证后缀名 
        bool IsValidPostfix(string strIn)
        {
            return Regex.IsMatch(strIn, @"\.(?i:gif|jpg)$");
        }

        //验证字符是否在4至12之间 
        bool IsValidByte(string strIn)
        {
            return Regex.IsMatch(strIn, @"^[a-z]{4,12}$");
        }

        //验证IP 
        bool IsValidIp(string strIn)
        {
            return Regex.IsMatch(strIn, @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
        }
        bool IsUrl(string strIn)
        {
            string pet = @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$";
            //@"^http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";        
            return Regex.IsMatch(strIn, pet);
        }

        bool IsDateTime(string strIn)
        {
            //string pet = @"^(?:(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(\/|-|\.)(?:0?2\1(?:29))$)|(?:(?:1[6-9]|[2-9]\d)?\d{2})(\/|-|\.)(?:(?:(?:0?[13578]|1[02])\2(?:31))|(?:(?:0?[1,3-9]|1[0-2])\2(29|30))|(?:(?:0?[1-9])|(?:1[0-2]))\2(?:0?[1-9]|1\d|2[0-8]))$";         
            string pet = @"^(?=\d)(?:(?!(?:1582(?:\.|-|\/)10(?:\.|-|\/)(?:0?[5-9]|1[0-4]))|(?:1752(?:\.|-|\/)0?9(?:\.|-|\/)(?:0?[3-9]|1[0-3])))(?=(?:(?!000[04]|(?:(?:1[^0-6]|[2468][^048]|[3579][^26])00))(?:(?:\d\d)(?:[02468][048]|[13579][26]))\D0?2\D29)|(?:\d{4}\D(?!(?:0?[2469]|11)\D31)(?!0?2(?:\.|-|\/)(?:29|30))))(\d{4})([-\/.])(0?\d|1[012])\2((?!00)[012]?\d|3[01])(?:$|(?=\x20\d)\x20))?((?:(?:0?[1-9]|1[012])(?::[0-5]\d){0,2}(?:\x20[aApP][mM]))|(?:[01]?\d|2[0-3])(?::[0-5]\d){1,2})?$";
            return Regex.IsMatch(strIn, pet);
        }

    }

}
