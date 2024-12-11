using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace resist_or_learn;

public class Level2 : LevelPlatformScene
{   
    private const string TILEMAP = "tilemap1.csv";
    public Level2(ContentManager contentManager) : base(contentManager)
    {
        LoadMap(CONTENT_DEFAULT + PLATFORM_DEFAULT + TILEMAP);
        playerPos = new Vector2(4*TS, TS);
    }

    protected override void LoadPickups()
    {
        pickUps = [new PickUp(texture, new Vector2(TS*15, TS*9), Game1.ResistorType.four_band)];
        foreach(PickUp pickUp in pickUps)
            sprites.Add(pickUp);
    }

}
