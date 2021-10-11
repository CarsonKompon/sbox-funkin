using Sandbox;

[CharacterBase]
public class CharacterGirlfriend : CharacterBase
{
    public CharacterGirlfriend(){
        id = "girlfriend";
        name = "Girlfriend";
        facingRight = true;
        excludeFromCharacterSelect = true;

        idleFrames = 20;
    }
}