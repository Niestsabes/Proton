using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsSceneManager : AbstractSceneManager
{
    public void NavigateToHome()
    {
        StartCoroutine(this.Navigate(SceneEnum.HOME));
    }

}
