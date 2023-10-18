using Godot;


public partial class Hitbox_component : Area2D
{
	[Export] Health_component HP;
	
	public void ApplyDamage(Attack attack){
		HP.TakeDamage(attack);
	}
	
}
