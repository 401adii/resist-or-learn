using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

namespace resitor_or_learn;

public class Timer
{
    public double time;
    public bool oneShot;
    public bool active;
    public bool timeOut;
    private double elapsed;
    

    public Timer(double time, bool oneShot = false, bool active = false)
    {
        this.time = time;
        this.oneShot = oneShot;
        this.active = active;
        timeOut = false;
    }

    public void Start()
    {
        active = true;
    }

    public void Stop()
    {
        active = false;
    }

    public void Update(GameTime gameTime)
    {
        if(timeOut == true)
            timeOut = false;
        
        if(active)
        {
            elapsed += gameTime.ElapsedGameTime.TotalSeconds;
            if(elapsed >= time)
            {
                timeOut = true;
                elapsed = 0;
                if(oneShot)
                    active = false;
            }
        }
    }


}