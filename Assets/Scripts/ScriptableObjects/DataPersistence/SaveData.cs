using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Save data.
/// 
/// </summary>

[CreateAssetMenu]
public class SaveData : ResettableScriptableObject {

	// Nested Class (lighter replacement for Dictionaries)
	// Required because Dictionaries are Not Serializable. Single GenericType represents Type of Data to be stored
	[Serializable]
	public class KeyValuePairLists <T> {
		// Keys are Unique Indentifiers for each Element of Data
		public List<string> keys = new List<string> ();
		// Values are Elements of Data
		public List<T> values = new List<T> ();


		// Clear all Previous Keys and Values
		public void Clear () {
			// Clear Keys
			keys.Clear ();
			// Clear Values
			values.Clear ();
		}


		// Set the Value of Key
		public void TrySetValue (string key, T value) {
			// Find the Index of Key&Value based on given (lambda expression)
			int index = keys.FindIndex (x => x == key);
			// If Index is Positive
			if (index > -1) {
				// Set Value at Index to given Value
				values[index] = value;
			// Otherwise if No keys
			} else {
				// Add the Given Key
				keys.Add (key);
				// Add the Given Value
				values.Add (value);
			}
		}


		// Get the Value of Key
		public bool TryGetValue (string key, ref T value) {
			// Find Index of Key&Value based on given (lambda expression)
			int index = keys.FindIndex (x => x == key);
			// If Index is Positive
			if (index > -1) {
				// Set Reference value to Value at Index
				value = values[index];
				// Return value was found
				return true;
			}
			// Otherwise: Return value was Not found
			return false;
		}
	}


	// Collections for Various (bool, int, string, Vector3 and Quaternion) Data Types
	public KeyValuePairLists<bool> boolKeyValuePairLists = new KeyValuePairLists<bool> ();
	public KeyValuePairLists<int> intKeyValuePairLists = new KeyValuePairLists<int> ();
	public KeyValuePairLists<string> stringKeyValuePairLists = new KeyValuePairLists<string> ();
	public KeyValuePairLists<Vector3> vector3KeyValuePairLists = new KeyValuePairLists<Vector3> ();
	public KeyValuePairLists<Quaternion> quaternionKeyValuePairLists = new KeyValuePairLists<Quaternion> ();


	// Function to Reset all Collections
	public override void Reset () {
		// Clear all the Collections (Clear Key&Value)
		boolKeyValuePairLists.Clear ();
		intKeyValuePairLists.Clear ();
		stringKeyValuePairLists.Clear ();
		vector3KeyValuePairLists.Clear ();
		quaternionKeyValuePairLists.Clear ();
	}


	// Generic SaveFunction; takes Collection and Value of same Type and tries to Set a Value
	private void Save<T> (KeyValuePairLists<T> lists, string key, T value) {
		lists.TrySetValue (key, value);
	}


	// Generic LoadFunction; takes Collection and Value of same Type and tries to Get a Value
	private bool Load<T> (KeyValuePairLists<T> lists, string key, ref T value) {
		return lists.TryGetValue (key, ref value);
	}



	// Bool SaveFunction PublicOverload (Takes only Bool values and tries to Set Value)
	public void Save (string key, bool value) {
		Save (boolKeyValuePairLists, key, value);
	}

	// Int SaveFunction PublicOverload (Takes only Int values and tries to Set Value)
	public void Save (string key, int value) { 
		Save (intKeyValuePairLists, key, value);
	}

	// String SaveFunction PublicOverload (Takes only String values and tries to Set Value)
	public void Save (string key, string value) {
		Save (stringKeyValuePairLists, key, value);
	}

	// Vector3 SaveFunction PublicOverload (Takes only Vector3 values and tries to Set Value)
	public void Save (string key, Vector3 value) {
		Save (vector3KeyValuePairLists, key, value);
	}

	// Quaternion SaveFunction PublicOverload (Takes only Quaternion values and tries to Set Value)
	public void Save (string key, Quaternion value) {
		Save (quaternionKeyValuePairLists, key, value);
	}



	// Bool LoadFunction Public Overload (Takes only Bool values and tries to Get Value)
	public bool Load (string key, ref bool value) {
		return Load (boolKeyValuePairLists, key, ref value);
	}

	// Int LoadFunction Public Overload (Takes only Int values and tries to Get Value)
	public bool Load (string key, ref int value) {
		return Load (intKeyValuePairLists, key, ref value);
	}

	// String LoadFunction Public Overload (Takes only String values and tries to Get Value)
	public bool Load (string key, ref string value) {
		return Load (stringKeyValuePairLists, key, ref value);
	}

	// Vector3 LoadFunction Public Overload (Takes only Vector3 values and tries to Get Value)
	public bool Load (string key, ref Vector3 value) {
		return Load (vector3KeyValuePairLists, key, ref value);
	}

	// Quaternion LoadFunction Public Overload (Takes only Quaternion values and tries to Get Value)
	public bool Load (string key, ref Quaternion value) {
		return Load (quaternionKeyValuePairLists, key, ref value);
	}
}
