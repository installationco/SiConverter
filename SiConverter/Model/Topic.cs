public class Topic {
    public string name { get; private set; }
    public Question[] questions { get; private set; }

    public Topic(string name, Question[] questions) {
        this.name = name;
        this.questions = questions;
    }
    
}
