using System.Collections.Generic;

[System.Serializable]
public class PlayerDataList
{
    public List<PlayerData> dataList;

    public PlayerDataList()
    {
        dataList = new List<PlayerData>();
    }
}
