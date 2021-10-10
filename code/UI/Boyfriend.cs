using System.ComponentModel;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public partial class Boyfriend : Panel
{

    public Image img;

    public Boyfriend()
    {
		img = Add.Image( "https://static.wikia.nocookie.net/debatesjungle/images/1/12/BOYFRIEND.png/revision/latest?cb=20210221080340", "boyfriend" );
    }
}
