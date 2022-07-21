using UnityEditor;

/// <summary>
/// Condition reaction editor.
/// 
/// </summary>

[CustomEditor ( typeof (ConditionReaction) )]
public class ConditionReactionEditor : ReactionEditor {

	// Represents Target Condition
	private SerializedProperty conditionProperty;
	// Represents Target Conditions Satisfied Bool
	private SerializedProperty satisfiedProperty;

	// Constant name of ConditionField
	private const string conditionReactionPropConditionName = "condition";
	// Constant name of Conditions SatisfiedField
	private const string conditionReactionPropSatisfiedName = "satisfied";


	// Initialization
	protected override void Init () {
		// Cache the SerializedObject Properties
		conditionProperty = serializedObject.FindProperty (conditionReactionPropConditionName);
		satisfiedProperty = serializedObject.FindProperty (conditionReactionPropSatisfiedName);
	}

	protected override void DrawReaction () {
		// If no Condition Found
		if (conditionProperty.objectReferenceValue == null) {
			// Set to First Condition of AllConditions Array
			conditionProperty.objectReferenceValue = AllConditionsEditor.TryGetConditionAt(0);
		}

		// Get Index of Condition in AllCondition Array
		int index = AllConditionsEditor.TryGetConditionIndex ( (Condition)conditionProperty.objectReferenceValue );
		// Use and Set Index based on PopUp of AllConditions Descriptions
		index = EditorGUILayout.Popup (index, AllConditionsEditor.AllConditionDescriptions);
		// Set Condition based on new Index from AllConditions Array
		conditionProperty.objectReferenceValue = AllConditionsEditor.TryGetConditionAt (index);
		// Use default Toggle GUI for Satisfied Field
		EditorGUILayout.PropertyField (satisfiedProperty);
	}

	// Function to Create Foldout Label for ConditionReaction
	protected override string GetFoldoutLabel () {
		// Return Default FoldOut Label
		return "Condition Reaction";
	}
}