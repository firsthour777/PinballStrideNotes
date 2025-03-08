





# TODOS:

Create Sample Skeleton Scripts
Create Virtual Buttons Config
Create Random Num Interface
Create SceneUnloader manager, that can nullifiy and remove things properly always.





Figure out how to get a component that is already receved by another class, perhaps there needs to be child parent hieracrchy with teh scripts?




Come up with standard on how to decalre variables and give them values.
Should it be only for public/private?
How to tell if the value is overridden in the editor?
Making it public implies that it will be overridden in the editor, but it still needs a default.
I think if it's private, then it must have its value set in start.
if it's public then it should be given a default value to show in the editor,
however for public variables that need Entities or things to drag and drop,
I need a notation mechanism of some kind.

I think setting to null for now will be a good way to do it.





Scripting Question
Should I just have entities, which only contain scripts?
Or should I have entities that contain scripts and components?












# QUESTIONS

Prefabs vs Scenes?
If it needs to be repeated then it's a Prefab.
You can add code to prefabs 














# SETTINGS CHANGE

Settings Changed:

Change FOV to 90, click the camera button in the top right.

Slightly increase the Gizmo Size by 2 clicks

Game Settings - Texture quality to Fast, but change it to best for deployment.









# EDITOR

When possible, do not load by code. Use drag and drop functionality.

















# SCENE

Root/Parent Scene should only have player logic and lighting elements.
Child scenes should have levels, enemies, and other game objects.

A Parent Scene doesn't know anything about its child scene,
however the Child Scene knows about the Parent Scene

At runtime, only the root scene is loaded, not the children,
which is done via code.
Only the parent scene is loaded by default.

Child scenes can be designated so that you can open them up in a tab with a certain type, this happens if they are not part of a parent scene.

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








# ASSET PIPELINE

Resources - Made outside of Stride
Models, Skeletons, Audio, Skyboxes, Fonts, Sprites, Textures, Animations,

Assets - Representations of Element inside of game during Design Time
Some Assets require Resource files.
Some Assets do not, things like Prefabs, Materials, Scenes, Colliders
Editors are used to Modify Assets

Entities - Assets within the Scene.

Components - Entities hold a list of Components, 
All Entities have at least 1 Entity, the Transform Component

Content - Assets get compiled into Content, Runtime Representation that are Loaded while the Game is Running
Game code is working with content.






# RESOURCES

Resources should be put in the Resources folder that is created with the Stride Project, to have a centralized location when sharing.




# TEXTURES

Generate Mipmaps - Allows for better performance when rendering textures at a smaller size than the original when seen at a distance. Uses more memory.
DO NOT USE IT FOR UI

Compress - Compresses the texture to save memory, but can cause artifacts.

Stream - Dynamic texture loading, don't use for things that are always seen, like the player character.
No loading splash screens.
Stream textures only load textures when needed.

Normal Map - basically its' a texture that has divots or cracks, which can reflect engine light, causing it to have depth but not affect the model itself.

Global Texture quality Settings - in the Game Settings




# MODELS

Groups - You can set groups to be rendered by a camera. One camera might not see a group, the other might see it, useful for certain types of games or fog of war, invis, etc.

How to flow of the Model Pipeline Works:

Texture - This is the base image to be applied to the model.
Material - This is the container for the texture, and other properties like shininess, transparency, etc.
Model - Apply the Material to the Model.
Model is technically a component on the Entity, so in a strange way the Model asset is preloaded with the Model component, which in itself has the Material component.




# HOW TO IMPORT THE MODEL THAT IS GIVING YOU MATERIAL TROUBLES

Most of the times Models aren't going to import correctly,
Export FBX from Blender
Get the Texture, usually provided, but if not you can export with Blender by Baking or other methods.
Shoot for good accuracy on the Texture to be complete.
Load the Model in Stride, Load the Texture and import as color, or something else if needed.
You may need other files to complete the reflections and lighting, it can get pretty complex.
Then create a new diffuse material, or type of material you need.
Drag and drop texture into material.
Drag and drop material into model.



# COLLIDERS

Static - Doesn't move colliders, like walls, floors, not affected by gravity, etc.
Can be moved by code if need be.

Rigid Bodies - Crates, balls, barrels, effected by other things like gravity or explosions.

Character Collider - Attach to Playable Character.

Custome Model Collider - Get your Model, Add Asset - Convex Hull - Select your model
If you want a more detailed collider, then go to that collider shape asset, then go to Decomposition and select it,
then fiddle with settings until you get to what you like.
Selecting decomposition will auto create the collider upon it to be more detailed and you can see that.

Get Components like this, you can also drag an drop if needed.
public StaticColliderComponent staticColliderComponent = Entity.Get<StaticColliderComponent>();

Character Collider has a lot of built in properties, for example step height, which if you step on something taht is slightly higher,
like a step, it will lift up onto it.
or a max slope, which detects if there is a slope angle greater than that, and it won't let it on the slope.
Giving a collider shape is needed, usually it should be a capsule.





# TRIGGERS

In your Collider set it to be a Trigger

Way 1 To Detect Collision:
staticColliderComponent.Collisions.CollectionChanged += BoxTriggerCollidedWith;

