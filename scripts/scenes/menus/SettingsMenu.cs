using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class SettingsMenu : MenuScene
{  
    private SceneManager sceneManager;
    private EreaseDataScene ereaseDataScene;
    private Button backBtn;
    private Button cntrlsBtn;
    private Button resetBtn;
    private Button soundBtn;
    private Button cheatSheetBtn;
    private Button tolBtn;
    private string SOUND = "SOUND";
    private string TOLERANCE = "TOLERANCE";
    private string CHEATSHEET = "CHEATSHEET";
    private string CONTROLS = "CONTROLS";
    private bool popUpOn;
    
    public SettingsMenu(ContentManager contentManager) : base(contentManager)
    {
        popUpOn = false;
        sceneManager = new SceneManager();
    }

    public override void Load()
    {
        base.Load();
        ereaseDataScene = new EreaseDataScene(contentManager);
        nextState = Game1.GameState.settings;
        buttons = [
            cntrlsBtn = new Button(textureButton, new Vector2(16, 80), CONTROLS, textureHover, texturePressed),
            soundBtn = new Button(textureButton, new Vector2(16, 160), SOUND, textureHover, texturePressed),
            tolBtn = new Button(textureButton, new Vector2(16, 240), TOLERANCE, textureHover, texturePressed),
            cheatSheetBtn = new Button(textureButton, new Vector2(16, 320), CHEATSHEET, textureHover, texturePressed),
            resetBtn = new Button(textureButton, new Vector2(16, 400), "RESET PROGRESS", textureHover, texturePressed),
            backBtn = new Button(textureButton, new Vector2 (16, 560), "BACK", textureHover, texturePressed),
            ];
        UpdateText(cntrlsBtn);
        UpdateText(cheatSheetBtn);
        UpdateText(tolBtn);
        UpdateText(soundBtn);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if(popUpOn){
            ereaseDataScene.Update(gameTime);
            if(ereaseDataScene.yesBtn.isPressed){
                ResetGameState();
                popUpOn = false;
                sceneManager.RemoveScene();
            }
            if(ereaseDataScene.noBtn.isPressed){
                popUpOn = false;
                sceneManager.RemoveScene();
            }
            return;
        }
        
        if(backBtn.isPressed)
            nextState = Game1.GameState.main_menu;
        
        if(cntrlsBtn.isPressed)
        {
            Toggle(cntrlsBtn);
        }
        if(cheatSheetBtn.isPressed)
        {
            Toggle(cheatSheetBtn);
        }
        if(tolBtn.isPressed)
        {
            Toggle(tolBtn);
        }
        if(soundBtn.isPressed)
        {
            Toggle(soundBtn);
        }
        if(resetBtn.isPressed)
        {
            popUpOn = true;
            sceneManager.AddScene(ereaseDataScene);

        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        if(popUpOn)
            sceneManager.GetCurrentScene().Draw(spriteBatch);
    }

    private void Toggle(Button btn)
    {
        string content = File.ReadAllText(Game1.SETTINGS_PATH);
        Dictionary<string, bool> settings = JsonSerializer.Deserialize<Dictionary<string, bool>>(content);
        btn.text = btn.text.Substring(0, btn.text.IndexOf(':'));
        settings[btn.text] = !settings[btn.text];
        File.WriteAllText(Game1.SETTINGS_PATH, JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true }));
        Game1.GetSettingsData();
        UpdateText(btn);
    }

    private void UpdateText(Button btn)
    {
        string content = File.ReadAllText(Game1.SETTINGS_PATH);
        Dictionary<string, bool> settings = JsonSerializer.Deserialize<Dictionary<string, bool>>(content);
        if(btn.text != CONTROLS){
            if(settings[btn.text] == true)
                btn.text += ": ON";
            else
                btn.text += ": OFF";
        }
        else{
            if(settings[btn.text] == true)
                btn.text += ": ARROWS";
            else
                btn.text += ": WSAD";
        }
    }

    private void ResetGameState()
    {
        File.WriteAllText(Game1.LEVELS_PATH,
        "{\n  \t\"Health\": 3,\n \t\"Level1\": true,\n \t\"Level2\": false,\n \t\"Level3\": false\n}");
    }
}

