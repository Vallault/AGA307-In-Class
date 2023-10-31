using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text enemyCountText;

    private void Start()
    {
        UpdateScore(0);
        UpdateTime(0);
        UpdateEnemyCount(0);
    }

    public void UpdateScore(int _score)
    {
        scoreText.text = "Score: " + _score.ToString();
    }

    public void UpdateTime(float _time)
    {
        timeText.text = _time.ToString("F2");
    }

    public void UpdateEnemyCount(int _count)
    {
        enemyCountText.text = "Enemy Count: " + _count.ToString();
    }
}
