using Sandbox;

public class CharacterBase : LibraryAttribute{
    public virtual string id {get;set;} = "boyfriend";
    public virtual string name {get;set;} = "Boyfriend";
    
    public virtual int idleFrames {get;set;} = 6;
    public virtual string spriteIdle {get;set;} = "/sprites/boyfriend/idle";
    public virtual string spriteUp {get;set;} = "/sprites/boyfriend/up";
    public virtual string spriteDown {get;set;} = "/sprites/boyfriend/down";
    public virtual string spriteLeft {get;set;} = "/sprites/boyfriend/left";
    public virtual string spriteRight {get;set;} = "/sprites/boyfriend/right";

    public virtual string GetSpriteFromState(BoyfriendState _state){
        if(_state == BoyfriendState.Up) return spriteUp;
        else if(_state == BoyfriendState.Down) return spriteDown;
        else if(_state == BoyfriendState.Left) return spriteLeft;
        else if(_state == BoyfriendState.Right) return spriteRight;
        else return spriteIdle;
    }
}