using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;

namespace QuickShare.Client
{
    public class QsResponse
    {
        public int? Code { get; set; }
        public string Msg { get; set; }
        public string ShareId { get; set; }
        public long? Start { get; set; }
        public long? Length { get; set; }

        [JsonIgnore]
        public bool IsOk
        {
            get
            {
                if (Code!=null)
                {
                    if (Code>=200&&Code<400)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public static QsResponse Parse(string jsonStr)
        {
            return JsonConvert.DeserializeObject<QsResponse>(jsonStr, new JsonSerializerSettings()
            {
                NullValueHandling=NullValueHandling.Ignore
            });
        }

        public static bool TryParse(string jsonStr,out QsResponse response)
        {
            try
            {
                response = Parse(jsonStr);
                return true;
            }
            catch (Exception e)
            {
                response = null;
                return false;
            }
        }
    }
    public class QsClient
    {
        public QsConfig Config { get; set; }

        public string Token { get; private set; }
        public DateTime LoginTime { get; private set; }
        public DateTime ExpireTime { get; private set; }
      
        public QsClient(QsConfig config=null)
        {
            Config = config;
            Init();
        }

        void Init()
        {
            Token = "-";
            LoginTime = DateTime.MinValue;
            ExpireTime = DateTime.MinValue;
        }

        public void SetToken(string token,DateTime expireTime)
        {
            Token = token;
            ExpireTime = ExpireTime;
        }

        public bool Login()
        {
            try
            {
                var client = new RestClient(Config.LoginUrl);
                var request = new RestRequest(Method.POST);
                request.AddParameter(Config.KeyAct, Config.ActLogin);
                request.AddParameter(Config.KeyAdminId, Config.AdminId);
                request.AddParameter(Config.KeyAdminPwd, Config.AdminPwd);

                var response = client.Execute(request);
                if (QsResponse.TryParse(response.Content,out var qsResponse))
                {
                    if (qsResponse.IsOk)
                    {
                        LoginTime = response.Cookies[0].TimeStamp;
                        ExpireTime = response.Cookies[0].Expires;
                        Token = response.Cookies[0].Value;
                        Console.WriteLine($"[Login Success] Code={qsResponse.Code}  Msg={qsResponse.Msg}");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"[Login Failed] Code={qsResponse.Code}  Msg={qsResponse.Msg}");
                    }
                }
                Console.WriteLine($"[Login Failed] Unknown response:{response.Content}");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"[Login Failed] {e}");
                return false;
            }
        }

        public bool CheckLogin()
        {
            if (DateTime.Now>=ExpireTime)
            {
                Console.WriteLine($"[Check Login] Need Login ...[Expired]");
                return Login();
            }
            Console.WriteLine($"[Check Login] Already Login ...[OK]");
            return true;
        }

        public bool UploadFile(string fileName,out string fileUrl)
        {
            fileUrl = null;
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"[Upload File Error] File {fileName} is not exists ...[Stop]");
                return false;
            }
            if (StartUpload(fileName,out var startInfo))
            {
                if (Upload(fileName,startInfo,out var uploadInfo))
                {
                    if (FinishUpload(uploadInfo))
                    {
                        fileUrl = new Uri(new Uri(Config.RootUrl), uploadInfo.ShareId).ToString();
                        return true;
                    }
                }
            }
            return false;
        }

        public bool StartUpload(string fileName,out QsResponse shareInfo)
        {
            shareInfo = null;
            if (!CheckLogin())
            {
                return false;
            }
            var shortFileName = Path.GetFileName(fileName);
            try
            {
                var client = new RestClient(Config.StartUploadUrl);
                var request = new RestRequest(Method.POST);
                request.AddParameter(Config.KeyFileName, shortFileName);
                request.AddCookie(Config.KeyToken, Token);

                var response = client.Execute(request);
                if (QsResponse.TryParse(response.Content, out var qsResponse))
                {
                    if (qsResponse.IsOk)
                    {
                        shareInfo = qsResponse;
                        Console.WriteLine($"[Start Upload Success] Info={qsResponse}");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"[Start Upload Failed] Code={qsResponse.Code}  Msg={qsResponse.Msg}");
                    }
                }
                Console.WriteLine($"[Start Upload Failed] Unknown response:{response.Content}");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"[Start Upload Failed] {e}");
                return false;
            }
        }



        public bool Upload(string fileName,QsResponse lastUploadedInfo,out QsResponse uploadInfo)
        {
            uploadInfo = null;
            if (!CheckLogin())
            {
                return false;
            }
            try
            {
                var fileInfo = new FileInfo(fileName);
                
                var uploaded = lastUploadedInfo.Start + lastUploadedInfo.Length;
                var end = uploaded < fileInfo.Length ? uploaded : fileInfo.Length;

                var readStream = File.OpenRead(fileName);

                readStream.Seek(lastUploadedInfo.Start.Value,SeekOrigin.Begin);
                int start = int.Parse(lastUploadedInfo.Start.Value.ToString());
                int len = int.Parse((end - lastUploadedInfo.Start.Value).ToString());
                byte[] chunk=new byte[len];
                readStream.Read(chunk,0 ,len );
                var client = new RestClient(Config.UploadUrl);
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "multipart/form-data");
                request.AddCookie(Config.KeyToken, Token);
               
                request.AddParameter(Config.KeyShareId, lastUploadedInfo.ShareId);
                request.AddParameter(Config.KeyStart, lastUploadedInfo.Start);
                request.AddParameter(Config.KeyLen, len);
                request.AddFileBytes(Config.KeyChunk, chunk, fileInfo.Name);
                readStream.Close();
                var response = client.Execute(request);
                if (QsResponse.TryParse(response.Content, out var qsResponse))
                {
                    if (qsResponse.IsOk)
                    {
                        uploadInfo = qsResponse;
                        Console.WriteLine($"[Upload Step Success] File={fileName} Info={qsResponse}");
                        if (uploadInfo.Start>=fileInfo.Length)
                        {
                            return true;
                        }
                        else
                        {
                            return Upload(fileName, uploadInfo, out var nextUploadInfo);
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine($"[Upload Step Failed] File={fileName} Code={qsResponse.Code}  Msg={qsResponse.Msg}");
                    }
                }
                Console.WriteLine($"[Upload Step Failed] File={fileName} Unknown response:{response.Content}");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"[Upload Step Failed] File={fileName} Error={e}");
                return false;
            }
        }

        public bool FinishUpload(QsResponse shareInfo)
        {
            if (!CheckLogin())
            {
                return false;
            }

            try
            {
                var client = new RestClient(Config.FinishUploadUrl);
                var request = new RestRequest(Method.POST);
                request.AddParameter(Config.KeyShareId, shareInfo.ShareId);
                request.AddCookie(Config.KeyToken, Token);

                var response = client.Execute(request);
                if (QsResponse.TryParse(response.Content, out var qsResponse))
                {
                    if (qsResponse.IsOk)
                    {
                        Console.WriteLine($"[Finish Upload Success] Code={qsResponse.Code}  Msg={qsResponse.Msg}");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"[Finish Upload Failed] Code={qsResponse.Code}  Msg={qsResponse.Msg}");
                    }
                }
                Console.WriteLine($"[Finish Upload Failed] Unknown response:{response.Content}");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"[Finish Upload Failed] {e}");
                return false;
            }
        }
    }
}
