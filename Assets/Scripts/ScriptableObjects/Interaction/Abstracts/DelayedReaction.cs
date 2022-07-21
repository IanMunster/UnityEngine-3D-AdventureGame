using System.Collections;
using UnityEngine;

/// <summary>
/// Delayed reaction.
/// 
/// </summary>

public abstract class DelayedReaction : Reaction {

	// All DelayedReactions need a DelayTime
	public float delay;

	// WaitForSeconds type for Delay
	protected WaitForSeconds wait;


	// Function 'hides' InitFunction from ReactionClass (Hide happens when Original Function doesnt meet Requirements for the Function in the InheritingClass.
	//											Previously Reaction just needed Call SpecificInit, but DelayedReaction need Delay set First)
	public new void Init () {
		// Set Wait to WaitForSecond
		wait = new WaitForSeconds (delay);
		// Call Specific Initialization
		SpecificInit ();
	}


	// Function 'hides' ReactFunction from ReactClass (Replaces Functionality with starting Coroutine instead)
	public new void React (MonoBehaviour monoBehaviour) {
		monoBehaviour.StartCoroutine (ReactCoroutine() );
	}


	// Coroutine to React
	protected IEnumerator ReactCoroutine () {
		// Wait for specified Time
		yield return wait;
		// Call ImmediaReaction Function (which must be defined in Inheriting Classes)
		ImmediateReaction ();
	}
}
