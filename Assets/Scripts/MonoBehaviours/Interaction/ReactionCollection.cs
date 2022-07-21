using UnityEngine;

/// <summary>
/// Reaction collection.
/// 
/// </summary>

public class ReactionCollection : MonoBehaviour {

	// Create Array for All Reactions within Collection
	public Reaction[] reactions = new Reaction[0];

	// Use this for initialization
	private void Start () {
		// Go through all Reactions
		for (int i = 0; i < reactions.Length; i++) {
			// (Function Hiding) Cast As Delayed Reaction
			DelayedReaction delayedReaction = reactions[i] as DelayedReaction;
			// If there is a DelayedReaction
			if (delayedReaction) {
				// Initialize the DelayedReaction 
				delayedReaction.Init ();
			// Otherwise: 
			} else {
				// Initialize the Reaction
				reactions[i].Init ();
			}
		}
	}


	// Function to React with Interactables (Delayed and Immediate)
	public void React () {
		// Go through the ReactionCollection
		for (int i = 0; i < reactions.Length; i++) {
			// (Function Hiding) Cast Delayed Reactions
			DelayedReaction delayedReaction = reactions[i] as DelayedReaction;

			// If there is a DelayedReaction
			if (delayedReaction) {
				// Start the Delayed Reaction
				delayedReaction.React (this);
			// Otherwise if there is No Delayed Reaction
			} else {
				// Start the React Immediate
				reactions[i].React(this);
			}
		}
	}
}
