using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Collections.Generic;

public partial class GameUI : Panel
{
    public static Label LeftScore { get; set; }
    public static Label RightScore { get; set; }

    public static bool show = false;

    public GameUI()
    {
        StyleSheet.Load("ui/gameui.scss");
        LeftScore = Add.Label("Score: 0", "score");
        RightScore = Add.Label("Score: 0", "score");
    }

    public override void Tick()
    {
        SetClass("hideRight", !show);
        //RootBody.SetClass( "hideLeft", Pos == -1);
        //RootBody.SetClass( "hideRight", Pos == 1);
    }

    [ClientRpc]
    public static void SetRightScore(int _score){
        RightScore.Text = "Score: " + _score.ToString();
    }

    [ClientRpc]
    public static void SetLeftScore(int _score){
        LeftScore.Text = "Score: " + _score.ToString();
    }

    [ClientRpc]
    public static void Show(bool _show = true){
        show = _show;
    }

    [ClientRpc]
    public static void Hide(bool _hide = true){
        show = !_hide;
    }

    

}
