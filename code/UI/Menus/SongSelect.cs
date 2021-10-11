using Sandbox;
using Sandbox.UI;
using System.Collections.Generic;

[UseTemplate]
public partial class SongSelect : Panel
{

    public Panel RootBody { get; set; }

    public bool Show = false;

    public SongSelect()
    {
        if(RootBody == null) Log.Warning("NO ROOTBODY!");
    }

    public override void Tick()
    {

        RootBody.SetClass( "hide", !Show);
    }

    public void buttonClick( Button _btn ){
        if(Show){
            Sound.FromScreen("menu_confirm");
            GameManager.mainMenu.Show = true;
            Show = false;
        }
    }

    public void buttonHover( Button _btn ){
        if(Show){
            if(_btn.HasHovered){
                Sound.FromScreen("menu_hover");
            }
        }
    }
   
}
