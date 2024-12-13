using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class SelectLevelMenu : MenuScene
{
    Button backBtn;
    public SelectLevelMenu(ContentManager contentManager) : base(contentManager){}
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }

    public override void Load()
    {
        base.Load();
        nextState = Game1.GameState.level_select;
        buttons = [
            backBtn = new Button(textureButton, new Vector2(100, 100), "BACK TO MENU", textureHover, texturePressed)
        ];
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if(backBtn.isPressed){
            nextState = Game1.GameState.main_menu;
        }
        
    }
}
