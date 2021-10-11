using Sandbox;
using System.Collections.Generic;


[Library( "funkin", Title = "Funkin" )]
public partial class FunkinGame : Sandbox.Game
{

    [Net] public static List<Client> CurrentPlayers {get; set;} = new();

    public static List<CharacterBase> Characters = new List<CharacterBase>();

    public FunkinGame()
    {
        if(IsServer){
            _ = new MainPanel();

        }
        InitCharacters();
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
	public static void InitCharacters(){
		Characters = new();
		//Load events from attribute
		foreach(CharacterBase _char in Library.GetAttributes<CharacterBase>()){
			Characters.Add(_char.Create<CharacterBase>());
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

    public static CharacterBase GetCharacterFromId( string _id )
    {
        foreach(var _char in Characters){
            if(_char.id == _id){
                return _char;
            }
        }
        return null;
    }

}