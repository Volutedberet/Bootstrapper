using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BootstrapManager : MonoBehaviour{
    public static BootstrapManager instance;
    public List<BootstrapModule> modules = new List<BootstrapModule>();
    public BootstrapModule endModule;
    
    int currentStep;
    
    void Awake(){
        if(instance == null){
            instance = this;
            return;
        }
        Destroy(this);
    }

    void Start(){
        StartStep();
    }

    public void OnStepDone(){
        currentStep++;

        if(currentStep < modules.Count){
            StartStep();        
        }else{
            if(endModule != null){
                endModule.BeginModule();            
            }else{
                Debug.LogError("No End Module Assigned!");
            }
        }
    }

    public void StartStep(){
        modules[currentStep++].BeginModule();
    }
}