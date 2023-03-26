using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneManager : AbstractSceneManager
{
    public void NavigateToGame()
    {
        StartCoroutine(this.Navigate(SceneEnum.GAME_INTRO));
    }

    public void NavigateToGallery()
    {
        StartCoroutine(this.Navigate(SceneEnum.GALLERY));
    }

    public void NavigateToCredit()
    {
        StartCoroutine(this.Navigate(SceneEnum.CREDITS));
    }

    public void QuitGame()
    {
        Application.Quit();
    } 
}
