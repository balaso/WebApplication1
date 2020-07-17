using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using WebApplication1.Constants;

namespace WebApplication1.Helper
{
    public static class ApiHelper
    {
        public static HttpWebRequest httpWebRequest { get; set; }

        public static void InitlizeRequest(string requestUrl, string ContentType, string method)
        {
            if (string.IsNullOrEmpty(requestUrl))
            {
                throw new ArgumentException("message", nameof(requestUrl));
            }

            httpWebRequest = WebRequest.Create(requestUrl) as HttpWebRequest;
            
            // set content type
            if (string.IsNullOrEmpty(ContentType))
            {
                httpWebRequest.ContentType = ContentType;
            }
            httpWebRequest.Method = method;
            
        }
        public static object MakeRequest(string requestUrl, object JSONRequest, Type JSONResponseType, string JSONmethod = RequestConstants.DefaultMenthod, string JSONContentType = RequestConstants.ContentType)
        {

            try
            {
                InitlizeRequest(requestUrl, JSONContentType, JSONmethod);

                /*** add request body for POST and PUT method ***/
                if ("POST".Equals(JSONmethod, StringComparison.OrdinalIgnoreCase) || "PUT".Equals(JSONmethod, StringComparison.OrdinalIgnoreCase))
                {
                    string sb = JsonConvert.SerializeObject(JSONRequest);
                    Byte[] bt = Encoding.UTF8.GetBytes(sb);
                    Stream st = ApiHelper.httpWebRequest.GetRequestStream();
                    st.Write(bt, 0, bt.Length);
                    st.Close();
                }

                using (HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse)
                {

                    if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format(
                        "Server error (HTTP {0}: {1}).", response.StatusCode,
                    response.StatusDescription));

                    // DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Response));
                    // object objResponse = JsonConvert.DeserializeObject();
                    Stream stream1 = response.GetResponseStream();
                    StreamReader sr = new StreamReader(stream1);
                    //string resp = response.Content.ReadAsStringAsync().Result; 
                    string strsb = sr.ReadToEnd();
                    object objResponse = JsonConvert.DeserializeObject(strsb, JSONResponseType);
                    //JsonConvert.DeserializeObject<JSONResponseType>(strsb);

                    return objResponse;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}