using Godot;

public class Attack{
	private float Impulse;
	private float Damage;


	public Attack(float damage){
		Damage = damage;
	}
	public Attack(float damage, float impulse){
		Impulse = impulse;
		Damage = damage;
	}


	public float GetNumberDamage(){
		return Damage;
	}

	public float GetNumberImpulse(){
		return Impulse;
	}

}