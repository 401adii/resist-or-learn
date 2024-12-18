using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace resist_or_learn;

public class Level1 : LevelPlatformScene
{   
    
    private const string TILEMAP = "tilemap1.csv";
    public Level1(ContentManager contentManager, string name) : base(contentManager, name)
    {
        LoadMap(CONTENT_DEFAULT + PLATFORM_DEFAULT + TILEMAP);
        playerPos = new Vector2(TS, TS);

    }

    protected override void LoadPickups()
    {
        pickUps = [
            new ResistorPickUp(resistorTexture, new Vector2(TS*14, TS*9), Game1.ResistorType.four_band),
            new ResistorPickUp(resistorTexture, new Vector2(TS*14, TS*4), Game1.ResistorType.four_band),
            // new PickUp(texture, new Vector2(TS*5, TS*8), Game1.ResistorType.four_band),
            ];
        base.LoadPickups();
    }

    protected override void LoadObstacles()
    {
        obstacles = [
            new Spikes(spikesTexture, new Vector2(TS * 6, TS*9)),
        ];
        base.LoadObstacles();
    }

}
