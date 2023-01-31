using System;
using TMPro;
using UnityEngine;

public class Leaderboards : MonoBehaviour
{
    public GameObject content;
    public TextMeshProUGUI userName;
    public TextMeshProUGUI userScore;

    private void Awake()
    {
        ReadUserScoreFromFile();
    }

    private void AddScore()
    {
        throw new NotImplementedException();
    }

    private void ReadUserScoreFromFile()
    {
        var (score, scoreName) = GameManager.ReadScoreFromFile();

        if (score != -1)
        {
            userScore.text = score.ToString();
            userName.text = scoreName;
        }
        else
        {
            userScore.text = "";
            userName.text = "No score yet!";
        }

    }
}