private void BoxTriggerCollidedWith(object sender, TrackingCollectionChangedEventArgs e)
{
   Collision collisionFromEventTrigger = (Collision)e.Item;

   var ballCollider = staticColliderComponent == collisionFromEventTrigger.ColliderA ? collisionFromEventTrigger.ColliderB : collisionFromEventTrigger.ColliderA;

   if(e.Action == NotifyCollectionChangedAction.Add){ // enters collision
      collisionStatus = $"{staticColliderComponent.Entity.Name} Collision detected with {ballCollider.Entity.Name}";
   }

   if(e.Action == NotifyCollectionChangedAction.Remove){ // exits collision
      collisionStatus = $"{staticColliderComponent.Entity.Name} Collision ended with {ballCollider.Entity.Name}";
   }

}
// Better Example
private void CollisionTriggerEvent(object sender, TrackingCollectionChangedEventArgs e)
{
   Collision collisionFromTriggerEventThatIsHappening = (Collision)e.Item;

   // There are a few ways to figure out which Collder is Which, do not assume A is always the Entity with the trigger.

   // decide by name, problem because its a string
   // collisionFromTriggerEventThatIsHappening.ColliderA.Entity.Name;
   // collisionFromTriggerEventThatIsHappening.ColliderB.Entity.Name;

   // the tutlorial way is to predefine your entities so for example

   StaticColliderComponent boxStaticCollider = Entity.Get<StaticColliderComponent>(); // get the collider component from the Entity with the trigger
   if(boxStaticCollider == collisionFromTriggerEventThatIsHappening.ColliderB) 
   // You can also do comparisons on the entity
   if(Entity == collisionFromTriggerEventThatIsHappening.ColliderA.Entity) // if the collider is the one with the trigger
   
   // After finding out which collider is which, you can check to see if the Add or Remove event happened when they collide or exit collision
   if(e.Action == NotifyCollectionChangedAction.Add){ // enters collision
   if(e.Action == NotifyCollectionChangedAction.Remove){ // exits collision




Way 2:

public override void Update()
{
   List<Collision> collisionsInBoxList = staticColliderComponent.Collisions.ToList<Collision>();

for(int i = 0; i < collisionsInBoxList.Count; i++)
{
   Collision collision = collisionsInBoxList[i];
   DebugText.Print($"Collision {i} detected with {collision.ColliderA.Entity.Name}", new Int2(500, 300));   // ColliderA is the object you have the trigger set on, Box
   DebugText.Print($"Collision {i} detected with {collision.ColliderB.Entity.Name}", new Int2(500, 320));   // ColliderB is the object that is colliding, Sphere
}


Way 1 is prefered so that you are not in the Update area.









# CODE


Start() is the init function
Update() is the function that happens every frame










# LOGGING and DEBUGGING

Log.Info("Hi Mom!"); 

6 Levels Ordered by Severity - cant set it globally, must set it in game per script
Debug - disabled by default
Verbose - disabled by default
Info
Warning
Error
Fatal

DebugText - shows up on the game screen.














# PERFORMANCE

Change Enumerable to List to load it all at once and get it in memory.
Use Enumerable for effeciency, but might cause load times later.









# LOCATION TRANSFORMS

Local makes easy sense, getting world position you must use the WorldMatrix

var localPosition = Entity.Transform.Position;
var worldPosition = Entity.Transform.WorldMatrix.TranslationVector;
DebugText.Print($"Local Position: {localPosition}", new Int2(10, 10), Color.Red);
DebugText.Print($"World Position: {worldPosition}", new Int2(10, 30), Color.Red);

When doing Transforms you should call
Sphere.Transform.UpdateWorldMatrix();
after doing so it will update the world matrix for you.

You can also call Local Updates, but UpdateWorldMatrix does it all anyways.

if you are transforming with a collider, then do
RigidbodyComponent rigidbodyComponent = Sphere.Get<RigidbodyComponent>();
rigidbodyComponent.LinearVelocity = Vector3.Zero;  // stops current movement
rigidbodyComponent.UpdatePhysicsTransformation();  // updates physics to be where its supposed to be on the transform















# DATA MEMBERS

You can expose public vars in the edtior just by doing public, you can specify order by using Stride.Core.DataMember(0) - put in the number you want
Add slider values like: [DataMemberRange(0, 100, 0.1, 1, 3)]
Ignore values with:  [DataMemberIgnore]
Add drop down values by first creating the enum, then giving that data member the enum's type, selects only from those enum values














# COMPONENTS

Generally you should create it through the editor, but you can do it by code via:
var ammoComponent1 = new AmmoComponentTutorial();
Entity.Add(ammoComponent1);
or:
var ammoComponent3 = Entity.GetOrCreate<AmmoComponentTutorial>(); // this is great to find a component or create one if it doesn't exist, but in genral this seems like bad practice
This also has an issue, because the start was already called in the Component itself, so start is going to trigger on the next frame, not the current frame, at least I think that's how it works.


# DELTA TIME

float deltaTime = (float)Game.UpdateTime.Elapsed.TotalSeconds; // how you get it, but strange there is no automatic delta time caller
// need to have a new extended method that is called by the update function called call update, but this won't work, not sure there is a way to possibly do this.
public float GetDeltaTime()
{
   float deltaTime = (float)Game.UpdateTime.Elapsed.TotalSeconds;
   return deltaTime;
}













# LOADING CONTENT

You can load content normally dragging into scene, which is probably the way to do it as it won't load in memory during the game.

Model loadedModel = Content.Load<Model>("Models/mannequinModel"); // Gets the Content

ModelComponent modelComponent = new ModelComponent(loadedModel);  // creates a Model Component
Entity newEntity = new Entity("NewModel");   // Creates a new entity
newEntity.Add(modelComponent);   // adds the model component to the entity
Entity.Scene.Entities.Add(newEntity);  // adds the entity to the scene





# CLONING

Entity clone0 = MasterSword.Clone();    // this is a clone not a reference
clone0.Transform.Scale = new Vector3(0.9f);
clone0.Transform.Position = new Vector3(1, 1, 0);
Entity.Scene.Entities.Add(clone0);
or if you want to add it to an entity:
Entity.AddChild(clone0);
or if you want to add it based on parent transform:
clone2.Transform.Parent = Entity.Transform;



# REMOVING ENTITIES AND CONTENT

Content.Unload(loadedModel);  // Unloads and removes the content. PROBLEM - keeps the entity
loadedModel = null;

Entity.Scene.Entities.Remove(clone1);
Entity.Scene.Entities.Remove(clone2); // doesn't work because clone 2 is a child, not on the same layer
Entity.RemoveChild(clone2);
clone2.Transform.Parent = null; 

// Don't forget to remove from memory if you do not need to retain them.
clone1 = null;
clone2 = null;






# MOUSE INPUT CAPTURE

if(Input.HasMouse){ // check for mouse existence
if (Input.IsMouseButtonDown(MouseButton.Left)){ // check for input
if (Input.IsMouseButtonPressed(MouseButton.Right)){
if (Input.IsMouseButtonReleased(MouseButton.Middle)){

// for scroll wheel you have to get its delta, up = 1 down = -1
scrollIndex += Input.MouseWheelDelta;
RedTeapot.Transform.Rotation = Quaternion.RotationX(-0.02f * scrollIndex);

Input.MousePosition;  // gives values between 0 and 1, which is 0% and 100% of the screen, can be somewhat useful when ratios required

Vector2 absoluteMousePosition = Input.AbsoluteMousePosition;    // Position on game screen in raw pixels, so if game screen is 400x400 then it gives the pixel position, useful for mouse tracking like spawn at pos



# KEYBOARD CAPTURE

if(Input.HasKeyboard){
if(Input.IsKeyDown(Keys.D1)){
if(Input.IsKeyPressed(Keys.Space)){
if(Input.IsKeyReleased(Keys.F10)){



# VIRTUAL BUTTONS

// Virtual Buttons are Commands or Buttons within the Game itself, that correspond to Hardware Buttons

Input.VirtualButtonConfigSet = Input.VirtualButtonConfigSet ?? new VirtualButtonConfigSet();

VirtualButtonBinding ForwardW = new VirtualButtonBinding("Forward", VirtualButton.Keyboard.W);
VirtualButtonBinding ForwardLeftMouse = new VirtualButtonBinding("Forward", VirtualButton.Mouse.Left);

VirtualButtonConfig virtualButtonConfigForward = new VirtualButtonConfig();
virtualButtonConfigForward.Add(ForwardW);
virtualButtonConfigForward.Add(ForwardUp);

float forward = Input.GetVirtualButtonValue(0, "Forward");  // this is a float because triggers can be between 0 and 1, but most inputs are 0 or 1.
if(forward > 0){
   BlueTeapot.Transform.Rotation *= Quaternion.RotationY(0.4f * forward * GetDeltaTime());
// use the forward variable when dettecting preassure sensitive controls like gamepads or pedals, etc.






# LERP

// You can build an easy transform movement system like so:

public float AnimationTime = 3;
private float elapsedTime = 0;
private Vector3 startPosition = Entity.Transform.Position;
private Vector3 endPosition = new Vector3(1, 2, 0);

elapsedTime += GetDeltaTime();
float lerpValue = elapsedTime / AnimationTime;
Entity.Transform.Position = Vector3.Lerp(startPosition, endPosition, lerpValue);

This will move something in LERP fashion very smoothly




# RANDOMNESS

private Random random;
random = new Random(Game.UpdateTime.Total.Milliseconds);
random.Next(0,100);




# PREFABS

Prefabs are a combination of assets put together as a template for ease of use to drop into a scene.
Think of a pile of boxes

How to load them:
Drag an drop from assets into the Scene, this is the general way.

Load via code:

// This way will just get the instances and add them individually
List<Entity> instance = pileOfBoxesPrefab.Instantiate(); // create a list of entities from the prefab
Entity.Scene.Entities.AddRange(instance); // adds the entities into the scene

// This way creates an Entity as a parent, and adds the instances as children
Prefab prefabContentLoadedViaPath = Content.Load<Prefab>("Prefabs/Pile of boxes");
List<Entity> prefabBoxesLoadedViaPath = prefabContentLoadedViaPath.Instantiate();
Entity pileOfBoxesEntity = new Entity("Prefab Entity", new Vector3(0, 0, -2));
for (int i = 0; i < prefabBoxesLoadedViaPath.Count; i++)
{
   pileOfBoxesEntity.AddChild(prefabBoxesLoadedViaPath[i]);
}
Entity.Scene.Entities.Add(pileOfBoxesEntity);










# UI

You cannot drag and drop, you must get it per code to do any manipulation.

public TextBlock nameEditText = page.RootElement.FindName("NameEditText") as EditText;
public EditText nameTextBlock = page.RootElement.FindVisualChildOfType<TextBlock>("NameTextBlock");
Button nameButton = page.RootElement.FindVisualChildOfType<Button>("NameButton");
nameButton.Click += ButtonClickedEvent;

Do not use inline functions when establishing click or text changed events

You can also create an entire UI page just from code, Do not do this.













# RAYCAST

If an Entity does not have a Physics Component (Collider) then the Physics Simulation will not be Initialized for it.

simulation = this.GetSimulation();  // get simulation for physics
private Entity laser;   // This entity is something like a bullet or projeticle to show where the ray cast is. Useful for Debugging, assign via editor or get with code laser = Entity.FindChild("Laser");

// Start
simulation = this.GetSimulation();

// set positions up.

Vector3 startPosition = Entity.Transform.Position;    // start position of the object shooting the raycast
Vector3 targetPosition = startPosition + new Vector3(0, 0, 3.0f); // this is the target position of the raycast

// returns a bool to signal if its hit or not
bool isHit = simulation.Raycast(
   startPosition, // starting position of the ray
   targetPosition,   // ending position of the ray
   out HitResult hitResult,   // if it hits, this gets filled with the result, which you then do things with like get the Point in space that it hit, or the Collider and tehn the Entity 
   // hitResult.Collider.Entity; or hitResult.Point
   CollisionFilterGroups.DefaultFilter, // Sets the collision group that hte raycast is part of, you should likely set this with a public var through the editor for it to be visible
   CollideWith, // Determines what the raycast can hit based on selecting the Collision Group it can collide with set this in the editor too
   isCollideWithTriggers // whether it hits triggers or not.   set this within the editor
);


// after you do it, you can do things with the hit result or not and do like a trace graphic like if a bullet shoots out and hits nothing.

if(isHit){
   float distanceToScale = Vector3.Distance(startPosition, hitResult.Point);
   hitResult.Collider.Entity;
   laser.Transform.Scale.Z = distanceToScale;      // scales the laser entity to the distance that it hit.
}
else{
   float distanceToScale = Vector3.Distance(startPosition, targetPosition);   // scales it only to the place you wanted the laser to go, meaning to its drop off point.
   laser.Transform.Scale.Z = distanceToScale;
}

You can also get the hitResult to see if it succeeded
if(hitResult.Succeeded){



// Raycast with Penetration

// Do the same thing except for the action call

var hitResults = new List<HitResult>();

simulation.RaycastPenetrating(
   startPosition,
   targetPosition,
   hitResultsList,   // This gets filled, basically every entity the ray hits
   CollisionFilterGroups.DefaultFilter, 
   CollideWith, 
   isCollideWithTriggers 
);

// Go through all the hitResults

if(hitResultsList.Count > 0)
{
   for(int i = 0; i < hitResultsList.Count; i++)
   {
      Log.Info($"Hit {i} {hitResultsList[i].Collider.Entity.Name}");
      HitResult hitResultFromList = hitResultsList[i];
   }
}
else
{
   DebugText.Print("No hits", new Int2(drawX, drawY), Color.Red);
}









# PROJECT - 3D to 2D

Project is to get a point in 3D space to a 2D screen position

public Entity GlobalSphere;
public CameraComponent Camera; // get the camera to put the text to
Texture backBuffer = GraphicsDevice.Presenter.BackBuffer;   // Renders to back buffer, then once it finishes, goes to front buffer to show to user.
Vector3 parentSpherePosition = GlobalSphere.Transform.WorldMatrix.TranslationVector;   // get an entity to project 2d text from their 3d position
Vector3 parentScreenPosition = Vector3.Project(    // This is the screen position, 3d to 2d
   parentSpherePosition,   // where our sphere that we want is
   0, // x this is the viewport, which in our case is the entire screen itself, this changes depending on where you are trying to project
   0, // y
   backBuffer.Width, 
   backBuffer.Height,
   0, // can be zero as this is minZ and we are going from 2D to 3D
   0, // maxZ
   Camera.ViewProjectionMatrix   // this is the calculation of the matrix, doing stuff I don't understand.
);
DebugText.Print($"Parent Sphere Position: {parentSpherePosition}", new Int2(parentScreenPosition.XY()), Color.Red);


# UNPROJECT - 2D to 3D

Unproject is to get a 2D screen position to a 3D point in space

Example is to click and then get the 2D position of the click on the screen and then put that 3d model at that 2d space using a raycast

public Entity GoldenSphere;

private Simulation simulation = this.GetSimulation();

if(Input.HasMouse){
   if(Input.IsMouseButtonPressed(MouseButton.Left)){


Texture backBuffer = GraphicsDevice.Presenter.BackBuffer;   // Renders to back buffer, then once it finishes, goes to front buffer to show to user.
Viewport viewPort = new Viewport(0,0,backBuffer.Width,backBuffer.Height);     // this is a created viewport based on the backbuffer, I think you can create your own viewport.


var nearPosition = viewPort.Unproject(             // this gets the near position of the click
   new Vector3(Input.AbsoluteMousePosition, 0),    // gets the input position of the click
   ViewportCamera.ProjectionMatrix,                  // the Projection Matrix of the camera which is basically the matrix of the viewport
   ViewportCamera.ViewMatrix,          // The view of the matrix
   Matrix.Identity                        // the mathematical identity of the matrix, which is what you perform math on
);

var farPosition = viewPort.Unproject(
   new Vector3(Input.AbsoluteMousePosition, 1.0f),   // gets the far position of the input click
   ViewportCamera.ProjectionMatrix,
   ViewportCamera.ViewMatrix,
   Matrix.Identity
);

bool isHit = simulation.Raycast(
   nearPosition,  // shoots ray from point to far point
   farPosition, 
   out HitResult hitResult
);

if(hitResult.Succeeded){      // where ever we hit the raycast, which ahs been shot from our mouse click into the screen, into 3d, we get the 3d space, then spawn the clone
   Entity cloneOfGoldenSphere = GoldenSphere.Clone();
   cloneOfGoldenSphere.Transform.Position = hitResult.Point;
   Entity.Scene.Entities.Add(cloneOfGoldenSphere);
}






# SCRIPTS

Startup Script - Only has the startup function, which calls once. - This is useless just use the Sync Script

Sync Script - Contains start and Update, don't forget the deltatime helper script.

Async Script - Contains Execute, allowing await












# ASYNC AWAIT

Use Async when you need to await something, or in the following situations:
DB Queries
Web Requests
File I/O
Delays
Anything that would freeze another part.
Need to do multiple tasks in parallel.

Works very similar to regular async, but you need a while loop to check if game is running
and then do a next frame on a script, or await something that is calling the next frame.

public override async Task Execute()
{
   StartAsyncStride();  // Calls the Start function that happens only once.

   while (Game.IsRunning)
   {
      await UpdateAsyncStride();    // Calls something that happens on every frame similar to update
      await Script.NextFrame();    // Tells game to move to next frame, if you don't have this game is frozen.
   }
}

private void StartAsyncStride()
{
   // init anything you need in here, you can also return it up if need be, but I usually use members.
}

private async Task UpdateAsyncStride()
{
   // gets called every frame, in this example we go to retrieve data from web api

   if (Input.HasKeyboard)
   {
      if (Input.Keyboard.IsKeyPressed(Keys.Space))
      {
         await RetrieveData();
      }
   }
}


private async Task RetrieveData()
{
   var client = new HttpClient();
   HttpResponseMessage response = await client.GetAsync("https://meowfacts.herokuapp.com/");
   if (response.StatusCode == System.Net.HttpStatusCode.OK)
   {
      string contentOfResponse = await response.Content.ReadAsStringAsync();
      var result = JsonSerializer.Deserialize<MeoFacts>(contentOfResponse);
      mewoData = result.data; // Extracts the list of facts
      // stackOverflowData = JsonSerializer.Deserialize<List<MeoFacts>>(contentOfResponse);
      Log.Info($"contentOfResponse: {contentOfResponse}");
      Log.Info($"newData: {mewoData}");
   }
}



// You can also use awaits to change things the moment something happens.

public override async Task Execute()
{
   StartAsyncStride();

   while (Game.IsRunning)
   {
      await UpdateAsyncStride();
      // await Script.NextFrame();     // if you are already waiting for the next frame you do not need to do this.
   }
}


private async Task UpdateAsyncStride()
{
   private Material yellowMaterialToApply = Content.Load<Material>("Materials/Yellow");

   Collision collisionOfBall = await staticColliderComponent.NewCollision();     // thisis why we don't use Script.NextFrame()
   // until has a new collision, the script won't do anything, we are waiting on the collision

   PhysicsComponent ballColliderComponent;
   if(staticColliderComponent == collisionOfBall.ColliderA)
   {
      ballColliderComponent = collisionOfBall.ColliderB;
   }
   else  // ColliderB
   {
      ballColliderComponent = collisionOfBall.ColliderA;
   }

   ModelComponent ballModel = ballColliderComponent.Entity.Get<ModelComponent>();

   Material originalMaterial = ballModel.GetMaterial(0);

   // Material originalMaterial = ballModel.Materials[0];

   ballModel.Materials[0] = yellowMaterialToApply;

   await staticColliderComponent.CollisionEnded();  // thisis why we don't use Script.NextFrame()

   ballModel.Materials[0] = originalMaterial;

   // it sets back the material after collision has ended with that await event. There are lots in stride.

}















# ANIMATION

Play animations with Animation Clips

Each Animation clip has a skeleton, model, and normal time frame features.

Add the animation component to your model or whatever you want the animation to play.

Animation Components have keys, so you can set a different animation clip to the key.

You attach a script to control those keys and animation component.

public float AnimationSpeed = 1.0f;             // speed of animation
public AnimationComponent animationComponent;   // the animation component, usually put on your entity that has a model, put animation clips here
private PlayingAnimation playingAnimation;      // this is the var that contains whatever animation is currently playing.

private static class AnimationState
{
   public const string Idling = "Idling";
   public const string Punching = "Punching";
   public const string Running = "Running";
}


playingAnimation = animationComponent.Play(AnimationState.Idling);
playingAnimation.TimeFactor = AnimationSpeed;   // sets play speed



playingAnimation = animationComponent.Crossfade(AnimationState.Running, TimeSpan.FromSeconds(0.5));   // changes animation smoothly over 0.5 seconds
playingAnimation.TimeFactor = AnimationSpeed;







// pauses the currently playing animation mid animation, pressing again starts the animation.
private void PauseAnimations()
{
   if (Input.IsKeyPressed(Keys.Space))
   {
      for(int i = 0; i < animationComponent.PlayingAnimations.Count; i++)
      {
         animationComponent.PlayingAnimations[i].Enabled = !animationComponent.PlayingAnimations[i].Enabled;
      }
   }
}


// increases or decreases animation speed.
private void AdjustAnimationSpeed()
{
   if (Input.IsKeyPressed(Keys.Up))
   {
      AnimationSpeed += 0.1f;
   }

   if (Input.IsKeyPressed(Keys.Down))
   {
      AnimationSpeed -= 0.1f;
   }

   playingAnimation.TimeFactor = AnimationSpeed;
}


if (Input.IsKeyPressed(Keys.P))     // press P while not punching to punch.
{
   playingAnimation = animationComponent.Crossfade(AnimationState.Punching, TimeSpan.FromSeconds(0.1));
   playingAnimation.TimeFactor = AnimationSpeed;
   playingAnimation.RepeatMode = AnimationRepeatMode.PlayOnce;    // only plays the animation once
}


if (!animationComponent.IsPlaying(AnimationState.Punching))
{
   if (playingAnimation.Name == AnimationState.Punching)
   { // this gets the latest Animation, so if the last animation was Punch, but is not playing, do this.
      // sets it back to idle after done with a punch.
      playingAnimation = animationComponent.Play(AnimationState.Idling);
      playingAnimation.TimeFactor = AnimationSpeed;
      playingAnimation.RepeatMode = AnimationRepeatMode.LoopInfinite;   // once it goes back to idle do it infinitely
   }
}










# AUDIO MUSIC SOUND

Sound Effect - Non Spatial Short Sounds
Spatialized Sound - Comes from a direction, 3d sound
Music - Large audio files

You can set compression rate for quality of sound files on the property of the asset
Spatialized - means it can turn into 3d
Stream from Disk - useful for long and huge music files

For 3D sounds you use an Audio Emitter component.
Use HRTF, Directional Factor, Environment is used for VR.

After an Audio Emitter is set up,
you need an Audio Listerner, usually onto the camera or whatever point needs to hear it.
This is how it knows the 3D makes the 3D feel to the sound.

Put the audio listener on the camera in ordedr to handle the 3d sounds.

Put the audio emitter on the object you want to emit audio
and put the script thats playing the sound on the object.


public Sound BackgroundMusic; // drag drop sound file in

private SoundInstance musicInstance = UkeleleSound.CreateInstance();

public AudioEmitterComponent GunAudioEmitterComponent;   // drag drop in your emitter spot, or the place the sound emits

private AudioEmitterSoundController gunAudioEmitterSoundController = GunAudioEmitterComponent[SoundsFromEntity.GunShot];;

ukeleleSoundInstance.Play();  // play sound

ukeleleSoundInstance.PlayExclusive();  // makes it so its the only sound playing, stops all other sound instances.

gunAudioEmitterSoundController.Play(); // plays sound in 3d space

You can use awaits to pause music and restart it

public Sound BackgroundMusic; // drag and drop baackground music

private SoundInstance musicInstance = BackgroundMusic.CreateInstance();

await musicInstance.ReadyToPlay();

if(musicInstance.PlayState == Stride.Media.PlayState.Playing)
{
   musicInstance.Pause();
}
else
{
   musicInstance.Play();
}









# CAMERA 

FIRST PERSON CAMERA AND MOVEMENT

To get a first person camera, you need to have a pivot point (empty entity), put it on the thing you want to have a fps view from.

Put the camera as a child to the pivot point.

Put the script onto the Character Model itself, not the pivot (child of model) or the camera.

Swap between mouse click in menu and being first person

You have to capture mouse movement speed, the model, and do a lot of math as in the example.

For character collision and moving a cahracter, you need to have the character rigid body on your entity,
You need to add the collider shape of capsule.


private bool isActive = true; // is your camera active, meaning not paused with mouse screen
public float MouseSpeed = 0.5f;  // This gets applied to camera rotation as the mouse moves in an FPS
public bool isXAxisInverted;     // set in editor if you want it to be inverted, will likely be read in from settings later on.
public bool isYAxisInverted;
public float MaxCameraAngleDegrees = 50.0f;  // Prevents continued rotation, you can set it in the editor.
public float MinCameraAngleDegrees = -50.0f;
private float maxCameraAngleRadians = MathUtil.DegreesToRadians(MaxCameraAngleDegrees);  // conversion to Radians.
private float  minCameraAngleRadians = MathUtil.DegreesToRadians(MinCameraAngleDegrees);
private Vector3 cameraRotation = Entity.Transform.RotationEulerXYZ;   // You must set this to get the correct camera position as it is in the editor.

public Entity CameraPivotPoint;  // this is the camera's pivot point which you should have for any type of camera on model situation.
public CharacterComponent FirstPersonCharacterComponent; // set in editor, this is the component which ahs the colliders

if (Input.IsKeyPressed(Keys.Escape))
{
   isActive = !isActive;   // pauses fps camera and stops looking around to instead use the mouse.
   Game.IsMouseVisible = !isActive;
   Input.UnlockMousePosition();
}


if (isActive)
{

   Input.LockMousePosition();    // locks mouse so it doesn't appear on the screen.

   var mouseMovement = Input.MouseDelta * MouseSpeed;    // gets the delta version of the mouse speed.

   cameraRotation.Y -= mouseMovement.X * ConvertInversionBool(isXAxisInverted);   // x movement is rotating the y axis, its negative because it most go the opposite way as if you are looking that way.
   cameraRotation.X += mouseMovement.Y * ConvertInversionBool(isYAxisInverted);  // this does not need negative as its the opposite of the y axis.

   cameraRotation.X = MathUtil.Clamp(cameraRotation.X, minCameraAngleRadians,  maxCameraAngleRadians);   // had to change this in the tutorial this clamps and prevents over rotation.


   // Entity.Transform.Rotation = Quaternion.RotationY(cameraRotation.Y);     // sets the rotation for Y, which is looking along the x axis movement.
   //commented out the above because ow that we have a character component, we want to rotate the character component instead
   FirstPersonCharacterComponent.Orientation = Quaternion.RotationY(cameraRotation.Y);   // this rotates the character component instead of the entire entity, which is what we want.
   CameraPivotPoint.Transform.Rotation = Quaternion.RotationX(cameraRotation.X);    // sets the rotation for X, based on the pivot point, otherwise you move the entire mesh.
   // you don't want to move the entire mesh on the Y axis as it will rotate the entire model, but its fine for X axis because that makes sense.
   // it might make sense if the model is completely symetrical on all sides, like a sphere, but idk

}




Now to do the actual movement:

public Vector3 MovementSpeed = new Vector3(3, 0, 4);  // base movement speed, x is left/right, z is forward/backward
// could add y here for something like jumping or levitation, but not for this tutorial.

public CharacterComponent FirstPersonCharacterComponent; // set in editor, this is the component which has the colliders

var velocity = new Vector3(); // the velocity for how much we are going to move an entity.

if (Input.HasKeyboard)
{

   // WASD movement
   if (Input.IsKeyDown(Keys.W))     // Must use Key Down
   {
      velocity.Z++;
   }

   if (Input.IsKeyDown(Keys.S))
   {
      velocity.Z--;
   }

   if (Input.IsKeyDown(Keys.A))
   {
      velocity.X++;
   }

   if (Input.IsKeyDown(Keys.D))
   {
      velocity.X--;
   }

   velocity.Normalize();   // Normalize it so we don't move faster diagonally.
   velocity *= MovementSpeed; // multiply by the base speed
   velocity = Vector3.Transform(velocity, Entity.Transform.Rotation);   // transform movement into the look direction
   FirstPersonCharacterComponent.SetVelocity(velocity);  // does the movement using the special Character collider

}



THIRD PERSON CAMERA AND MOVEMENT 

This builds off of # CAMERA

Make a Third Person Pivot, then set it as ca child of your regular Pivot.

You need a new script to add it to the character entity, which still has the player movement script on it.

The new script is a lot of the same code, new code below.

public Entity cameraThirdPersonPivotPoint;   // this is the third person pivot point.
// set the third person pivot
cameraThirdPersonPivotPoint.Transform.Position = new Vector3(0); // the reason why its zero is beacuse its the child of the actual pivot point,
// therefore we should set it to zero to be at the pivot point.

public Vector3 CameraOffsetThirdPerson = new Vector3(0,0,-3);   // off set for it to be behind, use other values to change offset per xyz axis
cameraThirdPersonPivotPoint.Transform.Position += CameraOffsetThirdPerson;  // offset behind player.

Now we need a raycast in order to figure out if he camera offset is correct from third person pivot to first person pivot.

private Simulation simulationObject = this.GetSimulation(); // gets Stride's physics runtime simulation for the raycast.
cameraThirdPersonPivotPoint.Transform.UpdateWorldMatrix();  // Update world position for the upcoming raycast.

public float MinimalHitDistance = 1.0f; // Minimal Distance between pivot points.

Vector3 raycastStart = CameraPivotPoint.Transform.WorldMatrix.TranslationVector;  // get world position of the pivot points
Vector3 raycastEnd = CameraThirdPersonPivotPoint.Transform.WorldMatrix.TranslationVector; 

bool isHit = simulationObject.Raycast(raycastStart, raycastEnd, out HitResult hitResult); // perform raycast

if(isHit)
{
   // calculate the distance between the start position and the hit point position.
   float hitDistance = Vector3.Distance(raycastStart, hitResult.Point);

   if(hitDistance > MinimalHitDistance)
   {
      CameraThirdPersonPivotPoint.Transform.Position.Z = -(hitDistance - 0.1f);  // this will move the pivot so that if it hits a wall or something like that,
      // then it will adjust and place the camera forward or backwards to the right distance so it doesnt go into the wall, although
      // this seems sort of soulful to have it in that way.
   }
   else
   {
      CameraThirdPersonPivotPoint.Transform.Position = new Vector3(0); // set this to the default position which is right behind the player.
   }

}









# NAVIGATION NAVIGATE NAV

In the Assets Folder,
There is a Game Settings File.

In it has a Navigation component.

In it has a Dynamic Navigation Option, which is useful for an RTS so that charactesr don't run into new objectss like buildings that get created
which you can select to be enabled.
It also has an included collision group so you set how or what the nav mesh gets generated around.

In the normal options it has Groups, a List

It has two types of collision groups,

each time we generate a nav mesh that is calculatable, 
we apply certain settings.

For exasmple in the two groups, Elves and Dwarves,
Each have a different height, and a mximum slop and a radius and a max climb height.

Meaning that Elves could potentially go on a slope that Dwarves cannot.
And that dwarves could go into smaller spaces than elves.

To create a new nav mesh, go to the scene folder and in that scene
create a Navigation Mesh and name it a navigation for that group you want to navigate,
for example creating one for the Elves and Dwarves.

Set the scene on the nav mesh.
You can set the collision groups, but leave for default.

You can see colldiders as to where the nav mesh gets geenrated from by turning on the physics,
shows a lot of static colliders for a level.

There are a lot of build settings, but mostly they are fine,
only really mess with them if its not being properly generated.

Then we have groups allowed on the navmesh.

Set the Elves group to it.

Create another and set Dwarves.

To see how the nav mesh is going to be generated
Add an empty entity for Nav Mesh A

Then add a Navigation Bounding Box component.

You can in the top right settings show the navigation filters to show what groups can nav where depending on the Navigation Bounding Box

You can change the size of the bounding box in order to encapsulate more colliders to generate the nav mesh on it.

You should try to get the Navigation Bounding Box to fit the areas you want the nav to happen as closely as possible, adjust the size.

In some situations like in our example, because the cell size is too big for the Elf mesh, it could not walk up the stairs,

so setting cell size to 0.1 allows for that to happen.

Also when adjusting height it helps too, as it looks for the next raised point.

If you do the same for the dwarf you'll see it not be able to go on the slope that the elf could.
It also allows it to walk under the walkway.

Select the Entity that you want to perform navigation, for example right click and move.
Add component -> Navigation.
Set the Navigation Mesh to what you want, for example elves.
Add it to the elf group.

Add a notifier of right clicking for movement, in this case a sphere.

Crate a script for Navigation, and set it to your camera, because your
Camera is the most logical place to place the script??? (maybe not)
Although really if you correctly get all your things matched up, then it should work no matter waht entity it's placed on.
Which brings up a good question, should I just have entities, which only contain scripts?
Or should I have entities that contain scripts and components?


public Entity Character = null;        // The character Entity you want to move
public Entity NavigationSphere = null;    // The nav sphere that gets placed down, think of it like a pathing line.
public NavigationComponent NavigationComponentForCharacter = null;      // this is the nav component on the character entity.
public CameraComponent ViewportCamera = null;   // this is the camera that you will be active while clicking.
public float MovementSpeed = 1.7f;   

private Simulation simulation;

private List<Entity> waypointSpheres;        // list of entities that get spawned
private List<Vector3> waypointPaths;         // list of locations to move to.

private int waypointIndex;    // this allows us to go through the waypointPaths as we move to each one.

private float DT => (float)Game.UpdateTime.Elapsed.TotalSeconds;     // delta time var

public override void Start()
{
   // inits
   simulation = this.GetSimulation();
   waypointSpheres = new List<Entity>();
   waypointPaths = new List<Vector3>();
   waypointIndex = 0;
}

if (Input.Mouse.IsButtonPressed(MouseButton.Left)) // when clicking, set new nav
{
   RemoveExistingNavSpheres();   
   SetNewTarget();
}
UpdateMovement();    // updates the movement, checks if we need to move or not.


private void UpdateMovement()
{
   if (waypointPaths.Count > 0)     // checks if we have waypoints.
   {
         // I think there is a better way to do this, but not for this tutorial.
      Vector3 nextWaypoint = waypointPaths[waypointIndex];     // gets the waypoint we need to move to
      Vector3 currentPosition = Character.Transform.WorldMatrix.TranslationVector;        // gets where we are

      float distance = Vector3.Distance(currentPosition, nextWaypoint);    // calculates the ditance to travel

      if(distance > 0.1f)  // check if distance is large enough to keep moving, basically we aren't there yet.
      {
         // performs movement using Delta Time and move speed.
         Vector3 velocity = nextWaypoint - currentPosition;    
         velocity.Normalize();
         velocity *= (DT * MovementSpeed);
         Character.Transform.Position += velocity;
         
      }
      else{    // if we have arrived at the waypoint, we need to increment the waypoint if it's not the last one.
         if(waypointIndex < waypointPaths.Count - 1)
         {
            waypointIndex++;
         }
         else{ // they are equal or somehow greater so we are done with the movement.
            RemoveExistingNavSpheres();
         }
      }
   }
}

private void RemoveExistingNavSpheres()   // Removes nav spheres from scene and clears waypoints from list.
{
   if (waypointSpheres != null)
   {
      for(int i = 0; i < waypointSpheres.Count; i++)
      {
         Entity.Scene.Entities.Remove(waypointSpheres[i]);
      }
      waypointSpheres.Clear();
      waypointPaths.Clear();
   }
   waypointSpheres = new List<Entity>();
}



private void SetNewTarget()
{
   // Determine the 3D Position in our Sceen based on where our mouse is.
   Texture backBuffer = GraphicsDevice.Presenter.BackBuffer;   // Renders to back buffer, then once it finishes, goes to front buffer to show to user.
   Viewport viewPort = new Viewport(0, 0, backBuffer.Width, backBuffer.Height);     // gets the view of the cam

   Vector3 nearPosition = viewPort.Unproject(
      new Vector3(Input.AbsoluteMousePosition, 0),
      ViewportCamera.ProjectionMatrix,
      ViewportCamera.ViewMatrix,
      Matrix.Identity
   );

   Vector3 farPosition = viewPort.Unproject(
      new Vector3(Input.AbsoluteMousePosition, 1.0f),
      ViewportCamera.ProjectionMatrix,
      ViewportCamera.ViewMatrix,
      Matrix.Identity
   );

   bool isHit = simulation.Raycast(
      nearPosition,
      farPosition,
      out HitResult hitResult
   );

   if (hitResult.Succeeded)
   {
      bool isFoundPath = NavigationComponentForCharacter.TryFindPath(    // This generates a path for travel, basically giving positions
      // this you can then use on your entities to travel.
         hitResult.Point,     // Position it needs to find a path to.
         waypointPaths    // End Result which gets stored into a List
      );

      if (isFoundPath)  // you don't have to have this, just check the count.
      {
         waypointIndex = 0;
         for (int i = 0; i < waypointPaths.Count; i++)      // cycle through list of waypoints
         {  // may want to set the entities to the scene in a parent entity.
            Entity cloneOfNavSphere = NavigationSphere.Clone();      // clone
            cloneOfNavSphere.Transform.Position = waypointPaths[i];  // set position
            waypointSpheres.Add(cloneOfNavSphere); // add to list
            Entity.Scene.Entities.Add(cloneOfNavSphere); // add to scene
         }
      }
   }
}












# TESTING

Stride uses xunit
Example:
https://doc.stride3d.net/latest/en/manual/troubleshooting/unit-tests.html

How to name tests:
https://enterprisecraftsmanship.com/posts/you-naming-tests-wrong/





















