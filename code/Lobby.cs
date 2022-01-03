using Sandbox;
using System.Collections.Generic;

public partial class Lobby : Entity
{
    [Net] public ulong LeftPlayer {get;set;} = 1;
    [Net] public ulong RightPlayer {get;set;} = 1;

    [Net] public string SongOne {get;set;} = "roses";
    [Net] public string SongTwo {get;set;} = "roses";

    [Net] public Boyfriend LeftBoyfriend {get;set;}
    [Net] public Boyfriend RightBoyfriend {get;set;}

    [Net] public bool InGame {get;set;} = false;
    [Net] public RealTimeSince SongTime {get;set;} = -100f;
    [Net] public ChartBase Chart {get;set;}
    [Net] public int Countdown {get;set;} = 3;

    [Net] public List<GameNote> Notes {get;set;} = new List<GameNote>();

    [Net] public float BPM {get;set;} = 120;

    public Lobby(){
        Transmit = TransmitType.Always;
    }

    public void StartGame(){
        Log.Info("STARTING GAME IN LOBBY " + FunkinGame.GetLobbyId(RightPlayer).ToString());
        var _chartName = SongOne;
        if(Rand.Int(0,1) == 0) _chartName = SongTwo;

        var _chart = FunkinGame.GetChartFromName(_chartName);

        Log.Info("Creating right player");
        InitRightPlayer(new CharacterBoyfriend());
        Log.Info("Creating left player");
        InitLeftPlayer(new CharacterSenpaiAngry());

        GameUI.Show();

        SongTime = -2f;
        Chart = _chart;
        InGame = true;
        Countdown = 3;

        BPM = _chart.Chart.Song.BPM;

        Log.Info("Loading the notes from the chart");
        LoadNotes(_chart);
    }

    public void LoadNotes(ChartBase _chart){
        Notes = new List<GameNote>();
        foreach(var _section in _chart.Chart.Song.Sections){
            foreach(var _note in _section.Notes){
                var _time = StepsToTime(_note[0].ToString().ToFloat());
                var _direction = _note[1].ToString().ToFloat();
                var _length = _note[2].ToString().ToFloat();
                var _mustHit = _section.MustHitSection;
                if(_direction > 3){
                    _direction -= 3;
                    _mustHit = !_mustHit;
                }
                var _gameNote = new GameNote(this, _time, _direction, _length, _mustHit);
                Notes.Add(_gameNote);
            }
        }
    }

    public List<GameNote> NextNotes(bool _mustHit){
        List<GameNote> _toReturn = new();
        foreach(var _note in Notes){
            if(_note.Time >SongTime - NoteTimings.Shit && _note.Time < SongTime + NoteTimings.Shit && _note.MustHit == _mustHit){
                _toReturn.Add(_note);
            }
        }
        return _toReturn;
    }

    //Used to convert funkin steps format to time in seconds
    public float StepsToTime(float _steps){
        //400 Steps = 1 Beat ( 1 Beat = 60s / BPM )
        return (_steps / 500) * (60 / BPM);
    }

    public void InitLeftPlayer(CharacterBase _char){
        var _position = new Vector2(508,900);
        var _recPosition = new Vector2(176,70);
        //if(Downscroll) _recPosition = new Vector2(_recPosition.x, 1080-_recPosition.y-162);
        LeftBoyfriend = SpawnCharacter(LeftPlayer, _char, _position, false);
        SpawnReceptors(LeftPlayer, _recPosition, false);
    }

    public void InitRightPlayer(CharacterBase _char){
        var _position = new Vector2(1367,900);
        var _recPosition = new Vector2(1036,70);
        //if(Downscroll) _recPosition = new Vector2(_recPosition.x, 1080-_recPosition.y-162);
        RightBoyfriend = SpawnCharacter(RightPlayer, _char, _position, true);
        SpawnReceptors(RightPlayer, _recPosition, true);
    }

    public Boyfriend SpawnCharacter(ulong _steamid, CharacterBase _char, Vector2 _position, bool _mustHit){
        var _bf = new Boyfriend(this, _steamid, _char.id, _position, _mustHit);
        return _bf;
        //Boyfriends.Add(_steamid);
    }

    public void SpawnReceptors(ulong _steamid, Vector2 _position, bool _mustHit){
        var i=0;
        while(i <= 3){
            var _rec = new Receptor(this, _steamid, i, _position + Vector2.Left*(162+6)*i, _mustHit);
            i++;
        }
    }
} 