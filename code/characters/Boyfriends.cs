using Sandbox;

[CharacterBase]
public class CharacterBoyfriend : CharacterBase
{
    public CharacterBoyfriend(){
        id = "boyfriend";
        name = "Boyfriend";

        idleFrames = 6;
    }
}

[CharacterBase]
public class CharacterBoyfriendPixel : CharacterBase
{
    public CharacterBoyfriendPixel(){
        id = "boyfriend_pixel";
        name = "Boyfriend Pixel";
        antialiasing = false;

        idleFrames = 5;
    }
}

[CharacterBase]
public class CharacterBoyfriendXmas : CharacterBase
{
    public CharacterBoyfriendXmas(){
        id = "boyfriend_xmas";
        name = "Boyfriend Xmas";

        idleFrames = 5;
    }
}