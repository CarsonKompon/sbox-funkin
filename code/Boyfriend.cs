using System;
using System.Xml.Schema;
using Sandbox;
using System.Collections.Generic;

public enum BoyfriendState { Left, Down, Up, Right, Idle }

public partial class Boyfriend : Entity
{

    public static List<Boyfriend> Players = new();

    public bool MustHit = false;
    public CharacterBase Character = new CharacterBoyfriend();
    public Base2D Actor;
    public ulong PlayerId;
    public TimeSince AnimationTimer;
    public TimeSince StateTimer;
    public TimeSince[] Press = new TimeSince[4];

    [Net, Predicted] public int Score {get;set;} = 0;
    [Net, Predicted] public int Combo {get;set;} = 0;
    [Net, Predicted] public int ComboBreaks {get;set;} = 0;
    [Net, Predicted] public new Vector2 Position {get; set;} = Vector2.Zero;
    [Net, Predicted] public BoyfriendState State {get; set;} = BoyfriendState.Idle;

    public Boyfriend(ulong _steamid, CharacterBase _char, Vector2 _position, bool _mustHit)
    {
        PlayerId = _steamid;
        Character = _char;
        Position = _position;
        MustHit = _mustHit;

        Actor = new();
        Actor.Sprite = "/sprites/boyfriend/idle_01.png";
        Actor.AddClass( _char.id );
        Actor.Position = Position;

        Players.Add(this);

    }

    [Event.Tick]
    public void Tick()
    {
        if(!IsClient) return;

        // //Destroy character if they no longer exist
        // var _toDestroy = true;
        // foreach(var _cl in FunkinGame.CurrentPlayers){
        //     if(_cl.SteamId == PlayerId){
        //         _toDestroy = false;
        //         break;
        //     }
        // }
        // if(_toDestroy){
        //     Actor.Delete();
        //     Delete();
        //     return;
        // }

        //Set sprite based on state and animation timer
        var _sprite = Character.GetSpriteFromState(State);
        var _currentFrame = 1;
        var _maxFrames = 2;
        var _frameTime = 0.12f;
        var _animTime = AnimationTimer;

        if(State == BoyfriendState.Idle){
            _maxFrames = Character.idleFrames;
            _frameTime = (60/GameManager.BPM)/_maxFrames;
        }else{
            if(StateTimer > 60/GameManager.BPM/2){
                State = BoyfriendState.Idle;
            }
        }
        while(_animTime >= _frameTime){
            _currentFrame++;
            if(State == BoyfriendState.Idle){
                if(_currentFrame > _maxFrames) _currentFrame -= _maxFrames;
            }else{
                if(_currentFrame > _maxFrames) _currentFrame = _maxFrames;
            }
            _animTime -= _frameTime;
        }
        Actor.Sprite = _sprite + "_" + String.Format("{0:00}", _currentFrame) + ".png";

        //Set the position of the actor
        Actor.Position = Position-Character.origin;

        //Flip the Actor if facing right
        Actor.SetClass("flip", Character.facingRight);
        
        //Set the antialiasing flag
        //Actor.SetClass("pixel", !Character.antialiasing);

        //Set the Score in GUI
        if(MustHit){
            GameManager.gameUI.RightScore.Text = "Score: " + Score.ToString();
        }else{
            GameManager.gameUI.LeftScore.Text = "Score: " + Score.ToString();
        }

        // //Note Hit Detection
        // if(PlayerId == 0 && GameNote.Notes.Count > 0){
        //     var _notes = GameManager.NextNotes(MustHit);
        //     foreach(var _note in _notes){
        //         if(_note != null){
        //             if(_note.MustHit == MustHit){
        //                 Press[_note.Direction] = 0f;
        //             }
        //         }
        //     }
        // }
    }

    [ClientRpc]
	public static void SetState(ulong _steamid, int _state, int _score){
		foreach(Boyfriend _ply in Players){
            if(_ply.PlayerId == _steamid){
                if((int)_ply.State != _state) _ply.AnimationTimer = 0f;
                _ply.State = (BoyfriendState)_state;
                _ply.StateTimer = 0f;
                _ply.Combo++;
                _ply.Score += _score;
                return;
            }
        }
	}

    [ClientRpc]
	public static void BreakCombo(ulong _steamid){
		foreach(Boyfriend _ply in Players){
            if(_ply.PlayerId == _steamid){
                _ply.Combo = 0;
                _ply.Score -= 10;
                _ply.ComboBreaks++;
                return;
            }
        }
	}

    [ClientRpc]
	public static void SetPress(ulong _steamid, int _press, float _val = 0f){
		foreach(Boyfriend _ply in Players){
            if(_ply.PlayerId == _steamid){
                _ply.Press[_press] = _val;
                return;
            }
        }
	}

	public static TimeSince GetPress(ulong _steamid, int _press){
		foreach(Boyfriend _ply in Players){
            if(_ply.PlayerId == _steamid){
                return _ply.Press[_press];
            }
        }
        return -1f;
	}

    public static bool GetMustHit(ulong _steamid){
		foreach(Boyfriend _ply in Players){
            if(_ply.PlayerId == _steamid){
                return _ply.MustHit;
            }
        }
        return false;
	}

    [ClientRpc]
	public static void SetCharacter(ulong _steamid, string _character){
		foreach(Boyfriend _ply in Players){
            if(_ply.PlayerId == _steamid){
                _ply.Character = FunkinGame.GetCharacterFromId(_character);
                return;
            }
        }
	}

    [ClientRpc]
	public static void SetPosition(ulong _steamid, Vector2 _position){
		foreach(Boyfriend _ply in Players){
            if(_ply.PlayerId == _steamid){
                _ply.Position = _position;
                return;
            }
        }
	}
}
