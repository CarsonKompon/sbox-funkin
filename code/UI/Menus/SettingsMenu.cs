using Sandbox;
using Sandbox.UI;
using System.Collections.Generic;

[UseTemplate]
public partial class SettingsMenu : Panel
{

    public Panel RootBody { get; set; }

    public static int Pos = -1;

    public SettingsMenu()
    {
        if(RootBody == null) Log.Warning("NO ROOTBODY!");
    }

    public override void Tick()
    {
        RootBody.SetClass( "hideLeft", Pos == -1);
        RootBody.SetClass( "hideRight", Pos == 1);
    }

    public void buttonDownscroll( Button _btn ){
        Sound.FromScreen("menu_confirm");
        GameManager.Downscroll = !GameManager.Downscroll;
        var _txt = "";
        if(GameManager.Downscroll) _txt = "DOWNSCROLL ON";
        else _txt = "DOWNSCROLL OFF";
        _btn.Text = _txt;
    }

    public void buttonBack( Button _btn ){
        Sound.FromScreen("menu_cancel");
        MenuBackground.SetHue((float)MenuColors.Yellow);
        MainMenu.Show();
        Pos = -1;
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
