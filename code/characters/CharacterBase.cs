using Sandbox;

public class CharacterBase : LibraryAttribute{
    public virtual string id {get;set;} = "boyfriend";
    public virtual string name {get;set;} = "Boyfriend";
    public virtual bool facingRight {get;set;} = false;
    public virtual bool excludeFromCharacterSelect {get;set;} = false;
    public virtual bool antialiasing {get;set;} = true;
    
    public virtual int idleFrames {get;set;} = 6;
    public virtual string spriteIdle{ get{ return "/sprites/" + id + "/idle"; } }
    public virtual string spriteUp{ get{ return "/sprites/" + id + "/up"; } }
    public virtual string spriteDown{ get{ return "/sprites/" + id + "/down"; } }
    public virtual string spriteLeft{ get{ return "/sprites/" + id + "/left"; } }
    public virtual string spriteRight{ get{ return "/sprites/" + id + "/right"; } }

    public virtual string GetSpriteFromState(BoyfriendState _state){
        if(_state == BoyfriendState.Up) return spriteUp;
        else if(_state == BoyfriendState.Down) return spriteDown;
        else if(_state == BoyfriendState.Left) return spriteLeft;
        else if(_state == BoyfriendState.Right) return spriteRight;
        else return spriteIdle;
    }
}