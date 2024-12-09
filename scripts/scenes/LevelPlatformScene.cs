using System;
using System.Collections.Generic;
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
    private const string PICK_UP = "resistor_pickup";
    
    private Texture2D texture;
    private List<Sprite> sprites;

    private Player player;
    private List<PickUp> pickUps;


    public LevelPlatformScene(ContentManager contentManager){
        this.contentManager = contentManager;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach(Sprite sprite in sprites)
            sprite.Draw(spriteBatch);
    }

    public void Load()
    {
        sprites = new();
        texture = contentManager.Load<Texture2D>(PLATFORM_DEFAULT + PLAYER);
        player = new Player(texture, new Vector2(100, 100));
        sprites.Add(player);


        texture = contentManager.Load<Texture2D>(PLATFORM_DEFAULT + PICK_UP);
        pickUps = [new PickUp(texture, new Vector2(200, 100), Game1.ResistorType.four_band)];
        foreach(PickUp pickUp in pickUps){
            sprites.Add(pickUp);
        }
    }

    public void Update(GameTime gameTime)
    {
        foreach(Sprite sprite in sprites)
            sprite.Update(gameTime);
        foreach(PickUp pickUp in pickUps)
        {
            if(player.Rect.Intersects(pickUp.Rect))
            {
                sprites.Remove(pickUp);
            }
        }
    }
}
