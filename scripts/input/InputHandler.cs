using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

namespace resist_or_learn;

public static class InputHandler
{
    private static int keysPressed = 0;
    public static string GetSingleInput()
    {
        string str = "";
        if(Keyboard.GetState().GetPressedKeyCount() < keysPressed)
            keysPressed--;

        if(Keyboard.GetState().GetPressedKeyCount() > keysPressed){
            foreach(Keys key in Keyboard.GetState().GetPressedKeys()){
                str = ((char)key).ToString();
                keysPressed++;
            }
        }
        return str;
    }
}
