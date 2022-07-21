using UnityEngine;

/// <summary>
/// Game object reaction.
/// 
/// </summary>

public class GameObjectReaction : DelayedReaction {

	// Reference to GameObject to be Enabled/Disabled
	public GameObject gameObject;
	// State of GameObject after Reaction
	public bool activeState;


	// Override inhereted Function
	protected override void ImmediateReaction () {
		// Set the GameObjects Active State
		gameObject.SetActive (activeState);
	}
}