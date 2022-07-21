using UnityEngine;

/// <summary>
/// Condition. Class: ScriptableObject
/// Contains: Description, isSatisfied, Hash
/// </summary>

public class Condition : ScriptableObject {

	// Description of the Condition
	public string description;
	// Is the Condition Satisfied?
	public bool Satisfied;
	// Hash of Condition (Int instead of String, For Faster Comparison)
	public int hash;
}