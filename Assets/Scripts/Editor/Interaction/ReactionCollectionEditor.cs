using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // Required when using Editor-Components (and Overrides)

/// <summary>
/// Reaction collection editor.
/// 
/// </summary>

// SubEditor with ReactioEditors
[CustomEditor (typeof (ReactionCollection) )]
public class ReactionCollectionEditor : EditorWithSubEditors <ReactionEditor, Reaction> {

	// Reference the ReactionCollection (Target)
	private ReactionCollection reactionCollection;
	// Reference to the ReactionProperty (SerializedProperty)
	private SerializedProperty reactionProperty;
	// Array of Reaction Types
	private Type[] reactionTypes;
	// Array og ReactionType Names
	private string[] reactionTypeNames;
	// Index in Collection, which is Selected
	private int selectedIndex;

	// Constant value of the DropArea (Components-Drop; Like Unity-Default Editor)
	private const float dropAreaHeight = 50f;
	// Constant value of Spacing between elements
	private const float controlSpacing = 5f;
	// Default name in ReactionProperty
	private const string reactionPropName = "Reactions";

	// Value of Vertical Spacing between Elements
	private readonly float verticalSpacing = EditorGUIUtility.standardVerticalSpacing;


	// Called when this Script is Enabled
	private void OnEnable () {
		// Cache the Target
		reactionCollection = (ReactionCollection)target;
		// Set Reaction Property by Finding the Property in Target
		reactionProperty = serializedObject.FindProperty (reactionPropName);
		// Check and Create the New ReactionEditors (subEditors)
		CheckAndCreateSubEditors (reactionCollection.reactions);
		// Create the Reaction Names
		SetReactionNamesArray ();
	}


	// Called when this Script is Disabled
	private void OnDisable () {
		// CleanUp all previous Editors
		CleanupEditors ();
	}



	// Function to Setup a SubEditor (reactionEditor)
	protected override void SubEditorSetup (ReactionEditor editor) {
		// Set the Editor Property
		editor.reactionsProperty = reactionProperty; 
	}


	// Called when Inspector is Opened (every Frame)
	public override void OnInspectorGUI () {
		// Update the Serialized Properties
		serializedObject.Update ();

		// Create the ReactionSubeditor
		CheckAndCreateSubEditors (reactionCollection.reactions);
		// Go through all SubEditors
		for (int i = 0; i < subEditor.Length; i++) {
			// Display All the Reactions
			subEditor[i].OnInspectorGUI ();
		}
		// If there are Reactions
		if (reactionCollection.reactions.Length > 0) {
			// add Space
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
		}
		// Create a Rectangle for FullWidth of the Inspector (with enough Height for DropArea)
		Rect fullWidthRect = GUILayoutUtility.GetRect (GUIContent.none, GUIStyle.none, GUILayout.Height(dropAreaHeight + verticalSpacing) );
		// Ceate a Rectangle for the Left GUI Controls
		Rect leftAreaRect = fullWidthRect;
		// Set Left Area to Half of Total
		leftAreaRect.y += verticalSpacing * 0.5f;
		// Set LeftArea With to Half of Total
		leftAreaRect.width *= 0.5f;
		// Slighty Less than Half (For DropArea)
		leftAreaRect.width -= controlSpacing * 0.5f;
		// Height Same as DropArea
		leftAreaRect.height = dropAreaHeight;
		// Create a Rectangle for Right GUI Controls (same as Left)
		Rect rightAreaRect = leftAreaRect;
		// Set RightGUI on Rightside
		rightAreaRect.x += rightAreaRect.width + controlSpacing;
		// Display Type popup and Button on LeftGUI
		TypeSelectionGUI (leftAreaRect);
		// Display the Drag&Drop Area on RightGUI
		DragAndDropAreaGUI (rightAreaRect);
		// Manage Events for Dropping on the Area
		DraggingAndDropping (rightAreaRect, this);

		// Apply the Modified Serialized Properties to Target
		serializedObject.ApplyModifiedProperties ();
	}


	// Select the Type of the Rectangle Containing InspectorGUI (Drop&Drag Area & ReactionCollectionsProperties)
	private void TypeSelectionGUI (Rect containingRect) {
		// Create a Rectangle for the Top
		Rect topHalf = containingRect;
		// Set to Half
		topHalf.height *= 0.5f;
		// Create BottomHalf
		Rect bottomHalf = topHalf;
		// set BottomHalf on Bottom
		bottomHalf.y += bottomHalf.height;

		//Display ReactionType popup on TopHalf
		selectedIndex = EditorGUI.Popup (topHalf, selectedIndex, reactionTypeNames);

		// If Button is Click, display BottomHalf
		if (GUI.Button (bottomHalf, "Add Selected Reaction")) {
			// Find Type of SelectedPopup
			Type reactionType = reactionTypes[selectedIndex];
			// Create Appropriate Reaction
			Reaction newReaction = ReactionEditor.CreateReaction (reactionType);
			// Add Reaction to ReactionCollectionArray
			reactionProperty.AddToObjectArray (newReaction);
		}
	}


