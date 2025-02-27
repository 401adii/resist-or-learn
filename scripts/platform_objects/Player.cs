using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace resist_or_learn;

public class Player : Sprite
{
    public Vector2 velocity;
    public int jumpCount;
    public bool grounded;
    private enum Controls{
        JUMP, LEFT, RIGHT
    }
    private Dictionary<Controls, Keys> chosenControls;
    bool direction;
    
    public Player(Texture2D texture, Vector2 position, Vector2 singleDimensions) : base(texture, position, singleDimensions)
    {
        velocity = new();
        chosenControls = new();
        grounded = false;
        jumpCount = 2;
        direction = true;
        GetControls();
    }

    public void Update(KeyboardState currState, KeyboardState prevState, GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        velocity.X = 0;
        velocity.Y += 40 * delta;

        
        if(velocity.Y > 30){
            velocity.Y = 30;
        }
        if(currState.IsKeyDown(chosenControls[Controls.RIGHT])){
            velocity.X += 250 * delta;
            direction = true;
        }
        if(currState.IsKeyDown(chosenControls[Controls.LEFT])){
            velocity.X -= 250 * delta;
            direction = false;
        }

        if(currState.IsKeyDown(chosenControls[Controls.JUMP]) && !prevState.IsKeyDown(chosenControls[Controls.JUMP]) && jumpCount > 0){
            velocity.Y = -600 * delta;
            jumpCount--;
        }
    
    }

    public override void Draw(SpriteBatch spriteBatch){
        if(direction)
            spriteBatch.Draw(texture, Rect, DisplayRect, Color.White);
        else
           spriteBatch.Draw(texture, Rect, DisplayRect, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
    }

    private void GetControls()
    {
        if(Game1.controls){
            chosenControls.Add(Controls.JUMP, Keys.Up);
            chosenControls.Add(Controls.LEFT, Keys.Left);
            chosenControls.Add(Controls.RIGHT, Keys.Right);
        }
        else{
            chosenControls.Add(Controls.JUMP, Keys.W);
            chosenControls.Add(Controls.LEFT, Keys.A);
            chosenControls.Add(Controls.RIGHT, Keys.D);
        }
    }
}
