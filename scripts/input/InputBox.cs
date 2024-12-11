using System;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace resist_or_learn;

public class InputBox : Button
{
    public new string text;
    public readonly int charLimit;
    private bool onlyNumeric;
    public bool focus;
    public bool enabled;
    
    public InputBox(Texture2D texture, Vector2 position, bool onlyNumeric = false, bool enabled = true, int charLimit = 15): base(texture, position){
        this.onlyNumeric = onlyNumeric;
        this.charLimit = charLimit;
        this.enabled = enabled;
        focus = false;
        text = "";
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        spriteBatch.DrawString(Game1.font, text, textPosition, Color.White);
    }
    
    public void HandleInput()
    {
        if(focus && enabled){
            char ch = InputHandler.GetSingleInput();
            if(onlyNumeric && Char.IsLetter(ch))
                return;
            if(ch != '\0' && ch != 8 && text.Length < charLimit)
                text += ch;
            if(ch == 8 && text.Length != 0)
                text = text[..^1];
        }
    }

    public new void Update()
    {
        Rectangle cursor = new(Mouse.GetState().Position.X, Mouse.GetState().Position.Y, 1, 1);
        if(cursor.Intersects(rectangle)){
            if(InputHandler.GetMouseOneShot(true)){
                focus = !focus;
            }
            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
                isPressed = true;
            else
                isPressed = false;
        }
    }
}
