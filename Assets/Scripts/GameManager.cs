using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private Camera _cam;
    private bool _isPaused;

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
            }

            return _instance;
        }
    }
    
    private void Awake()
    {
        _instance = this;
        _cam = Camera.main;
    }

    public void EndGame()
    {
        TextMeshProUGUI gameOverText = GameObject.FindWithTag("EndScreen").GetComponent<TextMeshProUGUI>();
        gameOverText.enabled = true;

        Time.timeScale = 0;
    }
    
    public void GenerateEdgeColliders()
    {
        Vector2 lowerCorner = _cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector2 upperCorner = _cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
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
        SceneManager.LoadScene(1);
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
