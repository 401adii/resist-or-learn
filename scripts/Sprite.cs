using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class Sprite
{
    public Texture2D texture;
    public Vector2 position;
    public bool isVisible;

    public Sprite(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
        isVisible = true;
    }
    
    public Sprite(Texture2D texture, Vector2 position, bool isVisible)
    {
        this.texture = texture;
        this.position = position;
        this.isVisible = isVisible;
    }

    public virtual void Update(GameTime gameTime)
    {

    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if(isVisible){
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
