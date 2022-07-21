using UnityEngine;
using UnityEditor; //Required when using Editor-Components (and Overrides)

/// <summary>
/// Serialized property extensions.
/// Extences the SerializedProperty :
/// </summary>

// Does not inherite from anything
public static class SerializedPropertyExtensions {
	// Extention Method Static
	public static void AddToObjectArray <T> (this SerializedProperty arrayProperty, T elementToAdd) 
		where T : Object {
		// If PropertyArray is Not an Array
		if (!arrayProperty.isArray) {
			// Throw Error (holds function if this happens)
			throw new UnityException ("SerializedProperty " + arrayProperty.name + " is NOT an Array.");
		}
		// Update the SerializedObject 
		arrayProperty.serializedObject.Update ();

		// Insert the ElementToAdd into the Properties Array at Index
		arrayProperty.InsertArrayElementAtIndex (arrayProperty.arraySize);
		// Insert the ElementToAdd ObjectReferenceValue (sub-properties) into the PropertiesArray 
		arrayProperty.GetArrayElementAtIndex (arrayProperty.arraySize - 1).objectReferenceValue = elementToAdd;

		// Apply the Modified Properties
		arrayProperty.serializedObject.ApplyModifiedProperties ();
	}


	// Public Function to Remove a Property from PropertyArray at Specific ArrayIndex
	public static void RemoveFromObjectArrayAt (this SerializedProperty arrayProperty, int index) {
		// If Index value is below Zero (out of Array)
		if (index < 0) {
			// Throw Error (holds function if this happens)
			throw new UnityException ("SerializedProperty " + arrayProperty.name + "");
		}
		// If PropertyArray is Not an Array
		if (!arrayProperty.isArray) {
			// Throw Error (holds function if this happens)
			throw new UnityException ("SerializedProperty " + arrayProperty.name + "");
		}
		// If ProppertyArray Size is negative 1
		if (index > arrayProperty.arraySize - 1) {
			// Throw new Error (holds function if this happens)
			throw new UnityException ("SerializedProperty " + arrayProperty.name + "" + "");
		}

		// Update the SerializedObject
		arrayProperty.serializedObject.Update ();
		// IF there is a Reference value at Index (object matches element)
		if (arrayProperty.GetArrayElementAtIndex (index).objectReferenceValue) {
			// Delete this Property at Index
			arrayProperty.DeleteArrayElementAtIndex (index);
		}
		// Otherwise if Not found, Delete empty Reference
		arrayProperty.DeleteArrayElementAtIndex (index);
		// Apply the Modified Properties
		arrayProperty.serializedObject.ApplyModifiedProperties ();
	}


	// Public Function to Remove a Property from PropertyArray, where T is the TargetObject (to remove)
	// Static: ExtensionMethod <Generic Function>
	public static void RemoveFromObjectArray <T> (this SerializedProperty arrayProperty, T elementToRemove)
		where T : Object {
		// If The PropertyArray is not an Array.
		if (!arrayProperty.isArray) {
			// Throw Error (holds function if this happens)
			throw new UnityException ("SerializedProperty " + arrayProperty.name + " is Not an Array.");
		}
		// If there is No ElementToRemove
		if (!elementToRemove) {
			// Throw Error (holds function if this happens)
			throw new UnityException ("Removing a Null Element is Not supported using this Method.");
		}

		// Update the SerializedObject
		arrayProperty.serializedObject.Update ();
		// Go through all entries on PropertyArray
		for (int i = 0; i < arrayProperty.arraySize; i++) {
			// Get the Correct Property at Index
			SerializedProperty elementProperty = arrayProperty.GetArrayElementAtIndex (i);
			// If the Property at Index matches the Property to Remove.
			if (elementProperty.objectReferenceValue == elementToRemove) {
				// Remove the Property from Array at Index
				arrayProperty.RemoveFromObjectArrayAt (i);
				// Return out of this Function
				return;
			}
		}
		// Otherwise if Property to Remove was Not Found; Throw Error.
		throw new UnityException ("Element " + elementToRemove.name + " was Not found in Property " + arrayProperty.name + ".");
	}
}
