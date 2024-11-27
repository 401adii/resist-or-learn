using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class Error : Sprite{
    public bool isVisible;
    public Error(Texture2D texture, Vector2 position) : base(texture, position){
        isVisible = false;
    }
}
