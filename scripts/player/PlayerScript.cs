using Godot;

public partial class PlayerScript : CharacterBody2D
{
	#region Floated number
		[Export] public float JUMP_VELOCITY		= 300.0f;
		[Export] public float ACCSELERATION 	= 10.0f;
		[Export] public float MAX_SPEED			= 125.0f;
		[Export] public float FRICTION_FORCE 	= 3.0f;
		[Export] public float MAX_STAMINA 		= 100.0f;

		private float GRAVITY = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	#endregion

	#region Componets
		[Export] public Health_component HEALTH;
		[Export] public Hitbox_component HITBOX;
	#endregion

	#region State machin and animation
		[Export] public StateMachin STATE_MACHIN;
		[Export] public AnimationTree ANIMATION_TREE;
		public AnimationNodeStateMachinePlayback ANIMATION_STATE_MACHIN; // This needs to be send in States
	#endregion

	#region Nodes
		[Export] private Label TEXT_INFO;
		public Node2D BODY;
	#endregion 

	#region Vectors
		public Vector2 VELOCITY;
		public Vector2 DIRECTION;
	#endregion

	
	public override void _Ready(){
	ANIMATION_STATE_MACHIN = (AnimationNodeStateMachinePlayback)ANIMATION_TREE.Get("parameters/playback");

	BODY = GetNode<Node2D>("body_controler");
	STATE_MACHIN.StartState(new Player_idle(this));
	}

	public override void _PhysicsProcess(double delta){
		DIRECTION = Input.GetVector("uc_left", "uc_right", "uc_up", "uc_down");
		Velocity = VELOCITY;
		
		// Debug option
		if (TEXT_INFO is Label) {
			TEXT_INFO.Text = $"vel : X {Mathf.Round(Velocity.X)} | Y {Mathf.Round(Velocity.Y)}" + 
			$"\ndir : X {Mathf.Round(DIRECTION.X)} | Y {Mathf.Round(DIRECTION.Y)} \nacs : {ACCSELERATION}";
		}

		MoveAndSlide();
	}
	
	/// <summary>
	/// The main method of character movement in the game
	/// </summary>
	/// <param name="delta"></param>
	/// <param name="direction">The direction of the character's movement</param>
	/// <param name="acceleration">Character Acceleration</param>
	/// <param name="frictionForce">The friction force. Affects how effectively the character will slow down</param>
	/// <param name="maxSpeed">Maximum character speed</param>
	public void MoveCharacter(float delta, Vector2 direction, float acceleration, float frictionForce, float maxSpeed){
		FlipBodyControl();

		if (IsOnWall()){
			VELOCITY.X = 0;
			GD.Print("True, is on wall");
		}

		if (Mathf.Abs(VELOCITY.X) > maxSpeed){
			VELOCITY.X = Mathf.MoveToward(VELOCITY.X, maxSpeed, frictionForce * 0.5f);
		}

		else if(direction.X > 0){
			VELOCITY.X = Mathf.Min(VELOCITY.X + acceleration, maxSpeed);
		}
		else if (direction.X < 0){
			VELOCITY.X = Mathf.Max(VELOCITY.X - acceleration, -maxSpeed);
		}

		else VELOCITY.X = Mathf.MoveToward(VELOCITY.X, 0, frictionForce);
	}

	/// <summary>
	/// The method is designed to instantly accelerate the character
	/// </summary>
	/// <param name="direction">The direction of the character's movement</param>
	/// <param name="pushForce">The force with which the character should be "pushed"</param>
	public void PushCharacter(Vector2 direction, float pushForce){
		VELOCITY.X += pushForce * direction.X;
	}
	/// <summary>
	/// The method controls the character's rotation by changing the "Scale" parameter of the "Body" node
	/// </summary>
	public void FlipBodyControl(){
		if (VELOCITY.X != 0){
			BODY.Scale = new Vector2(VELOCITY.Normalized().X > 0 ? 1 : -1, 1);
		}
	}

	/// <summary>
	/// Simple jump method
	/// </summary>
	/// <param name="jumpVelocity">The value of how high the character will jump</param>
	public void JumpCharacter(float jumpVelocity){
		if (IsOnFloor() && Input.IsActionPressed("uc_up")){
			VELOCITY.Y -= jumpVelocity;
		}
	}

	/// <summary>
	///	This method adds gravity to the character
	/// </summary>
	/// <param name="delta"></param>
	public void AddGravity(double delta){
		if (!IsOnFloor()){
			VELOCITY.Y += GRAVITY * (float)delta;
		}
		else if (IsOnFloor()){
			VELOCITY.Y = 0;
		}
	}
}


