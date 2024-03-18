using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public class leaderboard : MonoBehaviour
{
    [SerializeField] private Transform entryContainer;
    [SerializeField] private Transform entryTemplate;
    [SerializeField] private TextMeshPro ingameboard;
    private List<HighscoreEntry> _highscoreEntries;
    private List<Transform> _transforms = new List<Transform>();
    private string jsonname = "highscoreTable";
    private void Awake()
    {
        entryTemplate.gameObject.SetActive(false);
    }

    private void Update()
    {
        //press asdf to reset leaderboard
        if (Input.GetKeyDown(KeyCode.Semicolon) && Input.GetKeyDown(KeyCode.R))
        {
            print("Leaderboard has been reset");
            resetLeaderboard();
        }
    }

    public void load()
    {
        string jsonstring = PlayerPrefs.GetString(jsonname);
        
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonstring);

        //_highscoreEntries = highscores.HighscoreEntries;

        for(int i = 0; i < highscores.HighscoreEntries.Count; i++)
        {
            for (int j = i + 1; j < highscores.HighscoreEntries.Count; j++)
            {
                if (highscores.HighscoreEntries[j].time < highscores.HighscoreEntries[i].time)
                {
                    HighscoreEntry tmp = highscores.HighscoreEntries[i];
                    highscores.HighscoreEntries[i] = highscores.HighscoreEntries[j];
                    highscores.HighscoreEntries[j] = tmp;
                }
            }
        }
        _transforms = new List<Transform>();
        foreach (HighscoreEntry entry in highscores.HighscoreEntries)
        {
            CreateScoreEntryTransform(entry, entryContainer, _transforms);
        }
    }

    public void CreateScoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformlist)
    {
        float templateHeight = 40f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformlist.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformlist.Count + 1;

        string rankString;
        switch (rank)
        {
            case 1:
                rankString = "1ST";
                break;
            case 2:
                rankString = "2ND";
                break;
            case 3:
                rankString = "3RD";
                break;
            default:
                rankString = rank + "TH";
                break;

        }

        entryTransform.Find("Postxt").GetComponent<TextMeshProUGUI>().text = rankString;
        
        int time = (int)highscoreEntry.time;
        int seconds = time % 60;
        int minutes = (time - seconds) / 60;
        if(seconds >= 10)
            entryTransform.Find("Timetxt").GetComponent<TextMeshProUGUI>().text = minutes.ToString() + ":" + seconds;
        else 
            entryTransform.Find("Timetxt").GetComponent<TextMeshProUGUI>().text = minutes.ToString() + ":0" + seconds;
        
        string name = highscoreEntry.name;
        entryTransform.Find("Nametxt").GetComponent<TextMeshProUGUI>().text = name;

        transformlist.Add(entryTransform);
    }

    public void AddHighScoreEntry(float time, string name)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { time = time, name = name };
        
        string jsonstring = PlayerPrefs.GetString(jsonname);
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonstring);
        
        highscores.HighscoreEntries.Add(highscoreEntry);
        
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString(jsonname, json);
        PlayerPrefs.Save();
    }

    public void resetLeaderboard()
    {
        Highscores highscores = new Highscores();
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString(jsonname, json);
        PlayerPrefs.Save();
    }

    public void quitleaderboard()
    {
        foreach (Transform trans in _transforms)
        {
            //should probally delete instead of disabling but idk
            trans.gameObject.SetActive(false);
        }
    }

    public void InGameBoard()
    {
        string jsonstring = PlayerPrefs.GetString(jsonname);
        
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonstring);
        
        for(int i = 0; i < highscores.HighscoreEntries.Count; i++)
        {
            for (int j = i + 1; j < highscores.HighscoreEntries.Count; j++)
            {
                if (highscores.HighscoreEntries[j].time < highscores.HighscoreEntries[i].time)
                {
                    HighscoreEntry tmp = highscores.HighscoreEntries[i];
                    highscores.HighscoreEntries[i] = highscores.HighscoreEntries[j];
                    highscores.HighscoreEntries[j] = tmp;
                }
            }
        }

        int ingamect = 0;
        foreach (HighscoreEntry entry in highscores.HighscoreEntries)
        {
            if (ingamect < 6)
            {
                CreateScoreInGame(entry);
                ingamect++;
            }
        }
    }
    
    public void CreateScoreInGame(HighscoreEntry highscoreEntry)
    {
        ingameboard.text += "\n";
        
        int time = (int)highscoreEntry.time;
        int seconds = time % 60;
        int minutes = (time - seconds) / 60;
        if(seconds >= 10)
            ingameboard.text += minutes.ToString() + ":" + seconds;
        else 
            ingameboard.text += minutes.ToString() + ":0" + seconds;
        
        string name = highscoreEntry.name;
        ingameboard.text += " " + name;
    }

    public void stopInGameBoard()
    {
        ingameboard.text = "";
    }

    public class Highscores
    {
        public List<HighscoreEntry> HighscoreEntries;
    }

    [System.Serializable]
    public class HighscoreEntry
    {
        public float time;
        public string name;
    }
}
