using Sandbox;

[CharacterBase]
public class CharacterSenpai : CharacterBase
{
    public CharacterSenpai(){
        id = "senpai";
        name = "Senpai";
        antialiasing = false;

        idleFrames = 18;
    }
}

[CharacterBase]
public class CharacterSenpaiAngry : CharacterBase
{
    public CharacterSenpaiAngry(){
        id = "senpai_angry";
        name = "Senpai Angry";
        antialiasing = false;

        origin = new Vector2(200,600);

        idleFrames = 5;
    }
}