using Sandbox;

[CharacterBase]
public class CharacterPico : CharacterBase
{
    public CharacterPico(){
        id = "pico";
        name = "Pico";

        idleFrames = 7;
        spriteIdle = "/sprites/pico/idle";
        spriteUp = "/sprites/pico/up";
        spriteDown = "/sprites/pico/down";
        spriteLeft = "/sprites/pico/left";
        spriteRight = "/sprites/pico/right";
    }
}