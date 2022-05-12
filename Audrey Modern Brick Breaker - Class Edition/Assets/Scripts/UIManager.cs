using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; set; }

    public TextMeshProUGUI scoreTxt; // reference to the score text 


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    public void UpdateScore(int remainingBricks)
    {
        scoreTxt.text = remainingBricks.ToString() + " / " + GameManager.instance.totalNumberOfBricks;
        
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
