using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class FinishedLevelMenu : MenuScene
{
    public Button continueBtn;
    public Button mainMenuBtn;
    public Button selectLevelBtn;
    private Texture2D background;
    

    public FinishedLevelMenu(ContentManager contentManager) : base(contentManager){}

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(background, new Vector2(494, 379), Color.White);
        base.Draw(spriteBatch);
    }

    public override void Load()
    {
        base.Load();
        continueBtn = new Button(textureButton, new Vector2(510, 443), "CONTINUE", textureHover, texturePressed);
        selectLevelBtn = new Button(textureButton, new Vector2(510, 539), "CHOOSE LEVEL", textureHover, texturePressed);
        mainMenuBtn = new Button(textureButton, new Vector2(510, 635), "MAIN MENU", textureHover, texturePressed);
        buttons = [continueBtn, selectLevelBtn, mainMenuBtn];
        background = contentManager.Load<Texture2D>("gui/menu_background");
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if(mainMenuBtn.isPressed){
            nextState = Game1.GameState.main_menu;
        }
        
        if(selectLevelBtn.isPressed){
            nextState = Game1.GameState.level_select;
        }

        if(continueBtn.isPressed){
            nextState = Game1.GameState.load_next;
        }
    }
}
