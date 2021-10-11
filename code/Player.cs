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
    [Net, Predicted] public bool inputUpDown {get; set;} = false;
    [Net, Predicted] public bool inputDownPress {get; set;} = false;
    [Net, Predicted] public bool inputDownDown {get; set;} = false;
    [Net, Predicted] public bool inputLeftPress {get; set;} = false;
    [Net, Predicted] public bool inputLeftDown {get; set;} = false;
    [Net, Predicted] public bool inputRightPress {get; set;} = false;
    [Net, Predicted] public bool inputRightDown {get; set;} = false;

    [Net] public bool isActive {get; set;} = false;

	public override void Respawn()
	{
		SetModel( "models/citizen/citizen.vmdl" );
		Controller = new FunkinController();
		Animator = new StandardPlayerAnimator();
		Camera = new ThirdPersonCamera();

        Sound.FromScreen("roses_inst");

		base.Respawn();
	}

	public override void Simulate( Client cl )
	{
        
        var _steamid = cl.SteamId;

        if(isActive){

            if(inputUpDown){
                Boyfriend.SetState(_steamid, (int)BoyfriendState.Up);
            }else if(inputDownDown){
                Boyfriend.SetState(_steamid, (int)BoyfriendState.Down);
            }else if(inputLeftDown){
                Boyfriend.SetState(_steamid, (int)BoyfriendState.Left);
            }else if(inputRightDown){
                Boyfriend.SetState(_steamid, (int)BoyfriendState.Right);
            }else{
                Boyfriend.SetState(_steamid, (int)BoyfriendState.Idle);
            }

            if(Input.Pressed(InputButton.Reload)){
                if( IsServer ){
                    Boyfriend.SetCharacter(_steamid, Rand.FromList(FunkinGame.Characters).id);
                    Boyfriend.SetPosition(_steamid, new Vector2( Rand.Int(200,1920-200), Rand.Int(200, 1080-200)));
                }
            }
        }

        inputUpPress = false;
        inputDownPress = false;
        inputLeftPress = false;
        inputRightPress = false;

	}
}