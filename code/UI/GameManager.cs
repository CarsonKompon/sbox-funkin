using Sandbox;
using Sandbox.UI;
using System.Collections.Generic;

[UseTemplate]
public partial class GameManager : Panel
{

    public static List<ulong> Boyfriends {get; set;} = new();

    public static MenuBackground menuBackground = new();
    public static MainMenu mainMenu = new();
    public static SongSelect songSelect = new();

    public Boyfriend bf { get; set; } 

    public GameManager()
    {
        AddChild(menuBackground);
        AddChild(mainMenu);
        AddChild(songSelect);
    }

    public override void Tick()
	{
		base.Tick();

        
        // foreach(var _players in FunkinGame.CurrentPlayers){
            
        //     var _shouldSpawn = true;
        //     foreach(var _uid in Boyfriends){
        //         if(_uid == _players.SteamId){
        //             _shouldSpawn = false;
        //             break;
        //         }
        //     }
        //     if(_shouldSpawn){
        //         Log.Info("SPAWNING!!!");
        //         var _bf = new Boyfriend(_players.SteamId, Rand.FromList(FunkinGame.Characters));
        //         AddChild(_bf.Actor);
        //         Boyfriends.Add(_players.SteamId);
        //     }

        // }

	}
}
