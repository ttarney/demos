using System;
using System.Collections.Generic;
using System.Text;

namespace Corteva.IO.CropHealthImagery.Models
{
    public class OAuthCredentials
    {
        private string _id;
        public string OAuthId
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _url;
        public string URL
        {
            get { return _url; }
            set { _url = value; }
        }
    }
}
