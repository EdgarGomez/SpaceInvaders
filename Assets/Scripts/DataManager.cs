using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerData(string name, int score)
    {
        PlayerData data = new PlayerData(name, score);

        string path = Path.Combine(Application.persistentDataPath, "playerData.json");
        PlayerDataList dataList;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            dataList = JsonUtility.FromJson<PlayerDataList>(json);
        }
        else
        {
            dataList = new PlayerDataList();
        }

        dataList.dataList.Add(data);

        string newListJson = JsonUtility.ToJson(dataList);
        File.WriteAllText(path, newListJson);
    }

    public PlayerDataList LoadPlayerData()
    {
        string path = Path.Combine(Application.persistentDataPath, "playerData.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<PlayerDataList>(json);
        }
        else
        {
            return new PlayerDataList() { dataList = new List<PlayerData>() };
        }
    }
}
