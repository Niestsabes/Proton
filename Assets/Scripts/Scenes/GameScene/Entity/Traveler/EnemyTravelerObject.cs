using System.Collections;
using UnityEngine;

public class EnemyTravelerObject : TravelerObject
{
    public override void AddController(bool isAI)
    {
        this.controller = this.gameObject.AddComponent<TravelerControllerAIEnemy>();
    }
}