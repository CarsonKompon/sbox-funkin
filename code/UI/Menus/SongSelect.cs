using System;
using Sandbox;
using Sandbox.UI;
using System.Collections.Generic;

[UseTemplate]
public partial class SongSelect : Panel
{

    public static SongSelect Current;

    public Panel RootBody { get; set; }
    public Panel Menu { get; set; }

    public static int Pos = 1;

    public SongSelect()
    {
        if(RootBody == null) Log.Warning("NO ROOTBODY!");
        if(Menu == null) Log.Warning("NO MENU DIV!");

        Current = this;
        InitSongs();
    }

    public static void InitSongs()
    {
        foreach(var _child in Current.Menu.Children){
            _child.Delete();
        }
        foreach(var _chart in FunkinGame.Charts){
            var _song = _chart.Chart.Song;
            var _btn = new Button();
            _btn.Text = _chart.name.ToUpper();
            _btn.AddEventListener("onclick", () => {Current.buttonClick(_btn);});
            _btn.AddEventListener("onmouseover", () => {Current.buttonHover(_btn);});
            Current.Menu.AddChild(_btn);
        }
        var _backBtn = new Button();
        _backBtn.Text = "BACK";
        _backBtn.AddEventListener("onclick", () => {Current.buttonBack(_backBtn);});
        _backBtn.AddEventListener("onmouseover", () => {Current.buttonHover(_backBtn);});
        Current.Menu.AddChild(_backBtn);
    }

    public override void Tick()
    {
        RootBody.SetClass( "hideLeft", Pos == -1);
        RootBody.SetClass( "hideRight", Pos == 1);
    }

    public void buttonClick( Button _btn ){
        Sound.FromScreen("menu_confirm");
        MenuBackground.SetOpacity(0.0f);
        
        Pos = -1;

        if(!MainMenu.Multiplayer){
            FunkinGame.CreateLobby(Local.Client.SteamId, _btn.Text);
            //var _lobby = FunkinGame.GetLobbyId(Local.Client.SteamId);
            FunkinGame.JoinLobby(0, _btn.Text);
        }else{
            FunkinGame.JoinLobby(Local.Client.SteamId, _btn.Text);
        }
    }

    public void buttonBack( Button _btn ){
        Sound.FromScreen("menu_cancel");
        MenuBackground.SetHue((float)MenuColors.Yellow);
        MainMenu.Show();
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
        InitSongs();
        Pos = _pos;
    }
   
}
