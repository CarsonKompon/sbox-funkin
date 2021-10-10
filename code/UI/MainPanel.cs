using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public partial class MainPanel : HudEntity<RootPanel>
{
    public MainPanel()
    {
        if(!IsClient) return;

        RootPanel.SetTemplate("/ui/mainpanel.html");
        RootPanel.Style.PointerEvents = "all";
    }
}