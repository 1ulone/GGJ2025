using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip impact;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }
    //untuk button Quit
    public void quitGameButton()
    {
        Application.Quit();
        audioSource.PlayOneShot(impact, 2f);
    }

    //Untuk button start
    public void startGameButton()
    {
        SceneManager.LoadScene("SelectStage");
        audioSource.PlayOneShot(impact, 2f);
    }
}
