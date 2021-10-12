using Sandbox;
using Sandbox.UI;
using System.Collections.Generic;

[UseTemplate]
public partial class SettingsMenu : Panel
{

    public Panel RootBody { get; set; }

    public int Pos = -1;

    public SettingsMenu()
    {
        if(RootBody == null) Log.Warning("NO ROOTBODY!");
    }

    public override void Tick()
    {
        RootBody.SetClass( "hideLeft", Pos == -1);
        RootBody.SetClass( "hideRight", Pos == 1);
    }

    public void buttonBack( Button _btn ){
        Sound.FromScreen("menu_cancel");
        GameManager.menuBackground.SetHue((float)MenuColors.Yellow);
        GameManager.mainMenu.Pos = 0;
        Pos = -1;
    }

    public void buttonHover( Button _btn ){
        if(_btn.HasHovered){
            Sound.FromScreen("menu_hover");
            GameManager.menuBackground.Position = new Vector2(GameManager.menuBackground.Position.x, (Mouse.Position.y/Screen.Height)*-270);
        }
    }

}
