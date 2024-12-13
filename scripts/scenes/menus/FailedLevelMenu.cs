using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class FailedLevelMenu : MenuScene
{
    public Button menuBtn;
    public Button selectLevelBtn;
    public Button retryBtn;
    public FailedLevelMenu(ContentManager contentManager) : base(contentManager) {}

    public override void Load()
    {
        nextState = 0;
        base.Load();
        buttons = [
            retryBtn = new Button(textureButton, new Vector2(100, 100), "RETRY", textureHover, texturePressed),
            menuBtn = new Button(textureButton, new Vector2(100, 200), "BACK TO MENU", textureHover, texturePressed),
            selectLevelBtn = new Button(textureButton, new Vector2(100, 300), "CHOOSE LEVEL", textureHover, texturePressed),
        ];
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (menuBtn.isPressed){
            nextState = Game1.GameState.main_menu;
        }
        if(selectLevelBtn.isPressed){
            nextState = Game1.GameState.level_select;
        }
        if(retryBtn.isPressed){
            nextState = Game1.GameState.load_next;
        }
    }
}