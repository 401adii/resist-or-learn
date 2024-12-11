using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class MenuScene : IScene
{
    protected ContentManager contentManager;
    protected const string GUI_DEFAULT = "gui/";
    protected const string BUTTON_BLUE = "button_blue";
    protected const string HOVER = "_hover";
    protected const string PRESSED = "_pressed";
    protected Texture2D textureButton;
    protected Texture2D textureHover;
    protected Texture2D texturePressed;
    protected List<Button> buttons;
    public Game1.GameState nextState;

    public MenuScene(ContentManager contentManager)
    {
        this.contentManager = contentManager;
    }
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        foreach(Button button in buttons)
            button.Draw(spriteBatch);
    }

    public virtual void Load()
    {
    }

    public virtual void Update(GameTime gameTime)
    {
        foreach(Button button in buttons)
            button.Update();
    }
}