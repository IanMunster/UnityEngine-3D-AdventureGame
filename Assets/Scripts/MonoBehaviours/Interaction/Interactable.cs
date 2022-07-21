using System.Collections;
using UnityEngine;

/// <summary>
/// Interactable.
/// 
/// </summary>

public class Interactable : MonoBehaviour {

	// Position and Rotation of Interactable
	public Transform interactionLocation;
	// Collection of Conditions and relevant Reaction
	public ConditionCollection[] conditionCollections = new ConditionCollection[0];
	// If No ConditionCollection Reacted to, this default reaction
	public ReactionCollection defaultReactionCollection;


	// Player Calls this Function on Arriving At Interactable
	public void Interact () {
		// Go through all the ConditionCollections
		for (int i = 0; i < conditionCollections.Length; i++) {
			// If React Happens
			if (conditionCollections[i].CheckAndReact() ) {
				// Exit function
				return;
			}
		}
		// Otherwise use the Default Reaction
		defaultReactionCollection.React ();
	}


	// Use this for initialization
	//void Start () {}
	// Update is called once per frame
	//void Update () {}
}
