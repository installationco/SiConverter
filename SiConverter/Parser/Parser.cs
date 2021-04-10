using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;

public class Parser {
    public Parser() { }

    public Topic[][] parseFile(string url) {
        var doc = new XmlDocument();
        doc.LoadXml(contentsOf(url));
        var root = doc.LastChild.LastChild;
        var nodes = root.SelectNodes("p");
        for (var i = 0; i < nodes.Count; i += 2) {
            Console.WriteLine(nodes[i].InnerText);
            Console.WriteLine(nodes[i + 1].InnerText);
        }

        return null;
    }

    string contentsOfFb2(string url) {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        using (var client = new WebClient()) {
            client.Encoding = Encoding.GetEncoding(1251);
        
            var archiveName = "tmp.zip";
            var fileName = "tmp.fb2";
            client.DownloadFile(url, archiveName);
        
            var archive = ZipFile.OpenRead(archiveName);
            var entry = archive.Entries[0];

            using (var reader = new StreamReader(entry.Open())) {
                var content = reader.ReadToEnd();
                return content;
            }
        }
    }
    
    string contentsOf(string url) {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        using (var client = new WebClient()) {
            client.Encoding = Encoding.GetEncoding(1251);
            var content = client.DownloadString(url);
            var lines = content.Split('\n');
            var sum = "";
            for (var i = 2; i < lines.Length; i++) {
                sum += lines[i] + "\n";
            }

            return sum;
        }
    }

}
