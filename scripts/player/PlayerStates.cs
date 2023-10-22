using Godot;


//--------------------STATE_IDLE--------------------

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

		if (PLAYER.DIRECTION.X != 0){
			PLAYER.STATE_MACHIN.ChangeState(new Player_run(PLAYER));
		}
	}
}

//--------------------STATE_RUN--------------------

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
		if (PLAYER.DIRECTION.Y < 0 && PLAYER.IsOnFloor()){
			PLAYER.STATE_MACHIN.ChangeState(new Player_jump(PLAYER));
		}

		else if (PLAYER.VELOCITY.X == 0 && PLAYER.DIRECTION.X == 0){
			PLAYER.STATE_MACHIN.ChangeState(new Player_idle(PLAYER));
		}
		#endregion
	}
}

//--------------------STATE_JUMP--------------------

public partial class Player_jump : State {

	private PlayerScript PLAYER;
	public Player_jump(PlayerScript player){
		PLAYER = player;    
	}

	public override void _EnterTree(){
		base._EnterTree();
		GD.Print("Jump entered;");


		PLAYER.JumpCharacter(PLAYER.JUMP_VELOCITY, 10f);
		// PLAYER.ANIMATION_STATE_MACHIN.Travel("Jump");
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
		if (PLAYER.IsOnFloor()){
			PLAYER.STATE_MACHIN.ChangeState(new Player_run(PLAYER));
		}
		#endregion
    }
}

//--------------------STATE_FALL--------------------

// public partial class Player_fall : State {

// 	private PlayerScript PLAYER;
// 	public Player_fall(PlayerScript player){
// 		PLAYER = player;    
// 	}

// 	public override void _EnterTree(){
// 		base._EnterTree();
// 		GD.Print("Fall entered;");

// 		PLAYER.ANIMATION_STATE_MACHIN.Travel("Fall");
// 	}

// 	public override void _ExitTree(){
// 		base._ExitTree();
// 		GD.Print("Fall exited;");
// 	}

//     public override void _PhysicsProcess(double delta)
//     {
//         base._PhysicsProcess(delta);

// 		PLAYER.AddGravity(delta);
// 		PLAYER.MoveCharacter((float)delta, PLAYER.DIRECTION, PLAYER.ACCSELERATION * 0.05f, PLAYER.FRICTION_FORCE * 0.05f, PLAYER.MAX_SPEED * 1.65f);
		
// 		#region New state
// 		if (PLAYER.DIRECTION.Y < 0){
// 			PLAYER.STATE_MACHIN.ChangeState(new Player_jump(PLAYER));
// 		}

// 		else if (PLAYER.IsOnFloor() && PLAYER.DIRECTION.X == 0){
// 			PLAYER.STATE_MACHIN.ChangeState(new Player_idle(PLAYER));
// 		}
// 		else if (PLAYER.IsOnFloor() && PLAYER.DIRECTION.X != 0){
// 			PLAYER.STATE_MACHIN.ChangeState(new Player_run(PLAYER));
// 		}
// 		#endregion
//     }
// }

//--------------------STATE_CRAWL--------------------

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
    }
}