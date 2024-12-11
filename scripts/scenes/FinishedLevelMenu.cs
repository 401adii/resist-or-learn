using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class FinishedLevelMenu : IScene
{
    private ContentManager contentManager;
    private const string GUI_DEFAULT = "gui";
    private const string BUTTON_BLUE = "/button_blue";
    private const string HOVER = "_hover";
    private const string PRESSED = "_pressed";
    Texture2D texture;
    Texture2D textureHover;
    Texture2D texturePressed;
    public Button continueBtn;
    public Button mainMenuBtn;
    public Button selectLevelBtn;
    public Game1.GameState newState;

    public FinishedLevelMenu(ContentManager contentManager)
    {
        this.contentManager = contentManager;
        newState = 0;

    }
    public void Draw(SpriteBatch spriteBatch)
    {
        continueBtn.Draw(spriteBatch);
        selectLevelBtn.Draw(spriteBatch);
        mainMenuBtn.Draw(spriteBatch);
    }

    public void Load()
    {
        texture = contentManager.Load<Texture2D>(GUI_DEFAULT + BUTTON_BLUE);
        textureHover = contentManager.Load<Texture2D>(GUI_DEFAULT + BUTTON_BLUE + HOVER);
        texturePressed = contentManager.Load<Texture2D>(GUI_DEFAULT + BUTTON_BLUE + PRESSED);
        continueBtn = new Button(texture, new Vector2(100, 100), "continue", textureHover, texturePressed);
        selectLevelBtn = new Button(texture, new Vector2(100, 200), "select level", textureHover, texturePressed);
        mainMenuBtn = new Button(texture, new Vector2(100, 300), "main menu", textureHover, texturePressed);
    }

    public void Update(GameTime gameTime)
    {
        mainMenuBtn.Update();
        selectLevelBtn.Update();
        continueBtn.Update();
        if(mainMenuBtn.isPressed){
            newState = Game1.GameState.main_menu;
        }
        
        if(selectLevelBtn.isPressed){
            newState = Game1.GameState.level_select;
        }

        if(continueBtn.isPressed){
            newState = Game1.GameState.load_next;
        }
    }
}
