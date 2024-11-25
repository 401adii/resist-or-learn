using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace resist_or_learn;

public class InputBox : Button
{
    public string text;
    public readonly Vector2 textPosition;
    public readonly int charLimit;
    private bool onlyNumeric;
    private bool focus;
    
    public InputBox(Texture2D texture, Vector2 position, bool onlyNumeric = false, int charLimit = 15): base(texture, position){
        text = "";
        this.onlyNumeric = onlyNumeric;
        this.charLimit = charLimit;
        focus = false;
        textPosition = new(position.X + 8,position.Y + 12);
    }

    public void HandleInput()
    {
        if(focus){
            char ch = InputHandler.GetSingleInput();
            if(ch != '\0' && ch != 8 && text.Length < charLimit)
                text += ch;
            if(ch == 8 && text.Length != 0)
                text = text[..^1];
        }
    }

    public override void UpdateState()
    {
        if(isPressed){
            focus = !focus;
        }
    }
}
