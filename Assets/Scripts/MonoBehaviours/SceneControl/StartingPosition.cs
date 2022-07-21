using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Starting position.
/// 
/// </summary>

public class StartingPosition : MonoBehaviour {

	// Name to Indentify StartingPoint for a Scene
	public string startingPointName;

	// Collection of StartingPositions in the Scene
	private static List<StartingPosition> allStartingPositions = new List<StartingPosition> ();


	// Called when this Script is Enabled
	private void OnEnable () {
		// Add this StartingPosition to the StartingPositionList
		allStartingPositions.Add (this);
	}


	// Called when this Script is Disabled
	private void OnDisable () {
		// Remove this StartingPosition form the StartingPositionList
		allStartingPositions.Remove (this);
	}


	// Function to find a StartingPosition of given Name
	public static Transform FindStartingPosition (string pointName) {
		// Go through all StartingPoints in List
		for (int i = 0; i < allStartingPositions.Count; i++) {
			// If StartingPoint name equals the given Name
			if (allStartingPositions[i].startingPointName == pointName) {
				// Return the StartingPoints Transform
				return allStartingPositions[i].transform;
			}
		}
		// Otherwise: if not found in List, return Null
		return null;
	}
}
