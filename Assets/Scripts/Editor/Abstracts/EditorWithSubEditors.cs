using UnityEngine;
using UnityEditor; // Required when using Editor-Components (and Overides)

/// <summary>
/// Editor with sub editors.
/// BaseClass that Pairs Correct Editors to SubEditors.
/// </summary>

// Abstract class: cannot be Instantiated by itself. Should be Inhereted <GenericTypes for this Class>
// where TEditor is an Editor and TTarget is an Object.
public abstract class EditorWithSubEditors <TEditor, TTarget> : Editor 
	where TEditor : Editor 
	where TTarget : Object {

	// Array of all SubEditors in Game.
	protected TEditor[] subEditor;

	// Function to Check for and Create SubEditors (Set Target to SubEditorTarget).
	protected void CheckAndCreateSubEditors (TTarget[] subEditorTargets){
		// If SubEditors Found & ArrayLength of SubEditors is equal to ArrayLength of Targets.
		if (subEditor != null && subEditor.Length == subEditorTargets.Length) {
			// Stop Checking.
			return;
		}
		// Found old Editors or Not Correct Number of Editors, Clean all Previous Editors
		CleanupEditors ();
		// Make SubEditorsArray of Length of Send Targets
		subEditor = new TEditor[subEditorTargets.Length];
		// Go through all the SendTargets
		for (int i = 0; i < subEditor.Length; i++) {
			// Ceate a new Editor for SendTargetEditor
			subEditor[i] = CreateEditor (subEditorTargets[i] ) as TEditor;
			// Setup the Editor
			SubEditorSetup (subEditor[i]);
		}
	}

	// Clean up All Previous Created Editors/
	protected void CleanupEditors () {
		// If SubEditors already empty, Stop CleanUp/
		if (subEditor == null) {
			return;
		}
		// If previous Editors found, go through all found Editors
		for (int i = 0; i < subEditor.Length; i++) {
			// Destroy (Immediate: used in Editor) previous Editor
			DestroyImmediate (subEditor[i]);
		}
		// Set the SubEditors to Empty
		subEditor = null;
	}

	// Create a new Editor for a SubEditor
	//Protected Abstract: Doesnt exist in this class, need to be Created in Inherented Class
	protected abstract void SubEditorSetup (TEditor editor);
}
