using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class TestScene : IScene
{
    ContentManager contentManager;
    private Texture2D buttonTexture;
    private Button button;
    private InputBox inputBox;
    public static SpriteFont font;
    public TestScene(ContentManager contentManager)
    {
        this.contentManager = contentManager;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(button.texture, button.position, Color.White);
        spriteBatch.Draw(inputBox.texture, inputBox.position, Color.White);
        spriteBatch.DrawString(font, inputBox.text, inputBox.textPosition, Color.White);
    }

    public void Load()
    {
        buttonTexture = contentManager.Load<Texture2D>("gui/button_blue");
        button = new Button(buttonTexture, new Vector2(100, 100));
        inputBox = new InputBox(buttonTexture, new Vector2(200, 300));
        font = contentManager.Load<SpriteFont>("font");
    }

    public void Update(GameTime gameTime)
    {
        button.Update();
        inputBox.Update();
        inputBox.HandleInput();
    }
}