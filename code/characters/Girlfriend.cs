using Sandbox;

[CharacterBase]
public class CharacterGirlfriend : CharacterBase
{
    public CharacterGirlfriend(){
        id = "girlfriend";
        name = "Girlfriend";
        excludeFromCharacterSelect = true;

        idleFrames = 20;
    }
}