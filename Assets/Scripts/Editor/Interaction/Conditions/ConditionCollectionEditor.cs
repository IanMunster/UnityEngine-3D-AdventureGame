using UnityEngine;
using UnityEditor; //Required when using Editor-Components (and Overides)

/// <summary>
/// Condition collection editor.
/// 
/// </summary>

//Inherets from EditorWithSubEditor<>
//CustomEditor of the Type: ConditionCollection
[CustomEditor (typeof (ConditionCollection) )]
public class ConditionCollectionEditor : EditorWithSubEditors<ConditionEditor, Condition> {

	// Reference to the Collection of Properties
	public SerializedProperty collectionsProperty;

	// Editors Description Property
	private SerializedProperty descriptionProperty;
	// Editors Conditions Property
	private SerializedProperty conditionsProperty;
	// Editors ReactionCollection Property
	private SerializedProperty reactionCollectionProperty;

	// Target CoditionCollection to Create
	private ConditionCollection conditionCollection;

	// Constant value of Condition EditorsButton Width
	private const float conditionButtonWidth = 30f;
	// Constant value of Collection EditorsButton Width
	private const float collectionButtonWidth = 125f;
	// Name of the DescriptionProperty
	private const string conditionCollectionPropDescriptionName = "description";
	// Name of the ConditionsCollectionProperty
	private const string conditionCollectionPropRequiredConditionsName = "requiredConditions";
	// Name of the ReactionCollectionProperty
	private const string conditionCollectionPropReactionCollectionName = "reactionCollection";


	// Called when this Editor is first Enabled
	private void OnEnable () {
		// If there is no Target
		if (target == null) {
			// Destroy(Immediate: used in Editor) this script.
			DestroyImmediate (this);
			// Return out of this function
			return;
		}
		//Otherwise if there is a target:
		// Find target Descriptions Name
		descriptionProperty = serializedObject.FindProperty (conditionCollectionPropDescriptionName);
		// Find target ConditionsCollection Name
		conditionsProperty = serializedObject.FindProperty (conditionCollectionPropRequiredConditionsName);
		// Find target ReactionCollection Name
		reactionCollectionProperty = serializedObject.FindProperty (conditionCollectionPropReactionCollectionName);

		// Set the Target as a ConditionCollection Type
		conditionCollection = (ConditionCollection)target;
		// Call the Check and Create function (to create target) SubEditors
		CheckAndCreateSubEditors (conditionCollection.requiredConditions);
	}


	// Called when this Script is Disabled
	private void OnDisable () {
		// Destroy all SubEditors
		CleanupEditors ();
	}


	// Once Condition Editor is Created, display correct Information
	protected override void SubEditorSetup (ConditionEditor editor) {
		// Set the Editors Type to ConditionCollection.
		editor.editorType = ConditionEditor.EditorType.ConditionCollection;
		// Set the Editors Property to ConditionsProperty.
		editor.conditionsProperty = conditionsProperty;
	}


	// Called when Inspector is Opened (every Frame)
	public override void OnInspectorGUI () {
		// Update the Serialized Object
		serializedObject.Update ();
		// Check to Create the SubEditors
		CheckAndCreateSubEditors (conditionCollection.requiredConditions);
		// Create box for editor
		EditorGUILayout.BeginVertical (GUI.skin.box);
		// Indent the box
		EditorGUI.indentLevel ++;

		// Create horizontal box
		EditorGUILayout.BeginHorizontal ();
		// Show fold-out
		descriptionProperty.isExpanded = EditorGUILayout.Foldout (descriptionProperty.isExpanded, descriptionProperty.stringValue);

		// If Remove Button was pressed
		if (GUILayout.Button ("Remove Collection", GUILayout.Width (collectionButtonWidth) ) ) {
			// Remove Collection with ExtensionMethod ()
			collectionsProperty.RemoveFromObjectArray (conditionCollection);
		}

		// End Horizontal
		EditorGUILayout.EndHorizontal ();

		// Display rest of GUI (optional Call if expanded)
		if (descriptionProperty.isExpanded) {
			ExpandedGUI ();
		}

		// Dont indent
		EditorGUI.indentLevel --;
		// End Vertical box
		EditorGUILayout.EndVertical ();
		// Apply modification to SerializedObject properties
		serializedObject.ApplyModifiedProperties ();
	}


	// Expand the Inspector GUI
	private void ExpandedGUI () {
		// Create Space between Entries
		EditorGUILayout.Space ();
		// Set the Description Space
		EditorGUILayout.PropertyField (descriptionProperty);
		// Crate Space between Description and Fold-Out
		EditorGUILayout.Space ();

		// Set float for constant space of width
		float space =  EditorGUIUtility.currentViewWidth / 3f;

		// Begin Horizontal box
		EditorGUILayout.BeginHorizontal ();
		// Add Label with name Condition
		EditorGUILayout.LabelField ("Condition", GUILayout.Width(space) );
		// Add Label with name Statisfied?
		EditorGUILayout.LabelField ("Statisfied?", GUILayout.Width(space) );
		// Add LAbel with name Add/Remove
		EditorGUILayout.LabelField ("Add/Remove", GUILayout.Width(space) );
		// End Horizontal box
		EditorGUILayout.EndHorizontal ();

		// Begin Vertical box
		EditorGUILayout.BeginVertical (GUI.skin.box);
		// Go through all SubEditors
		for (int i = 0; i < subEditor.Length; i++) {
			// Call all SubEditors OnInspectorGUI Function
			subEditor[i].OnInspectorGUI ();
		}
		// End Horizontalbox? or Verical
		EditorGUILayout.EndHorizontal ();
	//?	EditorGUILayout.EndVertical ();

		// Begin HorizontalBox
		EditorGUILayout.BeginHorizontal ();
		// Create a FlexableSpace for Buttons
		GUILayout.FlexibleSpace ();
		// Create Add-NewCondition Button
		if (GUILayout.Button ("+", GUILayout.Width(conditionButtonWidth) ) ) {
			// Create new Condition
			Condition newCondition = ConditionEditor.CreateCondition ();
			// Add newCondition to Array
			conditionsProperty.AddToObjectArray(newCondition);
		}
		// End the Horizontal box
		EditorGUILayout.EndHorizontal ();
		// Create space between foldout Name and Property
		EditorGUILayout.Space ();
		// Set Propertyfield to Property
		EditorGUILayout.PropertyField (reactionCollectionProperty);
	}

	// Function to Create ConditionCollections
	public static ConditionCollection CreateConditionCollection () {
		// Create a New ConditionCollection
		ConditionCollection newConditionCollection = CreateInstance <ConditionCollection> ();
		// Create the Description
		newConditionCollection.description = "New ConditionCollection.";
		// Create new RequiredCondition Array (atleast 1)
		newConditionCollection.requiredConditions = new Condition[1];
		// Populate the RequiredConditions
		newConditionCollection.requiredConditions[0] = ConditionEditor.CreateCondition ();

		// Return the New ConditionCollection
		return newConditionCollection;
	}
}