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

    public void NavigateToGallery() { }

    public void NavigateToCredit() { }

    public void QuitGame()
    {
        Application.Quit();
    } 
}
