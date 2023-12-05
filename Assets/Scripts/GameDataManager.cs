using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ThisGameData contains all the variables we wish to store in our save file
/// </summary>
[Serializable]
public class ThisGameData
{
    public int highestScore;
    public int enemyKillTotal;
    public Vector3 lastCheckpoint;

    //Time
    public int hoursPlayed;
    public int minutesPlayed;
    public int secondsPlayed;
    public float totalSeconds;
}

public class GameDataManager : GameData
{
    //Singleton Setup
    public static GameDataManager INSTANCE;

    //The game data for this game
    public ThisGameData data = new ThisGameData();

    //Time of the last save
    public DateTime timeOfLastSave;

    #region Initialisation
    private void Awake()
    {
        //Singleton Setup
        if (INSTANCE != null)
            return;
        INSTANCE = this;

        //Load the game data
        data = LoadDataObject<ThisGameData>();

        //If the data doesn't exist, initialise it
        if (data == null)
        {
            data = new ThisGameData();
            Debug.Log("New Data File Created");

            //Initialise our default values;
            data.enemyKillTotal = 0;
            data.highestScore = 0;
        }

        //Set the time of our last save to now
        timeOfLastSave = DateTime.Now;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1))
            SaveData();
    }
    #endregion

    #region Overrides
    public override void SaveData()
    {
        ElapsedTime();
        SaveDataObject(data);
    }

    public override void DeleteData()
    {
        DeleteData();
    }
    #endregion

    #region Setting Data
    public void SetScore(int _score)
    {
        if (_score > data.highestScore)
            data.highestScore = _score;
        SaveData();
    }

    public void SetEnemyKillTotal()
    {
        data.enemyKillTotal++;
        SaveData();
    }

    public void SetLastCheckpoint(Vector3 _lastCheckpoint)
    {
        data.lastCheckpoint = _lastCheckpoint;
        SaveData();
    }

    public void SetTimePlayed()
    {
        ElapsedTime();
    }
    #endregion

    #region Getting Data
    public int GetHighestScore()
    {
        return data.highestScore;
    }

    public int GetEnemyKillTotat()
    {
        return data.enemyKillTotal;
    }

    public Vector3 GetLastCheckpoint()
    {
        return data.lastCheckpoint;
    }

    public string GetTimeFormatted()
    {
        ElapsedTime();
        return String.Format("[0:00}:{1:00}:{2:00}",
            data.hoursPlayed, data.minutesPlayed, data.secondsPlayed);
    }
    #endregion

    #region Game Time
    public void ElapsedTime()
    {
        DateTime now = DateTime.Now;
        int seconds = (now - timeOfLastSave).Seconds;
        data.totalSeconds += seconds;
        data.hoursPlayed = GetHours(data.totalSeconds);
        data.minutesPlayed = GetMinutes(data.totalSeconds);
        data.secondsPlayed = GetSeconds(data.totalSeconds);
        Debug.Log(data.hoursPlayed + " Hours, " + data.minutesPlayed +
                  " Minutes, " + data.secondsPlayed + " Seconds");
        timeOfLastSave = DateTime.Now;
    }
    int GetSeconds(float _seconds)
    {
        return Mathf.FloorToInt(_seconds % 60);
    }
    int GetMinutes(float _seconds)
    {
        return Mathf.FloorToInt(_seconds / 60);
    }
    int GetHours(float _seconds)
    {
        return Mathf.FloorToInt(_seconds / 3600);
    }
    #endregion
}