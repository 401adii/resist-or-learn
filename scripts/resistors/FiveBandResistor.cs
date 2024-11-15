using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class FiveBandResistor : Resistor
{
    public FiveBandResistor(Texture2D texture, Vector2 position, List<Band> bands) : base(texture, position, bands){}

    protected override void changeBandTypes()
    {
        bands[0] = new DigitBand(bands[0].texture, bands[0].position);
        bands[1] = new DigitBand(bands[1].texture, bands[0].position);
        bands[2] = new DigitBand(bands[2].texture, bands[0].position);
        bands[3] = new MultiplierBand(bands[3].texture, bands[0].position);
        bands[4] = new ToleranceBand(bands[4].texture, bands[0].position);
    }

    protected override float getResistance()
    {
        return float.Parse(bands[0].value.ToString() + bands[1].value.ToString() + bands[2].value.ToString()) * bands[3].value;
    }

    protected override float getTolerance()
    {
        return bands[4].value;
    }
}
