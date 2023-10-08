using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OpenSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenAbout()
    {
        SceneManager.LoadScene("About");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}

    
