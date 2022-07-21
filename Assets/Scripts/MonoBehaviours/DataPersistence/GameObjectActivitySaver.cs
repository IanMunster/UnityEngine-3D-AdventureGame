using UnityEngine;

/// <summary>
/// Game object activity saver.
/// 
/// </summary>

public class GameObjectActivitySaver : Saver {

	// Reference to GameObject to Load/Save Activity from
	public GameObject gameObjectToSave;


	// Function to Set GameObject Key
	protected override string SetKey () {
		// Name based on GameObject name, GameObject type and UniqueIndetifier
		return gameObjectToSave.name + gameObjectToSave.GetType().FullName + uniqueIndentifier;
	}


	// Function to Save GameObject State
	protected override void Save () {
		// Save the GameObject State
		saveData.Save (key, gameObjectToSave.activeSelf);
	}


	// Function to Load GameObject State
	protected override void Load () {
		// Bool to pass Function
		bool activeState = false;
		// If LoadFunction found Referenced ActiveState
		if (saveData.Load (key, ref activeState) ) {
			// Load the GameObjects ActiveState
			gameObjectToSave.SetActive (activeState);
		}
	}
}