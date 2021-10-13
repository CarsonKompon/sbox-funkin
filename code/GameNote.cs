using Sandbox;
using System.Collections.Generic;

public static class NoteTimings {
    public const float Shit = 0.162f;
    public const float Bad = 0.135f;
    public const float Good = 0.092f;
    public const float Sick = 0.033f;
}

public partial class GameNote : Entity
{
    public static List<GameNote> Notes = new();

    public float Time;
    public int Direction;
    public float Length;
    public bool MustHit;

    public ulong PlayerId = 0;
    public bool Missed = false;
    public bool IsBot = false;
    public bool ActorPassed = false;
    public bool HasActor = false;
    public Base2D Actor;

    [Net, Predicted] public new Vector2 Position {get; set;} = new Vector2( 1920/2, 1080/2 );

    public GameNote(float _time, float _direction, float _length, bool _mustHit)
    {
        Time = _time;
        Direction = (int)_direction;
        Length = _length;
        MustHit = _mustHit;

        Notes.Add(this);
    }

    [Event.Tick]
    public void Tick()
    {
        if(!IsClient) return;
        
        if(!HasActor && GameManager.Current.SongTime >= Time-2.0f){
            Actor = new();

            string _arrow = "";
            if(Direction == 0) _arrow = "left";
            else if(Direction == 1) _arrow = "down";
            else if(Direction == 2) _arrow = "up";
            else if(Direction == 3) _arrow = "right";

            Actor.Sprite = "/sprites/arrows/" + _arrow + ".png";
            Actor.AddClass("arrow");

            GameManager.Current.AddChild(Actor);

            HasActor = true;
        }

        if(!ActorPassed && GameManager.Current.SongTime >= Time){
            if(IsBot){
                Boyfriend.SetState(PlayerId, Direction, 0);
                ActorPassed = true;
                GameNote.Notes.Remove(this);
                Actor.Delete();
                Delete();
            }else{
                if(!Missed && GameManager.Current.SongTime > Time+NoteTimings.Shit){
                    Boyfriend.BreakCombo(PlayerId);
                    GameNote.Notes.Remove(this);
                    Missed = true;
                }
                if(GameManager.Current.SongTime >= Time+0.5f){
                    ActorPassed = true;
                    Actor.Delete();
                    Delete();
                    GameNote.Notes.Remove(this);
                }
            }
        }

        
        
        if(HasActor && !ActorPassed){
            foreach(var _rec in Receptor.Receptors){
                if(_rec.MustHit == MustHit && _rec.Direction == Direction){
                    //if(_rec.PlayerId == 0) IsBot = true;
                    PlayerId = _rec.PlayerId;
                    Position = new Vector2(_rec.Actor.Position.x, _rec.Actor.Position.y+(Time-GameManager.Current.SongTime)*800*GameManager.Current.Chart.Chart.Song.ScrollSpeed);
                    break;
                }
            }
            Actor.Position = Position;
        }

    }
}