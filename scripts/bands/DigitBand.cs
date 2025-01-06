using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class DigitBand : Band
{
    protected override Dictionary<Color, double> ValueMap { get; } = new()
    {
        {new Color(0f, 0f, 0f, 1f), 0},
        {new Color(0.28f, 0.16f, 0.09f, 1), 1},
        {new Color(0.89f, 0.08f, 0.16f, 1), 2},
        {new Color(0.96f, 0.42f, 0.11f, 1), 3},
        {new Color(1, 1, 0.25f, 1), 4},
        {new Color(0.42f, 0.7f, 0.15f, 1), 5},
        {new Color(0.19f, 0.34f, 0.74f, 1), 6},
        {new Color(0.7f, 0.29f, 0.93f, 1), 7},
        {new Color(0.4f, 0.36f, 0.35f, 1), 8},
        {new Color(1f, 1f, 1f, 1), 9},
    };
    public DigitBand(Texture2D texture, Vector2 position) : base(texture, position)
    {

    }
}
