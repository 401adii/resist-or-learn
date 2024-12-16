using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json.Nodes;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class SelectLevelMenu : MenuScene
{
    private Button backBtn;
    private Button lvl1Btn;
    private Button lvl2Btn;
    private Button lvl3Btn;
    public int newLevelIndex;
    private List<bool> levelData;
    public SelectLevelMenu(ContentManager contentManager) : base(contentManager)
    {
        newLevelIndex = -1;
        levelData = new();
        GetLevelData();
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }

    public override void Load()
    {
        base.Load();
        nextState = Game1.GameState.level_select;
        buttons = [
            backBtn = new Button(textureButton, new Vector2(100, 600), "BACK TO MENU", textureHover, texturePressed),
            lvl1Btn = new Button(textureButton, new Vector2(100, 100), "LEVEL 1", textureHover, texturePressed),
            lvl2Btn = new Button(textureButton, new Vector2(100, 200), "LEVEL 2", textureHover, texturePressed),
            lvl3Btn = new Button(textureButton, new Vector2(100, 300), "LEVEL 3", textureHover, texturePressed),
        ];
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if(backBtn.isPressed){
            nextState = Game1.GameState.main_menu;
        }
        if(lvl1Btn.isPressed && levelData[0]){
            newLevelIndex = 0;
            nextState = Game1.GameState.load_next;
        }
        if(lvl2Btn.isPressed && levelData[1]){
            newLevelIndex = 1;
            nextState = Game1.GameState.load_next;
        }
        if(lvl3Btn.isPressed && levelData[2]){
            newLevelIndex = 2;
            nextState = Game1.GameState.load_next;
        }
    }

    private void GetLevelData()
    {
        string content = File.ReadAllText(Game1.LEVELS_PATH);
        JsonNode node = JsonNode.Parse(content);
        if (node is JsonObject jsonObject)
        {
            foreach (var pair in jsonObject)
            {
                if(pair.Value.AsValue().TryGetValue<bool>(out _))
                    levelData.Add(pair.Value.GetValue<bool>());
            }
        }
    }
}
