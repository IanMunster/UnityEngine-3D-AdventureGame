using UnityEngine;

/// <summary>
/// Saver.
/// 
/// </summary>

public abstract class Saver : MonoBehaviour {

	// Unique String set by SceneDesigner to indentify what is being Saved
	public string uniqueIndentifier;
	// Reference to SaveData ScriptableObject where data is Stored
	public SaveData saveData;

	// String to Indentify what is being Saved (Should be set using Information about Data, aswell as UniqueIndentifier.)
	protected string key;

	// Reference to SceneController for Subscriptions
	private SceneController sceneController;


	// Called when Game Starts
	private void Awake () {
		// Find SceneController Reference
		sceneController = FindObjectOfType <SceneController> ();

		// If No SceneController Found
		if (!sceneController) {
			// Throw Error
			throw new UnityException ("SceneController could not be found, ensure that it exists in the PersistentScene.");
		}

		// Set Key based on information from InheretingClasses
		key = SetKey ();
	}


	// Called when this Script is Enabled
	private void OnEnable () {
		// Subscribe Save to BeforeSceneUnload
		sceneController.BeforeSceneUnload += Save;
		// Subscribe Load to AfterSceneLoad
		sceneController.AfterSceneLoad += Load;
	}


	// Called when this Script is Disabled
	private void OnDisable () {
		// Unsubscribe Save from BeforeSceneUnload
		sceneController.BeforeSceneUnload -= Save;
		// Unsubscribe Load from BeforeSceneLoad
		sceneController.AfterSceneLoad -= Load;
	}


	// Function Called in Awake, must Return Intended Key (key must be Unique across all SaverScripts
	protected abstract string SetKey ();
	// Function Called before SceneUnload, must call saveData.Save (pass Key and Relevant Data)
	protected abstract void Save ();
	// Function Called before SceneLoad, must call saveData.Load (with ref to get Data)
	protected abstract void Load ();

}
