

# SCENES

Root/Parent Scene should only have player logic and lighting elements.
Child scenes should have levels, enemies, and other game objects.

A Parent Scene doesn't know anything about its child scene,
however the Child Scene knows about the Parent Scene

At runtime, only the root scene is loaded, not the children,
which is done via code.
Only the parent scene is loaded by default.

Child scenes can be designated so that you can open them up in a tab with a certain type, this happens if they are not part of a parent scene.

Add a Child scene to a parent scene by dragging from asset view to the scene in the Entity Tree.

It looks similar to prefab, but there can only be one type of scene in the hiearchy of scenes.

Parent scenes can have an infinite number of child scenes.

Stride prevents circular scene loading.

Even if we drag and drop a scene, we still have to load it by code.


public UrlReference<Scene> ChildSceneToLoad; // set scene you want to load
private Scene childScene;  // holder for scene that gets loaded to a parent

// loads the scene into memory
childScene = Content.Load<Scene>(ChildSceneToLoad);   // you don't have to specify Scene type because we already did that with UrlReference

// if we want to load it in a particular transform position, we can do
childScene.Offset = new Vector3(0, offsetAmount * 0.25f, 0);   // this is the position of the child scene in the parent scene.

// sets the parent of the scene
childScene.Parent = Entity.Scene;   // sets it to this sceen.
// Entity.Scene.Children.Add(childScene);   // same thing but calls the Entity itself to load.

childScene.Parent = null;  // sets the parent to null, removing it from parent scene, which causes it to be unrendered, but still in memory.
Content.Unload(childScene);  // removes the child scene from memory.
childScene = null;   // does a similar thing, removes everything


// LOAD TO ROOT SCENE
// In order to unload the root secene, you can't just expect Entity.Scene to be the root.
// It must be the SceneSystem.
Content.Unload(SceneSystem.SceneInstance.RootScene);
SceneSystem.SceneInstance.RootScene = Content.Load<Scene>(sceneToLoad); // this will load the scene then put it as root all in one.

There also seems to be other ways, such as dispose, but this covers the tutorial ways and likely preferred.





How to get Root Scenes for Scene Manager type system?
SceneSystem.SceneInstance.RootScene?????


Get Count of Scenes??????????????
SceneSystem.SceneInstance.Count

Gets the Name??????
SceneSystem.SceneInstance.Name







