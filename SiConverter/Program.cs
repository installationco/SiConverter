using System;
using System.IO;

class Program {
    public static void Main(string[] args) {
        var sourceUrl = args[0];
        var resultPath = args[2];

//        var parser = new Parser();
//        var content = parser.parseFile(sourceUrl);

        var generator = new Generator();
        // Раскомментим, когда будет распаршеный контент
//        var result = generator.processContent(content);

        // А это закомментим
        var mockContent = MockContent.get();
        var result = generator.processContent(mockContent);

        Packer.pack(result, resultPath);
    }
}