using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class Band : Sprite
{   
    protected virtual Dictionary<Color, float> ValueMap{ get; } = new()
    {
        {new Color(0f, 0f, 0f, 1f), 1}
    };
    public float value;
    public Color color;
    
    public Band(Texture2D texture, Vector2 position) : base(texture, position)
    {
        List<Color> keys = ValueMap.Keys.ToList();
        Random rand = new Random();
        color = keys[rand.Next(keys.Count)];
        value = ValueMap[color];
        Debug.WriteLine(value);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, color);
    }

}
