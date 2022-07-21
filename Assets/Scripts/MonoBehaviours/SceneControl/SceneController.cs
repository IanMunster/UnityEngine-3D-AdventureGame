using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Required when using SceneManager-Components

/// <summary>
/// Scene controller.
/// 
/// </summary>

public class SceneController : MonoBehaviour {

	// Event Delegate Called before SceneUnload
	public event Action BeforeSceneUnload;
	// Event Delegate Called before SceneLoad
	public event Action AfterSceneLoad;

	// Canvas Controlling Fading form/to black
	public CanvasGroup faderCanvasGroup;
	// Fade-To-Black Duration
	public float fadeDuration = 1f;
	// Name of Starting Scene of Game
	public string startingSceneName = "SecurityRoom";
	// Name of Starting Position of Player in Game
	public string initialStartingPositionName = "DoorToMarket";
	// Reference to Players SaveData (Stores StartingPosition of Player)
	public SaveData playerSaveData;

	// Bool if Fade-To-Black is Active
	private bool isFading;

	// Use this for initialization
	private IEnumerator Start () {
		// Set initial Alpha to Black
		faderCanvasGroup.alpha = 1f;
		// Write initial StartingPosition of Player, so it can Load when first Scene is Loaded
		playerSaveData.Save (PlayerMovement.startingPositionKey, initialStartingPositionName);
		// Start First Scene, wait for it to Finish
		yield return StartCoroutine(LoadSceneAndSetActive(startingSceneName) );
		// Once Finished Loading, Start Fade to Black
		StartCoroutine(Fade(0f) );
	}


	// Called by SceneReaction (when Player switches Scenes)
	// Main External Point of Contact and Influence from rest of Project
	public void FadeAndLoadScene (SceneReaction sceneReaction) {
		// If Not Fading-to-Black yet
		if (!isFading) {
			// Start Fading to Black
			StartCoroutine (FadeAndSwitchScene(sceneReaction.sceneName) );
		}
	}


	// Coroutine to Fade and Switch between Scenes
	private IEnumerator FadeAndSwitchScene (string sceneName) {
		// Start fading to Black, and wait for Finish
		yield return StartCoroutine (Fade(1f) );
		// If Event has Subscribers
		if (BeforeSceneUnload != null) {
			// Call Event
			BeforeSceneUnload ();
		}
		// Unload Current Active Scene
		yield return SceneManager.UnloadSceneAsync (SceneManager.GetActiveScene().buildIndex);
		// Start Loading Given Scene, wait for Finish
		yield return StartCoroutine (LoadSceneAndSetActive(sceneName) );
		// If Event has Subscribers
		if (AfterSceneLoad != null) {
			// Call Event
			AfterSceneLoad ();
		}
		// Start Fading-to-Black and wait to Finish
		yield return StartCoroutine (Fade(0f) );
	}


	// Function to Load and Set a Scene as Active
	private IEnumerator LoadSceneAndSetActive (string sceneName) {
		// Allow Given Scene to Load over several Frames and Add it to LoadedScenes (just Persistant Scene at this Point)
		yield return SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive);
		// Find Scene that was Recently Loaded (Last Index of LoadedScenes)
		Scene newlyLoadedScene = SceneManager.GetSceneAt (SceneManager.sceneCount - 1);
		// Set New Loaded Scene as Active Scene (marks it as to-be unloaded next)
		SceneManager.SetActiveScene (newlyLoadedScene);
	}


	// Function to Fade Black-Screen
	private IEnumerator Fade (float finalAlpha) {
		// Set Fading Bool true (So Coroutine cant be Called Again)
		isFading = true;
		// Set CanvasGroup to black RayCasts into Scene (no more Input Accepted)
		faderCanvasGroup.blocksRaycasts = true;

		// Calculate Fade Speed (based on Current Alpha, Final Alpha and Duration of Fade)
		float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;
		// While Alpha hasnt reached FinalAlpha
		while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha) ) {
			// Move Alpha toward FinalAlpha
			faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
			// Wait fir a Frame, then continue
			yield return null;
		}

		// Set Fading Bool false after Fading
		isFading = false;
		// Allow Input 
		faderCanvasGroup.blocksRaycasts = false;
	}
}