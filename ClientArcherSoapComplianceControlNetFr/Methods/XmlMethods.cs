using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ClientArcherSoapComplianceControlNetFr.Models
{
    class XmlMethods
    {
        //To get a string from a xml
        public string GetXMLAsString(XmlDocument Input)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            Input.WriteTo(tx);

            string str = sw.ToString();
            return str;
        }

        //https://www.codeproject.com/Articles/1163664/Convert-XML-to-Csharp-Object
        //Deserialize a string to an object
        public T Deserialize<T>(string input) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        //Get the formula : key recordList[].contentId value formula
        public Dictionary<string, string> GetDictionary22677(Records records)
        {
            Dictionary<string, string> dictionary22677 = new Dictionary<string, string>();

            RecordsRecord[] recordList = records.Record;

            for (int i = 0; i < recordList.Length; i++)
            {
                RecordsRecordField[] fields = recordList[i].Field;
                for (int j = 0; j < fields.Length; j++)
                {

                    if (fields[j].id.Equals("22677"))
                    {
                        dictionary22677.Add(recordList[i].contentId, fields[j].Value);
                    }
                }
            }

            Console.WriteLine("Optional : To see what it s on the Dictionary : ");
            foreach (KeyValuePair<string, string> dic in dictionary22677)
            {
                Console.WriteLine("Key : " + dic.Key);
                Console.WriteLine("Formula : " + dic.Value);
            }
            Console.WriteLine("---------------------------------------");
            return dictionary22677;
        }

        //Get the formula : key recordList[].contentId value List of Dictionary key id of 22678/22679/22681 value string
        public Dictionary<string, List<Dictionary<string, string>>> GetDictionaryValues(Records records)
        {
            Dictionary<string, List<Dictionary<string, string>>> dictionaryValue = new Dictionary<string, List<Dictionary<string, string>>>();

            RecordsRecord[] recordList = records.Record;

            for (int i = 0; i < recordList.Length; i++)
            {
                RecordsRecordField[] fields = recordList[i].Field;
                List<Dictionary<string, string>> dictionaryList = new List<Dictionary<string, string>>();

                for (int j = 0; j < fields.Length; j++)
                {
                    Dictionary<string, string> dictionary22678 = new Dictionary<string, string>();
                    Dictionary<string, string> dictionary22679 = new Dictionary<string, string>();
                    Dictionary<string, string> dictionary22681 = new Dictionary<string, string>();

                    if (fields[j].id.Equals("22678"))
                    {
                        dictionary22678.Add("22678", fields[j].Value);
                        dictionaryList.Add(dictionary22678);
                        Console.WriteLine("Check : " + recordList[i].contentId + " - 22678 - " + fields[j].Value);
                        Console.WriteLine("---------------------------------------");
                    }
                    if (fields[j].id.Equals("22679"))
                    {
                        dictionary22679.Add("22679", fields[j].Value);
                        dictionaryList.Add(dictionary22679);
                        Console.WriteLine("Check : " + recordList[i].contentId + " - 22679 - " + fields[j].Value);
                        Console.WriteLine("---------------------------------------");
                    }
                    if (fields[j].id.Equals("22681"))
                    {
                        dictionary22681.Add("22681", fields[j].Value);
                        dictionaryList.Add(dictionary22681);
                        Console.WriteLine("Check : " + recordList[i].contentId + " - 22681 - " + fields[j].Value);
                        Console.WriteLine("---------------------------------------");
                    }

                }
                dictionaryValue.Add(recordList[i].contentId, dictionaryList);
            }
            return dictionaryValue;
        }

        public Dictionary<string, string> ReturnStringResult(Dictionary<string, string> dictionary22677, Dictionary<string, List<Dictionary<string, string>>> dictionaryOfList)
        {
            //containt formula on values
            Dictionary<string, string> returnStringResult = new Dictionary<string, string>();

            //containt (string) percentage result on values
            Dictionary<string, string> returnFinalStringResult = new Dictionary<string, string>();

            foreach (KeyValuePair<string, List<Dictionary<string, string>>> dic01 in dictionaryOfList)
            {
                foreach (KeyValuePair<string, string> dic02 in dictionary22677)
                {
                    if (dic01.Key.Equals(dic02.Key))
                    {
                        Console.WriteLine("On doit avoir 3 égalité : " + dic01.Key);
                        Console.WriteLine("Calcul : " + dic02.Value);
                        Console.WriteLine("---------------------------------------");
                        string textToTreplace = dic02.Value;

                        List<Dictionary<string, string>> listOfValue = dic01.Value;

                        for (int i = 0; i < listOfValue.Count; i++)
                        {

                            Dictionary<string, string> dictionaryX = listOfValue[i];

                            foreach (KeyValuePair<string, string> dicX in dictionaryX)
                            {

                                if (dicX.Key.Equals("22678"))
                                {
                                    Console.WriteLine("Key : " + 22678);
                                    Console.WriteLine("Valeur : " + dicX.Value);
                                    textToTreplace = textToTreplace.Replace("[22678]", dicX.Value);
                                }
                                if (dicX.Key.Equals("22679"))
                                {
                                    Console.WriteLine("Key : " + 22679);
                                    Console.WriteLine("Valeur : " + dicX.Value);
                                    textToTreplace = textToTreplace.Replace("[22679]", dicX.Value);
                                }
                                if (dicX.Key.Equals("22681"))
                                {
                                    Console.WriteLine("Key : " + 22681);
                                    Console.WriteLine("Valeur : " + dicX.Value);
                                    textToTreplace = textToTreplace.Replace("[22681]", dicX.Value);
                                    textToTreplace = textToTreplace.Replace(" ", "");
                                    textToTreplace = textToTreplace.Replace(",", ".");
                                    Console.WriteLine("textToTreplace : " + textToTreplace);
                                }
                            }

                            Console.WriteLine("---------------------------------------");
                        }


                        returnStringResult.Add(dic02.Key, textToTreplace);
                    }

                }

            }
            foreach (KeyValuePair<string, string> dicR in returnStringResult)
            {
                Console.WriteLine("dicR.Key : " + dicR.Key);
                Console.WriteLine("Check Calcul : " + dicR.Value);
                string calcul = dicR.Value;
                bool test = true;
                while (test)
                {
                    
                    try
                    {
                        DataTable dt = new DataTable();
                        int answer = (int)dt.Compute(calcul, "");
                        string rep = Convert.ToString(answer);
                        rep = rep + "%";
                        Console.WriteLine("rep : " + rep);
                        returnFinalStringResult.Add(dicR.Key, rep);
                        test = false;
                    }
                    catch (FormatException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                    catch (InvalidCastException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                    catch (SyntaxErrorException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }


                    try
                    {
                        DataTable dt = new DataTable();
                        Int64 answer = (Int64)dt.Compute(calcul, "");
                        string rep = Convert.ToString(answer);
                        rep = rep + "%";
                        Console.WriteLine("rep : " + rep);
                        returnFinalStringResult.Add(dicR.Key, rep);
                        test = false;
                    }
                    catch (FormatException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                    catch (InvalidCastException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                    catch (SyntaxErrorException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }

                    try
                    {

                        DataTable dt = new DataTable();
                        Double answer = (Double)dt.Compute(calcul, "");
                        string rep = Convert.ToString(answer);
                        rep = rep + "%";
                        Console.WriteLine("rep : " + rep);
                        returnFinalStringResult.Add(dicR.Key, rep);
                        test = false;
                    }
                    catch (FormatException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                    catch (InvalidCastException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                    catch (SyntaxErrorException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }

                    try
                    {
                        DataTable dt = new DataTable();
                        float answer = (float)dt.Compute(calcul, "");
                        string rep = Convert.ToString(answer);
                        rep = rep + "%";
                        Console.WriteLine("rep : " + rep);
                        returnFinalStringResult.Add(dicR.Key, rep);
                        test = false;
                    }
                    catch (FormatException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                    catch (InvalidCastException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                    catch (SyntaxErrorException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }

                    try
                    {
                        DataTable dt = new DataTable();
                        Decimal answer = (Decimal)dt.Compute(calcul, "");
                        string rep = Convert.ToString(answer);
                        rep = rep + "%";
                        Console.WriteLine("rep : " + rep);
                        returnFinalStringResult.Add(dicR.Key, rep);
                        test = false;
                    }
                    catch (FormatException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                    catch (InvalidCastException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                    catch (SyntaxErrorException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                    Console.WriteLine("Si boucle infinie nouvelle conversion à tester");
                    Console.WriteLine("---------------------------------------");
                }
            }



            return returnFinalStringResult;

        }

        //si toutes les conditions sont réunis je set la valeure du résultat dans valeur de  fields id = 22680
        public Records GetFinalObject(Records records, Dictionary<string, string> returnFinalStringResult)
        {

            RecordsRecord[] recordList = records.Record;
            for (int i = 0; i < recordList.Length; i++)
            {
                foreach (KeyValuePair<string, string> dic in returnFinalStringResult)
                {
                    //Console.WriteLine("Key : " + dic.Key);
                    //Console.WriteLine("Formula : " + dic.Value);
                    if (recordList[i].contentId.Equals(dic.Key))
                    {
                        RecordsRecordField[] fields = recordList[i].Field;
                        for (int j = 0; j < fields.Length; j++)
                        {
                            if (fields[j].id.Equals("22680"))
                            {
                                fields[j].Value = dic.Value;
                                Console.WriteLine("Key : " + dic.Key);
                                Console.WriteLine("Object of  fields id = 22680: " + fields[j].Value);
                            }
                        }
                    }

                }

            }
            return records;
        }


    }
}
