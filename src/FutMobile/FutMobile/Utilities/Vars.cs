using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace FutMobile.Utilities
{
    public class Vars
    {
        public string SENDGRID_KEY = "";
        public string FACEBOOK_KEY_appId = "";
        public string FACEBOOK_KEY_appSecret = "";
        public string GOOGLE_KEY_ClientId = "";
        public string GOOGLE_KEY_ClientSecret = "";

        public Vars()
        {
            var path = HttpRuntime.AppDomainAppPath + "\\.env";

            using (var stream = File.OpenRead(path))
            {
                DotNetEnv.Env.Load(stream);
            }
            SENDGRID_KEY = DotNetEnv.Env.GetString("SENDGRID_KEY");
            FACEBOOK_KEY_appId = DotNetEnv.Env.GetString("FACEBOOK_KEY_appId");
            FACEBOOK_KEY_appSecret = DotNetEnv.Env.GetString("FACEBOOK_KEY_appSecret");
            GOOGLE_KEY_ClientId = DotNetEnv.Env.GetString("GOOGLE_KEY_ClientId");
            GOOGLE_KEY_ClientSecret = DotNetEnv.Env.GetString("GOOGLE_KEY_ClientSecret");
        }
    }
}