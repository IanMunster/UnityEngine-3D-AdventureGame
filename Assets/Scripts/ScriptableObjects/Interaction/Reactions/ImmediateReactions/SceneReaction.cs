/// <summary>
/// Scene reaction.
/// 
/// </summary>

public class SceneReaction : Reaction {

	// Name of Scene ToBeLoaded
	public string sceneName;
	// Name of StartingPosition in LoadedScene
	public string startingPointInLoadedScene;
	// Reference to the SaveData Asset for storing StartingPosition
	public SaveData playerSaveData;

	// Reference to SceneController to Load/Unload Scenes
	private SceneController sceneController;


	// Overrides Inhereted Function
	protected override void SpecificInit () {
		// Find the SceneController
		sceneController = FindObjectOfType<SceneController> ();
	}


	// Overrides Inhereted Function
	protected override void ImmediateReaction () {
		// Save the StartingPositions Name to SaveData Asset
		playerSaveData.Save (PlayerMovement.startingPositionKey, startingPointInLoadedScene);
		// Start Scene Loading Process
		sceneController.FadeAndLoadScene (this);
	}
}