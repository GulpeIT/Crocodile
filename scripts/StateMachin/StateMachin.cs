using Godot;

public partial class StateMachin : Node
{
	private State CURRENT_STATE;

	public void StartState(State newState){
		CURRENT_STATE = newState;
		AddChild(CURRENT_STATE);
	}

	public void ChangeState(State newState){		
		RemoveChild(CURRENT_STATE);
		CURRENT_STATE = newState;
		AddChild(CURRENT_STATE);
	}
}
