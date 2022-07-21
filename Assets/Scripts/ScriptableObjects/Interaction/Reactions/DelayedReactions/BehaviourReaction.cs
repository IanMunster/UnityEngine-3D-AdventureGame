using UnityEngine;

/// <summary>
/// Behaviour reaction.
/// 
/// </summary>

public class BehaviourReaction : DelayedReaction {

	// Behaviour to be Enabled/Disabled
	public Behaviour behaviour;
	// State of the Behaviour after Reaction
	public bool enabledState;


	// Overrides Inhereted Function
	protected override void ImmediateReaction () {
		// Turn on the Behaviour
		behaviour.enabled = enabledState;
	}

}
