using System.Collections.Generic;
using System.Threading.Channels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class MainMenu : MenuScene
{
    private Button playBtn;
    private Button selectLevelBtn;
    private Button settingsBtn;
    private Button exitBtn;
    
    public MainMenu(ContentManager contentManager) : base(contentManager) {}
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        
    }

    public override void Load()
    {
        nextState = Game1.GameState.main_menu;
        textureButton = contentManager.Load<Texture2D>(GUI_DEFAULT + BUTTON_BLUE);
        textureHover = contentManager.Load<Texture2D>(GUI_DEFAULT + BUTTON_BLUE  + HOVER);
        texturePressed = contentManager.Load<Texture2D>(GUI_DEFAULT + BUTTON_BLUE + PRESSED);
        playBtn = new Button(textureButton, new Vector2(100, 100), "PLAY", textureHover, texturePressed);
        selectLevelBtn = new Button(textureButton, new Vector2(100, 200), "CHOOSE LEVEL", textureHover, texturePressed);
        settingsBtn = new Button(textureButton, new Vector2(100, 300), "SETTINGS", textureHover, texturePressed);
        exitBtn = new Button(textureButton, new Vector2(100, 400), "QUIT", textureHover, texturePressed);
        buttons = [playBtn, selectLevelBtn, settingsBtn, exitBtn];
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if(playBtn.isPressed){
            nextState = Game1.GameState.load_next;
        }
        if(exitBtn.isPressed){
            nextState = Game1.GameState.exit;
        }   
        if(settingsBtn.isPressed){
            nextState = Game1.GameState.settings;
        }   
        if(selectLevelBtn.isPressed){
            nextState = Game1.GameState.level_select;
        }        
    }
}