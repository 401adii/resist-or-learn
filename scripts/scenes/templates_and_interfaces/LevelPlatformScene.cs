using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
    private const string PLAYER = "player";
    private const string PICK_UP = "pickup";
    private const string TILEMAP = "tilemap0.csv";
    private const string TILESET = "tileset";
    private const string AUDIO = "audio/";
    private const string GUI = "gui/";
    private const string HEALTH = "health";
    private const string HEALTH_UP = "health_up";
    private const string SPIKES = "spikes";
    //VARIABLES
    private Texture2D texture;
    protected Texture2D resistorTexture;
    protected Texture2D healthUpTexture;
    protected Texture2D spikesTexture; 
    private Texture2D textureAtlas;
    protected List<Sprite> sprites;
    private Dictionary<Vector2, int> tilemap;
    private Player player;
    protected Vector2 playerPos;
    protected List<PickUp> pickUps;
    protected List<Sprite> obstacles;
    private List<Rectangle> textureStore;
    private List<Rectangle> intersectingTiles;
    private KeyboardState prevState;
    public Healthbar hpbar;
    public FinishedLevelMenu finishedLevelMenu;
    public FailedLevelMenu failedLevelMenu;
    public PauseMenu pauseMenu;
    private SoundEffect pickUpSfx;
    private Vector2 checkpointPos;
    
    public int health;
    private int resistorCount;

    //FLAGS
    protected string name;
    public bool resistorPickedUp;
    public bool errorFlag;
    public bool levelFinished;
    public bool levelFailed;
    public bool levelPaused;
    public Game1.ResistorType pickedUpType;
    

    public LevelPlatformScene(ContentManager contentManager, string name){
        this.contentManager = contentManager;
        this.name = name;
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
        health = GetHealthData();
        resistorCount = 0;
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
        
        spriteBatch.Draw(hpbar.texture, hpbar.Rect, hpbar.DisplayRect, Color.White);
        spriteBatch.Draw(resistorTexture, new Vector2(1210, 10), Color.White);
        spriteBatch.DrawString(Game1.font, resistorCount.ToString(), new Vector2(1190, 10), Color.White);

        if(levelFinished)
            sceneManager.GetCurrentScene().Draw(spriteBatch);
        
        if(levelFailed)
            sceneManager.GetCurrentScene().Draw(spriteBatch);
        
        if(levelPaused)
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
        //pickUps = [new ResistorPickUp(resistorTexture, new Vector2(200, 100), Game1.ResistorType.four_band)];
        foreach(PickUp pickUp in pickUps){
            sprites.Add(pickUp);
            if(pickUp is ResistorPickUp)
                resistorCount++;
        }
    }

    protected virtual void LoadObstacles()
    {
        foreach(Sprite sprite in obstacles){
            sprites.Add(sprite);
        }
    }

    public virtual void Load()
    {
        sprites = new();
        finishedLevelMenu = new FinishedLevelMenu(contentManager);
        failedLevelMenu = new FailedLevelMenu(contentManager);
        pauseMenu = new PauseMenu(contentManager);
        sceneManager.AddScene(finishedLevelMenu);
        texture = contentManager.Load<Texture2D>(PLATFORM_DEFAULT + PLAYER);
        player = new Player(texture, playerPos, new Vector2(64,64));
        checkpointPos = playerPos;
        sprites.Add(player);
        texture = contentManager.Load<Texture2D>(GUI + HEALTH);
        hpbar = new Healthbar(texture, new Vector2(0, 0), new Vector2(64,32), health);
        healthUpTexture = contentManager.Load<Texture2D>(PLATFORM_DEFAULT + HEALTH_UP);
        spikesTexture = contentManager.Load<Texture2D>(PLATFORM_DEFAULT + SPIKES);

        textureAtlas = contentManager.Load<Texture2D>(PLATFORM_DEFAULT + TILESET);
        resistorTexture = contentManager.Load<Texture2D>(PLATFORM_DEFAULT + PICK_UP);
        LoadPickups();
        LoadObstacles();

        pickUpSfx = contentManager.Load<SoundEffect>(AUDIO + PICK_UP);
    }

    public void Update(GameTime gameTime)
    {
        if(levelFinished || levelFailed || levelPaused){
            sceneManager.GetCurrentScene().Update(gameTime);
            if(levelPaused && pauseMenu.continueBtn.isPressed){
                levelPaused = false;
                sceneManager.RemoveScene();
            }
            return;
        }

        if(Keyboard.GetState().IsKeyDown(Keys.Escape)){
            levelPaused = true;
            sceneManager.AddScene(pauseMenu);
        }

        if(errorFlag){
            DecrementHealth();
            errorFlag = false;
            return;
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
                    if(pickUp is ResistorPickUp){
                        SoundManager.Play(pickUpSfx);
                        pickedUpType = pickUp.type;
                        resistorPickedUp = true;
                        resistorCount--;
                        checkpointPos = player.position;
                    }
                    if(pickUp is HealthPickUp){
                        IncrementHealth();
                    }
                        
                    pickUpToKill = pickUp;
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

        foreach(Sprite obstacle in obstacles){
            if(player.Rect.Intersects(obstacle.Rect)){
                player.position = checkpointPos;
                DecrementHealth();
            }
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

    public void UpdateLevelsJSON()
    {
        string content = File.ReadAllText(Game1.LEVELS_PATH);
        JsonNode node = JsonNode.Parse(content);
        node[name] = true;
        File.WriteAllText(Game1.LEVELS_PATH, node.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
    }

    private int GetHealthData()
    {
        string content = File.ReadAllText(Game1.LEVELS_PATH);
        JsonNode node = JsonNode.Parse(content);
        return node["Health"].AsValue().GetValue<int>();
    }

    private int DecrementHealth()
    {
        health--;
        hpbar.UpdateTexture(true);
        if(health == 0){
            levelFailed = true;
            sceneManager.AddScene(failedLevelMenu);
        }
        string content = File.ReadAllText(Game1.LEVELS_PATH);
        JsonNode node = JsonNode.Parse(content);
        int newVal = node["Health"].AsValue().GetValue<int>() - 1;
        node["Health"] =  newVal;
        File.WriteAllText(Game1.LEVELS_PATH, node.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
        return newVal;
    }

    private int IncrementHealth()
    {
        health++;
        if(health != 3)
            hpbar.UpdateTexture(false);
        string content = File.ReadAllText(Game1.LEVELS_PATH);
        JsonNode node = JsonNode.Parse(content);
        int newVal = node["Health"].AsValue().GetValue<int>() + 1;
        if(newVal < 3)
            node["Health"] =  newVal;
        else
            node["Health"] = 3;
        File.WriteAllText(Game1.LEVELS_PATH, node.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
        return newVal;
    }
}
