using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace FlightDetector
{
    static class XmlParserConstants
    {
        public const string Output = "output";
        public const string Chunk = "chunk";
        public const string Name = "name";
    }

    class XmlParser
    {
        private readonly XmlDocument _document = new XmlDocument();

        public void UploadXml(string path)
        {
            this._document.Load(path);
        }

        public string[] GetFeatures()
        {
            XmlNode outputNode = this._document.GetElementsByTagName("output")[0];

            // getting list first because we don't know how many features the XML contains
            List<string> features = ExtractFeaturesList(outputNode);

            return features.ToArray();
        }

        private static List<string> ExtractFeaturesList(XmlNode outputNode)
        {
            List<string> features = new List<string>();
            try
            {
                /* structure of Output node:
                 <output>
                    <line_separator>
                    <var_separator>
                    <chunk>
                        <name>
                        <type>
                        <format>
                        <node>
                    </chunk>
                 */
                int count = 0;
                foreach (XmlNode node in outputNode.ChildNodes)
                {
                    if (node.Name == XmlParserConstants.Chunk)
                    {
                        foreach (XmlNode chunkChildNode in node.ChildNodes)
                        {
                            if (chunkChildNode.Name == XmlParserConstants.Name)
                            {
                                string name = chunkChildNode.InnerText;
                                if (features.Count > 0 && name == features[count - 1])
                                {
                                    features[count - 1] += " 1";
                                    name += " 2";
                                }
                                features.Add(name);
                                count++;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("XML is not in the right structure");
            }

            return features;
        }
    }
}
