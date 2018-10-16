using System;
using System.Collections.Generic;
using System.Text;

namespace QuickShare.Client
{
    public class QsConfig
    {
        public int CookieMaxAge { get; set; }

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

        
    }
}
