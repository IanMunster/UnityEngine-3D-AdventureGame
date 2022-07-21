using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Required when using UI-Componenets

/// <summary>
/// Inventory.
/// Players InventorySystem
/// Function to Add items & Function to Remove Items
/// </summary>

public class Inventory : MonoBehaviour {
	
	// Constant value of the Max Number of ItemSlots (Public for InventoryEditor)
	public const int numItemSlots = 4;

	// Array with All ItemImages from InventorySlots 
	[SerializeField] private Image[] itemImages = new Image[numItemSlots];
	// Arrat with Items
	[SerializeField] private Item[] items = new Item[numItemSlots];

	// Function to Add Item to Inventory (Item to Add)
	public void AddItem (Item itemToAdd) {
		// Loop through all ItemSlots
		for (int i = 0; i < items.Length; i++) {
			// Check for Empty Slot
			if (items[i] == null) {
				// Add new Item
				items[i] = itemToAdd;
				// Display Correct ItemImage
				itemImages[i].sprite = itemToAdd.sprite;
				// Enable Sprite to Display
				itemImages[i].enabled = true;
				// Only add this Item (Dont enable all Inventory Slots) so Exit function
				return;
			}
		}
	}

	// Function to Remove Item from Inventory (Item to Remove)
	public void RemoveItem (Item itemToRemove) {
		// Look through all ItemSlots
		for (int i = 0; i < items.Length; i++) {
			// Check for Item in InventorySlot
			if (items[i] == itemToRemove) {
				// Remove the Item
				items[i] = itemToRemove;
				// Remove ItemImage
				itemImages[i].sprite = itemToRemove.sprite;
				// Disable Sprite to Display
				itemImages[i].enabled = false;
				// Only Remove this Item (Dont Disable all Inventory Slots) so Exit function
				return;
			}
		}
	}

}
