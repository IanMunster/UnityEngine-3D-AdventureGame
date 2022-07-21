using UnityEngine;

/// <summary>
/// Data resetter.
/// 
/// </summary>

public class DataResetter : MonoBehaviour {

	// All ResettableScriptableObjects to be Reset at Start of Game
	public ResettableScriptableObject[] resettableScriptableObjects;


	// Called at Start of Game
	private void Awake () {
		// Go through all ResettableScriptableObjects
		for (int i = 0; i < resettableScriptableObjects.Length; i++) {
			// Reset all ResettableScriptableObjects
			resettableScriptableObjects[i].Reset ();
		}
	}
}