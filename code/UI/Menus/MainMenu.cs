using Sandbox;
using Sandbox.UI;
using System.Collections.Generic;

[UseTemplate]
public partial class MainMenu : Panel
{

    public Panel RootBody { get; set; }

    public int Pos = 0;

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
        Sound.FromScreen("menu_confirm");
        GameManager.menuBackground.SetHue((float)MenuColors.Blue);
        GameManager.songSelect.Pos = 0;
        Pos = -1;
    }

    public void buttonMultiplayer( Button _btn ){
        Sound.FromScreen("menu_confirm");
        GameManager.menuBackground.SetOpacity(0.0f);
        
        Pos = -1;

        FunkinGame.JoinLobby(Local.Client.SteamId);
    }

    public void buttonSettings( Button _btn ){
        Sound.FromScreen("menu_confirm");
        GameManager.menuBackground.SetHue((float)MenuColors.Red);
        GameManager.settingsMenu.Pos = 0;
        Pos = 1;
    }

    public void buttonHover( Button _btn ){
        if(_btn.HasHovered){
            Sound.FromScreen("menu_hover");
            GameManager.menuBackground.Position = new Vector2(GameManager.menuBackground.Position.x, (Mouse.Position.y/Screen.Height)*-270);
        }
    }

}
