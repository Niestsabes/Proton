using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneManager : MonoBehaviour
{
    public void NavigateToGame()
    {
        SceneManager.LoadScene((int)SceneEnum.GAME);
    }

    public void NavigateToGallery()
    {
        SceneManager.LoadScene((int)SceneEnum.GALLERY);
    }

    public void NavigateToCredit()
    {
        SceneManager.LoadScene((int)SceneEnum.CREDITS);
    }

    public void QuitGame()
    {
        Application.Quit();
    } 
}
