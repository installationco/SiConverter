using System;
using System.Linq;

public class Generator {
    public Generator() { }

    public string processContent(Topic[][] content) {
        var innerContent = generateInfo() + generateRounds(content);
        var result = wrapHeader(innerContent);
        return result;
    }

    string wrapHeader(string inner) {
        var r = new Random(DateTime.Now.Millisecond);
        var packName = "Сгенерированный пак";
        var xmlns = "http://vladimirkhil.com/ygpackage3.0.xsd";
        var date = DateTime.Now.ToString("dd.MM.yyyy");
        var code1 = $"{r.Next(10)}{r.Next(10)}{r.Next(10)}{r.Next(10)}";
        var code2 = $"{r.Next(10)}{r.Next(10)}{r.Next(10)}{r.Next(10)}";
        var code3 = $"{r.Next(10)}{r.Next(10)}{r.Next(10)}{r.Next(10)}";
        var id = $"f7d00a1e-{code1}-{code2}-{code3}-54e5728ec6d0";
        var attributes = $"xmlns=\"{xmlns}\" difficulty=\"4\" date=\"{date}\" id=\"{id}\" version=\"4\" name=\"{packName}\"";
        var xmlVersion = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
        return $"{xmlVersion}\n<package {attributes}>{inner}</package>";
    }

    string generateInfo() {
        return "<info><authors><author>Конвертер паков</author></authors></info>";
    }

    string generateRounds(Topic[][] rounds) {
        var inner = "";
        for (var i = 0; i < rounds.Length; i++) {
            inner += generateRound(rounds[i], i);
        }
        
        return $"<rounds>{inner}</rounds>";
    }
    
    string generateRound(Topic[] themes, int index) {
        var inner = String.Join("\n", themes.Select(generateTopic));
        return $"<round name=\"Раунд {index}\"><themes>{inner}</themes></round>";
    }
    
    string generateTopic(Topic topic) {
        var result = $"<theme name=\"{topic.name}\"><questions>\n";
        result += String.Join("\n", topic.questions.Select(generateQuestion));
        result += "</questions></theme>\n";
        return result;
    }

    string generateQuestion(Question q) {
        return $"<question price=\"{q.cost}\"><scenario><atom>{q.question}</atom></scenario><right><answer>{q.answer}</answer></right></question>\n";
    }
    
}
