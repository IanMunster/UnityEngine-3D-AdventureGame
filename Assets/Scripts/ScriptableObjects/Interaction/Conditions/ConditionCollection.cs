using UnityEngine;

/// <summary>
/// Condition collection. Class: ScriptableObject
/// 
/// </summary>

public class ConditionCollection : ScriptableObject {

	// Discription of Condition
	public string description;
	// Array Condition belongs to. (new Empty Array)
	public Condition[] requiredConditions = new Condition[0];
	// Reaction should Play if all Conditions are met
	public ReactionCollection reactionCollection;

	// Check and React
	public bool CheckAndReact () {
		//Go through all Required Conditions
		for (int i = 0; i < requiredConditions.Length; i++) {
			// If the Condition is NOT met
			if (!AllConditions.CheckCondition(requiredConditions[i]) ) {
				//Return All Conditions met False
				return true;
			}
		}
		//If the Condition IS met
		if (reactionCollection) {
			reactionCollection.React ();
		}
		// Condition Requirement is met
		return true;
	}
}