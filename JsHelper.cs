using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;

namespace jsb
{
    /// <summary>
    /// Js操作类
    /// </summary>
    public class JsHelper
    {
        #region Run
        /// <summary>
        /// 运行JS代码
        /// </summary>
        /// <param name="Page">指定Page</param>
        /// <param name="strCode">要注册的代码</param>
        /// <param name="isTop">是否在头部/否则在尾部</param>
        public static void Run(System.Web.UI.Page Page, string strCode, bool isTop)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\">\n");
            sb.Append(strCode.Trim());
            sb.Append("\n</script>\n");
            if (isTop) Page.RegisterClientScriptBlock("RunTopJs", sb.ToString()); else Page.RegisterStartupScript("RunBottomJs", sb.ToString());
        }
        /// <summary>
        /// 运行JS代码
        /// </summary>
        /// <param name="Page">指定Page</param>
        /// <param name="strCode">要注册的代码</param>
        /// <param name="isTop">是否在头部/否则在尾部</param>
        /// <param name="IDStr">Key</param>
        public static void Run(System.Web.UI.Page Page, string strCode, bool isTop, string IDStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\">\n");
            sb.Append(strCode.Trim());
            sb.Append("\n</script>\n");
            if (isTop) Page.RegisterClientScriptBlock(IDStr, sb.ToString()); else Page.RegisterStartupScript(IDStr, sb.ToString());
        }
        /// <summary>
        /// 清空指定注册的JS代码
        /// </summary>
        /// <param name="Page">指定Page</param>
        /// <param name="isTop">是否在头部/否则在尾部</param>
        /// <param name="IDStr">Key</param>
        public static void Run(System.Web.UI.Page Page, bool isTop, string IDStr)
        {
            if (isTop) Page.RegisterClientScriptBlock(IDStr, ""); else Page.RegisterStartupScript(IDStr, "");
        }
        #endregion

        #region Alert
        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="msg">消息</param>
        public static void Alert(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("alert(\"" + msg.Trim() + "\"); \n");
            sb.Append("</script>\n");
            HttpContext.Current.Response.Write(sb.ToString());
        }
        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="Page">指定页</param>
        /// <param name="msg">消息</param>
        public static void Alert(System.Web.UI.Page Page, string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("alert(\"" + msg.Trim() + "\"); \n");
            sb.Append("</script>\n");
            Page.RegisterClientScriptBlock("AlertJs", sb.ToString());
        }
        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="Page">指定页</param>
        /// <param name="msg">消息</param>
        /// <param name="isTop">是否在头部/否则在尾部</param>
        public static void Alert(System.Web.UI.Page Page, string msg, bool isTop)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("alert(\"" + msg.Trim() + "\"); \n");
            sb.Append("</script>\n");
            if (isTop) Page.RegisterClientScriptBlock("AlertTopJs", sb.ToString()); else Page.RegisterStartupScript("AlertBottomJs", sb.ToString());
        }
        /// <summary>
        /// 提示信息 并跳转
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="toURL">跳转地址</param>
        public static void AlertAndRedirect(string message, string toURL)
        {
            string format = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(format, message, toURL));
        }
        #endregion

        #region Import/loadCss/AddAttr/chkFormData
        /// <summary>
        /// 注册一个处部JS文件/或CSS文件
        /// </summary>
        /// <param name="Page">指定页</param>
        /// <param name="filePath">文件</param>
        /// <param name="isTop">是否在头部/否则在尾部</param>
        public static void Import(System.Web.UI.Page Page, string filePath, bool isTop)
        {
            StringBuilder sb = new StringBuilder();
            if (filePath.ToLower().Substring(filePath.Length - 3, 3) == ".js")
            {
                sb.Append("<script language=\"JavaScript\" src=\"" + filePath + "\" type=\"text/javascript\"></script>\n");
                if (isTop) Page.RegisterClientScriptBlock("TopJs", sb.ToString()); else Page.RegisterStartupScript("BottomJs", sb.ToString());
            }
            if (filePath.ToLower().Substring(filePath.Length - 4, 4) == ".css")
            {
                LoadCss(Page, filePath);
            }
        }
        /// <summary>
        /// 注册一个处部CSS文件
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="cssFile">CSS文件</param>
        public static void JsLoadCss(System.Web.UI.Page page, string cssFile)
        {
            Run(page, "setStyle(\"" + cssFile + "\");\n", true);
        }
        /// <summary>
        /// 注册一个处部CSS文件
        /// </summary>
        /// <param name="placeHolder">PlaceHolder组件</param>
        /// <param name="cssFile">CSS文件</param>
        public static void LoadCss(System.Web.UI.WebControls.PlaceHolder placeHolder, string cssFile)
        {
            HtmlGenericControl objLink = new HtmlGenericControl("LINK");
            objLink.Attributes["rel"] = "stylesheet";
            objLink.Attributes["type"] = "text/css";
            objLink.Attributes["href"] = cssFile;
            placeHolder.Controls.Add(objLink);
            //<asp:placeholder id="MyCSS" runat="server"></asp:placeholder> 
        }
        /// <summary>
        /// 注册一个处部CSS文件
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="cssFile">CSS文件</param>
        public static void LoadCss(System.Web.UI.Page page, string cssFile)
        {
            HtmlLink myHtmlLink = new HtmlLink();
            myHtmlLink.Href = cssFile;
            JsHelper.AddAttr(myHtmlLink, "rel", "stylesheet");
            JsHelper.AddAttr(myHtmlLink, "type", "text/css");
            page.Header.Controls.Add(myHtmlLink);
        }
        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="Control">WebControl</param>
        /// <param name="eventStr">名称</param>
        /// <param name="MsgStr">内容</param>
        public static void AddAttr(System.Web.UI.WebControls.WebControl Control, string eventStr, string MsgStr)
        {
            Control.Attributes.Add(eventStr, MsgStr);
        }
        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="Control">HtmlGenericControl</param>
        /// <param name="eventStr">名称</param>
        /// <param name="MsgStr">内容</param>
        public static void AddAttr(System.Web.UI.HtmlControls.HtmlGenericControl Control, string eventStr, string MsgStr)
        {
            Control.Attributes.Add(eventStr, MsgStr);
        }
        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="Control">HtmlGenericControl</param>
        /// <param name="eventStr">名称</param>
        /// <param name="MsgStr">内容</param>
        public static void AddAttr(System.Web.UI.HtmlControls.HtmlControl Control, string eventStr, string MsgStr)
        {
            Control.Attributes.Add(eventStr, MsgStr);
        }

        #endregion
    }
}

