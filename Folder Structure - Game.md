




This structure is made from Stride's Own Standards
as well as expanding it as a bigger game is going to require tons of folders within each category.

Anything Specific should go into Scenes, but the idea is to make almost nothing specific to a scene.

However, certain navigation things might be the case, or certain prefabs, but again, that should never be the case.


Assets
    Animations
    Audio
    BlocksPrefabs - this is for the default models provided
    CollisionMeshes
    Materials
    Models
    Prefabs
        Category of Prefabs like Characters, Vehciles, Weapons, etc.
            The Prefab that draws from the other Assets into one thing.
    Scenes
        Scene Name
    StandsPrefabs - this is for the default models provided
    Textures
    UI

Code - 
    Scripts - This is the top level folder for all your scripts, think of folders as Namespaces. 
        If you dont put it here, they go at the top level as folders

        Skeletons - Contains Common Skeletons for usage
        
        Scenes
            SceneName   - If code is specific to one Scene, then it needs to go into a Scene Folder
                AssetName   - Specific Assets of the Scene that contains its code
        UserInterfaces
            Elements - If this is a UI Element like a health bar
            Menus - If this is a Menu that will contain UI Elements, buttons, etc.
            Buttons - Space for Buttons
        Assets
            AssetCategory - Try to come up with Categories you can put your asset in, to help with organization.
                AssetName

        Services
            GameService
            GameInstanceService
            MultiplayerService

        Tests
            Folder Structure for Tests will Match Above

        Possible Names:
        Libraries
        Controllers
        Models


PizzaModel.cs
PizzaController.cs
PizzaLibrary.cs
PizzaAsset.cs
PizzaElement.cs
























