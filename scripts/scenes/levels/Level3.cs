using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace resist_or_learn;

public class Level3 : LevelPlatformScene
{   
    private const string TILEMAP = "tilemap3.csv";
    public Level3(ContentManager contentManager, string name) : base(contentManager, name)
    {
        LoadMap(CONTENT_DEFAULT + PLATFORM_DEFAULT + TILEMAP);
        playerPos = new Vector2(TS, 18*TS);
    }

    protected override void LoadPickups()
    {
        pickUps = [
            new ResistorPickUp(resistorTexture, new Vector2(TS*17, TS*19), Game1.ResistorType.four_band),
            new ResistorPickUp(resistorTexture, new Vector2(TS*32, TS*19), Game1.ResistorType.four_band),
            new ResistorPickUp(resistorTexture, new Vector2(TS*1, TS*10), Game1.ResistorType.five_band),
            new ResistorPickUp(resistorTexture, new Vector2(TS*19, TS*7), Game1.ResistorType.five_band),
            new ResistorPickUp(resistorTexture, new Vector2(TS*36, TS*4), Game1.ResistorType.five_band),
            new HealthPickUp(healthUpTexture, new Vector2(TS*19, TS*9)),
            new HealthPickUp(healthUpTexture, new Vector2(TS*20, TS*19)),
            ];
        base.LoadPickups();
    }

    protected override void LoadObstacles()
    {
        obstacles = [
            new Spikes(spikesTexture, new Vector2(TS*7, TS*20)),
            new Spikes(spikesTexture, new Vector2(TS*8, TS*20)),
            new Spikes(spikesTexture, new Vector2(TS*9, TS*20)),
            new Spikes(spikesTexture, new Vector2(TS*10, TS*20)),
            new Spikes(spikesTexture, new Vector2(TS*11, TS*20)),
            new Spikes(spikesTexture, new Vector2(TS*12, TS*20)),
            new Spikes(spikesTexture, new Vector2(TS*24, TS*20)),
            new Spikes(spikesTexture, new Vector2(TS*25, TS*20)),
            new Spikes(spikesTexture, new Vector2(TS*26, TS*20)),
            new Spikes(spikesTexture, new Vector2(TS*27, TS*20)),
            new Spikes(spikesTexture, new Vector2(TS*28, TS*20)),
            new Spikes(spikesTexture, new Vector2(TS*29, TS*20)),
            new Spikes(spikesTexture, new Vector2(TS*16, TS*13)),
            new Spikes(spikesTexture, new Vector2(TS*17, TS*13)),
            new Spikes(spikesTexture, new Vector2(TS*18, TS*13)),
            new Spikes(spikesTexture, new Vector2(TS*19, TS*13)),
            new Spikes(spikesTexture, new Vector2(TS*20, TS*13)),
            new Spikes(spikesTexture, new Vector2(TS*21, TS*13)),
            new Spikes(spikesTexture, new Vector2(TS*12, TS*8)),
            new Spikes(spikesTexture, new Vector2(TS*13, TS*8)),
            new Spikes(spikesTexture, new Vector2(TS*14, TS*8)),
            new Spikes(spikesTexture, new Vector2(TS*15, TS*8)),
            new Spikes(spikesTexture, new Vector2(TS*16, TS*8)),
            new Spikes(spikesTexture, new Vector2(TS*23, TS*8)),
            new Spikes(spikesTexture, new Vector2(TS*24, TS*8)),
            new Spikes(spikesTexture, new Vector2(TS*25, TS*8)),
            new Spikes(spikesTexture, new Vector2(TS*26, TS*8)),
        ];
        base.LoadObstacles();
    }

}
