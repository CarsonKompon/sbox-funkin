using Sandbox;
using System;
using System.Collections.Generic;
using System.Text.Json;


[Library( "funkin", Title = "Funkin" )]
public partial class FunkinGame : Sandbox.Game
{

    [Net] public static List<Client> CurrentPlayers {get; set;} = new();
    [Net] public static List<Lobby> CurrentLobbies {get; set;} = new();

    public static List<CharacterBase> Characters = new List<CharacterBase>();
    public static List<ChartBase> Charts = new List<ChartBase>();

    public FunkinGame()
    {
        if(IsServer){
            _ = new MainPanel();

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

    public static void CreateLobby(ulong _steamid){
        var _lobby = new Lobby();
        _lobby.RightPlayer = _steamid;
        CurrentLobbies.Add(_lobby);
    }

    public static void JoinLobby(ulong _steamid){
        if(CurrentLobbies.Count == 0){
            CreateLobby(_steamid);
        }else{
            foreach(var _lobby in CurrentLobbies){
                if(_lobby.LeftPlayer == 0) {
                    _lobby.LeftPlayer = _steamid;
                    GameManager.StartGame(FunkinGame.GetChartFromName("roses"), _lobby.RightPlayer, _lobby.LeftPlayer);
                    GameManager.gameUI.Show = true;
                    break;
                }
            }
        }
    }

    public override void ClientJoined( Client client )
	{
		base.ClientJoined( client );
		var player = new FunkinPlayer();
		client.Pawn = player;
		player.Respawn();

        //CurrentPlayers.Add(client);
	}

	public override void ClientDisconnect( Client cl, NetworkDisconnectionReason reason )
	{
		base.ClientDisconnect( cl, reason );

        //CurrentPlayers.Remove(cl);
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