using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Xna.Framework.Input;

namespace resist_or_learn;

public static class InputHandler
{  

    static KeyboardState prevKeyboardState = new();
    static MouseState prevMouseState = new();
    
    public static char GetSingleInput()
    {
        char ch = '\0';
        foreach(Keys key in Keyboard.GetState().GetPressedKeys()){
            if((key == Keys.OemPeriod || key == Keys.OemComma) && prevKeyboardState != Keyboard.GetState()){
                ch = '.';
            }
            if((key >= Keys.D0 && key <= Keys.Z || key == Keys.Back) && prevKeyboardState != Keyboard.GetState()){
                if(Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                    ch = (char)key;
                else
                    ch = char.ToLower((char)key);
            }
        }
        prevKeyboardState = Keyboard.GetState();
        return ch;
    }

    public static double ConvertStringToEng(string s)
    {
        Dictionary<char,double> suffixes = new()
        {
            { 'm', 0.001 },
            { 'k', 1000 },
            { 'M', 1000000 },
            { 'G', 1000000000 }
        };
        string number = "";
        char suffix = '\0';

        foreach(char ch in s){
            if(Char.IsDigit(ch) || ch == '.')
                number += ch;
            else{
                if(suffixes.ContainsKey(ch) && suffix == '\0'){
                    suffix = ch;
                }
                else{
                    return -1;
                }
            }
        }

        if(number == ""){
            return -1;
        }

        if (double.TryParse(number, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out double output)){
            if(suffix == '\0')
                return output;
            else
                return output * suffixes[suffix];
        }

        return -1;
    }

    public static bool GetMouseOneShot(bool onPress)
    {
        MouseState state;
        bool output = false;
        state = Mouse.GetState();
        if(onPress){
            if(state.LeftButton == ButtonState.Pressed && state.LeftButton != prevMouseState.LeftButton){
                output = true;
            }
        }
        else{
            if(state.LeftButton == ButtonState.Released && state.LeftButton != prevMouseState.LeftButton){
                output = true;
            }
        }
        prevMouseState = Mouse.GetState();
        return output;
    }
}
