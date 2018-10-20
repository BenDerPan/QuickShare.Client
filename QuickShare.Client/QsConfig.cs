using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QuickShare.Client
{
    /// <summary>
    /// QuickShare client configuration
    /// </summary>
    public class QsConfig
    {
        public string ServerAddr { get; set; }
        public string AdminId { get; set; }
        public string AdminPwd { get; set; }
        public int CookieMaxAge { get; set; }
        public string CookiePath { get; set; }
        public string KeyAdminId { get; set; }
        public string KeyAdminPwd { get; set; }
        public string KeyToken { get; set; }
        public string KeyFileName { get; set; }
        public string KeyFileSize { get; set; }
        public string KeyShareId { get; set; }
        public string KeyStart { get; set; }
        public string KeyLen { get; set; }
        public string KeyChunk { get; set; }
        public string KeyAct { get; set; }
        public string KeyExpires { get; set; }
        public string KeyDownLimit { get; set; }
        public string ActStartUpload { get; set; }
        public string ActUpload { get; set; }
        public string ActFinishUpload { get; set; }
        public string ActLogin { get; set; }
        public string ActLogout { get; set; }
        public string ActShadowId { get; set; }
        public string ActPublishId { get; set; }
        public string ActSetDownLimit { get; set; }
        public string ActAddLocalFiles { get; set; }
        public string AllUsers { get; set; }


        public string PathLogin { get; set; }
        public string PathDownloadLogin { get; set; }
        public string PathDownload { get; set; }
        public string PathUpload { get; set; }
        public string PathStartUpload { get; set; }
        public string PathFinishUpload { get; set; }

        public string PathFileInfo { get; set; }
        public string PathClient { get; set; }

        /// <summary>
        /// Root url of QuickShare server
        /// </summary>
        [JsonIgnore]
        public string RootUrl => GetAbsUrl(ServerAddr,PathClient);
        /// <summary>
        /// Login url
        /// </summary>
        [JsonIgnore]
        public string LoginUrl => GetAbsUrl(ServerAddr, PathLogin);
        /// <summary>
        /// Download login url
        /// </summary>
        [JsonIgnore]
        public string DownloadLoginUrl => GetAbsUrl(ServerAddr, PathDownloadLogin);
        /// <summary>
        /// Download file url
        /// </summary>
        [JsonIgnore]
        public string DownloadUrl => GetAbsUrl(ServerAddr, PathDownload);
        /// <summary>
        /// Upload file url
        /// </summary>
        [JsonIgnore]
        public string UploadUrl => GetAbsUrl(ServerAddr, PathUpload);
        /// <summary>
        /// Start upload file url
        /// </summary>
        [JsonIgnore]
        public string StartUploadUrl => GetAbsUrl(ServerAddr, PathStartUpload);
        /// <summary>
        /// Finish upload file url
        /// </summary>
        [JsonIgnore]
        public string FinishUploadUrl => GetAbsUrl(ServerAddr, PathFinishUpload);
        /// <summary>
        /// Get file information details url
        /// </summary>
        [JsonIgnore]
        public string FileInfoUrl => GetAbsUrl(ServerAddr, PathFileInfo);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serverAddr">Quickshare server api address</param>
        public QsConfig(string serverAddr= "http://localhost:8888/")
        {
            ResetDefault();
            if (!string.IsNullOrEmpty(serverAddr))
            {
                ServerAddr = serverAddr;
            }
            
        }

        /// <summary>
        /// Save configuration to file
        /// </summary>
        /// <param name="configFile">saved file</param>
        /// <returns>success:true, failed:false</returns>
        public bool SaveConfig(string configFile = "config.json")
        {
            return SaveConfig(this, configFile);
        }

        /// <summary>
        /// Save configuration to file
        /// </summary>
        /// <param name="config">QsConfig instance</param>
        /// <param name="configFile">saved file</param>
        /// <returns>success:true, failed:false</returns>
        public static bool SaveConfig(QsConfig config,string configFile = "config.json")
        {
            try
            {
                using (FileStream stream = new FileStream(configFile, FileMode.OpenOrCreate, FileAccess.Write))
                using (StreamWriter w = new StreamWriter(stream))
                {
                    var configStr = JsonConvert.SerializeObject(config, Formatting.Indented);
                    w.WriteLine(configStr);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Load QsConfig from config file
        /// </summary>
        /// <param name="configFile">config file</param>
        /// <returns>success:QsConfig instance object, failed:null</returns>
        public static QsConfig LoadConfig(string configFile = "config.json")
        {
            try
            {
                using (FileStream stream = new FileStream(configFile, FileMode.Open, FileAccess.Read))
                using (StreamReader r = new StreamReader(stream))
                {
                    var config = JsonConvert.DeserializeObject<QsConfig>(r.ReadToEnd());
                    return config;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        } 


        /// <summary>
        /// Get absolute url
        /// </summary>
        /// <param name="baseUrl">base url</param>
        /// <param name="relativeUrl">relative url</param>
        /// <returns>absolute url</returns>
        string GetAbsUrl(string baseUrl,string relativeUrl)
        {
            return new Uri(new Uri(baseUrl), relativeUrl).ToString();
        }

        /// <summary>
        /// Reset default config
        /// </summary>
        void ResetDefault()
        {
            AdminId = "admin";
            AdminPwd = "quicksh@re";
            ServerAddr = "http://localhost:8888/";
            CookieMaxAge = 604800;
            CookiePath = "/";
            KeyAdminId = "adminid";
            KeyAdminPwd = "adminpwd";
            KeyToken = "token";
            KeyFileName = "fname";
            KeyFileSize = "size";
            KeyShareId = "shareid";
            KeyStart = "start";
            KeyLen = "len";
            KeyChunk = "chunk";
            KeyAct = "act";
            KeyExpires = "expires";
            KeyDownLimit = "downlimit";
            ActStartUpload = "startupload";
            ActUpload = "upload";
            ActFinishUpload = "finishupload";
            ActLogin = "login";
            ActLogout = "logout";
            ActShadowId = "shadowid";
            ActPublishId = "publishid";
            ActSetDownLimit = "setdownlimit";
            ActAddLocalFiles = "addlocalfiles";
            AllUsers = "addlocalfiles";
            PathLogin = "/login";
            PathDownloadLogin = "/download-login";
            PathDownload = "/download";
            PathUpload = "/upload";
            PathStartUpload = "/startupload";
            PathFinishUpload = "/finishupload";
            PathFileInfo = "/fileinfo";
            PathClient = "/";
        }
        
    }
}
