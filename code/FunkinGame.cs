using Sandbox;
using System;
using System.Collections.Generic;
using System.Text.Json;

[Library( "funkin", Title = "Funkin" )]
public partial class FunkinGame : Sandbox.Game
{

    [Net] public static List<Client> CurrentPlayers {get; set;} = new();
    [Net] public static List<Lobby> CurrentLobbies {get; set;} = new();
    [Net] public static List<Entity> CurrentEntities {get; set;} = new();

    public static MainPanel UI;

    public static List<CharacterBase> Characters = new List<CharacterBase>();
    public static List<ChartBase> Charts = new List<ChartBase>();

    public FunkinGame()
    {
        if(IsServer){
            UI = new MainPanel();
        }
        InitClasses();
    }

    [Event.Tick]
    public void Tick()
    {
        CurrentPlayers = new();
        for(var i=0;i<Client.All.Count;i++){
			CurrentPlayers.Add(Client.All[i]);
		}
    }

    [Event.Hotload] //Reload Characters on Hotload (Makes life easier while developing)
	public static void InitClasses(){
        //Load characters
		Characters = new();
		foreach(CharacterBase _char in Library.GetAttributes<CharacterBase>()){
			Characters.Add(_char.Create<CharacterBase>());
		}
        //Load charts
        Charts = new();
        foreach(ChartBase _chart in Library.GetAllAttributes<ChartBase>()){
            var _new = _chart.Create<ChartBase>();
            try
            {
                var _json = FileSystem.Mounted.ReadJson<ChartFile>(_new.jsonFile);

                _new.Chart = _json;

                Charts.Add(_new);
            }
            catch (Exception e) { Log.Trace(e); }
        }
	}

    [ServerCmd]
    public static void CreateLobby(ulong _steamid, string _song){
        Log.Info(_steamid.ToString() + " CREATING LOBBY");
        var _lobby = new Lobby();
        _lobby.RightPlayer = _steamid;
        _lobby.SongOne = _song;
        AddPlayerToLobby(_steamid, _lobby);
        CurrentLobbies.Add(_lobby);
    }

    [ServerCmd]
    public static void JoinLobby(ulong _steamid, string _song, int _lobbyId = -1){
        if(CurrentLobbies.Count == 0){
            CreateLobby(_steamid, _song);
        }else{
            var _foundLobby = false;
            foreach(var _lobby in CurrentLobbies){
                var _lobbyToCheck = _lobby;
                if(_lobbyId > -1) _lobbyToCheck = CurrentLobbies[_lobbyId];
                if(_lobbyToCheck.LeftPlayer == 1) {
                    Log.Info(_steamid.ToString() + " JOINING LOBBY");
                    _lobbyToCheck.LeftPlayer = _steamid;
                    _lobbyToCheck.SongTwo = _song;
                    AddPlayerToLobby(_steamid, _lobbyToCheck);
                    _lobbyToCheck.StartGame();
                    _foundLobby = true;
                    break;
                }
            }
            if(!_foundLobby) CreateLobby(_steamid, _song);
        }
    }

    public static void AddPlayerToLobby(ulong _steamid, Lobby _lobby){
        foreach(var _player in FunkinGame.CurrentPlayers){
            if(_player.SteamId == _steamid){
                (_player.Pawn as FunkinPlayer).Lobby = _lobby;
                break;
            }
        }
    }

    public static Lobby GetLobby(ulong _steamid){
        return CurrentLobbies[GetLobbyId(_steamid)];
    }

    public static int GetLobbyId(ulong _steamid){
        for(var i=0; i<CurrentLobbies.Count; i++){
            if(CurrentLobbies[i].LeftPlayer == _steamid) return i;
            if(CurrentLobbies[i].RightPlayer == _steamid) return i;
        }
        return -1;
    }

    public override void ClientJoined( Client client )
	{
		base.ClientJoined( client );
		var player = new FunkinPlayer();
		client.Pawn = player;
		player.Respawn();

         CurrentPlayers.Add(client);
	}

	public override void ClientDisconnect( Client cl, NetworkDisconnectionReason reason )
	{
		base.ClientDisconnect( cl, reason );

        CurrentPlayers.Remove(cl);
	}

