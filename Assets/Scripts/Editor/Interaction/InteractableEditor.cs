using UnityEngine;
using UnityEditor;

/// <summary>
/// Interactable editor.
/// 
/// </summary>

[CustomEditor (typeof (Interactable) )]
public class InteractableEditor : EditorWithSubEditors <ConditionCollectionEditor, ConditionCollection> {

	// Reference to Target Interactable
	private Interactable interactable;

	// Represents Transform of Interactable to Interact with
	private SerializedProperty interactionLocationProperty;
	// Represents ConditionCollection Array on Interactable
	private SerializedProperty collectionProperty;
	// Represents ReactionCollection which is used if None of ConditionCollections are
	private SerializedProperty defaultReactionProperty;

	// Constant value of Button Width
	private const float collectionButtonWidth = 125f;
	// Constant Name of Interaction Location Field
	private const string interactablePropInteractionLocationName = "interactionLocation";
	// Constant Name of ConditionCollection Field
	private const string interactablePropConditionCollectionName = "conditionCollection";
	// Constant Name of ReactionCollection Field
	private const string InteractablePropDefaultReactionCollectionName = "defaultReactionCollection";


	// Called when this Script is Enabled
	private void OnEnable () {
		// Cache Target Interactable
		interactable = (Interactable)target;

		// Cache the SerializeProperties
		interactionLocationProperty = serializedObject.FindProperty (interactablePropInteractionLocationName);
		collectionProperty = serializedObject.FindProperty (interactablePropConditionCollectionName);
		defaultReactionProperty = serializedObject.FindProperty (InteractablePropDefaultReactionCollectionName);

		// Create Editors for ConditionCollections
		CheckAndCreateSubEditors (interactable.conditionCollections);
	}


	// Called when this Script is Disableds
	private void OnDisable () {
		// Clean all Previous Editors
		CleanupEditors ();
	}


	// Called when ConditionCollectionEditors are Created
	protected override void SubEditorSetup (ConditionCollectionEditor editor) {
		// Give ConditionCollectionEditor a reference to the Array to which it belongs
		editor.collectionsProperty = collectionProperty;
	}


	// Called when Inspector is Open (Every Frame)
	public override void OnInspectorGUI () {
		// Update the SerializedObjects Propertiess
		serializedObject.Update ();

		// Create Editors for ConditionCollection
		CheckAndCreateSubEditors (interactable.conditionCollections);
		// Display InteractionLocation property
		EditorGUILayout.PropertyField (interactionLocationProperty);

		// go through all ConditionCollection Editors
//		for (int i = 0; i < subEditors.Length; i++) {
			// Display the ConditionCollection
//			subEditors[i].OnInspectorGUI ();
			// Create Space between Elements
			EditorGUILayout.Space ();
//		}

		// Begin HorizontalBox for ConditionCollection
		EditorGUILayout.BeginHorizontal ();
		// Create a FlexibleSpace
		GUILayout.FlexibleSpace ();

		// Create Button to Add ConditionCollection to all ConditionCollections 
		if (GUILayout.Button ("Add Collection", GUILayout.Width (collectionButtonWidth) ) ) {
			// Create a new Collection
			ConditionCollection newCollection = ConditionCollectionEditor.CreateConditionCollection ();
			// Add the newCollection to Collection Array
			collectionProperty.AddToObjectArray (newCollection);
		}

		// End Horizontal Box for ConditionCollection
		EditorGUILayout.EndHorizontal ();

		// Create Space between Elements
		EditorGUILayout.Space ();
		// Display default Reaction property
		EditorGUILayout.PropertyField (defaultReactionProperty);

		// Apply the Modified Properties to the SerializedObject
		serializedObject.ApplyModifiedProperties ();
	}
}
