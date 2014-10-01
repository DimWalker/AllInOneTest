using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common.SQL;

namespace HeartStone
{
    public partial class CardDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            string sql = "select * from carddetail ";
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnection(), CommandType.Text, sql);
            GridView1.DataSource = ds;
            GridView1.DataBind();

          //  Image1.ImageUrl 

        }
    }
}
