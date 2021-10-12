using System;
using System.Xml.Schema;
using Sandbox;
using System.Collections.Generic;

public partial class Receptor : Entity
{

    public static List<Receptor> Receptors = new();

    public bool MustHit = false;
    public int Direction = 0;
    public Base2D Actor;
    public ulong PlayerId;
    public TimeSince AnimationTimer;

    public Receptor(ulong _steamid, int _direction, Vector2 _position, bool _mustHit)
    {
        Direction = _direction;
        PlayerId = _steamid;
        MustHit = _mustHit;

        Actor = new();
        Actor.Sprite = "/sprites/arrows/left_unpressed.png";
        Actor.AddClass("receptor");
        Actor.Position = _position;

        Receptors.Add(this);

    }

    [Event.Tick]
    public void Tick()
    {
        if(!IsClient) return;

        string _arrow = "";
        if(Direction == 0) _arrow = "left";
        else if(Direction == 1) _arrow = "down";
        else if(Direction == 2) _arrow = "up";
        else if(Direction == 3) _arrow = "right";

        Actor.Sprite = "/sprites/arrows/" + _arrow + "_unpressed.png";


        // //Destroy receptor if they no longer exist
        // var _toDestroy = true;
        // foreach(var _cl in FunkinGame.CurrentPlayers){
        //     if(_cl.SteamId == PlayerId){
        //         _toDestroy = false;
        //         break;
        //     }
        // }
        // if(_toDestroy){
        //     foreach(var _arrow in Arrows){
        //         _arrow.Delete();
        //     }
        //     Delete();
        //     return;
        // }

        // //Set sprite based on state and animation timer
        // var _sprite = Character.GetSpriteFromState(State);
        // var _currentFrame = 1;
        // var _maxFrames = 2;
        // var _frameTime = 0.12f;
        // var _animTime = AnimationTimer;
        // if(State == BoyfriendState.Idle) _maxFrames = Character.idleFrames;
        // while(_animTime >= _frameTime){
        //     _currentFrame++;
        //     if(_currentFrame > _maxFrames) _currentFrame -= _maxFrames;
        //     _animTime -= _frameTime;
        // }
        // Actor.Sprite = _sprite + "_" + String.Format("{0:00}", _currentFrame) + ".png";

        // //Set the position of the actor
        // Actor.Position = Position;

        // //Flip the Actor if facing right
        // Actor.SetClass("flip", Character.facingRight);

        // //Set the antialiasing flag
        // //Actor.SetClass("pixel", !Character.antialiasing);
    }

    // [ClientRpc]
	// public static void SetState(ulong _steamid, int _state){
	// 	foreach(Boyfriend _ply in Players){
    //         if(_ply.PlayerId == _steamid){
    //             if((int)_ply.State != _state) _ply.AnimationTimer = 0f;
    //             _ply.State = (BoyfriendState)_state;
    //             return;
    //         }
    //     }
	// }

    // [ClientRpc]
	// public static void SetCharacter(ulong _steamid, string _character){
	// 	foreach(Boyfriend _ply in Players){
    //         if(_ply.PlayerId == _steamid){
    //             _ply.Character = FunkinGame.GetCharacterFromId(_character);
    //             return;
    //         }
    //     }
	// }

    // [ClientRpc]
	// public static void SetPosition(ulong _steamid, Vector2 _position){
	// 	foreach(Boyfriend _ply in Players){
    //         if(_ply.PlayerId == _steamid){
    //             _ply.Position = _position;
    //             return;
    //         }
    //     }
	// }
}
