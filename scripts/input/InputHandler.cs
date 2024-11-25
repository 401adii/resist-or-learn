using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Input;

namespace resist_or_learn;

public static class InputHandler
{  

    static KeyboardState prevState = new();
    
    public static char GetSingleInput()
    {
        char ch = '\0';
        foreach(Keys key in Keyboard.GetState().GetPressedKeys()){
            if((key >= Keys.D0 && key <= Keys.Z || key == Keys.Back) && prevState != Keyboard.GetState()){
                if(Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                    ch = (char)key;
                else
                    ch = char.ToLower((char)key);
            }
        }
        prevState = Keyboard.GetState();
        return ch;
    }
}
