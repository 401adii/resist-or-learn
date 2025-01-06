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
    private Texture2D logo;
    
    public MainMenu(ContentManager contentManager) : base(contentManager) {}
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(logo, new Vector2(16, 16), Color.White);
        base.Draw(spriteBatch);
        
    }

    public override void Load()
    {
        base.Load();
        nextState = Game1.GameState.main_menu;
        playBtn = new Button(textureButton, new Vector2(16, 320), "PLAY", textureHover, texturePressed);
        selectLevelBtn = new Button(textureButton, new Vector2(16, 400), "CHOOSE LEVEL", textureHover, texturePressed);
        settingsBtn = new Button(textureButton, new Vector2(16, 480), "SETTINGS", textureHover, texturePressed);
        exitBtn = new Button(textureButton, new Vector2(16, 560), "QUIT", textureHover, texturePressed);
        buttons = [playBtn, selectLevelBtn, settingsBtn, exitBtn];
        logo = contentManager.Load<Texture2D>("gui/logo");
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