using Sandbox;


public class FunkinController : WalkController
{
    public FunkinController(){
        SprintSpeed = 0f;
        WalkSpeed = 0f;
        DefaultSpeed = 0f;
        Acceleration = 0f;
        AirAcceleration = 0f;
        Gravity = 0f;
        AirControl = 0f;
    }
}

partial class FunkinPlayer : Player
{

    [Net, Predicted] public bool inputUpPress {get; set;} = false;
    [Net, Predicted] public bool inputUpRelease {get; set;} = false;
    [Net, Predicted] public bool inputDownPress {get; set;} = false;
    [Net, Predicted] public bool inputDownRelease {get; set;} = false;
    [Net, Predicted] public bool inputLeftPress {get; set;} = false;
    [Net, Predicted] public bool inputLeftRelease {get; set;} = false;
    [Net, Predicted] public bool inputRightPress {get; set;} = false;
    [Net, Predicted] public bool inputRightRelease {get; set;} = false;

	public override void Respawn()
	{
		SetModel( "models/citizen/citizen.vmdl" );
		Controller = new FunkinController();
		Animator = new StandardPlayerAnimator();
		Camera = new ThirdPersonCamera();

		
		base.Respawn();
	}

	public override void Simulate( Client cl )
	{
        
        var _steamid = cl.SteamId;

        if(inputUpPress){
            Boyfriend.SetImage(_steamid, "/sprites/boyfriend/up_01.png");
        }
        if(inputDownPress){
            Boyfriend.SetImage(_steamid, "/sprites/boyfriend/down_01.png");
        }
        if(inputLeftPress){
            Boyfriend.SetImage(_steamid, "/sprites/boyfriend/left_01.png");
        }
        if(inputRightPress){
            Boyfriend.SetImage(_steamid, "/sprites/boyfriend/right_01.png");
        }

        inputUpPress = false;
        inputUpRelease = false;
        inputDownPress = false;
        inputDownRelease = false;
        inputLeftPress = false;
        inputLeftRelease = false;
        inputRightPress = false;
        inputRightRelease = false;
	}
}