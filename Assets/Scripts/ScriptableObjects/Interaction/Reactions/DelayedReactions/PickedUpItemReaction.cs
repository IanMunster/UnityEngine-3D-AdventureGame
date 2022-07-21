/// <summary>
/// Picked up item reaction. Class: DelayedReaction
/// Reaction on Adding Item to Inventory (Finding Correct Inventory, Give Correct Item to Add)
/// </summary>

public class PickedUpItemReaction : DelayedReaction {
	// Reference to Item to Add
	public Item item;
	// Reference to Inventory to Add Item to
	private Inventory inventory;


	// Function to Find the Inventory of the Specific Type (init)
	// Protective Override: Member Acces to Override within Class
	protected override void SpecificInit () {
		// Find the Referenced Inventory to Add Item to
		inventory = FindObjectOfType <Inventory> ();
	}

	// Function to Add Found Item from Inventory
	// Protective Override: Member Acces to Override within Class
	protected override void ImmediateReaction () {
		// Add the Item (Call Add Item Function from Inventory)
		inventory.AddItem (item);
	}
}