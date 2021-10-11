using Sandbox;

[CharacterBase]
public class CharacterDad : CharacterBase
{
    public CharacterDad(){
        id = "dad";
        name = "Daddy Dearest";
        facingRight = true;

        idleFrames = 6;
    }
}

[CharacterBase]
public class CharacterDadXmas : CharacterBase
{
    public CharacterDadXmas(){
        id = "dad_xmas";
        name = "Daddy Dearest Xmas";
        facingRight = true;

        idleFrames = 6;
    }
}