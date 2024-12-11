using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class Resistor : Sprite
{
    public double resistance;
    public double tolerance;
    public List<Band> bands;

    public Resistor(Texture2D texture, Vector2 position, List<Band> bands) : base(texture, position)
    {
        this.bands = bands;
        changeBandTypes();
        resistance = getResistance();
        tolerance = getTolerance();
        Debug.WriteLine("Resistance: " + resistance.ToString());
        Debug.WriteLine("Tolerance " + tolerance.ToString());
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        foreach(Band band in bands)
            band.Draw(spriteBatch);
    }

    protected virtual void changeBandTypes()
    {

    }

    protected virtual double getResistance()
    {
        return 0.00f;
    }

    protected virtual double getTolerance()
    {
        return 0.00f;
    }
    
}
