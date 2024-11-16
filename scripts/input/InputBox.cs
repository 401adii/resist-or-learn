using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace resist_or_learn;

public class InputBox : Sprite
{
    public string value;
    private bool onlyNumeric;
    private bool focus;
    public InputBox(Texture2D texture, Vector2 position, bool onlyNumeric = false): base(texture, position){
        value = "";
        this.onlyNumeric = onlyNumeric;
        focus = true;
    }

    public void HandleInput()
    {            
        string str = InputHandler.GetSingleInput();
    }
}
