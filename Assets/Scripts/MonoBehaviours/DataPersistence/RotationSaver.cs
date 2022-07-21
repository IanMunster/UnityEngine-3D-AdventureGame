using UnityEngine;

/// <summary>
/// Rotation saver.
/// 
/// </summary>

public class RotationSaver : Saver {

	// Reference to Transform to Save/Load Rotation
	public Transform transformToSave;


	// Function to Set Key for Rotation
	protected override string SetKey () {
		// Name based on Rotation Name, Rotation Type and UniqueIndentifier
		return transformToSave.name + transformToSave.GetType().FullName + uniqueIndentifier;
	}


	// Function to Save Rotation
	protected override void Save () {
		// Save Rotation to key
		saveData.Save (key, transformToSave.rotation);
	}


	// Function to Load Rotation
	protected override void Load () {
		// Create Rotation to Pass to Function
		Quaternion rotation = Quaternion.identity;
		// If Rotation was Found in LoadFunction
		if (saveData.Load (key, ref rotation)) {
			// Load the Rotation
			transformToSave.rotation = rotation;
		}
	}
}