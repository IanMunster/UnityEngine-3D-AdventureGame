using UnityEngine;
using UnityEditor; // Required

/// <summary>
/// Text reaction editor.
///  
/// </summary>

[CustomEditor (typeof (TextReaction) )]
public class TextReactionEditor : ReactionEditor {

	// String of Message to Display
	private SerializedProperty messageProperty;
	// Color of Message to Display
	private SerializedProperty textColorProperty;
	// Delay of Message to Display
	private SerializedProperty delayProperty;

	// Name of Message Field
	private const string textReactionPropMessageName = "message";
	// Name of TextColor Field
	private const string textReactionPropTextColor = "textColor";
	// Name of Delay Field
	private const string textReactionPropDelayName = "delay";
	// Constant value to make GUI line up
	private const float areaWidthOffset = 19f;
	// Constant value to set How Many Lines Tall message area should be
	private const float messageGUILines = 3f;


	// Initialization
	protected override void Init () {
		// Cache the SerializedObject properties (message)
		messageProperty = serializedObject.FindProperty (textReactionPropMessageName);
		// Cache the SerializedObject properties (textColor)
		textColorProperty = serializedObject.FindProperty (textReactionPropTextColor);
		// Cache the SerializedObject properties (Delay)
		delayProperty = serializedObject.FindProperty (textReactionPropDelayName);
	}


	// Display the Reaction in Box
	protected override void DrawReaction () {
		// Start a Horizontal Box
		EditorGUILayout.BeginHorizontal ();
		// Display a Label (width offset so that TextArea lines up with GUI)
		EditorGUILayout.LabelField ("Message", GUILayout.Width (EditorGUIUtility.labelWidth - areaWidthOffset) );
		// Display interactable GUI element for TextMessage to be displayed over SeveralLines
		messageProperty.stringValue = EditorGUILayout.TextArea (messageProperty.stringValue, GUILayout.Height(EditorGUIUtility.singleLineHeight * messageGUILines) );
		// End the Horizontal Box
		EditorGUILayout.EndHorizontal ();

		// Display Default GUI for textColor and Delay
		EditorGUILayout.PropertyField (textColorProperty);
		EditorGUILayout.PropertyField (delayProperty);
	}

	// Give Label to foldout
	protected override string GetFoldoutLabel () {
		// Return the Foldout Label
		return "Text Reaction";
	}

}
