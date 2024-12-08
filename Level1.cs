using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace resist_or_learn;

public class Level1 : LevelPlatformScene
{   
    private const string TILEMAP = "tilemap1.csv";
    public Level1(ContentManager contentManager) : base(contentManager)
    {
        LoadMap(CONTENT_DEFAULT + PLATFORM_DEFAULT + TILEMAP);
    }

    protected override void LoadPickups()
    {
        pickUps = [new PickUp(texture, new Vector2(300, 100), Game1.ResistorType.three_band)];
        foreach(PickUp pickUp in pickUps)
            sprites.Add(pickUp);
    }
}
