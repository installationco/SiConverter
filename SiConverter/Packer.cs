using System.IO;
using System.IO.Compression;

class Packer {
    public static bool pack(string content, string path) {
        var root = Path.GetPathRoot(path);
        var dir = Directory.CreateDirectory(root + "/tmp");
        File.WriteAllText(dir.FullName + "/content.xml", content);
        File.WriteAllText(dir.FullName + "/[Content_Types].xml", generateContentType());
        var dir2 = Directory.CreateDirectory(dir.FullName + "/Texts");
        File.WriteAllText(dir2.FullName + "/authors.xml", generateAuthors());
        File.WriteAllText(dir2.FullName + "/sources.xml", generateSources());

        if (File.Exists(path)) {
            File.Delete(path);
        }
        ZipFile.CreateFromDirectory(dir.FullName, path);

        return true;
    }

    static string generateContentType() {
        return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
               "<Types xmlns=\"http://schemas.openxmlformats.org/package/2006/content-types\">" +
               "<Default ContentType=\"si/xml\" Extension=\"xml\"/>" +
               "</Types>";
    }
    static string generateAuthors() {
        return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
               "<Authors/>";
    }
    static string generateSources() {
        return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
               "<Sources/>";
    }
}
