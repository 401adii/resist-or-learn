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

    public FinishedLevelMenu(ContentManager contentManager) : base(contentManager){}

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }

    public override void Load()
    {
        base.Load();
        continueBtn = new Button(textureButton, new Vector2(100, 100), "continue", textureHover, texturePressed);
        selectLevelBtn = new Button(textureButton, new Vector2(100, 200), "select level", textureHover, texturePressed);
        mainMenuBtn = new Button(textureButton, new Vector2(100, 300), "main menu", textureHover, texturePressed);
        buttons = [continueBtn, selectLevelBtn, mainMenuBtn];
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
