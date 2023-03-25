using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TravelerObject))]
public abstract class TravelerController : MonoBehaviour
{
    public TravelerObject travelerObject { get; protected set; }

    void Awake()
    {
        this.AttachTraveler();
    }

    public abstract IEnumerator Act();

    protected void AttachTraveler()
    {
        if (this.travelerObject != null) return;
        this.travelerObject = this.gameObject.GetComponent<TravelerObject>();
    }
}
