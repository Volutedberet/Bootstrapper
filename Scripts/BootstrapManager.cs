using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BootstrapManager : MonoBehaviour{
    public static BootstrapManager instance;
    public List<BootstrapModule> modules = new List<BootstrapModule>();
    public BootstrapModule endModule;

    [Header("Display")]
    [SerializeField] Text cModuleNameText;
    [SerializeField] Slider cModuleProgress;
    
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
                cModuleNameText.text = "Finishing Up";
                endModule.BeginModule();            
            }else{
                Debug.LogError("No End Module Assigned!");
            }
        }
    }

    public void StartStep(){
        if(cModuleNameText != null){
            cModuleNameText.text = $"Current Task: {modules[currentStep].moduleName}";        
        }

        modules[currentStep].BeginModule();
    }

    public void UpdateModuleProgress(float prog){
        if(cModuleProgress != null){
            cModuleProgress.value = prog;
        }
    }
}