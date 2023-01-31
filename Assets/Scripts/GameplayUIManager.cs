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

    private int _score;
    private TextMeshProUGUI _scoreText;
    private bool _quitPanelOpen;
    
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
    
    private void OnEnable()
    {
        menuControls.Enable();
        menuControls.started += _ => PauseGame();

        GameManager.Instance.GenerateEdgeColliders();
        GameManager.Instance.IsPaused = false;
    }

    private void OnDisable()
    {
        menuControls.Disable();
    }
    
    public void AddScore()
    {
        Score += 100;
    }

    public void PauseGame()
    {
        if (_quitPanelOpen) return;
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
}
