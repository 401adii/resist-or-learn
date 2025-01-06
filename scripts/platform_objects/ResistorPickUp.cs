using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class ResistorPickUp : PickUp
{
    public ResistorPickUp(Texture2D texture, Vector2 position, Game1.ResistorType type) : base(texture, position)
    {
        this.type = type;
    }
}
