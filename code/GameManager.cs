using System;
using Sandbox;
using Sandbox.UI;
using System.Collections.Generic;

[UseTemplate]
public partial class GameManager : Panel
{

    //public static List<ulong> Boyfriends {get; set;} = new();

    public static GameManager Current;

    public static MenuBackground menuBackground = new();
    public static DisclaimerPanel disclaimerPanel = new();
    public static MainMenu mainMenu = new();
    public static SongSelect songSelect = new();
    public static SettingsMenu settingsMenu = new();
    
    public static GameUI gameUI = new();

    public static List<GameNote> Notes = new();

    public static float BPM = 120;

    public bool InGame = false;
    public int Countdown = 3;
    public ChartBase Chart;
    public RealTimeSince SongTime;

    public GameManager()
    {
        Current = this;

        AddChild(menuBackground);
        AddChild(disclaimerPanel);
        AddChild(mainMenu);
        AddChild(songSelect);
        AddChild(settingsMenu);
        AddChild(gameUI);
    }

    public override void Tick()
	{
		base.Tick();

        if(InGame)
        {
            
            // if(Time.Delta > 1.0f/60.0f){
            //     SongTime += Time.Delta - (1.0f/60.0f);
            // }
            if(Countdown > -1){
                var _snd = Countdown.ToString();
                if(Countdown == 0) _snd = "go";
                
                var _countdownTime = (-60.0f / BPM) * Countdown;
                
                if(SongTime > _countdownTime){
                    Sound.FromScreen("intro_" + _snd);
                    
                    if(Countdown == 0){
                        Sound.FromScreen(Chart.songInst);
                        Sound.FromScreen(Chart.songVoices);
                        SongTime = 0f;
                    }

                    Countdown--;
                }
            }

        }

	}

    public static void StartGame(ChartBase _chart){
        Current.InitPlayer(Local.Client.SteamId, new CharacterBoyfriend(), true);
        Current.InitPlayer(0, new CharacterSenpaiAngry(), false);

        Current.SongTime = -2f;
        Current.Chart = _chart;
        Current.InGame = true;
        Current.Countdown = 3;

        BPM = _chart.Chart.Song.BPM;

        Current.LoadNotes(_chart);
    }

    public void LoadNotes(ChartBase _chart){
        Notes = new();
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
                var _gameNote = new GameNote(_time, _direction, _length, _mustHit);
                Notes.Add(_gameNote);
            }
        }
    }

    public static List<GameNote> NextNotes(bool _mustHit){
        List<GameNote> _toReturn = new();
        foreach(var _note in Notes){
            if(_note.Time > Current.SongTime - NoteTimings.Shit && _note.Time < Current.SongTime + NoteTimings.Shit && _note.MustHit == _mustHit){
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

    public void InitPlayer(ulong _steamid, CharacterBase _char, bool _rightSide){
        var _position = new Vector2(508,900);
        var _recPosition = new Vector2(176,70);
        if(_rightSide){
            _position = new Vector2(1367,900);
            _recPosition = new Vector2(1036,70);
        }
        SpawnCharacter(_steamid, _char, _position, _rightSide);
        SpawnReceptors(_steamid, _recPosition, _rightSide);
    }

    public void SpawnCharacter(ulong _steamid, CharacterBase _char, Vector2 _position, bool _mustHit){
        var _bf = new Boyfriend(_steamid, _char, _position, _mustHit);
        AddChild(_bf.Actor);
        //Boyfriends.Add(_steamid);
    }

    public void SpawnReceptors(ulong _steamid, Vector2 _position, bool _mustHit){
        var i=0;
        while(i <= 3){
            var _rec = new Receptor(_steamid, i, _position + Vector2.Left*(162+6)*i, _mustHit);
            AddChild(_rec.Actor);
            i++;
        }
    }
}
