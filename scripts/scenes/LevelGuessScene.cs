using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace resist_or_learn;

public class LevelGuessScene : IScene
{
    private ContentManager contentManager;


    //CONSTS AND ENUMS
    enum ResistorType{
        three_band = 3,
        four_band = 4,
        five_band = 5,
    };
    private const string RESISTOR_BAND_DEFAULT = "/band";
    private const string RESISTOR_BASE_DEFAULT = "/base";
    private const string GUI_DEFAULT = "gui";
    private const string BUTTON_BLUE = "/button_blue";
    private const string BUTTON_GREEN = "/button_green";
    private const string BUTTON_BLUE_FOCUS = "/button_blue_focus";
    private const string ERROR = "gui/error";
    private const string WRONG = "gui/wrong";
    private const string CORRECT = "gui/correct";
    
    
    //TEXUTRES
    private Texture2D threeBandBaseTexture;
    private Texture2D fourBandBaseTexture;
    private Texture2D fiveBandBaseTexture;
    private List<Texture2D> threeBandTexture = [];
    private List<Texture2D> fourBandTexture = [];
    private List<Texture2D> fiveBandTexture = [];
    private Texture2D buttonTexture;
    private Texture2D buttonTextureFocus;
    private Texture2D buttonTextureGreen;
    private Texture2D errorTexture;
    private Texture2D correctTexture;
    private Texture2D wrongTexture;
    
    //OBJECTS
    private Resistor resistor;
    private InputBox resistanceInput;
    private InputBox toleranceInput;
    private Button submitButton;
    private Sprite resistanceError;
    private Sprite toleranceError;
    private Sprite resistanceWrong;
    private Sprite toleranceWrong;
    private Sprite resistanceCorrect;
    private Sprite toleranceCorrect;

    //FLAGS
    private bool isSubmitted;
    private bool isCorrect;
    
    public LevelGuessScene(ContentManager contentManager)
    {
        this.contentManager = contentManager;
        isSubmitted = false;
        isCorrect = false;
    }

    public void Load()
    {
        //texture loading
        LoadResistorBaseTextures();
        LoadResistorBandTextures(threeBandTexture, ResistorType.three_band); 
        LoadResistorBandTextures(fourBandTexture, ResistorType.four_band); 
        LoadResistorBandTextures(fiveBandTexture, ResistorType.five_band); 
        buttonTexture = contentManager.Load<Texture2D>(GUI_DEFAULT + BUTTON_BLUE);
        buttonTextureFocus = contentManager.Load<Texture2D>(GUI_DEFAULT + BUTTON_BLUE_FOCUS);
        buttonTextureGreen = contentManager.Load<Texture2D>(GUI_DEFAULT + BUTTON_GREEN);
        errorTexture = contentManager.Load<Texture2D>(ERROR);
        wrongTexture = contentManager.Load<Texture2D>(WRONG);
        correctTexture = contentManager.Load<Texture2D>(CORRECT);
        
        //creating objects
        resistor = CreateResistor(ResistorType.four_band);
        resistanceInput = new InputBox(buttonTexture, new Vector2(800, 150));
        toleranceInput = new InputBox(buttonTexture, new Vector2(800, 260), true);
        submitButton = new Button(buttonTextureGreen, new Vector2(800, 330), "          SUBMIT");
        resistanceError = new Sprite(errorTexture, new Vector2(1075, 145), false);
        toleranceError = new Sprite(errorTexture, new Vector2(1075, 255), false);
        resistanceWrong = new Sprite(wrongTexture, new Vector2(1075, 145), false);
        toleranceWrong = new Sprite(wrongTexture, new Vector2(1075, 255), false);
        resistanceCorrect = new Sprite(correctTexture, new Vector2(1075, 145), false);
        toleranceCorrect = new Sprite(correctTexture, new Vector2(1075, 255), false);
    }

