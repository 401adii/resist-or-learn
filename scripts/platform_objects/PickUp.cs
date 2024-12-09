using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class PickUp : Sprite
{
    public Game1.ResistorType resistor;
    public PickUp(Texture2D texture, Vector2 position, Game1.ResistorType resistor) : base(texture, position)
    {
        this.resistor = resistor;
    }

    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}
