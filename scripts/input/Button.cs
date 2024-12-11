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
    public Texture2D hoverTexture;
    public Texture2D pressedTexture;
    public Texture2D defaultTexture;
    protected Vector2 textPosition;    
    protected Rectangle rectangle;
    public bool isPressed;
    public Button(Texture2D texture, Vector2 position, string text = "", Texture2D hoverTexture = null, Texture2D pressedTexture = null) : base(texture, position)
    {
        rectangle = new((int)position.X, (int)position.Y, texture.Width, texture.Height);
        isPressed = false;
        this.text = text;
        textPosition = new(position.X + 8,position.Y + 12);
        this.hoverTexture = hoverTexture;
        this.pressedTexture = pressedTexture;
        defaultTexture = texture;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        spriteBatch.DrawString(Game1.font, text, textPosition, Color.White);
    }
    public void Update()
    {
        Rectangle cursor = new(Mouse.GetState().Position.X, Mouse.GetState().Position.Y, 1, 1);
        if(cursor.Intersects(rectangle)){
            if(hoverTexture != null)
                texture = hoverTexture;
                textPosition = new(position.X + 8,position.Y + 12);
            if(Mouse.GetState().LeftButton == ButtonState.Pressed && pressedTexture != null){
                texture = pressedTexture;
                textPosition = new(position.X + 8,position.Y + 18);
            }
            if(InputHandler.GetMouseOneShot(false)){
                isPressed = true;
            }
            else{
                isPressed = false;
            }
        }
        else {
            texture = defaultTexture;
            textPosition = new(position.X + 8,position.Y + 12);
        }
    }
}