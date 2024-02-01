using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOrcEnemies : Spawner
{
    protected override void LoadComponents()
    {
        prefabObj = transform.Find("OrcEnemies");
        base.LoadComponents();
    }
}
