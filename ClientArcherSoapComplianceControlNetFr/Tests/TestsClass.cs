using ClientArcherSoapComplianceControlNetFr.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ClientArcherSoapComplianceControlNetFr.Tests
{
    class TestsClass
    {

        public void testUser()
        {

            XmlMethods xmlMethods = new XmlMethods();


            string xmlUserString =
                "<?xml version='1.0' encoding='utf-16'?>" +
                "<User>" +
                "<name>Jean</name>" +
                "<age>19</age>" +
                "</User>";

            Console.WriteLine("return xml to string : ");
            Console.WriteLine(xmlUserString);

            User user = xmlMethods.Deserialize<User>(xmlUserString);


            Console.WriteLine("User Name : " + user.Name);

            Console.WriteLine("Test User Over");
            Console.ReadLine();
        }

        //return a string from a real xml
        //place Input.xml in the Files folder
        public void TestPercentageCompliance()
        {
            Console.WriteLine("Optional : Insert the Input.xml to Files folder");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Automatic : Convert the xml to a string");
            Console.WriteLine("Method : GetXMLAsString(XmlDocument Input)");

            XmlMethods xmlMethods = new XmlMethods();

            //method to convert xml to a string
            XmlDocument document = new XmlDocument();
            document.Load("\\Users\\Jeannory.Phou\\source\\repos\\git\\ClientArcherSoapComplianceControl" +
                            "\\ClientArcherSoapComplianceControl\\Files\\Input.xml");
            string stringXml = xmlMethods.GetXMLAsString(document);

            Console.WriteLine("Automatic : Return xml to string : ");
            Console.WriteLine(stringXml);
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Optional : Create a xml from string : ");

            //Convert string to xml
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(stringXml);

            //Edit the doc.xml and save it physically
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Save("C:\\Users\\Jeannory.Phou\\source\\repos\\git\\ClientArcherSoapComplianceControlNetFr" +
                                "\\ClientArcherSoapComplianceControlNetFr\\OutPut\\OutPut.xml");
        

            Console.WriteLine("Go to file \\OutPut\\OutPut.xml");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Route : bin/debug/netcoreapp2.1/Input.xml");
            Console.WriteLine("Step 3 : Generate the xsd");
            Console.WriteLine("Use Developer Command Prompt for VS 2017");
            Console.WriteLine("Command : xsd OutPut.xml");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Step 4 : Generate the class");
            Console.WriteLine("Command : xsd /c OutPut.xml");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Step 5 : Modify the class to deserialize the string (keep only what you need)");
            Console.WriteLine("Remplace with  : System.Xml.Serialization.XmlIgnoreAttribute()");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Automatic : Deserialize the string: ");
            Console.WriteLine("Method : xmlMethods.Deserialize<Records>(stringXml)");
            Console.WriteLine("---------------------------------------");

            //Deserialize the string
            Records records = xmlMethods.Deserialize<Records>(stringXml);

            Console.WriteLine("Optional : To see what it s on the Object : ");
            RecordsRecord[] recordList = records.Record;

            for (int i = 0; i < recordList.Length; i++)
            {

                RecordsRecordField[] fields = recordList[i].Field;
                Console.WriteLine("recordList[i].contentId : " + recordList[i].contentId);

                for (int j = 0; j < fields.Length; j++)
                {
                    Console.WriteLine("fields[j].id : " + fields[j].id);
                    try
                    {
                        Console.WriteLine("fields[j].Value : " + fields[j].Value);
                    }
                    catch (NullReferenceException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                    Console.WriteLine("---------------------------------------");
                }

            }



            Console.WriteLine("Step 6 : Dictionary with formula");
            Console.WriteLine("Method : GetDictionary22677(records)");

            Dictionary<string, string> dictionary22677 = xmlMethods.GetDictionary22677(records);

            Console.WriteLine("---------------------------------------");

            Console.WriteLine("Step 7 : Dictionary with Key contentId and List of Dictionary key and values of 22678/22679/22681");
            Console.WriteLine("Method : GetDictionaryValues(records)");

            Dictionary<string, List<Dictionary<string, string>>> dictionaryOfList = xmlMethods.GetDictionaryValues(records);
            Console.WriteLine("---------------------------------------");

            Console.WriteLine("Step 8 : Dictionary with Key contentId and (string) their formula with real value ");
            Console.WriteLine("Method : ReturnStringResult(dictionary22677, dictionaryOfList)");

            Dictionary<string, string> returnFinalStringResult = xmlMethods.ReturnStringResult(dictionary22677, dictionaryOfList);

            Console.WriteLine("---------------------------------------");

            Console.WriteLine("Step 9 : Get the Object with the résults (percentage) which is set ");
            Console.WriteLine("Method : GetFinalObject(records, returnFinalStringResult)");

            Records finalRecords = xmlMethods.GetFinalObject(records, returnFinalStringResult);

            Console.WriteLine("---------------------------------------");

            Console.WriteLine("Step 10 : Serialise the new object to xml files ");

            XmlSerializer xs = new XmlSerializer(typeof(Records));
            using (StreamWriter wr = new StreamWriter("C:\\Users\\Jeannory.Phou\\source\\repos\\git\\ClientArcherSoapComplianceControlNetFr" +
                    "\\ClientArcherSoapComplianceControlNetFr\\OutPut\\RecordsResult.xml"))
            {
                xs.Serialize(wr, finalRecords);
            }
            Console.WriteLine("---------------------------------------");

            Console.WriteLine("Step 11 : Convert the xml to string");


            //method to convert xml to a string
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("\\Users\\Jeannory.Phou\\source\\repos\\git\\ClientArcherSoapComplianceControlNetFr" +
                    "\\ClientArcherSoapComplianceControlNetFr\\OutPut\\RecordsResult.xml");
            string stringRecordsResult = xmlMethods.GetXMLAsString(xmlDocument);

            Console.WriteLine("Result : ");
            Console.WriteLine(stringRecordsResult);
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("END");
            Console.WriteLine("---------------------------------------");
            Console.ReadLine();




        }
    }
}
