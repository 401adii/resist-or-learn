using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class ThreeBandResistor : Resistor
{
    public ThreeBandResistor(Texture2D texture, Vector2 position, List<Band> bands) : base(texture, position, bands){}

    protected override void changeBandTypes()
    {
        bands[0] = new DigitBand(bands[0].texture, new Vector2(position.X + 49*4, position.Y));
        bands[1] = new DigitBand(bands[1].texture, new Vector2(position.X + 62*4, position.Y));
        bands[2] = new MultiplierBand(bands[2].texture, new Vector2(position.X + 90*4, position.Y));
    }

    protected override double getResistance()
    {
        return double.Parse(bands[0].value.ToString() + bands[1].value.ToString()) * bands[2].value;
    }

    protected override double getTolerance()
    {
        return -1;
    }
}
