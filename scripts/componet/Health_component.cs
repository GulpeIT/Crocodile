using Godot;

public partial class Health_component : Node2D
{
	[Export] float MaxHealth;
	private float Health;

	public override void _Ready(){
		Health = MaxHealth;
	}

	public void TakeDamage(Attack attack){
		Health -= attack.GetNumberDamage();
	}
	
	public void HealSelf(float num){
		Health += num;
	}
}
