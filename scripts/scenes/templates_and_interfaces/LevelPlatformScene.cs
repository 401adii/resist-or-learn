using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace resist_or_learn;

public class LevelPlatformScene : IScene
{
    private ContentManager contentManager;
    private SceneManager sceneManager;
    
    //CONSTS AND ENUMS
    protected const int TS = 64; //tilesize
    protected const string PLATFORM_DEFAULT = "platform/";
    protected const string CONTENT_DEFAULT = "../../../Content/";
    private const string PLAYER = "check";
    private const string PICK_UP = "resistor_pickup";
    private const string TILEMAP = "tilemap0.csv";
    private const string TILESET = "tileset";
    
    
    //VARIABLES
    protected Texture2D texture;
    private Texture2D textureAtlas;
    protected List<Sprite> sprites;
    private Dictionary<Vector2, int> tilemap;
    private Player player;
    protected Vector2 playerPos;
    protected List<PickUp> pickUps;
    private List<Rectangle> textureStore;
    private List<Rectangle> intersectingTiles;
    private KeyboardState prevState;
    public FinishedLevelMenu finishedLevelMenu;
    public FailedLevelMenu failedLevelMenu;
    public int health;

    //FLAGS
    public bool resistorPickedUp;
    public bool errorFlag;
    public bool levelFinished;
    public bool levelFailed;
    public Game1.ResistorType pickedUpType;

    public LevelPlatformScene(ContentManager contentManager){
        this.contentManager = contentManager;
        sceneManager = new();
        tilemap = new();
        resistorPickedUp = false;
        levelFinished = false;
        levelFailed = false;
        errorFlag = false;
        pickedUpType = Game1.ResistorType.four_band;
        LoadMap(CONTENT_DEFAULT + PLATFORM_DEFAULT + TILEMAP);
        textureStore = new(){
            new Rectangle(0, 0, TS, TS),
            new Rectangle(TS, 0, TS, TS),
            new Rectangle(TS*2, 0, TS, TS)
        };
        playerPos = new Vector2(0, 0);
        health = 1;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach(var item in tilemap){
            Rectangle dest = new(
                (int)item.Key.X*TS,
                (int)item.Key.Y*TS,
                TS,
                TS
            );
            Rectangle src = textureStore[item.Value];
            spriteBatch.Draw(textureAtlas, dest, src, Color.White);
        }
        foreach(Sprite sprite in sprites)
            sprite.Draw(spriteBatch);
        
        if(levelFinished)
            sceneManager.GetCurrentScene().Draw(spriteBatch);
        
        if(levelFailed)
            sceneManager.GetCurrentScene().Draw(spriteBatch);
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
                    if(value > -1)
                        tilemap[new Vector2(x, y)] = value;
                }
            }
            y++;
        }
    }

    protected virtual void LoadPickups()
    {
        pickUps = [new PickUp(texture, new Vector2(200, 100), Game1.ResistorType.four_band)];
        foreach(PickUp pickUp in pickUps)
            sprites.Add(pickUp);
    }

    public virtual void Load()
    {
        sprites = new();
        finishedLevelMenu = new FinishedLevelMenu(contentManager);
        failedLevelMenu = new FailedLevelMenu(contentManager);
        sceneManager.AddScene(finishedLevelMenu);
        texture = contentManager.Load<Texture2D>(PLATFORM_DEFAULT + PLAYER);
        player = new Player(texture, playerPos);
        sprites.Add(player);

        textureAtlas = contentManager.Load<Texture2D>(PLATFORM_DEFAULT + TILESET);
        texture = contentManager.Load<Texture2D>(PLATFORM_DEFAULT + PICK_UP);
        LoadPickups();
    }

    public void Update(GameTime gameTime)
    {
        if(levelFinished){
            finishedLevelMenu.Update(gameTime);
            return;
        }

        if(levelFailed){
            failedLevelMenu.Update(gameTime);
            return;
        }

        if(errorFlag){
            health--;
            if(health == 0){
                levelFailed = true;
                sceneManager.AddScene(failedLevelMenu);
                return;
            }
            errorFlag = false;
        }
        
        player.Update(Keyboard.GetState(), prevState, gameTime);
        prevState = Keyboard.GetState();
        if(pickUps.Count > 0){
            PickUp pickUpToKill = null;
            foreach(PickUp pickUp in pickUps)
            {
                pickUp.Update(gameTime);
                if(player.Rect.Intersects(pickUp.Rect))
                {
                    pickedUpType = pickUp.type;
                    pickUpToKill = pickUp;
                    resistorPickedUp = true;
                }
            }
            sprites.Remove(pickUpToKill);
            pickUps.Remove(pickUpToKill);
        }
        else if (pickUps.Count == 0 && !levelFailed){
            levelFinished = true;
            sceneManager.AddScene(finishedLevelMenu);
            return;
        }

        player.position.X += (int)player.velocity.X;
        intersectingTiles = GetIntersectingTilesHorizontal(player.Rect);
        foreach(Rectangle rect in intersectingTiles){
            if(tilemap.TryGetValue(new Vector2(rect.X, rect.Y), out int _val)) {
                Rectangle collision = new(
                    rect.X * TS,
                    rect.Y * TS,
                    TS,
                    TS
                );

                if(!player.Rect.Intersects(collision)){
                    continue;
                }

                if (player.velocity.X > 0.0f){
                    player.position.X = collision.Left - player.Rect.Width;
                } else if (player.velocity.X < 0.0f){
                    player.position.X = collision.Right;
                }
            }
        }

        player.position.Y += (int)player.velocity.Y;
        intersectingTiles = GetIntersectingTilesVertical(player.Rect);
        player.grounded = false;
        foreach(Rectangle rect in intersectingTiles){
            if(tilemap.TryGetValue(new Vector2(rect.X, rect.Y), out int _val)) {
                Rectangle collision = new(
                    rect.X * TS,
                    rect.Y * TS,
                    TS,
                    TS
                );

                if(!player.Rect.Intersects(collision)){
                    continue;
                }

                if (player.velocity.Y > 0.0f){
                    player.position.Y = collision.Top - player.Rect.Height;
                    player.velocity.Y = 1.0f;
                    player.jumpCount = 2;
                    player.grounded = true;
                } else if (player.velocity.Y < 0.0f){
                    player.position.Y = collision.Bottom;
                }
            }
        }

        if(!player.grounded && player.jumpCount == 2){
            player.jumpCount--;
        }
    }

    public List<Rectangle> GetIntersectingTilesHorizontal(Rectangle r){

        List<Rectangle> intersections = new();

        int widthInTiles = (r.Width - (r.Width % TS)) / TS;
        int heightInTiles = (r.Height - (r.Height % TS)) / TS;

        for(int x = 0; x <= widthInTiles; x++){
            for(int y = 0; y <= heightInTiles; y++){
                intersections.Add(new Rectangle(
                    (r.X + x*TS) / TS,
                    (r.Y + y*(TS - 1)) / TS,
                    TS,
                    TS
                ));
            }
        }
        return intersections;
    }
    public List<Rectangle> GetIntersectingTilesVertical(Rectangle r){

        List<Rectangle> intersections = new();

        int widthInTiles = (r.Width - (r.Width % TS)) / TS;
        int heightInTiles = (r.Height - (r.Height % TS)) / TS;

        for(int x = 0; x <= widthInTiles; x++){
            for(int y = 0; y <= heightInTiles; y++){
                intersections.Add(new Rectangle(
                    (r.X + x*(TS - 1)) / TS,
                    (r.Y + y*TS) / TS,
                    TS,
                    TS
                ));
            }
        }

        return intersections;
    }
}
