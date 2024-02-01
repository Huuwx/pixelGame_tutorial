using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSlimeEnemies : Spawner
{
    //SlimeScripts slimeScrp;

    //private void Start()
    //{
    //    slimeScrp = GetComponent<SlimeScripts>();
    //}

    protected override void LoadComponents()
    {
        prefabObj = transform.Find("SlimeEnemies");
        base.LoadComponents();
    }

    public IEnumerator SpawnCo()
    {
        Debug.Log("xuat hien lai");
        yield return new WaitForSeconds(1f);
        foreach (Transform prefab in this.prefabs)
        {
            if(prefab.gameObject.activeSelf == false)
            {
                prefab.gameObject.SetActive(true);
            }
        }
    }

    //protected override IEnumerator SpawnCo()
    //{

    //    return base.SpawnCo();
    //}
}
