using Godot;

public partial class Main_world : Node2D
{
	public override void _Ready(){
		GD.Print("start world");
	}

	public override void _Process(double delta){
		if (Input.IsActionJustPressed("um_canceled")){
			GetTree().Quit();
		}
	}
	
}
