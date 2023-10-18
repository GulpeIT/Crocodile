using Godot;

public partial class PlayerScript : CharacterBody2D
{
	#region Floated number
		[Export] public float JUMP_VELOCITY		= 300.0f;
		[Export] public float ACCSELERATION 	= 10.0f;
		[Export] public float MAX_SPEED			= 125.0f;
		[Export] public float FRICTION_FORCE 	= 2.0f;
		[Export] public float MAX_STAMINA 		= 100.0f;

		public float GRAVITY = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	#endregion

	#region Componets
		[Export] public Health_component HEALTH;
		[Export] public Hitbox_component HITBOX;
	#endregion

	#region State machin and animation
		[Export] public StateMachin STATE_MACHIN;
		[Export] private AnimationTree ANIMATION_TREE;
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
		if (TEXT_INFO is Label) TEXT_INFO.Text = 
		$" X : {Mathf.Round(Velocity.X)} | Y : {Mathf.Round(Velocity.Y)} " + 
		$"\n dir : X {Mathf.Round(DIRECTION.X)} Y {Mathf.Round(DIRECTION.Y)} | acs : {ACCSELERATION}"; 
		
		MoveAndSlide();
	}

	public void MoveCharacter(float delta, Vector2 direction, float acceleration, float frictionForce, float maxSpeed){
		FlipBodyControl();

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
	
	public void FlipBodyControl(){
		if (VELOCITY.X != 0) 
			BODY.Scale = new Vector2(VELOCITY.Normalized().X > 0 ? 1 : -1, 1);
	}

	public void JumpCharacter(float jumpVelocity, float needStaminaForJump){
		if (IsOnFloor() && Input.IsActionPressed("uc_up")){
			VELOCITY.Y -= jumpVelocity;
		}
	}

	public void AddGravity(double delta){
		if (!IsOnFloor()){
			VELOCITY.Y += GRAVITY * (float)delta;
		}
		else if (IsOnFloor()){
			VELOCITY.Y = 0;
		}
	}
}


