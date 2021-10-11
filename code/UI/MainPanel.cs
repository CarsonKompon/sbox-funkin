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

        RootPanel.AddChild<GameManager>();
        
    }
}
