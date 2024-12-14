using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace resist_or_learn;

public class Level3 : LevelPlatformScene
{   
    private const string TILEMAP = "tilemap1.csv";
    public Level3(ContentManager contentManager, string name) : base(contentManager, name)
    {
        LoadMap(CONTENT_DEFAULT + PLATFORM_DEFAULT + TILEMAP);
        playerPos = new Vector2(TS, TS);
    }

    protected override void LoadPickups()
    {
        pickUps = [new PickUp(texture, new Vector2(TS*3, TS*9), Game1.ResistorType.four_band)];
        foreach(PickUp pickUp in pickUps)
            sprites.Add(pickUp);
    }

}
