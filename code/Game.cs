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

}