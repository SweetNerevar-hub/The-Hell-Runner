using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LeaderboardData
{
    public List<string> names = new List<string>();
    public List<int> scores = new List<int>();
}

[CreateAssetMenu(fileName = "LeaderboardManager")]
public class LeaderboardManager : ScriptableObject
{
    private LeaderboardData data = new LeaderboardData();

    public void AddNewScore(string name, int score)
    {
        if (DoesNameExist(name))
        {
            int nameIndex = FindNameIndex(name);

            if (IsSamePlayerScoreHigher(nameIndex, score))
            {
                data.scores[nameIndex] = score;
                SortListsByScore();
                SaveLeaderboardData();
            }

            return;
        }   

        name.ToUpper();
        data.names.Add(name);
        data.scores.Add(score);

        SortListsByScore();
        SaveLeaderboardData();
    }

    public void DisplayLeaderboard()
    {
        Text rankingText = GameObject.Find("T_Rankings").GetComponent<Text>();

        rankingText.text = "";
        LoadLeaderboardData();

        if (data.scores.Count == 0)
        {
            return;
        }

        for (int i = 0; i < data.scores.Count; i++)
        {
            rankingText.text += $"{i + 1}.  {data.scores[i]}   {data.names[i]}\n";
        }
    }

    private void SortListsByScore()
    {
        int tempIndexIntValue;
        string tempIndexNameValue;

        for (int i = 0; i < data.scores.Count; i++)
        {
            for (int j = 0; j < data.scores.Count; j++)
            {
                if (data.scores[j] < data.scores[i])
                {
                    tempIndexIntValue = data.scores[i];
                    tempIndexNameValue = data.names[i];

                    data.scores[i] = data.scores[j];
                    data.names[i] = data.names[j];

                    data.scores[j] = tempIndexIntValue;
                    data.names[j] = tempIndexNameValue;
                }
            }
        }
    }

    private void SaveLeaderboardData()
    {
        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + "/lbd.json";
        System.IO.File.WriteAllText(path, json);
    }

    private void LoadLeaderboardData()
    {
        string path = Application.persistentDataPath + "/lbd.json";
        string lbd = System.IO.File.ReadAllText(path);

        data = JsonUtility.FromJson<LeaderboardData>(lbd);
    }

    private bool DoesNameExist(string name)
    {
        foreach (string n in data.names)
        {
            if (name == n)
            {
                return true;
            }
        }

        return false;
    }

    private int FindNameIndex(string name)
    {
        for (int i = 0; i < data.names.Count; i++)
        {
            if(name == data.names[i])
            {
                return i;
            }
        }

        return -1;
    }

    private bool IsSamePlayerScoreHigher(int playerIndex, int score)
    {
        return data.scores[playerIndex] < score;
    }
}
