using UnityEngine;

/// <summary>
/// All conditions. Class: ResettableScriptableObjects
/// 
/// </summary>

public class AllConditions : ResettableScriptableObject {

	// Array of all Conditions
	public Condition[] conditions;

	// Static Instance of this Script
	private static AllConditions instance;
	// Constant String to Loading Path of AllConditions
	private const string loadPath = "AllConditions";


	// Getter/Setter to find the Conditions and Set the conditions
	public static AllConditions Instance {
		// Get the Instance of AllConditions
		get { 
			// If instance is empty, find it in Scene 
			if (!instance) {
				instance = FindObjectOfType <AllConditions> ();
			}
			// If instance not in Scene, find it on its Path
			if (!instance) {
				instance = Resources.Load <AllConditions> (loadPath);
			}
			// If instance not in Path, display Error.
			if (!instance) {
				Debug.LogError ("AllConditions has not been Created yet. Go to Assets > Create > AllConditions");
			}
			// Return the found instance of AllConditions
			return instance;
		}
		set {
			// Set the new value instance of AllConditions
			instance = value;
		}
	}

	// Function to Reset AllConditions (GlobalState Reset)
	public override void Reset () {
		// If Conditions is Empty
		if (conditions == null) {
			// Stop Reset
			return;
		}
		// Go through all the Conditions
		for (int i = 0; i < conditions.Length; i++) {
			// Reset all SatisfiedStates to False;
			conditions [i].Satisfied = false;
		}
	}

	// Checks if the Condition reached its Satisfied State
	public static bool CheckCondition (Condition requiredCondition) {
		// Get an Instance to All Conditions, at time of Function Call
		Condition[] allConditions = Instance.conditions;
		// Make a Reference to the GlobalCondition, at time of Call (make sure its empty)
		Condition globalCondition = null;

		// When AllCondition/First-Condition is NOT Empty
		if (allConditions != null && allConditions[0] != null) {
			// Go through all the Conditions
			for (int i = 0; i < allConditions.Length; i++) {
				// If Condition-Hash equals the RequiredCondition-Hash
				if (allConditions[i].hash == requiredCondition.hash) {
					// Set the GlobalCondition Reference to the Conditions to Check
					globalCondition = allConditions[i];
				}
			}
		}
		// Otherwise if GlobalCondition is Empty
		if (!globalCondition) {
			// Return as False
			return false;
		}
		// Return the new GlobalConditions State
		return globalCondition.Satisfied == requiredCondition.Satisfied;
	}
}
