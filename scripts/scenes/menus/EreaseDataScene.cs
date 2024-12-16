using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class EreaseDataScene : MenuScene
{
    public Button yesBtn;
    public Button noBtn;
    public EreaseDataScene(ContentManager contentManager) : base(contentManager)
    {
        Load();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        spriteBatch.DrawString(Game1.font, "ARE YOU SURE?", new Vector2(400, 200), Color.White);
        spriteBatch.DrawString(Game1.font, "ALL PROGRESS WILL BE LOST!", new Vector2(350, 240), Color.White);
    }

    public override void Load()
    {
        base.Load();
        buttons = [
            yesBtn = new Button(textureButton, new Vector2(300, 350), "YES", textureHover, texturePressed),
            noBtn = new Button(textureButton, new Vector2(500, 350), "NO", textureHover, texturePressed),
        ];
    }

}