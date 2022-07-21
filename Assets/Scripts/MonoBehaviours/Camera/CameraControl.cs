using System.Collections;
using UnityEngine;

/// <summary>
/// Camera control.
/// 
/// </summary>

public class CameraControl : MonoBehaviour {

	// Should Camera be Moved by this Script
	public bool moveCamera = true;
	// Smoothing of Camera during Slerp
	public float smoothing = 7f;
	// Offset from PlayersPosition
	public Vector3 offset = new Vector3 (0f, 1.5f, 0f);
	// Reference to the Players Position
	public Transform playerPosition;


	// Use this for initialization
	private IEnumerator Start () {
		// If Camera Shouldnt Move
		if (!moveCamera) {
			// Do Nothing
			yield break;
		}
		// Wait Single Frame (ensures all other Starts are Called first)
		yield return null;
		// Set Rotation of Camera to look at the Players Position minus the Offset
		transform.rotation = Quaternion.LookRotation (playerPosition.position - transform.position + offset);
	}
	
	// LateUpdate is called After all Position Updates
	private void LateUpdate () {
		// If the Camera Shouldnt move
		if (!moveCamera) {
			// Do Nothing
			return;
		}
		// Find new Rotation aimed at PlayerPosition minus Offset
		Quaternion newRotation = Quaternion.LookRotation (playerPosition.position - transform.position + offset);
		// Spherically Interpolate between current rotation and NewRotation
		transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, smoothing * Time.deltaTime);
	}
}