using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace resist_or_learn;

public class Button : Sprite
{
    public readonly string text;
    public readonly Vector2 textPosition;    
    private Rectangle rectangle;
    public bool isPressed;
    public Button(Texture2D texture, Vector2 position, string text = "") : base(texture, position)
    {
        rectangle = new((int)position.X, (int)position.Y, texture.Width, texture.Height);
        isPressed = false;
        this.text = text;
        textPosition = new(position.X + 8,position.Y + 12);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        spriteBatch.DrawString(Game1.font, text, textPosition, Color.White);
    }
    public virtual void UpdateState() {}
    public void Update()
    {
        Rectangle cursor = new(Mouse.GetState().Position.X, Mouse.GetState().Position.Y, 1, 1);
        if(cursor.Intersects(rectangle)){
            if(InputHandler.GetMouseOneShot(false)){
                isPressed = true;
                UpdateState();
            }
            else{
                isPressed = false;
            }
        }
    }
}