using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace resist_or_learn;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public enum ResistorType{
        three_band = 3,
        four_band = 4,
        five_band = 5,
    };
    private enum GameState{
        platform,
        guess,
    }
    private GameState state;
    public static SpriteFont font;
    public LevelPlatformScene currentLevel;
    public LevelGuessScene guessScene;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        base.Initialize();
        
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        font = Content.Load<SpriteFont>("font");
        currentLevel = new Level1(Content);
        state = GameState.platform;
        SceneManager.AddScene(currentLevel);
    }

    protected override void Update(GameTime gameTime)
    {

        switch(state){
        case GameState.platform:
            if(currentLevel.resistorPickedUp){
                currentLevel.resistorPickedUp = false;
                guessScene = new LevelGuessScene(Content, currentLevel.pickedUpType);
                SceneManager.AddScene(guessScene);
                state = GameState.guess;
            }
            if(currentLevel.levelFinished){
                //TO DO: WHAT HAPPENS WHEN FINISHED LEVEL
            }
            break;
        case GameState.guess:
            if(guessScene.isSubmitted){
                if(guessScene.isCorrect)
                    Debug.WriteLine("correct!");
                else
                    Debug.WriteLine("wrong!");

                SceneManager.RemoveScene();
                state = GameState.platform;
            }   
            break;
        default: break;
        }
        SceneManager.GetCurrentScene().Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Gray);
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        SceneManager.GetCurrentScene().Draw(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }


}
