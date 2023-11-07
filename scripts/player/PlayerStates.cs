using System;
using Godot;


//STATE_IDLE
/// <summary>
/// One of the character's states is "Idle"
/// </summary>
public partial class Player_idle : State{

	private PlayerScript PLAYER;

	public Player_idle(PlayerScript player){
		PLAYER = player;    
	}

	public override void _EnterTree(){
		base._EnterTree();
		GD.Print("Idle entred;");

		PLAYER.ANIMATION_STATE_MACHIN.Travel("Idle");
	}

	public override void _ExitTree(){
		base._ExitTree();
		GD.Print("Idle exited;");
	}

	public override void _PhysicsProcess(double delta){
		base._PhysicsProcess(delta);
		PLAYER.AddGravity(delta);

		PLAYER.MoveCharacter((float)delta, PLAYER.DIRECTION, 0, PLAYER.FRICTION_FORCE, PLAYER.MAX_SPEED);

		#region New state
		if (PLAYER.IsOnFloor()){
			if (PLAYER.DIRECTION.X != 0){
				if (PLAYER.DIRECTION.Y > 0)	PLAYER.STATE_MACHIN.ChangeState(new Player_crawl(PLAYER));
				else PLAYER.STATE_MACHIN.ChangeState(new Player_run(PLAYER));
			}
		}
		else if (!PLAYER.IsOnFloor()){
			PLAYER.STATE_MACHIN.ChangeState(new Player_fall(PLAYER));
		}
		#endregion
	}
}

//STATE_RUN
/// <summary>
/// One of the character's states is "Run"
/// </summary>
public partial class Player_run : State {

	private PlayerScript PLAYER;
	public Player_run(PlayerScript player){
		PLAYER = player;    
	}

	public override void _EnterTree(){
		base._EnterTree();
		GD.Print("Run entered;");

		PLAYER.ANIMATION_STATE_MACHIN.Travel("Run");
	}

	public override void _ExitTree(){
		base._ExitTree();
		GD.Print("Run exited;");
	}
	
	public override void _PhysicsProcess(double delta){
		base._PhysicsProcess(delta);

		PLAYER.AddGravity(delta);
		PLAYER.MoveCharacter((float)delta, PLAYER.DIRECTION, PLAYER.ACCSELERATION, PLAYER.FRICTION_FORCE, PLAYER.MAX_SPEED);
		
		#region New state
		if (PLAYER.IsOnFloor()){
			if (PLAYER.DIRECTION.Y < 0) PLAYER.STATE_MACHIN.ChangeState(new Player_jump(PLAYER));
			if (PLAYER.DIRECTION.X != 0  && PLAYER.DIRECTION.Y > 0)	PLAYER.STATE_MACHIN.ChangeState(new Player_slide(PLAYER));

			else if (PLAYER.VELOCITY.X == 0 && PLAYER.DIRECTION.X == 0)	PLAYER.STATE_MACHIN.ChangeState(new Player_idle(PLAYER));
		}
		else if (!PLAYER.IsOnFloor()) PLAYER.STATE_MACHIN.ChangeState(new Player_fall(PLAYER));
		#endregion
	}
}

//STATE_JUMP
/// <summary>
/// One of the character's states is "Jump"
/// </summary>
public partial class Player_jump : State {

	private float JUMP_ACC_FORCE = 0.0f;

	private PlayerScript PLAYER;
	public Player_jump(PlayerScript player){
		PLAYER = player;    
	}
	public Player_jump(PlayerScript player, float jumpAccForce){
		PLAYER = player;
		JUMP_ACC_FORCE = jumpAccForce;
	}

	public override void _EnterTree(){
		base._EnterTree();
		GD.Print("Jump entered;");

		PLAYER.JumpCharacter(PLAYER.JUMP_VELOCITY);
		PLAYER.PushCharacter(PLAYER.DIRECTION, JUMP_ACC_FORCE);

		PLAYER.ANIMATION_TREE.Set("parameters/TypeJump/Blend2/blend_amount", new Random().Next(2));
		PLAYER.ANIMATION_STATE_MACHIN.Travel("TypeJump");
	}

	public override void _ExitTree(){
		base._ExitTree();
		GD.Print("Jump exited;");
	}

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		PLAYER.AddGravity(delta);
		
		PLAYER.MoveCharacter((float)delta, PLAYER.DIRECTION, PLAYER.ACCSELERATION * 0.15f, PLAYER.FRICTION_FORCE * 0.05f, PLAYER.MAX_SPEED * 1.65f);
	 
