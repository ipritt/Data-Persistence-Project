using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoresManager : MonoBehaviour
{
    public TMP_Text highScoreNames;
    public TMP_Text highScoreScores;

    private List<PlayerManager.SaveData> highScoreList = new List<PlayerManager.SaveData>(10);

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            highScoreList.Add(new PlayerManager.SaveData { name = string.Empty, score = 0} );
        }

        SavePlayerData();
        SetLabels();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }
    }

    private void SavePlayerData()
    {
        LoadPlayerData();

        var playerManagerSaveData = new PlayerManager.SaveData()
        {
            name = PlayerManager.instance.playerName,
            score = PlayerManager.instance.currentScore
        };

        if (highScoreList.Count > 0)
        {
            for (int x = 0; x <= highScoreList.Count; x++)
            {
                if (PlayerManager.instance.currentScore > highScoreList[x].score)
                {
                    highScoreList.Insert(x, playerManagerSaveData);
                    TrimHighScoreList();
                    break;
                }
            }
        }
        else
        {
            highScoreList.Add(playerManagerSaveData);
        }

        var json = JsonConvert.SerializeObject(highScoreList);

        File.WriteAllText(Application.persistentDataPath + "/highScores.json", string.Empty);
        File.WriteAllText(Application.persistentDataPath + "/highScores.json", json);
    }

    private void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/highScores.json";
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);

            var jsonObjectHolder = JsonConvert.DeserializeObject<List<PlayerManager.SaveData>>(json);
            highScoreList.InsertRange(0, jsonObjectHolder ?? new List<PlayerManager.SaveData>());
            TrimHighScoreList();
        }
    }

    private void SetLabels()
    {
        foreach(PlayerManager.SaveData data in highScoreList)
        {
            highScoreNames.text += data.name + "\n";
            highScoreScores.text += data.score.Equals(0) ? "\n" : data.score + "\n"; 
        }
    }

    private void TrimHighScoreList()
    {
        if (highScoreList.Count > 10) { highScoreList.RemoveAt(10); }
    }
}