	// 
	private static void DragAndDropAreaGUI (Rect containingRect) {
		// Create GUI Box
		GUIStyle centeredStyle = GUI.skin.box;
		// with MiddleAligned Text
		centeredStyle.alignment = TextAnchor.MiddleCenter;
		// and ButtonColor
		centeredStyle.normal.textColor = GUI.skin.button.normal.textColor;

		// Create Box in Area with CenteredAlinged-Style
		GUI.Box (containingRect, "Drop new Reactions here", centeredStyle);
	}


	// Function when Reaction is Dropped on Drag Area
	private static void DraggingAndDropping (Rect dropArea, ReactionCollectionEditor editor) {
		// Cache Current Event
		Event currentEvent = Event.current;

		// If the Mouse is Not in DropArea
		if (!dropArea.Contains (currentEvent.mousePosition) ) {
			// End this Function
			return;
		}

		// Switch on the CurrentEventType
		switch (currentEvent.type) {
		// If Mouse IS dragging Something
		case EventType.DragUpdated:
			// Display if Drag is Valid (based on IsDragValid-Function)
			DragAndDrop.visualMode = IsDragValid () ? DragAndDropVisualMode.Link : DragAndDropVisualMode.Rejected;
			// Check if Event is Not used Elsewhere
			currentEvent.Use ();
			//Break out of Switch
			break;

		// If Mouse WAS Dragging Something
		case EventType.DragPerform:
			// Accept Drag Event
			DragAndDrop.AcceptDrag ();
			// Go through all Objects being Dragged
			for (int i = 0; i < DragAndDrop.objectReferences.Length; i++) {
				// Find ScriptAsset being Dragged
				MonoScript script = DragAndDrop.objectReferences[i] as MonoScript;
				// Find the Reaction Type on that Script
				Type reactionType = script.GetClass ();
				// Create Reaction of that Type
				Reaction newReaction = ReactionEditor.CreateReaction (reactionType);
				// Add the newReaction to ReactionCollectionArray
				editor.reactionProperty.AddToObjectArray (newReaction);
			}

			// Check if Envent is Not used Elsewhere
			currentEvent.Use ();
			// Break out of Switch
			break;
		}
	}


	// Function to Check if Drag Item is a Valid Reaction
	private static bool IsDragValid () {
		// Go through Object being Dragged
		for (int i = 0; i < DragAndDrop.objectReferences.Length; i++) {
			// If No ScriptAssets found
			if (DragAndDrop.objectReferences[i].GetType () != typeof (MonoScript)) {
				// Return that the Drag is Invalid
				return false;
			}
			//Otherwise cast the ScriptAsset
			MonoScript script = DragAndDrop.objectReferences[i] as MonoScript;
			// find Class contained in ScriptAsset
			Type scriptType = script.GetClass ();

			// If Script does Not Inherit Reaction
			if (!scriptType.IsSubclassOf (typeof (Reaction) ) ) {
				// Return that the Drag is Invalid
				return false;
			}
			// If the Script is an Abstract (not Created)
			if (scriptType.IsAbstract) {
				// Return that the Drag is Invalid
				return false;
			}
		}
		// Otherwise if None of Dragged Objects returned Invaled, Return Valid
		return true;
	}


	// Function to Set correct Names of Reactions
	private void SetReactionNamesArray () {
		// Store ReactionType
		Type reactionType = typeof (Reaction);
		// Get all Types in same Assembly (all runtime Scripts) as the ReactionType
		Type[] allTypes = reactionType.Assembly.GetTypes ();
		// Create empty List for Subtypes of Reaction
		List<Type> reactionSubTypeList = new List<Type> ();

		// Go through all Types in Assembly
		for (int i = 0; i < allTypes.Length; i++) {
			// If Non-Abstract Subclass of Reaction
			if (allTypes[i].IsSubclassOf (reactionType) && !allTypes[i].IsAbstract) {
				// Add Subclass to List
				reactionSubTypeList.Add (allTypes[i]);
			}
		}

		// Convert List to Array
		reactionTypes = reactionSubTypeList.ToArray ();

		// Create an EmptyList of Strings to store Names of ReactionTypes
		List<string> reactionTypeNameList = new List<string> ();

		// Go through ReactionTypes
		for (int i = 0; i < reactionTypes.Length; i++) {
			// Add names to List
			reactionTypeNameList.Add (reactionTypes[i].Name);
		}

		// Convert List to Array
		reactionTypeNames = reactionTypeNameList.ToArray ();
	}
}