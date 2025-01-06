using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class FiveBandResistor : Resistor
{
    public FiveBandResistor(Texture2D texture, Vector2 position, List<Band> bands) : base(texture, position, bands){}

    protected override void changeBandTypes()
    {
        bands[0] = new DigitBand(bands[0].texture, new Vector2(position.X + 34*4, position.Y));
        bands[1] = new DigitBand(bands[1].texture, new Vector2(position.X + 48*4, position.Y));
        bands[2] = new DigitBand(bands[2].texture, new Vector2(position.X + 62*4, position.Y+8));
        bands[3] = new MultiplierBand(bands[3].texture, new Vector2(position.X + 76*4, position.Y+8));
        bands[4] = new ToleranceBand(bands[4].texture, new Vector2(position.X + 104*4, position.Y));
    }

    protected override double getResistance()
    {
        return double.Parse(bands[0].value.ToString() + bands[1].value.ToString() + bands[2].value.ToString()) * bands[3].value;
    }

    protected override double getTolerance()
    {
        return bands[4].value;
    }
}
