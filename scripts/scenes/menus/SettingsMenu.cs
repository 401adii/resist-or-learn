using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class SettingsMenu : MenuScene
{

    private Button backBtn;
    
    public SettingsMenu(ContentManager contentManager) : base(contentManager)
    {
    }

    public override void Load()
    {
        base.Load();
        nextState = Game1.GameState.settings;
        backBtn = new Button(textureButton, new Vector2 (800, 50), "BACK", textureHover, texturePressed);
        buttons = [backBtn];
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if(backBtn.isPressed)
            nextState = Game1.GameState.main_menu;

    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}

