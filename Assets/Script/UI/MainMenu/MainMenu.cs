using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //untuk button Quit
    public void quitGameButton()
    {
        Application.Quit();
    }

    //Untuk button start
    public void startGameButton()
    {
        SceneManager.LoadScene("SelectStage");
    }
}
