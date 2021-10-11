using System.Xml.Schema;
using Sandbox;
using System.Collections.Generic;

public partial class Boyfriend : Base2D
{

    public static List<Boyfriend> Characters {get; set;} = new(); 

    public ulong PlayerId {get; set;}
    public string Sprite {get; set;}

    public Boyfriend()
    {
		AddClass( "boyfriend" );
        Position = new Vector2( Rand.Int(200,1920-200), Rand.Int(200, 1080-200));
        PlayerId = Client.All[0].SteamId;
        Log.Info("Steam ID: " + PlayerId.ToString());

        Characters.Add(this);
    }

    public override void Tick()
	{
		base.Tick();

        Style.SetBackgroundImage(Sprite);
        
        //Destroy character if they no longer exist
        var _toDestroy = true;
        foreach(var _cl in FunkinGame.CurrentPlayers){
            if(_cl.SteamId == PlayerId){
                _toDestroy = false;
                break;
            }
        }
        if(_toDestroy){
            Delete();
        }

	}

    [ClientRpc]
	public static void SetImage(ulong _steamid, string _image){
		foreach(Boyfriend _char in Characters){
            if(_char.PlayerId == _steamid){
                _char.Sprite = _image;
                return;
            }
        }
        Log.Info("Couldn't find the character");
	}
}
