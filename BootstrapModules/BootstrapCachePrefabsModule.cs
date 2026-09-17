using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BootstrapCachePrefabsModule : BootstrapModule{
    public List<GameObject> prefabsToCache = new List<GameObject>();
    public float timeBetweenPrefabs;

    public override void BeginModule(){
        base.BeginModule();
        StartCoroutine(CachePrefabs());
    }

    private IEnumerator CachePrefabs(){
        float prog = 0; 

        Vector3 spawnPos = Camera.main.transform.position + new Vector3(0, 0, 10);
        
        foreach(var pref in prefabsToCache){
            GameObject temp = Instantiate(pref, spawnPos, Quaternion.identity);
            prog++;
            BootstrapManager.instance.UpdateModuleProgress(prog/prefabsToCache.Count);
            yield return new WaitForSeconds(timeBetweenPrefabs);
            Destroy(temp);
        }

        yield return new WaitForSeconds(0.25f);
        FinishModule();
    }
}