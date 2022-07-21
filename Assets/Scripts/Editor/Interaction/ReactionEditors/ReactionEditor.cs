using System;
using UnityEngine;
using UnityEditor; //Required when using Editor-Components and Overrides

/// <summary>
/// Reaction editor.
/// 
/// </summary>

public abstract class ReactionEditor : Editor {

	// Bool if ReactionEditor is Expanded
	public bool showReaction;
	// Represents the SerializedProperty of Array Reaction belong to
	public SerializedProperty reactionsProperty;

	// Target Reaction
	private Reaction reaction;

	// Constant value of ButtonWidth
	private const float buttonWidth = 30f;


	// Called when Script is Enalbed
	private void OnEnable () {
		// Cache the Target Reaction
		reaction = (Reaction)target;
		// Call the Initialization for Inheriting Class
		Init ();
	}


	// Function to Initialize Reaction (Should be Overridden by Inheriting Classes that need Initalization
	protected virtual void Init () {}


	// Called when Inspector is Open (Every Frame)
	public override void OnInspectorGUI () {
		// Update the SerializedObject Properties
		serializedObject.Update ();

		// Create VerticalBox for Reaction
		EditorGUILayout.BeginVertical ();
		// Indent ReactionBox
		EditorGUI.indentLevel ++;

		// Create HorizontalBox for Reaction
		EditorGUILayout.BeginHorizontal ();
		// Display FoldOut for Reaction with Custom Label
		showReaction = EditorGUILayout.Foldout (showReaction, GetFoldoutLabel() );
		// Create Button to Remove Reaction
		if (GUILayout.Button ("-", GUILayout.Width (buttonWidth) ) ) {
			// Remove Reaction from ReactioCollection
			reactionsProperty.RemoveFromObjectArray (reaction);
		}
		// End HorizontalBox for Reaction
		EditorGUILayout.EndHorizontal ();

		// If FoldOut is Open
		if (showReaction) {
			// Draw GUI specific to Inheriting ReactionEditor
			DrawReaction ();
		}

		// Stop Indent ReactionBox
		EditorGUI.indentLevel --;
		// End VerticalBox for Reaction
		EditorGUILayout.EndVertical ();

		// Apply Modified Properties to SerializedObject
		serializedObject.ApplyModifiedProperties ();
	}


	// Funtion to Create Reaction with given Type
	public static Reaction CreateReaction (Type reactionType) {
		// Create Reaction of given Type
		return (Reaction)CreateInstance (reactionType);
	}


	// Function to Draw the ReactionGUI (Could be Overridden by Inheriting Classes, default is draw unitydefault for properties
	protected virtual void DrawReaction () {
		// If not Overriden, draw Default
		DrawDefaultInspector ();
	}


	// Function to Get FoldOut Label (Must be Overriden by Inheriting Classes)
	protected abstract string GetFoldoutLabel ();

}
