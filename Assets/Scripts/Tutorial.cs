using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private static int _currentPanel;
    
    public List<GameObject> tutorialPanels;

    private void Awake()
    {
        _currentPanel = 0;
        tutorialPanels[0].SetActive(true);
    }

    public void NextPanel()
    {
        _currentPanel++;
        
        if (_currentPanel >= tutorialPanels.Count) GameManager.StartGame();
        else
        {
            tutorialPanels[_currentPanel - 1].SetActive(false);
            tutorialPanels[_currentPanel].SetActive(true);
        }
    }
}
