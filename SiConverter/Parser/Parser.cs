using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;

public class Parser {
    public Parser() { }

    public Topic[][] parseFile(string url) {
        var doc = XDocument.Parse(contentsOf(url));
        var root = doc.Root;
        var body = root.LastNode as XElement;
        Console.WriteLine(body.Name);
        return null;
    }

    string contentsOf(string url) {
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

}
