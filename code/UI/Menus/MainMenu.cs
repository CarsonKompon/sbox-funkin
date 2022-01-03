using Sandbox;
using Sandbox.UI;
using System.Collections.Generic;

[UseTemplate]
public partial class MainMenu : Panel
{

    public Panel RootBody { get; set; }

    public static bool Multiplayer = false;

    public static int Pos = 0;

    public MainMenu()
    {
        if(RootBody == null) Log.Warning("NO ROOTBODY!");
    }

    public override void Tick()
    {
        RootBody.SetClass( "hideLeft", Pos == -1);
        RootBody.SetClass( "hideRight", Pos == 1);
    }

    public void buttonSingleplayer( Button _btn ){
        Multiplayer = false;
        Sound.FromScreen("menu_confirm");
        MenuBackground.SetHue((float)MenuColors.Blue);
        SongSelect.Show();
        Pos = -1;
    }

    public void buttonMultiplayer( Button _btn ){
        Multiplayer = true;
        Sound.FromScreen("menu_confirm");
        MenuBackground.SetHue((float)MenuColors.Blue);
        SongSelect.Show();
        Pos = -1;

        //GameManager.menuBackground.SetOpacity(0.0f);
        //FunkinGame.JoinLobby(Local.Client.SteamId);
    }

    public void buttonSettings( Button _btn ){
        Sound.FromScreen("menu_confirm");
        MenuBackground.SetHue((float)MenuColors.Red);
        SettingsMenu.Show();
        Pos = 1;
    }

    public void buttonHover( Button _btn ){
        if(_btn.HasHovered){
            Sound.FromScreen("menu_hover");
            MenuBackground.SetPosition((Mouse.Position.y/Screen.Height)*-270);
        }
    }

    [ClientRpc]
    public static void Show(int _pos = 0){
        Pos = _pos;
    }

}
