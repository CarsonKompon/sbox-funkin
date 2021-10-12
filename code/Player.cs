using System.IO;
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

    [Net] public bool inputUpPress {get; set;} = false;
    [Net] public bool inputUpDown {get; set;} = false;
    [Net] public bool inputDownPress {get; set;} = false;
    [Net] public bool inputDownDown {get; set;} = false;
    [Net] public bool inputLeftPress {get; set;} = false;
    [Net] public bool inputLeftDown {get; set;} = false;
    [Net] public bool inputRightPress {get; set;} = false;
    [Net] public bool inputRightDown {get; set;} = false;

    [Net, Predicted] public int score {get; set;} = 0;

    [Net] public bool isActive {get; set;} = false;

	public override void Respawn()
	{
		SetModel( "models/citizen/citizen.vmdl" );
		Controller = new FunkinController();
		Animator = new StandardPlayerAnimator();
		Camera = new ThirdPersonCamera();

        //Sound.FromScreen("roses_inst");

		base.Respawn();
	}

	public override void Simulate( Client cl )
	{
        
        var _steamid = cl.SteamId;

        //Boyfriend.SetState(_steamid, (int)BoyfriendState.Up);

        if(inputUpPress){
            Boyfriend.SetPress(_steamid, (int)BoyfriendState.Up);
        }else if(inputDownPress){
            Boyfriend.SetPress(_steamid, (int)BoyfriendState.Down);
        }else if(inputLeftPress){
            Boyfriend.SetPress(_steamid, (int)BoyfriendState.Left);
        }else if(inputRightPress){
            Boyfriend.SetPress(_steamid, (int)BoyfriendState.Right);
        }

        Receptor.SetPress(_steamid, 0, inputLeftDown);
        Receptor.SetPress(_steamid, 1, inputDownDown);
        Receptor.SetPress(_steamid, 2, inputUpDown);
        Receptor.SetPress(_steamid, 3, inputRightDown);

        //Note Hit Detection
        if(GameNote.Notes.Count > 0){
            var _notes = GameManager.NextNotes(Boyfriend.GetMustHit(_steamid));
            foreach(var _note in _notes){
                if(_note != null){
                    if(Boyfriend.GetMustHit(_steamid) == _note.MustHit){
                        if(GameManager.Current.SongTime >= _note.Time-NoteTimings.Shit && GameManager.Current.SongTime <= _note.Time+NoteTimings.Shit){
                            if(Boyfriend.GetPress(_steamid, _note.Direction) <= Time.Delta){
                                var _timing = NoteTimings.Shit;
                                if(GameManager.Current.SongTime >= _note.Time-NoteTimings.Sick && GameManager.Current.SongTime <= _note.Time+NoteTimings.Sick){
                                    _timing = NoteTimings.Sick;
                                }else if(GameManager.Current.SongTime >= _note.Time-NoteTimings.Good && GameManager.Current.SongTime <= _note.Time+NoteTimings.Good){
                                    _timing = NoteTimings.Good;
                                }else if(GameManager.Current.SongTime >= _note.Time-NoteTimings.Bad && GameManager.Current.SongTime <= _note.Time+NoteTimings.Bad){
                                    _timing = NoteTimings.Bad;
                                }
                                var _score = 50;
                                var _scoreNotif = "/sprites/ui/shit.png";
                                if(_timing == NoteTimings.Bad){
                                    _score = 100;
                                    _scoreNotif = "/sprites/ui/bad.png";
                                }
                                if(_timing == NoteTimings.Good){
                                    _score = 200;
                                    _scoreNotif = "/sprites/ui/good.png";
                                }
                                if(_timing == NoteTimings.Sick){
                                    _score = 350;
                                    _scoreNotif = "/sprites/ui/sick.png";
                                }

                                //Spawn timing notifier
                                var _notif = new ScoreNotifier(_scoreNotif);
                                _notif.AddClass("notifier");
                                GameManager.Current.AddChild(_notif);
                                
                                Boyfriend.SetState(_steamid, _note.Direction, _score);
                                Boyfriend.SetPress(_steamid, _note.Direction, 10f);
                                _note.Actor.Delete();
                                _note.Delete();
                                GameManager.Notes.Remove(_note);
                            }else{
                                // for(var i=0;i<4;i++){
                                //     if(i != _note.Direction){
                                //         if(Boyfriend.GetPress(_steamid, i) <= Time.Delta){
                                //             Boyfriend.SetPress(_steamid, i, 10f);
                                //             Boyfriend.BreakCombo(_steamid);
                                //             //_note.Actor.Delete();
                                //             //_note.Delete();
                                //             GameManager.Notes.Remove(_note);
                                //             break;
                                //         }
                                //     }
                                // }
                            }
                        }
                    }
                }
            }
        }
        

        if(Input.Pressed(InputButton.Reload)){
            if( IsServer ){
                Boyfriend.SetCharacter(_steamid, Rand.FromList(FunkinGame.Characters).id);
                Boyfriend.SetPosition(_steamid, new Vector2( Rand.Int(200,1920-200), Rand.Int(200, 1080-200)));
            }
        }

        inputUpPress = false;
        inputDownPress = false;
        inputLeftPress = false;
        inputRightPress = false;

	}
}