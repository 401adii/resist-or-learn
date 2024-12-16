using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;

namespace resist_or_learn;

public static class SoundManager
{
    public static void Play(SoundEffect sfx)
    {
        if(Game1.sound)
        {
            sfx.Play();
        }
    }
}