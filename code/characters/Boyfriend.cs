using Sandbox;

[CharacterBase]
public class CharacterBoyfriend : CharacterBase
{
    public CharacterBoyfriend(){
        id = "boyfriend";
        name = "Boyfriend";

        idleFrames = 6;
        spriteIdle = "/sprites/boyfriend/idle";
        spriteUp = "/sprites/boyfriend/up";
        spriteDown = "/sprites/boyfriend/down";
        spriteLeft = "/sprites/boyfriend/left";
        spriteRight = "/sprites/boyfriend/right";
    }
}