/// <summary>
/// Condition reaction.
/// 
/// </summary>

public class ConditionReaction : Reaction {

	// Reference to the Condition to change
	public Condition condition;
	// Reference to State the Condition will be changed to
	public bool satisfied;


	// Overrides Inhereted Function
	protected override void ImmediateReaction () {
		// Set the Conditions Satisfied state to given Satisfied State
		condition.Satisfied = satisfied;
	}
}