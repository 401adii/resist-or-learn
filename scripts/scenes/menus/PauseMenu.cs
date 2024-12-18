using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace resist_or_learn;

public class PauseMenu : MenuScene
{

    public Button continueBtn;
    private Button menuBtn;
    public PauseMenu(ContentManager contentManager) : base(contentManager)
    {
    }

    public override void Load()
    {
        base.Load();
        nextState = 0;
        buttons = [
                continueBtn = new Button(textureButton, new Vector2(100, 100), "CONTINUE", textureHover, texturePressed),
                menuBtn = new Button(textureButton, new Vector2(100, 200), "MENU", textureHover, texturePressed),
        ];
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if(menuBtn.isPressed){
            nextState = Game1.GameState.main_menu;
        }
    }
}