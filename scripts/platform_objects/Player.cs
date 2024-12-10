using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace resist_or_learn;

public class Player : Sprite
{
    public Vector2 velocity;
    public Player(Texture2D texture, Vector2 position) : base(texture, position)
    {
        velocity = new();
    }

    public override void Update(GameTime gameTime)
    {
        velocity = Vector2.Zero;
        if(Keyboard.GetState().IsKeyDown(Keys.D)){
            velocity.X += 2;
        }
        if(Keyboard.GetState().IsKeyDown(Keys.A)){
            velocity.X -= 2;
        }
        if(Keyboard.GetState().IsKeyDown(Keys.W)){
            velocity.Y -= 2;
        }
        if(Keyboard.GetState().IsKeyDown(Keys.S)){
            velocity.Y += 2;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}
