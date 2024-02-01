using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected List<Transform> prefabs;

    protected Transform prefabObj;

    protected void Reset()
    {
        this.LoadComponents();   
    }

    protected virtual void LoadComponents()
    {
        this.LoadPrefabs();
    }

    protected virtual void LoadPrefabs()
    {
        foreach(Transform prefab in prefabObj)
        {
            this.prefabs.Add(prefab);
        }
    }

    protected void HidePrefab()
    {
        foreach(Transform prefab in this.prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    //public virtual IEnumerator SpawnCo(GameObject enm)
    //{
    //    Debug.Log("xuat hien lai");
    //    yield return new WaitForSeconds(5f);
        
    //    enm.SetActive(true);
    //}
}
