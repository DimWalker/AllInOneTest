using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;

namespace HeartStone
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class HeartStone : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string xmldata = context.Request.Form["xmldata"];
            XDocument xd = XDocument.Parse(xmldata);
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            xd.Save(baseDir + "\\XmlData\\page_legend.xml");
            //XElement xe = xd.Descendants("CardDetail").First();
            //byte[] imgHex = Common.Http.HttpWebHelper.GetImgHex(xe.Descendants("ImgSrc").First().Value);
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
