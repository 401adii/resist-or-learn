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
        spriteBatch.DrawString(Game1.font, "ARE YOU SURE?", new Vector2(568, 161), Color.White);
        spriteBatch.DrawString(Game1.font, "ALL PROGRESS WILL BE LOST!", new Vector2(505, 200), Color.White);
    }

    public override void Load()
    {
        base.Load();
        buttons = [
            yesBtn = new Button(textureButton, new Vector2(368, 244), "YES", textureHover, texturePressed),
            noBtn = new Button(textureButton, new Vector2(656, 244), "NO", textureHover, texturePressed),
        ];
    }

}