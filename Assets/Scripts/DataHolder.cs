using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataHolder : MonoBehaviour
{
    public static DataHolder instance;

    private int currentScore;
    public int bestScoreEver;

    public string playerName;
    public string bestPlayerName;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadBestScore();
    }

    public void GetPlayerName(string name)
    {
        playerName = name;
    }

    public void GetDataPoint(int value)
    {
        currentScore = value;
        CheckHighScore();
    }

    private void CheckHighScore()
    {
        if (currentScore > bestScoreEver)
        {
            bestScoreEver = currentScore;
            bestPlayerName = playerName;
        }
    }

    [System.Serializable]
    class SaveData
    {
        public int bestScoreData;
        public string bestPlayerNameData;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.bestScoreData = bestScoreEver;
        data.bestPlayerNameData = bestPlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScoreEver = data.bestScoreData;
            bestPlayerName = data.bestPlayerNameData;
        }
    }
}
