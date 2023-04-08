using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject quitMenu;
    public Text bestScoreText;

    string playerName;

    void Start()
    {
        ShowBestScoreInMenu();
    }

    public void ReadInput(string name)
    {
        playerName = name;
    }

    public void StartGame()
    {
        DataHolder.instance.GetPlayerName(playerName);
        SceneManager.LoadScene(1);
    }

    void ShowBestScoreInMenu()
    {
        int bestScore = DataHolder.instance.bestScoreEver;
        string playerName = DataHolder.instance.bestPlayerName;
        if (bestScore == 0)
        {
            bestScoreText.text = "PLAY ME!";
        }
        else
        {
            bestScoreText.text = $"Best Score : {playerName} : {bestScore}";
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Confirmation()
    {
        mainMenu.SetActive(false);
        quitMenu.SetActive(true);
    }

    public void QuitGame()
    {
        DataHolder.instance.SaveBestScore();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
