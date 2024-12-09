using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace resist_or_learn;

public class Player : Sprite
{
    public Player(Texture2D texture, Vector2 position) : base(texture, position)
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        if(Keyboard.GetState().IsKeyDown(Keys.Right)){
            position.X += 2;
        }
        if(Keyboard.GetState().IsKeyDown(Keys.Left)){
            position.X -= 2;
        }
        if(Keyboard.GetState().IsKeyDown(Keys.Up)){
            position.Y -= 2;
        }
        if(Keyboard.GetState().IsKeyDown(Keys.Down)){
            position.Y += 2;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}
