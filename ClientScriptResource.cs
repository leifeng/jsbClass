using System;
using System.Collections.Generic;
using System.Text;

namespace jsb
{
    public class ClientScriptResource
    {
    }

    /*
     * 载入js
     * 
      string rsname = "jsb.Resources.a.js";
      Type rstype = typeof(jsb.ClientScriptResource);
      ClientScriptManager cs = Page.ClientScript;
      ResourcePath.InnerHtml = cs.GetWebResourceUrl(rstype, rsname);
      cs.RegisterClientScriptResource(rstype, rsname);
     
     */



    /*
     * 载入图片
   img scr=ClientScript.GetWebResourceUrl(typeof(jsb.ClientScriptResource), "jsb.Resources.home.jpg")
     */



    /*  
     * 
     * 载入css
     * 
     * HtmlLink cssLink = new HtmlLink();
        cssLink.Href = ClientScript.GetWebResourceUrl(typeof(jsb.ClientScriptResource), "jsb.Resources.b.css");
        cssLink.Attributes.Add("rel", "stylesheet");
        cssLink.Attributes.Add("type", "text/css");
        Page.Header.Controls.Add(cssLink);
     * or
     * 
     * 
     * jsb.JsHelper.LoadCss(Page, ClientScript.GetWebResourceUrl(typeof(jsb.ClientScriptResource), "jsb.Resources.b.css"));
     * 
     * 
     * 
     */
}
