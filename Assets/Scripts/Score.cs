using System;
using TMPro;
using UnityEngine;

[Serializable]
public class Score : MonoBehaviour
{
    private int _placement;
    
    public new string name;  // Shadows Object.name, required though as this is what the microservice expects and returns
    public int score;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI placementText;

    public int Placement
    {
        get => _placement;
        set
        {
            _placement = value;
            placementText.text = "#" + _placement.ToString();
        }
    }
    
    public void LoadFromJson(string jsonData)
    {
        JsonUtility.FromJsonOverwrite(jsonData, this);

        nameText.text = name;
        scoreText.text = score.ToString();
    }
}
