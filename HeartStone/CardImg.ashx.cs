using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using Common.SQL;
using System.Data;

namespace HeartStone
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class CardImg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            // 获取请求ID
            string CardName = context.Request.QueryString["CardName"];
            if (CardName == null)
            {
                throw new ApplicationException("Must specify ID");
            }
            // 创建获取相应记录的参数化命令


            SqlParameter[] prm = new SqlParameter[1];
            prm[0] = new SqlParameter("@CardName", SqlDbType.VarChar, 50);
            prm[0].Value = CardName;

            string sql = "select img from carddetail where cardname = @CardName";
            SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql, prm);

            if (dr.Read())
            {
                // 指定缓冲区大小，字节流读入的缓冲区，开始写操作的缓冲区索引
                int bufferSize = 100;
                byte[] bytes = new byte[bufferSize];
                long bytesRead;
                long readFrom = 0;
                // 每次读取字段的100个字节
                do
                {
                    bytesRead = dr.GetBytes(0, readFrom, bytes, 0, bufferSize);
                    context.Response.BinaryWrite(bytes);
                    readFrom += bufferSize;
                } while (bytesRead == bufferSize);
            }
            dr.Close();
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
