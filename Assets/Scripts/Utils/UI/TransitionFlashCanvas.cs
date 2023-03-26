using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionFlashCanvas : MonoBehaviour
{
    public static Color DANGER = Color.red;
    public static Color NORMAL = Color.white;

    public Image image;
    public Animator animator;
    private bool animating = false;

    public IEnumerator Animate()
    {
        this.animating = true;
        this.animator.SetTrigger("flash");
        yield return new WaitUntil(() => { return !this.animating; });
        this.image.color = Color.white;
    }

    public void AnimationEnd()
    {
        image.gameObject.SetActive(true);
        this.animating = false;
    }

    public void SetColor(Color color)
    {
        this.image.color = color;
    }
}
