using Sandbox;
using System.Collections.Generic;


[Library( "funkin", Title = "Funkin" )]
public partial class FunkinGame : Sandbox.Game
{

    [Net] public static List<Client> CurrentPlayers {get; set;} = new();

    public FunkinGame()
    {
        if(IsServer){
            _ = new MainPanel();
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

    [Event.Tick]
    public void Tick()
    {
        CurrentPlayers = new();
        for(var i=0;i<Client.All.Count;i++){
			CurrentPlayers.Add(Client.All[i]);
		}
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
        }else if( Input.Released( InputButton.Forward ) ){
            player.inputUpRelease = true;
        }

        if( Input.Pressed( InputButton.Back ) ){
            player.inputDownPress = true;
        }else if( Input.Released( InputButton.Back ) ){
            player.inputDownRelease = true;
        }

        if( Input.Pressed( InputButton.Left ) ){
            player.inputLeftPress = true;
        }else if( Input.Released( InputButton.Left ) ){
            player.inputLeftRelease = true;
        }

        if( Input.Pressed( InputButton.Right ) ){
            player.inputRightPress = true;
        }else if( Input.Released( InputButton.Right ) ){
            player.inputRightRelease = true;
        }

	}

}