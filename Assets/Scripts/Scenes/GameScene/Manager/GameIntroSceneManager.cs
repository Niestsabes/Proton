using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIntroSceneManager : AbstractSceneManager
{
    private bool isQuitting = false;

    void Start()
    {
        Invoke("Quit", 35);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) { this.Quit(); }
    }

    private void Quit()
    {
        if(!this.isQuitting) {
            this.isQuitting = true;
            StartCoroutine(this.Navigate(SceneEnum.GAME));
        }
    }
}
