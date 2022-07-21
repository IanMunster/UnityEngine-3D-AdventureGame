using UnityEngine;

/// <summary>
/// Item. Class: ScriptableObjects
/// This can be Created as Asset. Currently Hold Sprite of Item. 
/// *Note: Can be Modified for more Complex Inventory Systems*
/// </summary>

[CreateAssetMenu]
public class Item : ScriptableObject {
	// Sprite of Item
	public Sprite sprite;
}