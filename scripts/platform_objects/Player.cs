using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace resist_or_learn;

public class Player : Sprite
{
    public Vector2 velocity;
    public int jumpCount;
    public bool grounded;
    public Player(Texture2D texture, Vector2 position, Vector2 singleDimensions) : base(texture, position, singleDimensions)
    {
        velocity = new();
        grounded = false;
        jumpCount = 2;
    }

    public void Update(KeyboardState currState, KeyboardState prevState, GameTime gameTime)
    {
        animationManager.Update(gameTime);
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        velocity.X = 0;
        velocity.Y += 40 * delta;
        if(velocity.Y > 30){
            velocity.Y = 30;
        }
        if(currState.IsKeyDown(Keys.D)){
            velocity.X += 250 * delta;
        }
        if(currState.IsKeyDown(Keys.A)){
            velocity.X -= 250 * delta;
        }

        if(currState.IsKeyDown(Keys.W) && !prevState.IsKeyDown(Keys.W) && jumpCount > 0){
            velocity.Y = -500 * delta;
            jumpCount--;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, Rect, animationManager.GetFrame(), Color.White);
    }
}
