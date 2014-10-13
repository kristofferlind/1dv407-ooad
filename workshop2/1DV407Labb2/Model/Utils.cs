using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

//using System;
//using System.Globalization; //behövs för att ta reda på vilken vecka en TimeDate tillhör
//using System.Text.RegularExpressions;
//using System.IO;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Binary; //behövs för (de)serialisering av data
//using System.Security;
//using System.Xml;
//using System.Xml.Serialization;
//using System.Collections.Generic;

namespace _1DV407Labb2.Model
{
    static class Utils
    {

        public static void XmlSerialize<T>(string filePath, T data, Type[] extraTypes) where T : class
        {

            XmlSerializer serializer = new XmlSerializer(typeof(T), extraTypes);
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fileStream, data);
                //fileStream.Close();
            }

        }

        public static T XmlDeserialize<T>(string filePath, Type[] extraTypes) where T : class
        {
            T data;
            XmlSerializer serializer = new XmlSerializer(typeof(T), extraTypes);
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                data = (T)serializer.Deserialize(fileStream);
                fileStream.Close();
            }
            return data;
        }
    }
}
