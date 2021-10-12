using System.ComponentModel;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

public partial class ScoreNotifier : Base2D
{

    public TimeSince LifeTime;
    public float Speed;

    public ScoreNotifier(string _sprite)
    {
        LifeTime = 0f;
        Position = new Vector2(1920/2,1080/2);
		Sprite = _sprite;
        Speed = -1;
    }

    public override void Tick()
	{
        Position = new Vector2(Position.x, Position.y+Speed);
        Speed += 0.025f;

		base.Tick();
	}


}