	public override void Simulate( Client cl )
	{
		base.Simulate( cl );

        //Log.Info(FunkinGame.CurrentPlayers.Count);

        if(cl.Pawn is FunkinPlayer){

            var player = cl.Pawn as FunkinPlayer;
            
            if( Input.Pressed( InputButton.Forward ) ){
                player.inputUpPress = true;
            }
            player.inputUpDown = Input.Down( InputButton.Forward );

            if( Input.Pressed( InputButton.Back ) ){
                player.inputDownPress = true;
            }
            player.inputDownDown = Input.Down( InputButton.Back );

            if( Input.Pressed( InputButton.Left ) ){
                player.inputLeftPress = true;
            }
            player.inputLeftDown = Input.Down( InputButton.Left );

            if( Input.Pressed( InputButton.Right ) ){
                player.inputRightPress = true;
            }
            player.inputRightDown = Input.Down( InputButton.Right );
        }

	}

    // public void LoadNotes(ChartBase _chart){
    //     Notes = new();
    //     foreach(var _section in _chart.Chart.Song.Sections){
    //         foreach(var _note in _section.Notes){
    //             var _time = StepsToTime(_note[0].ToString().ToFloat());
    //             var _direction = _note[1].ToString().ToFloat();
    //             var _length = _note[2].ToString().ToFloat();
    //             var _mustHit = _section.MustHitSection;
    //             if(_direction > 3){
    //                 _direction -= 3;
    //                 _mustHit = !_mustHit;
    //             }
    //             var _gameNote = new GameNote(_time, _direction, _length, _mustHit);
    //             Notes.Add(_gameNote);
    //         }
    //     }
    // }

    // public static List<GameNote> NextNotes(bool _mustHit){
    //     List<GameNote> _toReturn = new();
    //     foreach(var _note in Notes){
    //         if(_note.Time > Current.SongTime - NoteTimings.Shit && _note.Time < Current.SongTime + NoteTimings.Shit && _note.MustHit == _mustHit){
    //             _toReturn.Add(_note);
    //         }
    //     }
    //     return _toReturn;
    // }

    // //Used to convert funkin steps format to time in seconds
    // public float StepsToTime(float _steps){
    //     //400 Steps = 1 Beat ( 1 Beat = 60s / BPM )
    //     return (_steps / 500) * (60 / BPM);
    // }

    // public void InitPlayer(ulong _steamid, CharacterBase _char, bool _rightSide){
    //     var _position = new Vector2(508,900);
    //     var _recPosition = new Vector2(176,70);
    //     if(_rightSide){
    //         _position = new Vector2(1367,900);
    //         _recPosition = new Vector2(1036,70);
    //     }
    //     if(Downscroll) _recPosition = new Vector2(_recPosition.x, 1080-_recPosition.y-162);
    //     SpawnCharacter(_steamid, _char, _position, _rightSide);
    //     SpawnReceptors(_steamid, _recPosition, _rightSide);
    // }

    // public Boyfriend SpawnCharacter(ulong _steamid, CharacterBase _char, Vector2 _position, bool _mustHit){
    //     var _bf = new Boyfriend(_steamid, _char, _position, _mustHit);
    //     AddChild(_bf.Actor);
    //     return _bf;
    //     //Boyfriends.Add(_steamid);
    // }

    // public void SpawnReceptors(ulong _steamid, Vector2 _position, bool _mustHit){
    //     var i=0;
    //     while(i <= 3){
    //         var _rec = new Receptor(_steamid, i, _position + Vector2.Left*(162+6)*i, _mustHit);
    //         AddChild(_rec.Actor);
    //         i++;
    //     }
    // }

    public static CharacterBase GetCharacterFromId( string _id )
    {
        foreach(var _char in Characters){
            if(_char.id == _id){
                return _char;
            }
        }
        return null;
    }

    public static ChartBase GetChartFromName( string _name )
    {
        foreach(var _chart in Charts){
            if(_chart.name.ToLower() == _name.ToLower()){
                return _chart;
            }
        }
        return null;
    }

}