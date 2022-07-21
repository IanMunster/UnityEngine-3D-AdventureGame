using UnityEngine;
using UnityEditor; // Required when using Editor-Components (and Override)

/// <summary>
/// Condition editor.
/// 
/// </summary>

[CustomEditor ( typeof (Condition) )]
public class ConditionEditor : Editor {

	// Enum represents where Condition is being Displayed is Inspector
	public enum EditorType {
		// ConditionAsset is for Single Condition Asset is selected Child of AllConditions Asset
		// AllConditionAsset is for AllCondition Asset is selected (nested Editor)
		// ConditionCollection is for Interactable is selected (nested Editor, within a ConditionCollection)
		ConditionAsset, AllConditionAsset, ConditionCollection
	}


	// Type of Editor
	public EditorType editorType;
	// Represents Array of Conditions on a ConditionCollection
	public SerializedProperty conditionsProperty;

	// Represents String Description of Editors Target
	private SerializedProperty descriptionProperty;
	// Represents Bool Satified of Editors Target 
	private SerializedProperty satisfiedProperty;
	// Represents Hash Number of Editors Target
	private SerializedProperty hashProperty;
	// Refrence to Target
	private Condition condition;

	// Constant value of Button
	private const float conditionButtonWidth = 30f;
	// Constant value of the SatisfiedToggle Offset
	private const float toggleOffset = 30f;
	// Name of Description Field
	private const string conditionPropDescriptionName = "description";
	// Name of Satisfied Field
	private const string conditionPropSatisfied = "satisfied";
	// Name of Hash Field
	private const string conditionPropHashName = "hash";
	// Default Description if no Conditions have been Created
	private const string blackDescription = "No conditions set.";


	// Called when script is Enabled
	private void OnEnable () {
		// Cache the Target
		condition = (Condition)target;

		// If this Editor has Persisted through the destruction
		if (target == null) {
			// Destroy the remaining Editor
			DestroyImmediate (this);
			// do Nothing
			return;
		}

		// Otherwise: Cache the SerializedProperties
		descriptionProperty = serializedObject.FindProperty (conditionPropDescriptionName);
		satisfiedProperty = serializedObject.FindProperty (conditionPropSatisfied);
		hashProperty = serializedObject.FindProperty (conditionPropHashName);
	}


	// Called when Inspector is Open (Every Frame)
	public override void OnInspectorGUI () {
		// Call different GUI depending on Condition
		switch (editorType) {
		case EditorType.AllConditionAsset:
			AllConditionsAssetGUI ();
			break;
		case EditorType.ConditionAsset:
			ConditionAssetGUI ();
			break;
		case EditorType.ConditionCollection:
			InteractableGUI ();
			break;
		default:
			throw new UnityException ("Unknown ConditionEditor.EditorType.");
		}
	}


	// Display each Condition when AllConditions Asset is Selected
	private void AllConditionsAssetGUI () {
		// Begin Horizontal box for Condition
		EditorGUILayout.BeginHorizontal (GUI.skin.box);
		// Indent ConditionBox
		EditorGUI.indentLevel ++;
		// Display Condition DescriptionS
		EditorGUILayout.LabelField (condition.description);

		// Create Button to Remove Condition from AllConditions
		if (GUILayout.Button ("-", GUILayout.Width(conditionButtonWidth) ) ) {
			// Remove Condition from AllConditions
			AllConditionsEditor.RemoveCondition (condition);
		}

		// Stop ConditionBox indent
		EditorGUI.indentLevel --;
		// End ConditionBox
		EditorGUILayout.EndHorizontal ();
	}


	// Display Single ConditionAsset is Selected as Child of AllConditions Asset
	private void ConditionAssetGUI () {
		// Begin Single Condition Box
		EditorGUILayout.BeginHorizontal (GUI.skin.box);
		// Indent Single ConditionBox
		EditorGUI.indentLevel ++;

		// Display the Description of Condition
		EditorGUILayout.LabelField (condition.description);

		// Stop Indent Singe ConditionBox
		EditorGUI.indentLevel --;
		// End Single ConditionBox
		EditorGUILayout.EndHorizontal ();
	}


	// Display InteractableAsset
	private void InteractableGUI () {
		// Update the Serialize Object Properties
		serializedObject.Update ();

		//Width of PopUp, Toggle and Remove Button.
		float width = EditorGUIUtility.currentViewWidth / 3f;
		// Begin HorizontalBox for Interactable
		EditorGUILayout.BeginHorizontal ();

		// Find Index for Target based on AllConditions Array
		int conditionIndex = AllConditionsEditor.TryGetConditionIndex (condition);

		// if ConditionIndex was not Found
		if (conditionIndex == -1) {
			// Set Index to First in Array
			conditionIndex = 0;
		}

		// Set Index based on User Selection of the Condition (by the user)
		conditionIndex = EditorGUILayout.Popup (conditionIndex, AllConditionsEditor.AllConditionDescriptions, GUILayout.Width (width) );
		// Find Condition in AllCondition Array
		Condition globalCondition = AllConditionsEditor.TryGetConditionAt (conditionIndex);
		// Set Description based on GlobalConditions Description
		descriptionProperty.stringValue = globalCondition != null ? globalCondition.description : blackDescription;
		// Set Hash based on Description
		hashProperty.intValue = Animator.StringToHash (descriptionProperty.stringValue);
		// Display Toggle Satisfied Bool
		EditorGUILayout.PropertyField (satisfiedProperty, GUIContent.none, GUILayout.Width (width + toggleOffset) );

		// Create Button to Remove Condition from ConditionCollections Array
		if (GUILayout.Button ("-", GUILayout.Width (conditionButtonWidth) ) ) {
			// Remove Condition from ConditionCollection
			conditionsProperty.RemoveFromObjectArray(condition);
		}

		// End Horizontal IntactableBox
		EditorGUILayout.EndHorizontal ();
		// Apply Update Properties to SerializedObject
		serializedObject.ApplyModifiedProperties ();
	}


	// Function to Create a NewCondition (Static: it can be Called without an Editor being Instanced)
	public static Condition CreateCondition () {
		// Create a new Instance of Condition
		Condition newCondition = CreateInstance <Condition> ();
		// Default Description of Condition
		string blankDescription = "No conditions set.";
		// Set newConditions Description to First in AllConditions Array
		Condition globalCondition = AllConditionsEditor.TryGetConditionAt (0);
		// Set the Description of newCondition (based on if Description exists in Global)
		newCondition.description = globalCondition != null ? globalCondition.description : blackDescription;
		// Set the Hash based on newConditions Description
		SetHash (newCondition);
		// Return the newCondition
		return newCondition;
	}


	// Function to Create a Condition based on Given Description
	public static Condition CreateCondition (string description) {
		// Create new Instance of Condition
		Condition newCondition = CreateInstance <Condition> ();
		// Set Description based on TargetDescription
		newCondition.description = description;
		// Set Hash of NewCondition based on Description
		SetHash (newCondition);
		// Return the newCondition
		return (newCondition);
	}


	// Function to Set the Hash of a Condition (based on Description, used for quick comparison)
	private static void SetHash (Condition condition) {
		// Set the Conditions Hash
		condition.hash = Animator.StringToHash (condition.description);
	}

}
