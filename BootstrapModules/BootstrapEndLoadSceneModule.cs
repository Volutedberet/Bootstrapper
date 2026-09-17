using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapEndLoadSceneModule : BootstrapModule{
    public int sceneIDToLoad;

    public virtual void BeginModule(){
        base.BeginModule();
        SceneManager.LoadScene(sceneIDToLoad);
    }
}