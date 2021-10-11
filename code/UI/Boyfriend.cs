using System;
using System.Xml.Schema;
using Sandbox;
using System.Collections.Generic;

public enum BoyfriendState { Idle, Up, Down, Left, Right }

public partial class Boyfriend : Entity
{

    public static List<Boyfriend> Characters = new();

    public CharacterBase Character = new CharacterBoyfriend();
    public Base2D Actor;
    public ulong PlayerId;
    public TimeSince AnimationTimer;

    [Net] public new Vector2 Position {get; set;} = new Vector2( Rand.Int(200,1920-200), Rand.Int(200, 1080-200));
    [Net, Predicted] public BoyfriendState State {get; set;} = BoyfriendState.Idle;

    public Boyfriend(ulong _steamid, string _charId)
    {
        Character = FunkinGame.GetCharacterFromId(_charId);

        Actor = new();
        Actor.Sprite = "/sprites/boyfriend/idle_01.png";
        Actor.AddClass( "boyfriend" );
        Actor.Position = Position;
        PlayerId = _steamid;

        Characters.Add(this);

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
    }

    [ClientRpc]
	public static void SetState(ulong _steamid, int _state){
		foreach(Boyfriend _char in Characters){
            if(_char.PlayerId == _steamid){
                if((int)_char.State != _state) _char.AnimationTimer = 0f;
                _char.State = (BoyfriendState)_state;
                return;
            }
        }
        Log.Info("Couldn't find character with SteamID " + _steamid.ToString());
	}
}
