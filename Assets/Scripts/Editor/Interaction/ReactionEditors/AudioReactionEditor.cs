using UnityEditor;

/// <summary>
/// Audio reaction editor.
/// 
/// </summary>

[CustomEditor ( typeof (AudioReaction) )]
public class AudioReactionEditor : ReactionEditor {
	// 
	protected override string GetFoldoutLabel () {
		// 
		return "Audio Reaction";
	}
}