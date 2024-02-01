using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSkeletonEnemies : Spawner
{
    protected override void LoadComponents()
    {
        prefabObj = transform.Find("SkeletonEnemies");
        base.LoadComponents();
    }
}
