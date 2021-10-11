using Sandbox;
using Sandbox.UI;
using System.Collections.Generic;

public partial class MenuBackground : Panel
{

    public Panel Background;

    public bool Show = true;

    public MenuBackground()
    {
        StyleSheet.Load("ui/menus/menubackground.scss");
        AddClass("bg");
    }

    public override void Tick()
    {
        
    }

    public void SetPos(float _am){
        Style.Top = -240.0f + ((_am/100.0f) * 480.0f);
    }
   
}
