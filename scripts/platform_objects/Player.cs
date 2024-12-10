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
        if(Keyboard.GetState().IsKeyDown(Keys.D)){
            position.X += 2;
        }
        if(Keyboard.GetState().IsKeyDown(Keys.A)){
            position.X -= 2;
        }
        if(Keyboard.GetState().IsKeyDown(Keys.W)){
            position.Y -= 2;
        }
        if(Keyboard.GetState().IsKeyDown(Keys.S)){
            position.Y += 2;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}