    public void Update(GameTime gameTime)
    {
        FocusControl();
        resistanceInput.Update();
        resistanceInput.HandleInput();
        toleranceInput.Update();
        toleranceInput.HandleInput();
        submitButton.Update();
        if(submitButton.isPressed){
            HandleSubmit();
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        resistor.Draw(spriteBatch);
        spriteBatch.DrawString(Game1.font, "Resistance: ", new Vector2(800, 120), Color.White);
        resistanceInput.Draw(spriteBatch);
        toleranceInput.Draw(spriteBatch);
        spriteBatch.DrawString(Game1.font, "Tolerance: ", new Vector2(800, 230), Color.White);
        submitButton.Draw(spriteBatch);
        if(resistanceError.isVisible == true)
            resistanceError.Draw(spriteBatch);
        if(toleranceError.isVisible == true)
            toleranceError.Draw(spriteBatch);
        if(resistanceCorrect.isVisible == true)
            resistanceCorrect.Draw(spriteBatch);
        if(toleranceCorrect.isVisible == true)
            toleranceCorrect.Draw(spriteBatch);
        if(resistanceWrong.isVisible == true)
            resistanceWrong.Draw(spriteBatch);
        if(toleranceWrong.isVisible == true)
            toleranceWrong.Draw(spriteBatch);
    }

    private void LoadResistorBaseTextures()
    {
        threeBandBaseTexture = contentManager.Load<Texture2D>(ResistorType.three_band.ToString() + RESISTOR_BASE_DEFAULT);
        fourBandBaseTexture = contentManager.Load<Texture2D>(ResistorType.four_band.ToString() + RESISTOR_BASE_DEFAULT);
        fiveBandBaseTexture = contentManager.Load<Texture2D>(ResistorType.five_band.ToString() + RESISTOR_BASE_DEFAULT);
    }

    private void LoadResistorBandTextures(List<Texture2D> l, ResistorType t)
    {
        for(int i = 0; i < (int)t; i++)
        {
            Texture2D texture = contentManager.Load<Texture2D>(t.ToString() + RESISTOR_BAND_DEFAULT + (i+1).ToString());
            l.Add(texture);
        }
    }

    private List<Band> LoadResistorBandsList(List<Texture2D> textures)
    {
        List<Band> resistorBandList = [];
        for(int i = 0; i < textures.Count; i++)
        {
            resistorBandList.Add(new Band(textures[i], Vector2.Zero));
        }
        return resistorBandList;
    }

    private Resistor CreateResistor(ResistorType t)
    {
        Resistor r = null;
        switch(t){
        case ResistorType.three_band:
            r = new ThreeBandResistor(threeBandBaseTexture, new Vector2(100, 120), LoadResistorBandsList(threeBandTexture));
            break;
        case ResistorType.four_band:
            r = new FourBandResistor(fourBandBaseTexture, new Vector2(100, 112), LoadResistorBandsList(fourBandTexture));
            break;
        case ResistorType.five_band:
            r = new FiveBandResistor(fiveBandBaseTexture, new Vector2(100, 100), LoadResistorBandsList(fiveBandTexture));
            break;
        }

        return r;
    }

    public void FocusControl()
    {   
        if(resistanceInput.isPressed && toleranceInput.focus)
            toleranceInput.focus = false;
        
        if(toleranceInput.isPressed && resistanceInput.focus)
            resistanceInput.focus = false;
        
        if(resistanceInput.focus){
            resistanceInput.texture = buttonTextureFocus;
        }
        else{
            resistanceInput.texture = buttonTexture;
        }
        if(toleranceInput.focus){
            toleranceInput.texture = buttonTextureFocus;
        }
        else{
            toleranceInput.texture = buttonTexture;
        }
    }
    public void HandleSubmit()
    {
        if(isSubmitted)
            return;

        double inputResistanceValue = InputHandler.ConvertStringToEng(resistanceInput.text);
        double inputToleranceValue = InputHandler.ConvertStringToEng(toleranceInput.text);

        if(inputResistanceValue == -1){
            Debug.WriteLine("resistance error");
            resistanceError.isVisible = true;
        }
        else{
            resistanceError.isVisible = false;
            resistanceInput.enabled = false;
        }

        if(inputToleranceValue == -1){
            Debug.WriteLine("tolerance error");
            toleranceError.isVisible = true;
        }
        else{
            toleranceError.isVisible = false;
            toleranceInput.enabled = false;
        }

        if(toleranceError.isVisible || resistanceError.isVisible)
            return;
        
        isSubmitted = true;
        Debug.WriteLine("Input resistance: " + inputResistanceValue);
        Debug.WriteLine("Input tolerance: " + inputToleranceValue);
        
        if(inputResistanceValue == resistor.resistance)
            resistanceCorrect.isVisible = true;
        else
            resistanceWrong.isVisible = true;

        if(inputToleranceValue == resistor.tolerance)
            toleranceCorrect.isVisible = true;
        else
            toleranceWrong.isVisible = true;

        if(resistanceCorrect.isVisible && toleranceCorrect.isVisible)
            isCorrect = true;
    }
}
