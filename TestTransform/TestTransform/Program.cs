using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTransform
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Environment.CurrentDirectory;
            string xmlFilePath = Path.Combine(currentDirectory, "Input.xml");
            string xml = File.ReadAllText(xmlFilePath);

            string resultXml = XmlAckXmlTransformer.TransformXml(xml);


        }
    }
}
