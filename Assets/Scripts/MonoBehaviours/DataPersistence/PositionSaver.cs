using UnityEngine;

/// <summary>
/// Position saver.
/// 
/// </summary>

public class PositionSaver : Saver {

	// Reference to Transform to Save/Load Position
	public Transform transformToSave;


	// Function to Set Key of Transform
	protected override string SetKey () {
		// Name based on Transform name, Transform Type and UniqueIndetifier
		return transformToSave.name + transformToSave.GetType().FullName + uniqueIndentifier;
	}


	// Function to Save the Position
	protected override void Save () {
		// Save Postion to Key
		saveData.Save (key, transformToSave.position);
	}


	// Function to Load Position
	protected override void Load () {
		// Position to Pass Funcition
		Vector3 position = Vector3.zero;
		// If Position was Found in LoadFunction
		if (saveData.Load (key, ref position) ) {
			// Load the Position
			transformToSave.position = position;
		}
	}
}
