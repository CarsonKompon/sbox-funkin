using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Collections.Generic;

public partial class GameUI : Panel
{
    public Label LeftScore { get; set; }
    public Label RightScore { get; set; }

    public bool Show = false;

    public GameUI()
    {
        StyleSheet.Load("ui/gameui.scss");
        LeftScore = Add.Label("Score: 0", "score");
        RightScore = Add.Label("Score: 0", "score");
    }

    public override void Tick()
    {
        SetClass("hideRight", !Show);
        //RootBody.SetClass( "hideLeft", Pos == -1);
        //RootBody.SetClass( "hideRight", Pos == 1);
    }

    

}
