using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class LevelPlatformScene : IScene
{
    private ContentManager contentManager;
    
    //CONSTS AND ENUMS
    protected const int ts = 64; //tilesize
    protected const string PLATFORM_DEFAULT = "platform/";
    protected const string CONTENT_DEFAULT = "../../../Content/";
    private const string PLAYER = "player";
    private const string PICK_UP = "resistor_pickup";
    private const string TILEMAP = "tilemap0.csv";
    private const string TILESET = "tileset";
    protected Texture2D texture;
    private Texture2D textureAtlas;
    protected List<Sprite> sprites;

    protected Dictionary<Vector2, int> tilemap;
    private Player player;
    protected List<PickUp> pickUps;
    protected List<Rectangle> textureStore;
    


    public LevelPlatformScene(ContentManager contentManager){
        this.contentManager = contentManager;
        tilemap = new();
        LoadMap(CONTENT_DEFAULT + PLATFORM_DEFAULT + TILEMAP);
        textureStore = new(){
            new Rectangle(0, 0, ts, ts),
            new Rectangle(ts, 0, ts, ts),
            new Rectangle(ts*2, 0, ts, ts)
        };
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach(Sprite sprite in sprites)
            sprite.Draw(spriteBatch);

        foreach(var item in tilemap){
            Rectangle dest = new(
                (int)item.Key.X*ts,
                (int)item.Key.Y*ts,
                ts,
                ts
            );
            Rectangle src = textureStore[item.Value - 1];
            spriteBatch.Draw(textureAtlas, dest, src, Color.White);
        }
    }

    protected void LoadMap(string path)
    {
        StreamReader reader = new(path);

        int y = 0;
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] items = line.Split(',');
            for(int x = 0; x < items.Length; x++)
            {
                if(int.TryParse(items[x], out int value)){
                    if(value > 0)
                        tilemap[new Vector2(x, y)] = value;
                }
            }
            y++;
        }
    }

    protected virtual void LoadPickups()
    {
        pickUps = [new PickUp(texture, new Vector2(200, 100), Game1.ResistorType.four_band)];
        foreach(PickUp pickUp in pickUps){
            sprites.Add(pickUp);
        }
    }

    public virtual void Load()
    {
        textureAtlas = contentManager.Load<Texture2D>(PLATFORM_DEFAULT + TILESET);
        sprites = new();
        texture = contentManager.Load<Texture2D>(PLATFORM_DEFAULT + PLAYER);
        player = new Player(texture, new Vector2(100, 100));
        sprites.Add(player);

        texture = contentManager.Load<Texture2D>(PLATFORM_DEFAULT + PICK_UP);
        LoadPickups();

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
