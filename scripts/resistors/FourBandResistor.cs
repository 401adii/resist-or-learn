using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class FourBandResistor : Resistor
{
    public FourBandResistor(Texture2D texture, Vector2 position, List<Band> bands) : base(texture, position, bands){}

    protected override void changeBandTypes()
    {
        bands[0] = new DigitBand(bands[0].texture, new Vector2(position.X + 38*4, position.Y));
        bands[1] = new DigitBand(bands[1].texture, new Vector2(position.X + 55*4, position.Y));
        bands[2] = new MultiplierBand(bands[2].texture, new Vector2(position.X + 72*4, position.Y + 3*4));
        bands[3] = new ToleranceBand(bands[3].texture, new Vector2(position.X + 99*4, position.Y));
    }

    protected override float getResistance()
    {
        return float.Parse(bands[0].value.ToString() + bands[1].value.ToString()) * bands[2].value;
    }

    protected override float getTolerance()
    {
        return bands[3].value;
    }
}
