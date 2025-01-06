using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace resist_or_learn;

public class Level2 : LevelPlatformScene
{   
    private const string TILEMAP = "tilemap2.csv";
    public Level2(ContentManager contentManager, string name) : base(contentManager, name)
    {
        LoadMap(CONTENT_DEFAULT + PLATFORM_DEFAULT + TILEMAP);
        playerPos = new Vector2(TS, TS*18);
        backgroundColor = new Color(239, 175, 121);
    }

    protected override void LoadPickups()
    {
        pickUps = [
            new ResistorPickUp(resistorTexture, new Vector2(TS*1, TS*5), Game1.ResistorType.four_band),
            new ResistorPickUp(resistorTexture, new Vector2(TS*32, TS*19), Game1.ResistorType.four_band),
            new ResistorPickUp(resistorTexture, new Vector2(TS*17, TS*1), Game1.ResistorType.four_band),
            new HealthPickUp(healthUpTexture, new Vector2(TS*17, TS*4)),
            new HealthPickUp(healthUpTexture, new Vector2(TS*10, TS*1))
            ];
        base.LoadPickups();
    }

    protected override void LoadObstacles()
    {
        obstacles = [
            new Spikes(spikesTexture, new Vector2(TS*13, TS*20)),
            new Spikes(spikesTexture, new Vector2(TS*14, TS*20)),
            new Spikes(spikesTexture, new Vector2(TS*15, TS*20)),
            new Spikes(spikesTexture, new Vector2(TS*16, TS*20)),
            new Spikes(spikesTexture, new Vector2(TS*17, TS*20)),
        ];
        base.LoadObstacles();
    }
}
