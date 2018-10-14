using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClientArcherSoapComplianceControlNetFr.Models
{
    [XmlRoot("User")]
    public class User
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("age")]
        public int Age { get; set; }

        public User()
        {
        }
    }
}
