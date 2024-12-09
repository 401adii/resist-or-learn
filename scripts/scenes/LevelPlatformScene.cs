using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class LevelPlatformScene : IScene
{
    private ContentManager contentManager;
    
    //CONSTS AND ENUMS
    private const string PLATFORM_DEFAULT = "platform/";
    private const string PLAYER = "player";



    //TEXTURES
    Texture2D playerTexture;

    
    //OBJECTS
    Player player;
    public LevelPlatformScene(ContentManager contentManager){
        this.contentManager = contentManager;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        player.Draw(spriteBatch);
    }

    public void Load()
    {
        playerTexture = contentManager.Load<Texture2D>(PLATFORM_DEFAULT + PLAYER);
        player = new Player(playerTexture, new Vector2(100, 100));
    }

    public void Update(GameTime gameTime)
    {
    }
}
