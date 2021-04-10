using System;
using System.IO;

class Program {
    public static void Main(string[] args) {
        var sourceUrl = args[0];
        var resultPath = args[2];

        var dataManager = new DataManger();
        var content = dataManager.GetPack(sourceUrl);

        var generator = new Generator();
        var result = generator.processContent(content);

        Packer.pack(result, resultPath);
    }
}