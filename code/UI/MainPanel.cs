using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

[Library]
public partial class MainPanel : HudEntity<RootPanel>
{
    
    public MainPanel()
    {
        if(!IsClient) return;

        RootPanel.StyleSheet.Load("/ui/mainpanel.scss");
        RootPanel.Style.PointerEvents = "all";

        //RootPanel.AddChild<GameManager>();
        RootPanel.AddChild<MenuBackground>();
        RootPanel.AddChild<DisclaimerPanel>();
        RootPanel.AddChild<FunkinChatBox>();
        RootPanel.AddChild<MainMenu>();
        RootPanel.AddChild<SongSelect>();
        RootPanel.AddChild<SettingsMenu>();
        RootPanel.AddChild<GameUI>();
        
    }

    [ClientRpc]
    public void AddChild(Base2D _actor){
        Log.Info("I'm here!");
        RootPanel.AddChild(_actor);
    }
    
}