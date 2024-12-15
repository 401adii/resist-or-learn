using System.Diagnostics;
using Microsoft.Xna.Framework;
using resitor_or_learn;

namespace resist_or_learn;

public class AnimationManager
{
    private int frames;
    private int cols;
    private Vector2 size;
    private int time;
    private Timer timer;
    private int activeFrame;
    private int rowPos;
    private int colPos;
    
    public AnimationManager(int frames, int cols, Vector2 size, int time)
    {
        this.frames = frames;
        this.cols = cols;
        this.size = size;
        this.time = time;
        timer = new Timer(time);
        timer.Start();
        activeFrame = 0;
        rowPos = 0;
        colPos = 0;
    }

    public void Update(GameTime gameTime)
    {
        timer.Update(gameTime);
        if(timer.timeOut)
        {
            activeFrame++;
            colPos++;
            Debug.WriteLine(activeFrame);
            if(activeFrame >= frames)
            {
                activeFrame = 0;
                colPos = 0;
                rowPos = 0;
            }

            if(colPos > cols)
            {
                colPos = 0;
                rowPos++;
            }
        }
    }

    public void ResetAnimation()
    {
        activeFrame = 0;
        colPos = 0;
        rowPos = 0;
    }

    public Rectangle GetFrame()
    {
        Rectangle test = new Rectangle(colPos * (int)size.X, rowPos * (int)size.Y, (int)size.X, (int)size.Y);
        Debug.WriteLine(test);
        return test;//new Rectangle(colPos * (int)size.X, rowPos * (int)size.Y, (int)size.X, (int)size.Y);
    }
}