using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

namespace resist_or_learn;

public static class InputHandler
{
    private static bool isPressed = false;
    public static string GetSingleInput()
    {
        string str = "";

        if(Keyboard.GetState().GetPressedKeyCount() == 0){
            isPressed = false;
        }

        foreach(Keys key in Keyboard.GetState().GetPressedKeys()){
            if(Keyboard.GetState().GetPressedKeyCount() > 0 && !isPressed){
                str = ((char)key).ToString();
                isPressed = true;
            }
        }

        return str;
    }
}
