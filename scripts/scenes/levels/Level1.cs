using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace resist_or_learn;

public class Level1 : LevelPlatformScene
{   
    
    private const string TILEMAP = "tilemap1.csv";
    public Level1(ContentManager contentManager, string name) : base(contentManager, name)
    {
        LoadMap(CONTENT_DEFAULT + PLATFORM_DEFAULT + TILEMAP);
        playerPos = new Vector2(TS*2, TS*18);
        backgroundColor = new Color(166, 207, 208);
    }

    protected override void LoadPickups()
    {
        pickUps = [
            new ResistorPickUp(resistorTexture, new Vector2(TS*38, TS*15), Game1.ResistorType.three_band),
            new ResistorPickUp(resistorTexture, new Vector2(TS*1, TS*12), Game1.ResistorType.three_band),
            new ResistorPickUp(resistorTexture, new Vector2(TS*8, TS*2), Game1.ResistorType.four_band),
            new HealthPickUp(healthUpTexture, new Vector2(TS*36, TS*4)),
            ];
        base.LoadPickups();
    }

    protected override void LoadObstacles()
    {
        obstacles = [

        ];
        base.LoadObstacles();
    }

}
