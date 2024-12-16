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

    private const string RESISTOR_BAND_DEFAULT = "/band";
    private const string RESISTOR_BASE_DEFAULT = "/base";
    private const string GUI_DEFAULT = "gui";
    private const string BUTTON_BLUE = "/button_blue";
    private const string BUTTON_GREEN = "/button_green";
    private const string BUTTON_BLUE_FOCUS = "/button_blue_focus";
    private const string HOVER = "_hover";
    private const string PRESSED = "_pressed";
    private const string ERROR = "gui/error";
    private const string WRONG = "gui/wrong";
    private const string CORRECT = "gui/correct";
    private const string CHEATSHEET = "/cheatsheet";
    private readonly Dictionary<Game1.ResistorType, Vector2> RESISTOR_POSITIONS = new()
    {
        {Game1.ResistorType.three_band, new Vector2(100, 120)},
        {Game1.ResistorType.four_band, new Vector2(100, 112)},
        {Game1.ResistorType.five_band, new Vector2(100, 100)},
    };
    
    //TEXUTRES
    private Texture2D baseTexture;
    private List<Texture2D> bandTextures = [];
    private Texture2D buttonTexture;
    private Texture2D buttonTextureFocus;
    private Texture2D buttonTextureGreen;
    private Texture2D buttonTextureGreenHover;
    private Texture2D buttonTextureGreenPressed;
    private Texture2D errorTexture;
    private Texture2D correctTexture;
    private Texture2D wrongTexture;
    private Texture2D cheatsheetTexture;
    
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
    private Cheatsheet cheatsheet;

    //FLAGS AND PUBLIC VARIABLES
    public bool isSubmitted;
    public  bool isCorrect;
    public Game1.ResistorType resistorType;
    
    public LevelGuessScene(ContentManager contentManager, Game1.ResistorType resistorType)
    {
        this.contentManager = contentManager;
        isSubmitted = false;
        isCorrect = false;
        this.resistorType = resistorType;
    }

    public void Load()
    {
        //loading and creating a resistor object
        switch(resistorType){
        case Game1.ResistorType.three_band:
            LoadResistorTextures(); 
            resistor = new ThreeBandResistor(baseTexture, RESISTOR_POSITIONS[resistorType], LoadResistorBandsList(bandTextures));
            break;
        case Game1.ResistorType.four_band:
            LoadResistorTextures(); 
            resistor = new FourBandResistor(baseTexture, RESISTOR_POSITIONS[resistorType], LoadResistorBandsList(bandTextures));
            break;
        case Game1.ResistorType.five_band:
            LoadResistorTextures();
            resistor = new FiveBandResistor(baseTexture, RESISTOR_POSITIONS[resistorType], LoadResistorBandsList(bandTextures)); 
            break;
        default:
            break;
        }

        //loading other textures
        buttonTexture = contentManager.Load<Texture2D>(GUI_DEFAULT + BUTTON_BLUE);
        buttonTextureFocus = contentManager.Load<Texture2D>(GUI_DEFAULT + BUTTON_BLUE_FOCUS);
        buttonTextureGreen = contentManager.Load<Texture2D>(GUI_DEFAULT + BUTTON_GREEN);
        buttonTextureGreenHover = contentManager.Load<Texture2D>(GUI_DEFAULT + BUTTON_GREEN + HOVER);
        buttonTextureGreenPressed = contentManager.Load<Texture2D>(GUI_DEFAULT + BUTTON_GREEN + PRESSED);
        errorTexture = contentManager.Load<Texture2D>(ERROR);
        wrongTexture = contentManager.Load<Texture2D>(WRONG);
        correctTexture = contentManager.Load<Texture2D>(CORRECT);
        cheatsheetTexture = contentManager.Load<Texture2D>(GUI_DEFAULT + CHEATSHEET);
        
        //creating other objects
        resistanceInput = new InputBox(buttonTexture, new Vector2(800, 150));
        toleranceInput = new InputBox(buttonTexture, new Vector2(800, 260), true);
        toleranceInput.enabled = Game1.tolerance;
        submitButton = new Button(buttonTextureGreen, new Vector2(800, 330), "          SUBMIT", buttonTextureGreenPressed, buttonTextureGreenHover);
        resistanceError = new Sprite(errorTexture, new Vector2(1075, 145), false);
        toleranceError = new Sprite(errorTexture, new Vector2(1075, 255), false);
        resistanceWrong = new Sprite(wrongTexture, new Vector2(1075, 145), false);
        toleranceWrong = new Sprite(wrongTexture, new Vector2(1075, 255), false);
        resistanceCorrect = new Sprite(correctTexture, new Vector2(1075, 145), false);
        toleranceCorrect = new Sprite(correctTexture, new Vector2(1075, 255), false);
        cheatsheet = new Cheatsheet(cheatsheetTexture, new Vector2(100, 680), new Vector2(640, 320));
    }

    public void Update(GameTime gameTime)
    {
        cheatsheet.Update(gameTime);
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
        cheatsheet.Draw(spriteBatch);
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

    private void LoadResistorTextures()
    {
        baseTexture = contentManager.Load<Texture2D>(resistorType.ToString() + RESISTOR_BASE_DEFAULT);
        for(int i = 0; i < (int)resistorType; i++)
        {
            Texture2D texture = contentManager.Load<Texture2D>(resistorType.ToString() + RESISTOR_BAND_DEFAULT + (i+1).ToString());
            bandTextures.Add(texture);
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

    private void FocusControl()
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
    private void HandleSubmit()
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

        if(inputToleranceValue == -1 && toleranceInput.enabled){
            Debug.WriteLine("tolerance error");
            toleranceError.isVisible = true;
        }
        else if (inputToleranceValue != -1 && toleranceInput.enabled){
            toleranceError.isVisible = false;
            toleranceInput.enabled = false;
        }
        else{
            toleranceError.isVisible = false;
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

        if(inputToleranceValue == -1)
            toleranceCorrect.isVisible = true;
        else if(inputToleranceValue == resistor.tolerance)
            toleranceCorrect.isVisible = true;
        else
            toleranceWrong.isVisible = true;

        if(resistanceCorrect.isVisible && toleranceCorrect.isVisible)
            isCorrect = true;
    }
}
