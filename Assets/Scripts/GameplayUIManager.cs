using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GameplayUIManager : MonoBehaviour
{
    public InputAction menuControls;
    public GameObject pauseScreen;
    public GameObject quitButton;
    public GameObject continueButton;
    public GameObject quitPanel;
    public GameObject gameOverScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI finalScoreText;

    private int _score;
    private int _lives = 1;
    private bool _quitPanelOpen;
    

    private int Score
    {
        get => _score;
        set
        {
            _score = value;
            scoreText.text = "Score: " + _score.ToString("N0");
        }
    }

    private int Lives
    {
        get => _lives;
        set
        {
            if (value > 3) value = 3;
            _lives = value;
            livesText.text = "Lives: " + _lives;
        }
    }
    
    private void OnEnable()
    {
        menuControls.Enable();
        menuControls.started += _ => PauseGame();

        GameManager.Instance.GenerateEdgeColliders();
        GameManager.Instance.IsPaused = false;
        GameManager.Instance.isGameOver = false;
    }

    private void OnDisable()
    {
        menuControls.Disable();
    }
    
    public void AddScore()
    {
        Score += 100;
    }

    public void AddLife()
    {
        Lives++;
    }
    
    public void RemoveLife()
    {
        if (--Lives < 1) EndGame();
        Score -= 50;  // Score penalty for losing a life
    }

    public void PauseGame()
    {
        if (_quitPanelOpen || GameManager.Instance.isGameOver) return;
        GameManager.Instance.IsPaused = !GameManager.Instance.IsPaused;
        pauseScreen.SetActive(!pauseScreen.activeSelf);
    }

    public void ToggleQuitConfirm()
    {
        quitPanel.SetActive(!quitPanel.activeSelf);
        _quitPanelOpen = !_quitPanelOpen;
        
        quitButton.SetActive(!quitButton.activeSelf);
        continueButton.SetActive(!continueButton.activeSelf);
    }

    private void EndGame()
    {
        GameManager.Instance.IsPaused = true;
        GameManager.Instance.isGameOver = true;

        scoreText.enabled = false;
        livesText.enabled = false;
        finalScoreText.text = "Final Score: " + Score;
        
        gameOverScreen.SetActive(true);
    }

    public void SaveAndQuit()
    {
        GameManager.SaveScoreToFile(Score, nameText.text);
        StartCoroutine(GameManager.PublishScore(Score, nameText.text));
        GameManager.LoadMainMenu();
    }
}
