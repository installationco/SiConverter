using AngleSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class DataManger
{
    public Topic[][] GetPack(string link)
    {
        if (!link.ToLower().EndsWith("/print")) {
            if (link.EndsWith("/")) {
                link += "print";
            } else {
                link += "/print";
            }
        }
        List<Topic> Themes = new List<Topic>();
        var config = Configuration.Default.WithDefaultLoader();
        AngleSharp.Dom.IDocument document = BrowsingContext.New(config).OpenAsync(link).Result;
        IEnumerable<AngleSharp.Dom.IElement> themeBlocks = document.QuerySelectorAll("div").Where(m => m.GetAttribute("style") == "margin-top:20px;");

        foreach (var themeBlock in themeBlocks)
        {
            IEnumerable<AngleSharp.Dom.IElement> ps = themeBlock.QuerySelectorAll("p").Where(p =>
            (!p.QuerySelector("i")?.TextContent.Contains("Источники:") ?? true) &&
            (!p.QuerySelector("i")?.TextContent.Contains("Комментарий:") ?? true));

            var collapses = themeBlock.QuerySelectorAll("div").Where(m => m.ClassName == "collapsible collapsed").ToList();
            string themeName = themeBlock.TextContent.Split('\n')?[2] ?? "Неизвестно";

            var size = ps.Count() / 2;
            var questions = new Question[size];
            for (int i = 0; i < size; i++)
            {
                string objective = ps.ElementAt(i * 2).TextContent.Replace("\n", " ").Trim();
                string answer = ps.ElementAt(i * 2 + 1).TextContent.Replace("\n", " ").Replace("Ответ:", "").Trim();
                questions[i] = new Question(objective, answer, (i + 1) * 10);
            }
            
            Topic theme = new Topic(themeName, questions);

            Themes.Add(theme);
        }


        return split(Themes.ToArray());
    }

    T[][] split<T>(T[] source) {
        var sizes = new int[] { 7, 6, 5, 4, 3 };
        foreach (var s in sizes) {
            if (source.Length % s == 0) {
                return split2(source, s);
            }
        }

        var count = source.Length / 6;
        var result = new T[count + 1][];
        for (var i = 0; i < count; i++) {
            result[i] = new T[6];
            for (var j = 0; j < 6; j++) {
                result[i][j] = source[i * 6 + j];
            }
        }

        result[count] = new T[source.Length % 6];
        for (var i = 0; i < source.Length % 6; i++) {
            result[count][i] = source[count * 6 + i];
        }

        return result;
    }

    T[][] split2<T>(T[] source, int s) {
        var result = new T[source.Length / s][];
        for (var i = 0; i < source.Length / s; i++) {
            result[i] = new T[s];
            for (var j = 0; j < s; j++) {
                result[i][j] = source[i * s + j];
            }
        }

        return result;
    }
}
