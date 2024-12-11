using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class FinishedLevelMenu : IScene
{
    private ContentManager contentManager;
    private const string BUTTON_BLUE = "/button_blue";
    private const string GUI_DEFAULT = "gui";
    Texture2D texture;
    public Button mainMenu;
    public Button selectLevel;

    public FinishedLevelMenu(ContentManager contentManager)
    {
        this.contentManager = contentManager;    
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        mainMenu.Draw(spriteBatch);
        selectLevel.Draw(spriteBatch);
    }

    public void Load()
    {
        texture = contentManager.Load<Texture2D>(GUI_DEFAULT + BUTTON_BLUE);
        mainMenu = new Button(texture, new Vector2(100, 100), "main menu");
        selectLevel = new Button(texture, new Vector2(100, 300), "select level");
    }

    public void Update(GameTime gameTime)
    {
        
    }
}
