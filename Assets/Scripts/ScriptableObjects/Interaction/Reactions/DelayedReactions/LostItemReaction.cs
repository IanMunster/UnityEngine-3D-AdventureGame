/// <summary>
/// Lost item reaction. Class: DelayedReaction
/// Reaction on Removing Item from Inventory (Finding Correct Inventory, Give Correct Item To Remove)
/// </summary>

public class LostItemReaction : DelayedReaction {
	// Reference to Item to Remove
	public Item item;
	// Reference to Inventory to Remove Item from
	private Inventory inventory;

	// Function to Find the Inventory of the Specific Type (init)
	// Protective Override: Member Acces to Override within Class
	protected override void SpecificInit () {
		// Find the Referenced Inventory to Remove Item From
		inventory = FindObjectOfType <Inventory> ();
	}

	// Function to Remove Found Item from Inventory
	// Protective Override: Member Acces to Override within Class
	protected override void ImmediateReaction () {
		// Remove the Item (Call Remove Item Function from Inventory)
		inventory.RemoveItem (item);
	}

}