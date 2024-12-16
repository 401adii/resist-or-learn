using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class Healthbar : Sprite
{
    public Healthbar(Texture2D texture, Vector2 position, Vector2 dimensions, int health) : base(texture, position, dimensions)
    {
        colPos = 3 - health;
    }

    public void UpdateTexture(bool shift)
    {  
        if(shift)
            colPos++;
        else
            colPos--;
        if(rowPos >= 3)
        {
            colPos = 0;
        }
        
    }
}