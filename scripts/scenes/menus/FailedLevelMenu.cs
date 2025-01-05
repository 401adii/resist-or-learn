using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class FailedLevelMenu : MenuScene
{
    public Button menuBtn;
    public Button selectLevelBtn;
    public Button retryBtn;
    private Texture2D background;
    public FailedLevelMenu(ContentManager contentManager) : base(contentManager) {}


    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(background, new Vector2(494, 379), Color.White);
        base.Draw(spriteBatch);
    }
    public override void Load()
    {
        nextState = 0;
        base.Load();
        buttons = [
            retryBtn = new Button(textureButton, new Vector2(510, 443), "RETRY", textureHover, texturePressed),
            menuBtn = new Button(textureButton, new Vector2(510, 539), "BACK TO MENU", textureHover, texturePressed),
            selectLevelBtn = new Button(textureButton, new Vector2(510, 635), "CHOOSE LEVEL", textureHover, texturePressed),
        ];
        background = contentManager.Load<Texture2D>("gui/menu_background");
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