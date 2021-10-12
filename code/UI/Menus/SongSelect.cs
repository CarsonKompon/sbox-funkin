using System;
using Sandbox;
using Sandbox.UI;
using System.Collections.Generic;

[UseTemplate]
public partial class SongSelect : Panel
{

    public Panel RootBody { get; set; }
    public Panel Menu { get; set; }

    public int Pos = 1;

    public SongSelect()
    {
        if(RootBody == null) Log.Warning("NO ROOTBODY!");
        if(Menu == null) Log.Warning("NO MENU DIV!");

        InitSongs(this);
    }

    public static void InitSongs(SongSelect _ss)
    {
        foreach(var _child in _ss.Menu.Children){
            _child.Delete();
        }
        foreach(var _chart in FunkinGame.Charts){
            var _song = _chart.Chart.Song;
            var _btn = new Button();
            _btn.Text = _chart.name.ToUpper();
            _btn.AddEventListener("onclick", () => {_ss.buttonClick(_btn);});
            _btn.AddEventListener("onmouseover", () => {_ss.buttonHover(_btn);});
            _ss.Menu.AddChild(_btn);
        }
        var _backBtn = new Button();
        _backBtn.Text = "BACK";
        _backBtn.AddEventListener("onclick", () => {_ss.buttonBack(_backBtn);});
        _backBtn.AddEventListener("onmouseover", () => {_ss.buttonHover(_backBtn);});
        _ss.Menu.AddChild(_backBtn);
    }

    public override void Tick()
    {
        RootBody.SetClass( "hideLeft", Pos == -1);
        RootBody.SetClass( "hideRight", Pos == 1);
    }

    public void buttonClick( Button _btn ){
        Sound.FromScreen("menu_confirm");
        GameManager.menuBackground.SetOpacity(0.0f);
        
        Pos = -1;

        GameManager.StartGame(FunkinGame.GetChartFromName(_btn.Text));
    }

    public void buttonBack( Button _btn ){
        Sound.FromScreen("menu_cancel");
        GameManager.menuBackground.SetHue((float)MenuColors.Yellow);
        GameManager.mainMenu.Pos = 0;
        Pos = 1;
    }

    public void buttonHover( Button _btn ){
        if(_btn.HasHovered){
            Sound.FromScreen("menu_hover");
            GameManager.menuBackground.Position = new Vector2(GameManager.menuBackground.Position.x, (Mouse.Position.y/Screen.Height)*-270);
        }
    }
   
}
