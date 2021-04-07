public class Question {
    public string question { get; private set; }
    public string answer { get; private set; }
    public int cost { get; private set; }

    public Question(string question, string answer, int cost) {
        this.question = question;
        this.answer = answer;
        this.cost = cost;
    }
}
