using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Http;
using System.Text;
using System.Xml.Linq;

namespace HeartStone
{
    public partial class GrabCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //string content = HttpWebHelper.GetWebContent("http://db.duowan.com/lushi/card/list/eyJwIjoxLCJzb3J0IjoiaWQuZGVzYyJ9.html", Encoding.UTF8);
                GrebWebSite();
            }
        }

        private void   GrebWebSite()
        {
            string content = HttpWebHelper.GetWebContent("http://db.duowan.com/lushi/card/list/eyJSYXJpdHkiOiJcdTRmMjBcdThiZjQiLCJwIjoiMSIsInNvcnQiOiJpZC5kZXNjIn0_3_.html", Encoding.UTF8);            
            divFrame.InnerHtml = content;             
        }
    }
}