		#region New state
		if (PLAYER.VELOCITY.Y > 0 && !PLAYER.IsOnFloor()){
			PLAYER.STATE_MACHIN.ChangeState(new Player_fall(PLAYER));
		}
		#endregion
    }
}

//STATE_FALL
/// <summary>
/// One of the character's states is "Falling"
/// </summary>
public partial class Player_fall : State {

	private PlayerScript PLAYER;
	public Player_fall(PlayerScript player){
		PLAYER = player;    
	}

	public override void _EnterTree(){
		base._EnterTree();
		GD.Print("Fall entered;");

		PLAYER.ANIMATION_STATE_MACHIN.Travel("Fall");
	}

	public override void _ExitTree(){
		base._ExitTree();
		GD.Print("Fall exited;");
	}

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		PLAYER.AddGravity(delta);
		PLAYER.MoveCharacter((float)delta, PLAYER.DIRECTION, PLAYER.ACCSELERATION * 0.05f, PLAYER.FRICTION_FORCE * 0.05f, PLAYER.MAX_SPEED * 1.65f);
		
		#region New state
		if (PLAYER.IsOnFloor()){
			if (PLAYER.DIRECTION.X == 0) PLAYER.STATE_MACHIN.ChangeState(new Player_idle(PLAYER));
			if (PLAYER.DIRECTION.X != 0) PLAYER.STATE_MACHIN.ChangeState(new Player_run(PLAYER));
		}

		#endregion
    }
}

//STATE_CRAWL
/// <summary>
/// One of the character's states is "Crawl"
/// </summary>
public partial class Player_crawl : State {

	private PlayerScript PLAYER;
	public Player_crawl(PlayerScript player){
		PLAYER = player;    
	}

	public override void _EnterTree(){
		base._EnterTree();
		GD.Print("Crawl entered;");
	}

	public override void _ExitTree(){
		base._ExitTree();
		GD.Print("Crawl exited;");
	}

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		PLAYER.AddGravity(delta);
		PLAYER.MoveCharacter((float)delta, PLAYER.DIRECTION, PLAYER.ACCSELERATION, PLAYER.FRICTION_FORCE, PLAYER.MAX_SPEED * 0.5f);

		#region New state
		if (PLAYER.IsOnFloor()){
			if(PLAYER.DIRECTION.X != 0 && PLAYER.DIRECTION.Y <= 0){
				PLAYER.STATE_MACHIN.ChangeState(new Player_run(PLAYER));
			}
			else if(PLAYER.DIRECTION.X == 0 && PLAYER.VELOCITY.X == 0){
				PLAYER.STATE_MACHIN.ChangeState(new Player_idle(PLAYER));
			}
		}
		else if (!PLAYER.IsOnFloor()){
			PLAYER.STATE_MACHIN.ChangeState(new Player_fall(PLAYER));
		}
		#endregion
    }
}

//STATE_SLIDE
/// <summary>
/// One of the character's states is "Slide"
/// </summary>
public partial class Player_slide : State {

	private PlayerScript PLAYER;
	public Player_slide(PlayerScript player){
		PLAYER = player;    
	}

	public override void _EnterTree(){
		base._EnterTree();
		GD.Print("Slide entered;");

		PLAYER.PushCharacter(PLAYER.DIRECTION, 100.0f);

	}

	public override void _ExitTree(){
		base._ExitTree();
		GD.Print("Slide exited;");
	}

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		PLAYER.AddGravity(delta);
		PLAYER.MoveCharacter((float)delta, PLAYER.DIRECTION, 0f, PLAYER.FRICTION_FORCE * 0.60f, PLAYER.MAX_SPEED * 0.5f);

		#region New state
		if(PLAYER.IsOnFloor()){
			if (Mathf.Abs(PLAYER.VELOCITY.X) <= PLAYER.MAX_SPEED * 0.5f){
				if (PLAYER.DIRECTION.Y > 0) PLAYER.STATE_MACHIN.ChangeState(new Player_crawl(PLAYER));
				
				else PLAYER.STATE_MACHIN.ChangeState(new Player_run(PLAYER));
			}
			else if(Input.IsActionJustPressed("uc_up") && Mathf.Abs(PLAYER.VELOCITY.X) < 320.0f){
				PLAYER.STATE_MACHIN.ChangeState(new Player_jump(PLAYER, 100.0f));
			}
		}
		else if (!PLAYER.IsOnFloor()) PLAYER.STATE_MACHIN.ChangeState(new Player_fall(PLAYER));
		#endregion
    }
}
