using UnityEngine;

public class BootstrapTestModule : BootstrapModule{
    public override void BeginModule(){
        base.BeginModule();
        Debug.Log("Test Module Working");
        FinishModule();
    }
}