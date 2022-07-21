using System;
using UnityEngine;
using UnityEditor; //Required when using Editor-Components (and overrides)

/// <summary>
/// Save data editor.
/// Custom Editor for SaveData, Pairs Correct Properties with DataTypes
/// Used to Add and Remove SaveData's Properties from and to SaveData
/// </summary>

[CustomEditor (typeof (SaveData) )]
public class SaveDataEditor : Editor {
	
	// Reference to Target
	private SaveData saveData;
	// Delegate for GUI representing Bool values
	private Action<bool> boolSpecificGUI;
	// Delegate for GUI representing Int values
	private Action<int> intSpecificGUI;
	// Delegate for GUI representing String values
	private Action<string> stringSpecificGUI;
	// Delegate for GUI representing Vector3 values
	private Action<Vector3> vector3SpecificGUI;
	// Delegate for GUI representing Quaternion values
	private Action<Quaternion> quaternionSpecificGUI;


	// Called when this Script is Enabled
	private void OnEnable () {
		// Cache Target
		saveData = (SaveData)target;

		// Set the Value for BoolDelegate as Toggle 'Read-Only' GUI function
		boolSpecificGUI = value => { EditorGUILayout.Toggle (value); };
		// Set the Value for IntDelegate as LabelString 'Read-Only' GUI function
		intSpecificGUI = value => { EditorGUILayout.LabelField ( value.ToString() ); };
		// Set the Value for StringDelegate as Label 'Read-Only' GUI function
		stringSpecificGUI = value => { EditorGUILayout.LabelField (value); };
		// Set the Value for Vector3Delegate as Vector3Field 'Read-Only' GUI function
		vector3SpecificGUI = value => { EditorGUILayout.Vector3Field (GUIContent.none, value); };
		// Set the Value for QuaternionDelegate as Vector3Field withRotation 'Read-Only' GUI function
		quaternionSpecificGUI = value => { EditorGUILayout.Vector3Field (GUIContent.none, value.eulerAngles); };
	}


	// Called when Inspetor is Open (every Frame)
	public override void OnInspectorGUI () {
		// Display Data for each Data Type
		KeyValuePairListGUI ("Bools", saveData.boolKeyValuePairLists, boolSpecificGUI);
		KeyValuePairListGUI ("Integers", saveData.intKeyValuePairLists, intSpecificGUI);
		KeyValuePairListGUI ("Strings", saveData.stringKeyValuePairLists, stringSpecificGUI);
		KeyValuePairListGUI ("Vector3s", saveData.vector3KeyValuePairLists, vector3SpecificGUI);
		KeyValuePairListGUI ("Quaternions", saveData.quaternionKeyValuePairLists, quaternionSpecificGUI);
	}


	//
	private void KeyValuePairListGUI <T> ( string label, SaveData.KeyValuePairLists<T> keyValuePairList, Action<T> specificGUI) {
		// Create Box for all DataTypes
		EditorGUILayout.BeginVertical (GUI.skin.box);
		// Indent each Box
		EditorGUI.indentLevel ++;
		// Dispaly Label for DataType
		EditorGUILayout.LabelField (label);

		// If DataTypes where Found
		if (keyValuePairList.keys.Count > 0) {
			// Go through all Found DataTypes
			for (int i = 0; i < keyValuePairList.keys.Count; i++) {
				// Create box for DataType
				EditorGUILayout.BeginHorizontal ();

				// Display Label and Properties
				EditorGUILayout.LabelField (keyValuePairList.keys[i]);
				specificGUI (keyValuePairList.values[i]);

				// End box for DataType
				EditorGUILayout.EndHorizontal ();
			}

		}

		// Stop Indent level
		EditorGUI.indentLevel --;
		// End VerticalBox for all DataTypes
		EditorGUILayout.EndVertical ();
	}

}
