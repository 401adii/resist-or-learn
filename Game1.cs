using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace resist_or_learn;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    DigitBand digitBand;
    ToleranceBand toleranceBand;
    MultiplierBand multiplierBand;
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

        // TODO: use this.Content to load your game content here
        Texture2D texture = Content.Load<Texture2D>("four_band/band1");
        digitBand = new DigitBand(texture, Vector2.Zero);
        toleranceBand = new ToleranceBand(texture, new Vector2(100, 100));
        multiplierBand = new MultiplierBand(texture, new Vector2(200, 200));
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
        _spriteBatch.Draw(digitBand.texture, digitBand.position, digitBand.color);
        _spriteBatch.Draw(multiplierBand.texture, multiplierBand.position, multiplierBand.color);
        _spriteBatch.Draw(toleranceBand.texture, toleranceBand.position, toleranceBand.color);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
