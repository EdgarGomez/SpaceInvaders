[System.Serializable]
public class PlayerData
{
    public string name;
    public int score;
    public string date;

    public PlayerData(string name, int score)
    {
        this.name = name;
        this.score = score;
        this.date = System.DateTime.Now.ToString("MM/dd/yyyy");
    }
}
