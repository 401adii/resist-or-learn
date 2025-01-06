using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class ToleranceBand : Band
{
    protected override Dictionary<Color, double> ValueMap { get; }= new ()
    {
        {new Color(0.28f, 0.16f, 0.09f, 1), 1},
        {new Color(0.89f, 0.08f, 0.16f, 1), 2},
        {new Color(0.42f, 0.7f, 0.15f, 1), 0.5f},
        {new Color(0.19f, 0.34f, 0.74f, 1), 0.25f},
        {new Color(0.7f, 0.29f, 0.93f, 1), 0.1f},
        {new Color(0.98f, 0.78f, 0, 1), 5},
        {new Color(0.87f, 0.91f, 0.89f, 1), 10},
    };
    public ToleranceBand(Texture2D texture, Vector2 position) : base(texture, position)
    {

    }
}
