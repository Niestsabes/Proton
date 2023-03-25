using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AbstractSceneManager : MonoBehaviour
{
    [Header("GameObject Components")]
    [SerializeField] private TransitionCanvas transitionCanvas;

    protected IEnumerator Navigate(SceneEnum scene)
    {
        yield return this.transitionCanvas.Close();
        SceneManager.LoadScene((int)scene);
    }
}
