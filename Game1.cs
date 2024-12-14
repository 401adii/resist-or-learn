using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using resitor_or_learn;

namespace resist_or_learn;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public const string LEVELS_PATH = "../../../data/levels.json";
    public const string SETTINGS_PATH = "../../../data/level.json";
    public enum ResistorType{
        three_band = 3,
        four_band = 4,
        five_band = 5,
    };
    public enum GameState{
        platform,
        guess,
        load_next,
        level_select,
        main_menu,
        settings,
        finish,
        exit
    }
    private GameState state;
    private SceneManager sceneManager;
    public static SpriteFont font;
    public MainMenu mainMenu;
    public SelectLevelMenu selectLevelMenu;
    public SettingsMenu settingsMenu;
    public LevelPlatformScene currentLevel;
    public List<LevelPlatformScene> levels;
    public int currentLevelIndex;
    public LevelGuessScene guessScene;
    public Timer transitionTimer;
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.ApplyChanges();
        sceneManager = new();
    }

    protected override void Initialize()
    {
        base.Initialize();

        
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        font = Content.Load<SpriteFont>("font");
        ReadLevelData();
        InitializeLevels();
        currentLevel = levels[currentLevelIndex];
        state = GameState.main_menu;
        mainMenu = new MainMenu(Content);
        selectLevelMenu = new SelectLevelMenu(Content);
        settingsMenu = new SettingsMenu(Content);
        sceneManager.AddScene(mainMenu);
        transitionTimer = new Timer(3.0f, true);
    }

    protected override void Update(GameTime gameTime)
    {

        transitionTimer.Update(gameTime);
        switch(state){
        
        case GameState.platform:
            if(currentLevel.resistorPickedUp){
                currentLevel.resistorPickedUp = false;
                guessScene = new LevelGuessScene(Content, currentLevel.pickedUpType);
                sceneManager.AddScene(guessScene);
                state = GameState.guess;
            }
            if(currentLevel.levelFinished){
                if(currentLevelIndex == levels.IndexOf(currentLevel))
                    currentLevelIndex++;
                if(currentLevel.finishedLevelMenu.nextState != 0)
                    state = currentLevel.finishedLevelMenu.nextState;
            }
            if(currentLevel.levelFailed){
                currentLevelIndex = 0;
                currentLevel.health = 3;
                if(currentLevel.failedLevelMenu.nextState != 0)
                    state = currentLevel.failedLevelMenu.nextState;
            }
            break;
        
        
        case GameState.guess:
            if(guessScene.isSubmitted){
                if(guessScene.isCorrect)
                    Debug.WriteLine("correct!");
                else{
                    Debug.WriteLine("wrong!");
                    currentLevel.errorFlag = true;
                }
                transitionTimer.Start();
                if(transitionTimer.timeOut){
                    sceneManager.RemoveScene();
                    state = GameState.platform;
                }
            }   
            break;
        
        
        case GameState.load_next:
            InitializeLevels();
            if(currentLevelIndex == levels.Count){
                state = GameState.finish;
                break;
            }   
            sceneManager.RemoveScene();
            int temp = currentLevel.health;
            //currentLevel = new LevelPlatformScene(Content);
            currentLevel = levels[currentLevelIndex];
            currentLevel.health = temp;
            sceneManager.AddScene(currentLevel);
            state = GameState.platform;
            break;
        
        case GameState.main_menu:
            MenuBehaviour(mainMenu);
            mainMenu.nextState = GameState.main_menu;
            break;

        case GameState.level_select:
            MenuBehaviour(selectLevelMenu);
            selectLevelMenu.nextState = GameState.level_select;
            break;

        case GameState.settings:
            MenuBehaviour(settingsMenu);
            settingsMenu.nextState = GameState.settings;
            break;
        
        case GameState.finish:
            state = GameState.main_menu;      
            currentLevelIndex = 0;      
            InitializeLevels();
            break;
        
        
        case GameState.exit:
            Exit();
            break;
        
        default: break;
        }
        sceneManager.GetCurrentScene().Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Gray);
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        sceneManager.GetCurrentScene().Draw(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    void ReadLevelData()
    {
        string content = File.ReadAllText(LEVELS_PATH);
        Dictionary<string, bool> levelInfo = JsonSerializer.Deserialize<Dictionary<string, bool>>(content);
        int index = levelInfo.Count - 1;
        foreach(string key in levelInfo.Keys.Reverse()){
            if(levelInfo[key]){
                currentLevelIndex = index;
                break;
            }
            index--;
        }
    }

    private void InitializeLevels()
    {
        levels = [
            new Level1(Content, "Level1"),
            new Level2(Content, "Level2"),
            new Level3(Content, "Level3"),
        ];
    }

    private void MenuBehaviour(MenuScene sc)
    {
        if(sceneManager.GetCurrentScene() != sc){
            sceneManager.RemoveScene();
            sceneManager.AddScene(sc);
        }

        if(state != sc.nextState){
            state = sc.nextState;
        }
    }

}
