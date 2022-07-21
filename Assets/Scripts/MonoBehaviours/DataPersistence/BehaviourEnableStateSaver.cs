using UnityEngine;

/// <summary>
/// Behaviour enable state saver.
/// 
/// </summary>

public class BehaviourEnableStateSaver : Saver {

	// Reference to Behaviour to Save/Load EnableState
	public Behaviour behaviourToSave;


	// Function to Set the Key of Behaviour
	protected override string SetKey () {
		// Key based on Name of Behaviour, Type of Behaviours and UniqueIndentifier
		return behaviourToSave.name + behaviourToSave.GetType().FullName + uniqueIndentifier;
	}


	// Function to Save EnabledState
	protected override void Save () {
		// Save the Enabled State on key
		saveData.Save (key, behaviourToSave.enabled);
	}


	// Function to Load EnabledState
	protected override void Load () {
		// Create Bool to pass by LoadFunction
		bool enabledState = false;
		// If LoadFunction found Referenced Key EnabledState
		if (saveData.Load (key, ref enabledState) ) {
			// Load the EnabledState
			behaviourToSave.enabled = enabledState;
		}
	}
}