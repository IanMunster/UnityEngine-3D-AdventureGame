using UnityEngine;

/// <summary>
/// Animation reaction.
/// 
/// </summary>

public class AnimationReaction : DelayedReaction {

	// Reference to Animator
	public Animator animator;
	// Name of Trigger to be Set
	public string trigger;

	// Hash representing Trigger to be Set
	private int triggerHash;


	// Overrides Inhereted Function
	protected override void SpecificInit () {
		// Set the Animator TriggerHash to triggerHash
		triggerHash = Animator.StringToHash (trigger);
	}


	// Overrides Inhereted Function
	protected override void ImmediateReaction () {
		// Set Animators Trigger
		animator.SetTrigger (triggerHash);
	}

}
