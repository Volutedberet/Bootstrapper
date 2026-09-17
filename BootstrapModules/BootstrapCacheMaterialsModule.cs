using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BootstrapCacheMaterialsModule : BootstrapModule{
    public List<Material> materialsToCache = new List<Material>();
    public float timeBetweenMaterials;

    MeshRenderer displayRenderer;
    GameObject displayObj;

    public override void BeginModule(){
        base.BeginModule();
        displayObj = Instantiate(Resources.Load("MaterialCacheCube") as GameObject);
        displayObj.transform.position = Camera.main.transform.position + new Vector3(0, 0, 10);

        displayRenderer = displayObj.GetComponent<MeshRenderer>();
        StartCoroutine(CacheMaterials());
    }

    private IEnumerator CacheMaterials(){
        float prog = 0; 
        
        foreach(var mat in materialsToCache){
            displayRenderer.material = mat;
            prog++;
            BootstrapManager.instance.UpdateModuleProgress(prog/materialsToCache.Count);
            yield return new WaitForSeconds(timeBetweenMaterials);
        }

        yield return new WaitForSeconds(0.25f);
        Destroy(displayObj);
        FinishModule();
    }
}