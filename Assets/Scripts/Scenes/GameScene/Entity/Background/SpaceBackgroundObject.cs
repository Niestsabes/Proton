using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SpaceBackgroundObject : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Vector2 scrollDirection;

    private SpriteRenderer backgroundObject;

    private Vector2 maxOffset;

    void Awake()
    {
        this.backgroundObject = this.GetComponent<SpriteRenderer>();
        this.ResizeBackgroundObject();
    }

    void Update()
    {
        this.MoveBackground();
    }

    private void ResizeBackgroundObject()
    {
        this.backgroundObject.size = this.backgroundObject.sprite.bounds.max * 16;
        this.maxOffset = this.backgroundObject.sprite.bounds.max * 2;
    }

    private void MoveBackground()
    {
        this.transform.Translate(Time.deltaTime * this.scrollDirection);
        if (this.transform.position.x >= this.maxOffset.x) this.transform.Translate(Vector2.left * this.maxOffset.x);
        else if (this.transform.position.x <= -this.maxOffset.x) this.transform.Translate(Vector2.right * this.maxOffset.x);
        if (this.transform.position.y >= this.maxOffset.y) this.transform.Translate(Vector2.down * this.maxOffset.y);
        else if (this.transform.position.y <= -this.maxOffset.y) this.transform.Translate(Vector2.up * this.maxOffset.y);
    }
}
