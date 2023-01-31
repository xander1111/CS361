using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private bool _isPaused;
    public bool isGameOver;

    public bool IsPaused
    {
        get => _isPaused;
        set
        {
            _isPaused = value;
            Time.timeScale = _isPaused ? 0f : 1f;
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
                DontDestroyOnLoad(_instance);
            }

            return _instance;
        }
    }
    
    private void Awake()
    {
        _instance = this;
    }

    public void GenerateEdgeColliders()
    {
        Camera cam = Camera.main;
        
        Vector2 lowerCorner = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector2 upperCorner = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
        float screenWidth = upperCorner.x - lowerCorner.x;
        float screenHeight = upperCorner.y - lowerCorner.y;

        GameObject sideCollider1 = new GameObject("CameraEdgeCollier_Side1");
        GameObject sideCollider2 = new GameObject("CameraEdgeCollier_Side2");
        GameObject topCollider = new GameObject("CameraEdgeCollier_Top");
        GameObject bottomCollider = new GameObject("CameraEdgeCollier_Bottom");
        
        sideCollider1.AddComponent<BoxCollider2D>().isTrigger = true;
        sideCollider2.AddComponent<BoxCollider2D>().isTrigger = true;
        topCollider.AddComponent<BoxCollider2D>().isTrigger = true;
        bottomCollider.AddComponent<BoxCollider2D>().isTrigger = true;

        sideCollider1.tag = "EdgeCollider_Sides";
        sideCollider2.tag = "EdgeCollider_Sides";
        topCollider.tag = "EdgeCollider_Top";
        bottomCollider.tag = "EdgeCollider_Bottom";

        sideCollider1.transform.position = new Vector3(upperCorner.x + 1, 0, 0);
        sideCollider2.transform.position = new Vector3(lowerCorner.x - 1, 0, 0);
        topCollider.transform.position = new Vector3(0, upperCorner.y + 1, 0);
        bottomCollider.transform.position = new Vector3(0, lowerCorner.y - 1, 0);

        sideCollider1.transform.localScale = new Vector3(2, screenHeight + 1, 1);
        sideCollider2.transform.localScale = new Vector3(2, screenHeight + 1, 1);
        topCollider.transform.localScale = new Vector3(screenWidth + 1, 2, 1);
        bottomCollider.transform.localScale = new Vector3(screenWidth + 1, 2, 1);

    }

    public static void StartGame()
    {
        string saveFile = Application.persistentDataPath + "/save.json";
        if (File.Exists(saveFile)) SceneManager.LoadScene(1);
        else
        {
            string jsonData = JsonUtility.ToJson(new SaveData { score = 0, name = "" });
            File.WriteAllText(saveFile, jsonData);

            SceneManager.LoadScene(3);
        }
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void LoadLeaderboards()
    {
        SceneManager.LoadScene(2);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void SaveScoreToFile(int score, string name)
    {
        string saveFile = Application.persistentDataPath + "/save.json";
        string jsonData = JsonUtility.ToJson(new SaveData { score = score, name = name });
        File.WriteAllText(saveFile, jsonData);
    }

    public static (int, string) ReadScoreFromFile()
    {
        string saveFile = Application.persistentDataPath + "/save.json";
        if (!File.Exists(saveFile)) return (-1, "");
        
        string jsonData = File.ReadAllText(saveFile);
        SaveData data = JsonUtility.FromJson<SaveData>(jsonData);

        return (data.score, data.name);
    }
}
