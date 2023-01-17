using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace CleanUpWebConfig
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"web.config";
            if (args.Length != 1)
            {
                path = @".\" + path;
            } else { 
                path = args[0] + @"\" + path; 
            }
            
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
            ns.AddNamespace("bindings", "urn:schemas-microsoft-com:asm.v1");
            
            //clean comments under assemblyBinding
            XmlNodeList comments = doc.SelectNodes("//bindings:assemblyBinding/comment()", ns);

            foreach (XmlNode comment in comments)
            {
                comment.ParentNode.RemoveChild(comment);
            }

            //clean comments under dependentAssembly
            comments = doc.SelectNodes("//bindings:dependentAssembly/comment()", ns);

            foreach (XmlNode comment in comments)
            {
                comment.ParentNode.RemoveChild(comment);
            }
            doc.Save(path);
        }
    }
}
