using System.ComponentModel;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

public partial class Base2D : Panel
{

    public Vector2 Position = Vector2.Zero;

    public Base2D()
    {
		
    }

    public override void Tick()
	{
		base.Tick();

        Style.Left = Position.x;
        Style.Top = Position.y;

	}
}
