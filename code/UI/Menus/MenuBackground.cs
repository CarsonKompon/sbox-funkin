using System.Text.RegularExpressions;
using System;
using Sandbox;
using Sandbox.UI;
using System.Collections.Generic;

[UseTemplate]
public partial class MenuBackground : Base2D
{
    public Base2D FilterPanel { get; set; }

    public float ToOpacity = 1.0f;
    public float Opacity = 1.0f;

    public float ToHue = 50.0f;
    public float Hue = 50.0f;

    public bool Show = true;

    public MenuBackground()
    {
        Position = new Vector2(-240, -270/2);
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

    public void SetHue(float _h){
        ToHue = _h;
    }

    public void SetOpacity(float _o){
        ToOpacity = _o;
    }
   
}

public enum MenuColors {
    Red = 0,
    Yellow = 50,
    Blue = -90
}
