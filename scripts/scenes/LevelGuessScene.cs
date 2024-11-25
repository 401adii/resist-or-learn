using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace resist_or_learn;

public class LevelGuessScene : IScene
{
    private ContentManager contentManager;
        //CONSTS AND ENUMS
    enum ResistorType{
        three_band = 3,
        four_band = 4,
        five_band = 5,
    };
    private const string RESISTOR_BAND_DEFAULT = "/band";
    private const string RESISTOR_BASE_DEFAULT = "/base";
    private const string GUI_DEFAULT = "gui";
    private const string BUTTON_BLUE = "/button_blue";
    //TEXUTRES
    private Texture2D threeBandBaseTexture;
    private Texture2D fourBandBaseTexture;
    private Texture2D fiveBandBaseTexture;
    private List<Texture2D> threeBandTexture = [];
    private List<Texture2D> fourBandTexture = [];
    private List<Texture2D> fiveBandTexture = [];
    private Texture2D buttonTexture;
    private Resistor resistor;
    private InputBox resistanceInput;
    private InputBox toleranceInput;
    public LevelGuessScene(ContentManager contentManager)
    {
        this.contentManager = contentManager;
    }

    public void Load()
    {
        
        LoadResistorBaseTextures(); // DO NOT DELETE!!!
        LoadResistorBandTextures(threeBandTexture, ResistorType.three_band); // DO NOT DELETE!!!
        LoadResistorBandTextures(fourBandTexture, ResistorType.four_band); // DO NOT DELETE!!!
        LoadResistorBandTextures(fiveBandTexture, ResistorType.five_band); // DO NOT DELETE!!!
        buttonTexture = contentManager.Load<Texture2D>(GUI_DEFAULT + BUTTON_BLUE);
        resistor = CreateResistor(ResistorType.four_band);
        resistanceInput = new InputBox(buttonTexture, new Vector2(800, 150));
        toleranceInput = new InputBox(buttonTexture, new Vector2(800, 260));
    }

    public void Update(GameTime gameTime)
    {
        resistanceInput.Update();
        resistanceInput.HandleInput();
        toleranceInput.Update();
        toleranceInput.HandleInput();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(resistor.texture, resistor.position, Color.White);
        foreach(Band band in resistor.bands)
            spriteBatch.Draw(band.texture, band.position, band.color);
        spriteBatch.DrawString(Game1.font, "Resistance: ", new Vector2(800, 120), Color.White);
        spriteBatch.Draw(resistanceInput.texture, resistanceInput.position, Color.White);
        spriteBatch.DrawString(Game1.font, resistanceInput.text, resistanceInput.textPosition, Color.White);
        spriteBatch.DrawString(Game1.font, "Tolerance: ", new Vector2(800, 230), Color.White);
        spriteBatch.Draw(toleranceInput.texture, toleranceInput.position, Color.White);
        spriteBatch.DrawString(Game1.font, toleranceInput.text, toleranceInput.textPosition, Color.White);
    }

    private void LoadResistorBaseTextures()
    {
        threeBandBaseTexture = contentManager.Load<Texture2D>(ResistorType.three_band.ToString() + RESISTOR_BASE_DEFAULT);
        fourBandBaseTexture = contentManager.Load<Texture2D>(ResistorType.four_band.ToString() + RESISTOR_BASE_DEFAULT);
        fiveBandBaseTexture = contentManager.Load<Texture2D>(ResistorType.five_band.ToString() + RESISTOR_BASE_DEFAULT);
    }

    private void LoadResistorBandTextures(List<Texture2D> l, ResistorType t)
    {
        for(int i = 0; i < (int)t; i++)
        {
            Texture2D texture = contentManager.Load<Texture2D>(t.ToString() + RESISTOR_BAND_DEFAULT + (i+1).ToString());
            l.Add(texture);
        }
    }

    private List<Band> LoadResistorBandsList(List<Texture2D> textures)
    {
        List<Band> resistorBandList = [];
        for(int i = 0; i < textures.Count; i++)
        {
            resistorBandList.Add(new Band(textures[i], Vector2.Zero));
        }
        return resistorBandList;
    }

    private Resistor CreateResistor(ResistorType t)
    {
        Resistor r = null;
        switch(t){
        case ResistorType.three_band:
            r = new ThreeBandResistor(threeBandBaseTexture, new Vector2(100, 120), LoadResistorBandsList(threeBandTexture));
            break;
        case ResistorType.four_band:
            r = new FourBandResistor(fourBandBaseTexture, new Vector2(100, 112), LoadResistorBandsList(fourBandTexture));
            break;
        case ResistorType.five_band:
            r = new FiveBandResistor(fiveBandBaseTexture, new Vector2(100, 100), LoadResistorBandsList(fiveBandTexture));
            break;
        }

        return r;
    }
}
