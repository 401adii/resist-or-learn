using System.Collections;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace resist_or_learn;

public class Cheatsheet : Sprite
{

    private int startingPosY;
    private Rectangle hoverRect;
    private bool isUp;

    public Cheatsheet(Texture2D texture, Vector2 position, Vector2 dimensions) : base(texture, position, dimensions)
    {
        isVisible = Game1.cheatsheet;
        Debug.WriteLine(isVisible);
        startingPosY = (int)position.Y;
        isUp = false;
        hoverRect = new Rectangle((int)position.X, (int)position.Y, 140, 40);
    }

    public override void Update(GameTime gameTime)
    {
        Rectangle cursor = new(Mouse.GetState().Position.X, Mouse.GetState().Position.Y, 1, 1);
        if(cursor.Intersects(hoverRect))
            isUp = true;
        if(!cursor.Intersects(Rect))
            isUp = false;
        
        if(isUp)
            Show();
        else
            Hide();
    }

    private void Show()
    {
        if(position.Y >= startingPosY - 300)
            position.Y-=10;
    }

    private void Hide()
    {
        if(position.Y <= startingPosY)
            position.Y += 10;
    }
}