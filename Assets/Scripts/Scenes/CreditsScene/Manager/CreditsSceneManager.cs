using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsSceneManager : MonoBehaviour
{
    public void NavigateToHome()
    {
        SceneManager.LoadScene((int)SceneEnum.HOME);
    }
}
