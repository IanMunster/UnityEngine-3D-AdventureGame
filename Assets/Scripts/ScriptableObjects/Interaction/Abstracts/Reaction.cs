using UnityEngine;

/// <summary>
/// Reaction.
/// 
/// </summary>

public abstract class Reaction : ScriptableObject {

	// Called from ReactionCollection
	// Function containing all ReactionRequirements and Calls SpecificInit for InheritingClasses
	public void Init () {
		SpecificInit ();
	}


	// Virtual Function; can be Overriden and Used for needs of Inheriting Class
	protected virtual void SpecificInit () {}


	// Called from ReactionCollection
	// Function containing all ReactionRequirements and Calls Immediate Reaction
	public void React (MonoBehaviour monoBehaviour) {
		ImmediateReaction ();
	}


	// Core of Reaction (must be Overriden)
	protected abstract void ImmediateReaction ();
}
