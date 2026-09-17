using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BootstrapModule : MonoBehaviour{
    public string moduleName;

    public virtual void BeginModule(){
        Debug.Log($"Starting Bootstrap Module: {moduleName}");
    }

    public void FinishModule(){
        Debug.Log($"Finished Bootstrap Module: {moduleName}");
        BootstrapManager.instance.UpdateModuleProgress(0);
        BootstrapManager.instance.OnStepDone();
    }
}
