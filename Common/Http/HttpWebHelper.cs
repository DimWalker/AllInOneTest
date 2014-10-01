/*
图片下载参考地址：http://blog.csdn.net/vip__888/article/details/5646260
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;

namespace Common.Http
{
    public sealed class HttpWebHelper
    {
        public HttpWebHelper() { }

        #region 获取cookie

        #endregion

        #region 图片下载
        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public static bool DowloadImg(string Url, string savePath)
        {
            return DowloadImg(Url, savePath);
        }
        /// <summary>
        /// 下载图片，理论上传参cookCon可以验证码
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="savePath"></param>
        /// <param name="cookCon"></param>
        /// <returns></returns>
        public static bool DowloadImg(string Url, string savePath, CookieContainer cookCon)
        {
            bool bol = true;

            try
            {

                File.WriteAllBytes(savePath, GetImgHex(Url, cookCon));

            }
            catch (IOException ex)
            {
                bol = false;
            }
            catch (Exception ex)
            {
                bol = false;
            }
            return bol;
        }

        /// <summary>
        /// 获取图片二进制数组
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static byte[] GetImgHex(string Url)
        {
            return GetImgHex(Url, new CookieContainer());
        }
        /// <summary>
        /// 获取图片二进制数组
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="cookCon"></param>
        /// <returns></returns>
        public static byte[] GetImgHex(string Url, CookieContainer cookCon)
        {

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(Url);
            //属性配置
            webRequest.AllowWriteStreamBuffering = true;
            webRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
            webRequest.MaximumResponseHeadersLength = -1;
            webRequest.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
            webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; Maxthon; .NET CLR 1.1.4322)";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "GET";
            webRequest.Headers.Add("Accept-Language", "zh-cn");
            webRequest.Headers.Add("Accept-Encoding", "gzip,deflate");
            webRequest.KeepAlive = true;
            webRequest.CookieContainer = cookCon;
            try
            {
                //获取服务器返回的资源
                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (Stream sream = webResponse.GetResponseStream())
                    {
                        List<byte> list = new List<byte>();
                        while (true)
                        {
                            int data = sream.ReadByte();
                            if (data == -1)
                            { break; }
                            list.Add((byte)data);
                        }
                        return list.ToArray();
                    }
                }                
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 抓取网页
        //根据Url地址得到网页的html源码 
        public static string GetWebContent(string Url)
        {
            return GetWebContent(Url, Encoding.Default);
        }

        public static string GetWebContent(string Url, Encoding encoding)
        {
            string strResult = "";
            HttpWebRequest request;
            try
            {
                //声明一个HttpWebRequest请求 
                request = (HttpWebRequest)WebRequest.Create(Url);
                //设置连接超时时间 
                request.Timeout = 60000;                
                request.Headers.Set("Pragma", "no-cache");              
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; Maxthon; .NET CLR 1.1.4322)"; 
                request.Method = "GET";                 
                request.KeepAlive = false ;
                request.ServicePoint.ConnectionLimit = 50;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    //if (response.StatusCode != HttpStatusCode.OK) //如果服务器未响应，那么继续等待相应
                    //{ 
                    
                    //}
                    using (Stream streamReceive = response.GetResponseStream())
                    {
                        using (StreamReader streamReader = new StreamReader(streamReceive, encoding))
                        {
                            strResult = streamReader.ReadToEnd();
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            request.Abort();
            return strResult;
        }
        #endregion
    }
}
