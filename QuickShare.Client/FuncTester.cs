using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QuickShare.Client
{
    public class HttpFileHelper
    {
        public static bool Login(string url, string user, string password, out Dictionary<string, string> cookie)
        {
            cookie = null;
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);

                request.Parameters.Add(new Parameter("act", "login", ParameterType.QueryString));
                request.Parameters.Add(new Parameter("adminid", user, ParameterType.QueryString));
                request.Parameters.Add(new Parameter("adminpwd", password, ParameterType.QueryString));

                var response = client.Execute(request);

                dynamic content = JsonConvert.DeserializeObject(response.Content);
                if (content["Code"] == 200)
                {
                    cookie = new Dictionary<string, string>();
                    foreach (var c in response.Cookies)
                    {
                        cookie.Add(c.Name, c.Value);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool StartUpload(string postUrl, string fileName, Dictionary<string, string> cookies, out dynamic shareInfo)
        {
            shareInfo = null;
            try
            {
                var client = new RestClient(postUrl);
                var request = new RestRequest(Method.POST);
                request.Parameters.Add(new Parameter("fname", fileName, ParameterType.QueryString));
                if (cookies != null)
                {
                    foreach (var c in cookies)
                    {
                        request.AddCookie(c.Key, c.Value);
                    }
                }
                var response = client.Execute(request);
                //{"ShareId":"ebb82bf187099bdb35dfb6d2d453e8b75d2493fe9313ee8cc5d127e5d4e1e0c2","Start":0,"Length":10000000}
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    shareInfo = JsonConvert.DeserializeObject(response.Content);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool Upload(string url, string fileName, string shareID, int start, int length, Dictionary<string, string> cookies, out dynamic uploadedInfo)
        {
            uploadedInfo = null;
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);

                if (cookies != null)
                {
                    foreach (var c in cookies)
                    {
                        request.AddCookie(c.Key, c.Value);
                    }
                }
                request.AddHeader("Content-Type", "multipart/form-data");
                //request.RequestFormat = DataFormat.Json;
                var chunk = File.ReadAllBytes(fileName);

                //request.RequestFormat = DataFormat.Json;
                //request.Parameters.Add(new Parameter("shareid", shareID,ParameterType.QueryString));
                request.AddParameter("shareid", shareID);
                //request.Parameters.Add(new Parameter("start", start, ParameterType.QueryString));
                request.AddParameter("start", start);
                //request.Parameters.Add(new Parameter("len", chunk.Length, ParameterType.QueryString));
                request.AddParameter("len", chunk.Length);
                //request.Parameters.Add(new Parameter("chunk", chunk, ParameterType.QueryString));
                //request.AddParameter("chunk", chunk);
                //request.AddFileBytes("chunk", chunk, "a.txt");

                request.AddFileBytes("chunk", chunk, "a.txt");

                var response = client.Execute(request);
                //{"ShareId":"5572cde0dba022ef7c8e0ae11dddea527129fb87d169d54ee75340194971a8d0","Start":68,"Length":10000000}
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    uploadedInfo = JsonConvert.DeserializeObject(response.Content);

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool FinishedUpload(string url, dynamic uploadedInfo, Dictionary<string, string> cookies)
        {
            try
            {

                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);

                if (cookies != null)
                {
                    foreach (var c in cookies)
                    {
                        request.AddCookie(c.Key, c.Value);
                    }
                }

                request.AddParameter("shareid", uploadedInfo["ShareId"].ToString());

                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    dynamic aa = JsonConvert.DeserializeObject(response.Content);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static string sendPost(string postUrl, List<string> attachments, Dictionary<string, string> cookies)
        {
            try
            {
                var client = new RestClient(postUrl);
                var request = new RestRequest(Method.POST);
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("content-type", "multipart/form-data");
                if (attachments != null)
                {
                    foreach (string attach in attachments)
                    {
                        string attachFile = attach;
                        if (File.Exists(attachFile))
                        {
                            request.AddFile("files", attach);
                        }
                    }
                }
                if (cookies != null)
                {
                    foreach (var c in cookies)
                    {
                        request.AddCookie(c.Key, c.Value);
                    }
                }
                var response = client.Execute(request);
                var content = response.Content;
                return content;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
