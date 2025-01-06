using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using resitor_or_learn;

namespace resist_or_learn;

public class PickUp : Sprite
{
    public Game1.ResistorType type;
    private float startPosY;
    private bool animationGoUp;
    private Timer oscTimer;
    private Timer moveTimer;
    public PickUp(Texture2D texture, Vector2 position) : base(texture, position)
    {
        
        startPosY = position.Y;
        animationGoUp = true;
        oscTimer = new Timer(0.8f);
        moveTimer = new Timer(0.05f);
        oscTimer.Start();
        moveTimer.Start();
    }

    public override void Update(GameTime gameTime)
    {         
        oscTimer.Update(gameTime);
        moveTimer.Update(gameTime);

        if (oscTimer.timeOut){
            animationGoUp = !animationGoUp;
        }
        
        if(moveTimer.timeOut){
            if(animationGoUp){
                position.Y -= 1;
            }
            else{
                position.Y += 1;
            }
        }

    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}
