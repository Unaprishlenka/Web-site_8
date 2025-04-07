using System.Text;
using System.Xml.Serialization;


namespace LR_CiT8.Utils
{
    public class XmlConverter
    {
        public static string ToXml<T>(T data)
        {
            var serializer = new XmlSerializer(typeof(T));
            var sb = new StringBuilder();

            using var writer = new StringWriter(sb);
            writer.WriteLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
            writer.WriteLine(@"<?xml-stylesheet type=""text/xsl"" href=""/xslt/style.xslt""?>");

            serializer.Serialize(writer, data);
            return sb.ToString();   
        }
    }
}
