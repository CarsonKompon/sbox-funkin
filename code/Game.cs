using Sandbox;


[Library( "funkin", Title = "Funkin" )]
public partial class FunkinGame : Sandbox.Game
{

    public FunkinGame()
    {
        if(IsServer){
            _ = new MainPanel();
        }
    }

    public override void ClientJoined( Client client )
	{
		base.ClientJoined( client );

		// Create a pawn and assign it to the client.
		var player = new MyPlayer();
		client.Pawn = player;

		player.Respawn();
	}

}