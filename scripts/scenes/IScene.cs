using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public interface IScene
{
    public void Load();
    public void Update(GameTime gameTime);
    public void Draw(SpriteBatch spriteBatch);

    
}
