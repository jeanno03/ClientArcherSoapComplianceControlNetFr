﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientArcherSoapComplianceControlNetFr.Tools
{
    public static class ConfigurationManagerExtensions
    {
        public static string GetAppConfigValues(this NameValueCollection appSettings, string cle)
        {
            string valeur = appSettings[cle];
            return valeur;
        }
    }
}
