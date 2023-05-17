using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSelection : MonoBehaviour
{
    private bool isAbout = false;
    private bool isScore = false;
    public GameObject MenuPanel;
    public GameObject AboutPanel;
    public GameObject ScorePanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("SpaceInvaders");
    }

    public void IsAbout()
    {
        isAbout = !isAbout;
        if (isAbout)
        {
            MenuPanel.SetActive(false);
            AboutPanel.SetActive(true);
        }
        else
        {
            MenuPanel.SetActive(true);
            AboutPanel.SetActive(false);
        }
    }

    public void IsScore()
    {
        isScore = !isScore;
        if (isScore)
        {
            MenuPanel.SetActive(false);
            ScorePanel.SetActive(true);
        }
        else
        {
            MenuPanel.SetActive(true);
            ScorePanel.SetActive(false);
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}

