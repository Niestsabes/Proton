using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionFlashCanvas : MonoBehaviour
{
    public Image image;
    public Animator animator;
    private bool animating = false;

    public IEnumerator Animate()
    {
        this.animating = true;
        this.animator.SetTrigger("flash");
        yield return new WaitUntil(() => { return !this.animating; });
    }

    public void AnimationEnd()
    {
        image.gameObject.SetActive(true);
        this.animating = false;
    }
}
