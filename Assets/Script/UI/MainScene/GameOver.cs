using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static GameOver Instance { get; private set; }
    [SerializeField]private GameObject rootGO;
    bool isOpened = false;

    private void Awake()
    {
  
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }
    
    public void ShowGO()
    {
        if(isOpened)
        {
            Debug.Log("GO Alredy Opened");
        }
        else
        {
            rootGO.SetActive(true);
            isOpened = true;
        }

    }

    public void HideGO()
    {
        if(isOpened)
        {
            rootGO.SetActive(false);
            isOpened = false;
        }
        else
        {
            Debug.Log("Go Alredy Closed");
        }
            

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("SelectStage");
    }
}
