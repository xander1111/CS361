using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Leaderboards : MonoBehaviour
{
    public GameObject content;  // The GameObject that should be the parent of each Score object
    public TextMeshProUGUI userName;
    public TextMeshProUGUI userScore;
    public GameObject scorePrefab;

    private void Awake()
    {
        ReadUserScoreFromFile();
        GenerateLeaderboard();
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

    private void GenerateLeaderboard()
    {
        for (int i = 1; i <= 10; i++)
        {
            StartCoroutine(GenerateScore(i));
        }
    }

    private IEnumerator GenerateScore(int pos)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/highScore/" + pos);
        yield return www.SendWebRequest();
        
        if (www.result != UnityWebRequest.Result.Success && www.responseCode != 409) Debug.Log(www.error);
        else
        {
            if (www.responseCode != 409) // Code 409 returned when no score at the given position exists
            {
                Score score = Instantiate(scorePrefab).GetComponent<Score>();
                score.LoadFromJson(www.downloadHandler.text);
                score.Placement = pos;
                score.transform.SetParent(content.transform, false);
                score.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (pos - 1) * -50 - 25);
            }
        }
    }
}
