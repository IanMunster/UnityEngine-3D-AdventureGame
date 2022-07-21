using UnityEngine;

/// <summary>
/// Audio reaction.
/// 
/// </summary>

public class AudioReaction : Reaction {

	// Reference to the AudioSource to play Clip
	public AudioSource audioSource;
	// Reference to AudioClip to play
	public AudioClip audioClip;
	// Delay before Clip plays
	public float delay;


	// Override Inhereted Function
	protected override void ImmediateReaction () {
		// Set AudioSource Clip and Play with given Delay
		audioSource.clip = audioClip;
		audioSource.PlayDelayed (delay);
	}
}