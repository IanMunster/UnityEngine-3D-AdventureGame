using System.Collections;
using UnityEngine;
using UnityEditor; // Required when Using Editor-Components (and Overides)

/// <summary>
/// Inventory editor. Class: Editor
/// Used to overide the InspectorGUI to match with Inventory System
/// </summary>

// Cast Type of Script (Custom Editor of Inventory)
[CustomEditor (typeof (Inventory) )]
public class InventoryEditor : Editor {

	// ItemImages Array
	private SerializedProperty itemImagesProperty;
	// Item Array
	private SerializedProperty itemsProperty;
	// Which Items Should Be Shown
	private bool[] showItemSlots = new bool[Inventory.numItemSlots];

	// Names to LookFor ()
	private const string inventoryPropItemImagesName = "itemImages";
	private const string inventoryPropItemsName = "items";

	// Find the Inventory & Items
	private void OnEnable () {
		// Find the SerializedProperty's (Find the Serialized Property of this Name)
		itemImagesProperty = serializedObject.FindProperty (inventoryPropItemImagesName);
		itemsProperty = serializedObject.FindProperty (inventoryPropItemsName);
	}

	// Override the Inspector GUI (Exists in EditorClass)
	public override void OnInspectorGUI () {
		// Update the SerializedObjects
		serializedObject.Update ();
		// Go through all the InventoryItemSlots
		for (int i = 0; i < Inventory.numItemSlots; i++) {
			// Call the GUI of each ItemSlot
			ItemSlotGUI(i);
		}
		// Apply the Modified Properties of the SerializedObject
		serializedObject.ApplyModifiedProperties ();
	}


	// Function to Display Item GUI
	private void ItemSlotGUI (int index) {
		// Make the Item Display Vertically (Arrange in Order, within box)
		EditorGUILayout.BeginVertical (GUI.skin.box);
		// Dont Overlap rest of the Box (Indent Slightly)
		EditorGUI.indentLevel ++;
		// Foldout to Show Properties in GUI (Show item on indexNumber, Show name ItemSlot + indexNumber)
		showItemSlots [index] = EditorGUILayout.Foldout (showItemSlots[index], "Item slot "+index);
		// Check if Showing an Item
		if (showItemSlots[index]) {
			// Take SerializedProperty and show Default (retuns SerializedProperty of specific Element)
			EditorGUILayout.PropertyField (itemImagesProperty.GetArrayElementAtIndex(index) );
			EditorGUILayout.PropertyField (itemsProperty.GetArrayElementAtIndex(index) );
		}
		// Dont Leave rest of the Box
		EditorGUI.indentLevel --;
		// End the Vertical Layer Group
		EditorGUILayout.EndVertical ();
	}
}
