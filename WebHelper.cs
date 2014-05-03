using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
namespace jsb
{
  public   class WebHelper
    {

        //url是要访问的网站地址，charSet是目标网页的编码，如果传入的是null或者""，那就自动分析网页的编码
        private string getHtml(string url, string charSet)
        {
            //创建WebClient实例myWebClient
            WebClient myWebClient = new WebClient();
            // 需要注意的：
            //有的网页可能下不下来，有种种原因比如需要cookie,编码问题等等
            //这是就要具体问题具体分析比如在头部加入cookie
            // webclient.Headers.Add("Cookie", cookie);
            //这样可能需要一些重载方法。根据需要写就可以了

            //获取或设置用于对向 Internet 资源的请求进行身份验证的网络凭据。
            myWebClient.Credentials = CredentialCache.DefaultCredentials;
            //如果服务器要验证用户名,密码
            //NetworkCredential mycred = new NetworkCredential(struser, strpassword);
            //myWebClient.Credentials = mycred;
            //从资源下载数据并返回字节数组。（加@是因为网址中间有"/"符号）
            byte[] myDataBuffer = myWebClient.DownloadData(url);
            string strWebData = Encoding.Default.GetString(myDataBuffer);

            //获取网页字符编码描述信息
            Match charSetMatch = Regex.Match(strWebData, "<meta([^<]*)charset=([^<]*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string webCharSet = charSetMatch.Groups[2].Value;
            if (charSet == null || charSet == "")
                charSet = webCharSet;

            if (charSet != null && charSet != "" && Encoding.GetEncoding(charSet) != Encoding.Default)
                strWebData = Encoding.GetEncoding(charSet).GetString(myDataBuffer);
            return strWebData;
        }

    }
}
