using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSceneManager : AbstractSceneManager
{
    public void NavigateToGame()
    {
        StartCoroutine(this.Navigate(SceneEnum.GAME));
    }

    public void NavigateToHome()
    {
        StartCoroutine(this.Navigate(SceneEnum.HOME));
    }
}
