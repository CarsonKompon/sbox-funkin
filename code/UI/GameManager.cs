using Sandbox;
using Sandbox.UI;
using System.Collections.Generic;

[UseTemplate]
public partial class GameManager : Panel
{

    public static List<ulong> Boyfriends {get; set;} = new();
    public Boyfriend bf { get; set; } 

    public GameManager()
    {
        
    }

    public override void Tick()
	{
		base.Tick();

        
        foreach(var _players in FunkinGame.CurrentPlayers){
            
            var _shouldSpawn = true;
            foreach(var _uid in Boyfriends){
                if(_uid == _players.SteamId){
                    _shouldSpawn = false;
                    break;
                }
            }
            if(_shouldSpawn){
                Log.Info("SPAWNING!!!");
                var _bf = new Boyfriend();
                _bf.PlayerId = _players.SteamId;
                _bf.Sprite = "/sprites/boyfriend/idle_01.png";
                AddChild(_bf);
                Boyfriends.Add(_players.SteamId);
            }

        }

	}
}
