using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using System.Text;

namespace HeartStone
{
    public partial class InsertCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //Insert();
                UpdateLegend();
            }
        }

        private void Insert()
        {
            for (int i = 1; i <= 9; i++)
            {
                XDocument xd = XDocument.Load(Server.MapPath("~\\XmlData\\page" + i.ToString() + ".xml"));
                //var xe = from n in xd.Root.Descendants ("CardDetail") 
                //         select n;
                foreach (XElement xe in xd.Root.Descendants("CardDetail"))
                {
                    string sql = "select 1 from CardDetail where CardName='" + xe.Descendants("Name").FirstOrDefault().Value + "'";
                    object rslt = Common.SQL.SqlHelper.ExecuteScalar(Common.SQL.SqlHelper.GetConnection(), CommandType.Text, sql);
                    if (rslt != null && rslt.Equals(1))
                    {
                        continue;
                    }

                    byte[] imgHex = null;
                    int errorCount = 0;
                    bool success = false;
                    while (errorCount < 3 && !success)
                    {
                        try
                        {
                            imgHex = Common.Http.HttpWebHelper.GetImgHex(xe.Descendants("ImgSrc").FirstOrDefault().Value);
                            success = true;
                        }
                        catch
                        {
                            errorCount++;
                        }
                    }

                    InsertCardDetail(xe.Descendants("Name").FirstOrDefault().Value,
                        xe.Descendants("Decription").FirstOrDefault().Value,
                        int.Parse(
                            string.IsNullOrEmpty(xe.Descendants("Cost").FirstOrDefault().Value) ? "0" : xe.Descendants("Cost").FirstOrDefault().Value),
                        int.Parse(
                            string.IsNullOrEmpty(xe.Descendants("Damage").FirstOrDefault().Value) ? "0" : xe.Descendants("Damage").FirstOrDefault().Value),
                        int.Parse(
                            string.IsNullOrEmpty(xe.Descendants("HP").FirstOrDefault().Value) ? "0" : xe.Descendants("HP").FirstOrDefault().Value),
                        xe.Descendants("CardType").FirstOrDefault().Value,
                        xe.Descendants("Occupation").FirstOrDefault().Value,
                        xe.Descendants("Varity").FirstOrDefault().Value,
                        imgHex
                     );

                    if (xe.Descendants("SkillForm").Count() > 0)
                    {
                        foreach (XElement skll in xe.Descendants("SkillForm").Descendants("Skill"))
                        {
                            InsertCardSkill(xe.Descendants("Name").FirstOrDefault().Value, skll.Value);
                        }
                    }
                }
            }
        }
        //
        private void InsertCardDetail(string CardName, string Decription, int Cost, int Damage, int HP, string CardType, string Occupation, string Varity, byte[] Img)
        {
            SqlParameter[] prm = new SqlParameter[9];
            prm[0] = new SqlParameter("@CardName", SqlDbType.VarChar, 50);
            prm[0].Value = CardName;

            prm[1] = new SqlParameter("@Decription", SqlDbType.VarChar, 8000);
            prm[1].Value = Decription;

            prm[2] = new SqlParameter("@Cost", SqlDbType.Int);
            prm[2].Value = Cost;

            prm[3] = new SqlParameter("@Damage", SqlDbType.Int);
            prm[3].Value = Damage;

            prm[4] = new SqlParameter("@HP", SqlDbType.Int);
            prm[4].Value = HP;

            prm[5] = new SqlParameter("@CardType", SqlDbType.VarChar, 50);
            prm[5].Value = CardType;

            prm[6] = new SqlParameter("@Occupation", SqlDbType.VarChar, 50);
            prm[6].Value = Occupation;


            prm[7] = new SqlParameter("@Varity", SqlDbType.VarChar, 50);
            prm[7].Value = Varity;

            prm[8] = new SqlParameter("@Img", SqlDbType.Image);
            prm[8].Value = Img;

            StringBuilder sb = new StringBuilder(256);
            sb.AppendLine("if not exists(select 1 from CardDetail where CardName = @CardName)");
            sb.AppendLine("begin");
            sb.AppendLine("INSERT INTO  [CardDetail] ([CardName] ,[Decription] ,[Cost] ,[Damage] ,[HP] ,[CardType] ,[Occupation] ,[Varity] ,[Img]) " +
                 "VALUES (@CardName, @Decription, @Cost, @Damage, @HP, @CardType, @Occupation, @Varity, @Img)");
            sb.AppendLine("end");

            Common.SQL.SqlHelper.ExecuteNonQuery(Common.SQL.SqlHelper.GetConnection(), CommandType.Text, sb.ToString(), prm);
        }

        private void InsertCardSkill(string CardName, string Skill)
        {
            SqlParameter[] prm = new SqlParameter[2];
            prm[0] = new SqlParameter("@CardName", SqlDbType.VarChar, 50);
            prm[0].Value = CardName;

            prm[1] = new SqlParameter("@Skill", SqlDbType.VarChar, 50);
            prm[1].Value = Skill;

            StringBuilder sb = new StringBuilder(256);
            sb.AppendLine("if not exists(select 1 from CardSkill where CardName = @CardName and Skill=@Skill)");
            sb.AppendLine("begin");
            sb.AppendLine("INSERT INTO  [CardSkill] ([CardName] ,[Skill]) " +
                "VALUES (@CardName, @Skill)");
            sb.AppendLine("end");

            Common.SQL.SqlHelper.ExecuteNonQuery(Common.SQL.SqlHelper.GetConnection(), CommandType.Text, sb.ToString(), prm);
        }


        private void UpdateLegend()
        {
            XDocument xd = XDocument.Load(Server.MapPath("~\\XmlData\\page_legend.xml"));
            //var xe = from n in xd.Root.Descendants ("CardDetail") 
            //         select n;
            foreach (XElement xe in xd.Root.Descendants("CardDetail"))
            {
                string sql = "update CardDetail set Varity = '传说' where CardName='" + xe.Descendants("Name").FirstOrDefault().Value + "'";
                Common.SQL.SqlHelper.ExecuteNonQuery(Common.SQL.SqlHelper.GetConnection(), CommandType.Text, sql);                
            }
        }
    }
}
