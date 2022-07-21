using UnityEngine;
using UnityEditor; //Required when using Editor-Components (and Overrides)

/// <summary>
/// Data resetter editor.
/// Creates Custom Editor for the DataResetters (on ScriptableResettableObjects)
/// </summary>

[CustomEditor (typeof (DataResetter) )]
public class DataResetterEditor : Editor {

	// Reference to Target of this Editor
	private DataResetter dataResetter;
	// Represents only Field in Target
	private SerializedProperty resettersProperty;

	// Constant Value of InspectorsButton
	private const float buttonWidth = 30f;
	// Constant Name of Resettable Scriptable Object in field
	private const string dataResetterPropResettableScriptableObjectsName = "resettableScriptableObjects";


	// Called when Script is Enabled
	private void OnEnable () {
		// Cache Property
		resettersProperty = serializedObject.FindProperty (dataResetterPropResettableScriptableObjectsName);
		// Cache the Target
		dataResetter = (DataResetter)target;

		// If no DataResetters Found (Array is Null)
		if (dataResetter.resettableScriptableObjects == null) {
			// Prevent Null reference
			dataResetter.resettableScriptableObjects = new ResettableScriptableObject[0];
		}
	}


	// Called when Inspector is Open (every frame)
	public override void OnInspectorGUI () {
		// Update state of SerializedObject properties (to current values of target)
		serializedObject.Update ();

		// Go through all Resetters
		for (int i = 0; i < resettersProperty.arraySize; i++) {
			// Create GUI for Resetters, with Appropriate Properties
			SerializedProperty resettableProperty = resettersProperty.GetArrayElementAtIndex (i);
			EditorGUILayout.PropertyField (resettableProperty);
		}

		// Begin HorizontalBox
		EditorGUILayout.BeginHorizontal ();

		// Create Button to Add Array Element
		if (GUILayout.Button ("+", GUILayout.Width (buttonWidth) ) ) {
			// Add element to Array
			resettersProperty.InsertArrayElementAtIndex (resettersProperty.arraySize);
		}

		// Create Button to Remove Array Element
		if (GUILayout.Button ("-", GUILayout.Width (buttonWidth) ) ) {
			// If resetterToRemove is found
			if (resettersProperty.GetArrayElementAtIndex (resettersProperty.arraySize - 1).objectReferenceValue ) {
				// Remove element from Array
				resettersProperty.DeleteArrayElementAtIndex (resettersProperty.arraySize - 1);
			}
			// Otherwise if last Elment is not Null, make last Element Null
			resettersProperty.DeleteArrayElementAtIndex (resettersProperty.arraySize - 1);

		}

		// End HorizontalBox
		EditorGUILayout.EndHorizontal ();

		// Apply the Modified Properties to Target
		serializedObject.ApplyModifiedProperties ();
	}
}
