using System;
using System.Diagnostics;
using System.Formats.Tar;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class PickUp : Sprite
{
    public Game1.ResistorType resistor;
    private float startPosY;
    private bool animationGoUp;
    private int counter;
    public PickUp(Texture2D texture, Vector2 position, Game1.ResistorType resistor) : base(texture, position)
    {
        this.resistor = resistor;
        startPosY = position.Y;
        animationGoUp = true;
        counter = 0;
    }

    public override void Update(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        counter++;
        if(delta * counter >= 0.1f){
            if(animationGoUp){
                position.Y -= 1;
            }
            else{
                position.Y += 1;
            }
            counter = 0;
        }                
        
        if(position.Y <= startPosY - 4){
            animationGoUp = false;
            position.Y = startPosY - 4;
        }

        if(position.Y >= startPosY + 4){
            animationGoUp = true;
            position.Y = startPosY + 4;
        }

    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}
