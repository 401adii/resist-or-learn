using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace resist_or_learn;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    //CONSTS AND ENUMS
    enum ResistorType{
        three_band = 3,
        four_band = 4,
        five_band = 5,
    };
    private const string RESISTOR_BAND_DEFAULT = "/band";
    private const string RESISTOR_BASE_DEFAULT = "/base";
    //TEXUTRES
    Texture2D threeBandBaseTexture;
    Texture2D fourBandBaseTexture;
    Texture2D fiveBandBaseTexture;
    List<Texture2D> threeBandTexture = [];
    List<Texture2D> fourBandTexture = [];
    List<Texture2D> fiveBandTexture = [];
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        //Loading textures
        LoadResistorBaseTextures(); // DO NOT DELETE!!!
        LoadResistorBandTextures(threeBandTexture, ResistorType.three_band); // DO NOT DELETE!!!
        LoadResistorBandTextures(fourBandTexture, ResistorType.four_band); // DO NOT DELETE!!!
        LoadResistorBandTextures(fiveBandTexture, ResistorType.five_band); // DO NOT DELETE!!!


        Resistor resistor = CreateResistor(ResistorType.three_band);

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Gray);

        // TODO: Add your drawing code here
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        _spriteBatch.End();
        base.Draw(gameTime);
    }

    private void LoadResistorBaseTextures()
    {
        threeBandBaseTexture = Content.Load<Texture2D>(ResistorType.three_band.ToString() + RESISTOR_BASE_DEFAULT);
        fourBandBaseTexture = Content.Load<Texture2D>(ResistorType.four_band.ToString() + RESISTOR_BASE_DEFAULT);
        fiveBandBaseTexture = Content.Load<Texture2D>(ResistorType.five_band.ToString() + RESISTOR_BASE_DEFAULT);
    }

    private void LoadResistorBandTextures(List<Texture2D> l, ResistorType t)
    {
        for(int i = 0; i < (int)t; i++)
        {
            Texture2D texture = Content.Load<Texture2D>(t.ToString() + RESISTOR_BAND_DEFAULT + (i+1).ToString());
            l.Add(texture);
        }
    }

    private List<Band> LoadResistorBandsList(List<Texture2D> textures)
    {
        List<Band> resistorBandList = [];
        for(int i = 0; i < textures.Count; i++)
        {
            resistorBandList.Add(new Band(textures[i], new Vector2(100, 100)));
        }
        return resistorBandList;
    }

    private Resistor CreateResistor(ResistorType t)
    {
        Resistor r = null;
        switch(t){
        case ResistorType.three_band:
            r = new ThreeBandResistor(threeBandBaseTexture, Vector2.Zero, LoadResistorBandsList(threeBandTexture));
            break;
        case ResistorType.four_band:
            r = new ThreeBandResistor(fourBandBaseTexture, Vector2.Zero, LoadResistorBandsList(fourBandTexture));
            break;
        case ResistorType.five_band:
            r = new ThreeBandResistor(fiveBandBaseTexture, Vector2.Zero, LoadResistorBandsList(fiveBandTexture));
            break;
        }

        return r;
    }
}
