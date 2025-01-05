using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class PauseMenu : MenuScene
{

    public Button continueBtn;
    private Button menuBtn;
    private Texture2D background;
    public PauseMenu(ContentManager contentManager) : base(contentManager)
    {
    }

    public override void Load()
    {
        base.Load();
        nextState = 0;
        buttons = [
                continueBtn = new Button(textureButton, new Vector2(510, 528), "CONTINUE", textureHover, texturePressed),
                menuBtn = new Button(textureButton, new Vector2(510, 624), "MENU", textureHover, texturePressed),
        ];
        background = contentManager.Load<Texture2D>("gui/menu_background");
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if(menuBtn.isPressed){
            nextState = Game1.GameState.main_menu;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(background, new Vector2(494, 464), Color.White);
        base.Draw(spriteBatch);
    }
}