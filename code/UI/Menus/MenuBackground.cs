using System.Text.RegularExpressions;
using System;
using Sandbox;
using Sandbox.UI;
using System.Collections.Generic;

[UseTemplate]
public partial class MenuBackground : Base2D
{
    public Base2D FilterPanel { get; set; }

    public static float ToOpacity = 1.0f;
    public static float Opacity = 1.0f;

    public static float ToHue = 50.0f;
    public static float Hue = 50.0f;

    public static MenuBackground This;

    public bool Show = true;

    public MenuBackground()
    {
        Position = new Vector2(-240, -270/2);
        This = this;
    }

    public override void Tick()
    {
        Hue = MathC.Lerp(Hue, ToHue, 0.0125f);
        Opacity = MathC.Lerp(Opacity, ToOpacity, 0.0125f);

        FilterPanel.Style.BackdropFilterBrightness = 1.0f;
        FilterPanel.Style.BackdropFilterContrast = 1.0f;
        FilterPanel.Style.BackdropFilterHueRotate = Hue;
        FilterPanel.Style.BackdropFilterSaturate = 1.0f;

        Style.Opacity = Opacity;
        
        base.Tick();
    }

    [ClientRpc]
    public static void SetHue(float _h){
        ToHue = _h;
    }

    [ClientRpc]
    public static void SetOpacity(float _o){
        ToOpacity = _o;
    }

    [ClientRpc]
    public static void SetPosition(float _y){
        This.Position = new Vector2(This.Position.x, _y);
    }
   
}

public enum MenuColors {
    Red = 0,
    Yellow = 50,
    Blue = -90
}
