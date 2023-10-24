using Godot;

public abstract partial class State : Node{

    public override void _EnterTree(){
        base._EnterTree();
    }

    public override void _ExitTree(){
        base._ExitTree();
    }

    public override void _PhysicsProcess(double delta){
        base._PhysicsProcess(delta);
    }
}