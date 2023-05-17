using UnityEngine;
using TMPro;

public class ScoreboardController : MonoBehaviour
{
    public GameObject scoreEntryPrefab;
    public Transform scoreEntryParent;

    void Start()
    {
        LoadScores();
    }

    void LoadScores()
    {
        PlayerDataList dataList = DataManager.instance.LoadPlayerData();
        if (dataList != null)
        {
            dataList.dataList.Sort((data1, data2) => data2.score.CompareTo(data1.score));

            foreach (PlayerData data in dataList.dataList)
            {
                GameObject scoreEntry = Instantiate(scoreEntryPrefab, scoreEntryParent);
                TextMeshProUGUI scoreText = scoreEntry.GetComponentInChildren<TextMeshProUGUI>();
                scoreText.text = $"{data.name} -||- {data.score} -||- {data.date}";
            }
        }
        else
        {
            GameObject scoreEntry = Instantiate(scoreEntryPrefab, scoreEntryParent);
            TextMeshProUGUI scoreText = scoreEntry.GetComponentInChildren<TextMeshProUGUI>();
            scoreText.text = "No Scores Found Yet";
        }
    }

}
