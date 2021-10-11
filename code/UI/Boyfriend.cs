using System;
using System.Xml.Schema;
using Sandbox;
using System.Collections.Generic;

public enum BoyfriendState { Idle, Up, Down, Left, Right }

public partial class Boyfriend : Entity
{

    public static List<Boyfriend> Players = new();

    public CharacterBase Character = new CharacterBoyfriend();
    public Base2D Actor;
    public ulong PlayerId;
    public TimeSince AnimationTimer;

    [Net, Predicted] public new Vector2 Position {get; set;} = new Vector2( Rand.Int(200,1920-200), Rand.Int(200, 1080-200));
    [Net, Predicted] public BoyfriendState State {get; set;} = BoyfriendState.Idle;

    public Boyfriend(ulong _steamid, CharacterBase _char)
    {
        Character = _char;

        Actor = new();
        Actor.Sprite = "/sprites/boyfriend/idle_01.png";
        Actor.AddClass( "boyfriend" );
        Actor.Position = Position;

        PlayerId = _steamid;

        Players.Add(this);

    }

    [Event.Tick]
    public void Tick()
    {
        if(!IsClient) return;

        //Destroy character if they no longer exist
        var _toDestroy = true;
        foreach(var _cl in FunkinGame.CurrentPlayers){
            if(_cl.SteamId == PlayerId){
                _toDestroy = false;
                break;
            }
        }
        if(_toDestroy){
            Actor.Delete();
            Delete();
            return;
        }

        //Set sprite based on state and animation timer
        var _sprite = Character.GetSpriteFromState(State);
        var _currentFrame = 1;
        var _maxFrames = 2;
        var _frameTime = 0.12f;
        var _animTime = AnimationTimer;
        if(State == BoyfriendState.Idle) _maxFrames = Character.idleFrames;
        while(_animTime >= _frameTime){
            _currentFrame++;
            if(_currentFrame > _maxFrames) _currentFrame -= _maxFrames;
            _animTime -= _frameTime;
        }
        Actor.Sprite = _sprite + "_" + String.Format("{0:00}", _currentFrame) + ".png";

        //Set the position of the actor
        Actor.Position = Position;

        //Set the antialiasing flag
        Actor.SetClass("pixel", !Character.antialiasing);
    }

    [ClientRpc]
	public static void SetState(ulong _steamid, int _state){
		foreach(Boyfriend _ply in Players){
            if(_ply.PlayerId == _steamid){
                if((int)_ply.State != _state) _ply.AnimationTimer = 0f;
                _ply.State = (BoyfriendState)_state;
                return;
            }
        }
        Log.Info("Couldn't find character with SteamID " + _steamid.ToString());
	}

    [ClientRpc]
	public static void SetCharacter(ulong _steamid, string _character){
		foreach(Boyfriend _ply in Players){
            if(_ply.PlayerId == _steamid){
                _ply.Character = FunkinGame.GetCharacterFromId(_character);
                return;
            }
        }
        Log.Info("Couldn't find character with SteamID " + _steamid.ToString());
	}

    [ClientRpc]
	public static void SetPosition(ulong _steamid, Vector2 _position){
		foreach(Boyfriend _ply in Players){
            if(_ply.PlayerId == _steamid){
                _ply.Position = _position;
                return;
            }
        }
        Log.Info("Couldn't find character with SteamID " + _steamid.ToString());
	}
}
