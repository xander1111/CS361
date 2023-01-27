using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private int _score;
    private TextMeshProUGUI _scoreText;

    private int Score
    {
        get => _score;
        set
        {
            _score = value;
            if (!_scoreText) _scoreText = GameObject.FindWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
            _scoreText.text = "Score: " + _score.ToString("N0");
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }

            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    public void EndGame()
    {
        TextMeshProUGUI gameOverText = GameObject.FindWithTag("EndScreen").GetComponent<TextMeshProUGUI>();
        gameOverText.enabled = true;

        Time.timeScale = 0;
    }

    public void AddScore()
    {
        Score += 100;
    }
}
