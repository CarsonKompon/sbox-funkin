using Sandbox;
using Sandbox.UI;
using System.Collections.Generic;

[UseTemplate]
public partial class MainMenu : Panel
{

    public Panel RootBody { get; set; }

    public bool Show = true;

    public MainMenu()
    {
        if(RootBody == null) Log.Warning("NO ROOTBODY!");
    }

    public override void Tick()
    {
        RootBody.SetClass( "hide", !Show);
    }

    public void buttonSingleplayer( Button _btn ){
        if(Show){
            Sound.FromScreen("menu_confirm");
            GameManager.songSelect.Show = true;
            Show = false;
        }
    }

    public void buttonHover( Button _btn ){
        if(Show){
            if(_btn.HasHovered){
                Sound.FromScreen("menu_hover");
                GameManager.menuBackground.SetPos(0);
            }
        }
    }
   
}
