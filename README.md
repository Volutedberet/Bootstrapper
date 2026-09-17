# Bootstrapper

#### The simple and modular bootstrap solution, for all your bootstrapping needs

# Features

- A Simple, module based setup
- Built-In modules for forcing material and prefab caching
- No overhead and No performance impact
- Expandable and easily replacable GUI for showcasing the progress of the current module
- Human made (Fuck AI :3)

# Setup

#### Setting up is really easy, either clone the repo into your project, or use the unity package to import it, then add the BootstrapManager prefab to your bootstrap scene

#### Once imported and set up, on the BootstrapEndLoadSceneModule component, set the ID of the scene you want to load once the bootstrap is over

> [!NOTE]
> You can also make a custom module to handle what happens when the bootstrap ends. Check the API on how you can do that

# API

## BootstrapManager Class

### This is what handles the bootstrap, and the calling of modules

Modules are set up in the `BootstrapManager.modules` List, and each module must extend BootstrapModule, for example:

```cs
using UnityEngine;

public class BootstrapTestModule : BootstrapModule{
    public override void BeginModule(){
        base.BeginModule();
        Debug.Log("Test Module Working");
        FinishModule();
    }
}
```

There is also a spot for the module that get's called once every single module in the `BootstrapManager.modules` is done

```cs
BootstrapManager.endModule
```

The Bootstrap Manager also has some functions that are useful, for example `UpdateModuleProgress(float)` updates the progress bar (If you have it assigned)

> [!TIP]
> The modules added to the bootstrap manager execute in order, keep that in mind if a module needs results from another module to work

# Adding Custom Modules

#### Custom modules allow you to run custom logic during your bootstep

#### These can be useful for loading in settings and save files; Requesting version info; Setting up systems that are present for the entire runtime of your game; etc

## How To Set Up a Module

#### Create a new script, and make it extend `BootstrapModule`, then create an override function called `BeginModule()`

```cs
public class ExampleModule : BootstrapModule{
    public override void BeginModule(){
        base.BeginModule();
        //This is where your module logic will go
    }
}
```

You Can call `base.BeginModule()` but it's not necesarry since it only logs out that the module has begun <br>
Then you can just do whatever you need to do in your module

Once Your module has finished it's job, call the `FinishModule()` function, which will tell the manager to start the next one

> [!TIP]
> Your `FinishModule()` does not need to be in the `BeginModule()` function, it can be called from anywhere once your module is finished

## Additional Module Stuff

#### During your module, you might iterate over things, for example this built in module iterates over materials, and forces unity to cache them

```cs
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
```

#### When you have a module that might run for a few seconds, you can use `BootstrapManager.instance.UpdateModuleProgress(prog/materialsToCache.Count)` which will update the progress bar

> [!IMPORTANT]
> If you don't have a slider assigned in the `BootstrapManager`, this won't do anything

### Module Names

#### The `BootstrapModule` class also has a name variable built in called `moduleName`. This is used by the manager to display the current task, and it's also the name logged when calling `base.BeginModule()`

> [!IMPORTANT]
> If you don't have a Text assigned in the `BootstrapManager`, this will only log it, and not display it

# Final Words

Ty for using my asset, hope it helps you out :3
