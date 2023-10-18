using Godot;
using System;
using System.Drawing;
using System.Dynamic;

public partial class Cursor_script : Node2D
{
	
	private Sprite2D CursorSprite;
	[Export] private Vector2 CursorSize = new Vector2(1, 1);
	

	public override void _Ready(){
		DisplayServer.MouseSetMode(DisplayServer.MouseMode.Hidden);
		
		CursorSprite = GetNode<Sprite2D>("cursor_sprite");
		CursorSprite.Visible = true;
		Scale = CursorSize;
	}


	public override void _PhysicsProcess(double delta){	
		Position = GetGlobalMousePosition();
		mouseLeftButtonClick();
	}

	
	private void mouseLeftButtonClick(){
		if (Input.IsActionPressed("uc_mouse_LB")){
			CursorSprite.Frame = 1;
		}
		else if (Input.IsActionJustReleased("uc_mouse_LB")){
			CursorSprite.Frame = 0;
		}
	}

}
