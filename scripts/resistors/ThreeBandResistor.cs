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
        bands[0] = new DigitBand(bands[0].texture, bands[0].position);
        bands[1] = new DigitBand(bands[1].texture, bands[0].position);
        bands[2] = new MultiplierBand(bands[2].texture, bands[0].position);
    }

    protected override float getResistance()
    {
        return float.Parse(bands[0].value.ToString() + bands[1].value.ToString()) * bands[2].value;
    }

    protected override float getTolerance()
    {
        return -1;
    }
}
