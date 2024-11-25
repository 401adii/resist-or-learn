using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace resist_or_learn;

public class Button : Sprite
{
    private Rectangle rectangle;
    public bool isPressed;
    public Button(Texture2D texture, Vector2 position) : base(texture, position)
    {
        rectangle = new((int)position.X, (int)position.Y, texture.Width, texture.Height);
        isPressed = false;
    }

    public virtual void UpdateState() {}
    public void Update()
    {
        Rectangle cursor = new(Mouse.GetState().Position.X, Mouse.GetState().Position.Y, 1, 1);
        if(cursor.Intersects(rectangle)){
            if(Mouse.GetState().LeftButton == ButtonState.Pressed){
                isPressed = true;
            }
            if(Mouse.GetState().LeftButton == ButtonState.Released && isPressed){
                UpdateState();
                isPressed = false;
            }
        }
        else{
            isPressed = false;
        }
    }
}