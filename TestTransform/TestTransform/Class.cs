using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;

namespace TestTransform
{
    public static class XmlAckXmlTransformer
    {
        public static string TransformXml(string receivedXml)
        {
            string currDirectory = Environment.CurrentDirectory;
            string transformFile = Path.Combine(currDirectory, "Transform.xslt");

            XslCompiledTransform xslTransform = new XslCompiledTransform();
            xslTransform.Load(transformFile);

            string resultXml = String.Empty;
            using (MemoryStream inStream = new MemoryStream(Encoding.UTF8.GetBytes(receivedXml)))
            using (XmlTextReader reader = new XmlTextReader(inStream))
            using (MemoryStream outStream = new MemoryStream())
            using (XmlTextWriter writer = new XmlTextWriter(outStream, Encoding.UTF8))
            {
                writer.WriteStartDocument();
                xslTransform.Transform(reader, writer);
                outStream.Seek(0, SeekOrigin.Begin);
                using (StreamReader streamReader = new StreamReader(outStream))
                {
                    resultXml = streamReader.ReadToEnd();
                }
            }
            return resultXml;
        }
    }
}
