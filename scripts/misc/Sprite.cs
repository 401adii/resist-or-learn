using System;
using System.Diagnostics;
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
    public Rectangle Rect
    {
        get
        {
            if(texture.Width == dimensions.X)
                return new Rectangle((int) position.X, (int)position.Y, texture.Width, texture.Height);
            else
                return new Rectangle((int) position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
        }
    }
    public Rectangle DisplayRect
    {
        get
        {
            return new Rectangle(colPos*(int)dimensions.X, rowPos*(int)dimensions.Y, (int)dimensions.X, (int)dimensions.Y);
        }
    }

    public Vector2 dimensions;
    public int colPos;
    public int rowPos;

    public Sprite(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
        dimensions = new Vector2(texture.Width, texture.Height);
        isVisible = true;
        colPos = 0;
        rowPos = 0;
    }

    public Sprite(Texture2D texture, Vector2 position, Vector2 dimensions)
    {
        this.texture = texture;
        this.position = position;
        this.dimensions = dimensions;
        isVisible = true;
        colPos = 0;
        rowPos = 0;
    }
    
    public Sprite(Texture2D texture, Vector2 position, bool isVisible)
    {
        this.texture = texture;
        this.position = position;
        this.isVisible = isVisible;
        dimensions = new Vector2(texture.Width, texture.Height);
        colPos = 0;
        rowPos = 0;
    }

    public virtual void Update(GameTime gameTime)
    {

    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if(isVisible){
            spriteBatch.Draw(texture, Rect, DisplayRect, Color.White);
        }
    }
}
