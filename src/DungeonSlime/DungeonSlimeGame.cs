using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using DungeonSlime.Engine;
using DungeonSlime.Engine.Graphics;
using System.Diagnostics;
using System;
using DungeonSlime.Engine.Utils;

namespace DungeonSlime;

public class DungeonSlimeGame : Core
{
    private SpriteFont _font;
    private Texture2D _logo;
    private Rectangle iconSourceRect = new(0, 0, 128, 128);
    private Rectangle wordmarkSourceRect = new(150, 34, 458, 58);

    private AnimatedSprite _slime;
    private AnimatedSprite _bat;

    public DungeonSlimeGame()
        : base("DungeonSlime", width: 1920, height: 1080) { }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        Logger.Trace($"Running on ");
        Logger.Info($"Running on ");
        Logger.Debug($"Running on ");
        Logger.Warning($"Running on ");
        Logger.Error($"Running on ");
        Logger.Critical($"Running on ");
        _font = Content.Load<SpriteFont>("fonts/font");
        _logo = Content.Load<Texture2D>("images/logo");

        TextureAtlas atlas = TextureAtlas.FromFile(Content, "data", "atlas-definition.xml");

        _slime = atlas.CreateAnimatedSprite("slime-animation");
        _slime.Scale = new Vector2(4.0f, 4.0f);

        _bat = atlas.CreateAnimatedSprite("bat-animation");
        _bat.Scale = new Vector2(4.0f, 4.0f);
    }

    protected override void Update(GameTime gameTime)
    {
        if (
            GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        )
            Exit();

        _bat.Update(gameTime);
        _slime.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        SpriteBatch.DrawString(_font, "Hello World", new Vector2(100, 100), Color.White);
#if OPENGL
        SpriteBatch.DrawString(_font, "OpenGL", new Vector2(132, 135), Color.Blue);
#endif

#if DIRECTX
        SpriteBatch.DrawString(_font, "DirectX", new Vector2(132, 135), Color.Green);
#endif

        SpriteBatch.Draw(
            _logo,
            new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height) / 2,
            iconSourceRect,
            Color.Red * 0.5f,
            0,
            new Vector2(_logo.Width, _logo.Height) / 2,
            1.0f,
            SpriteEffects.None,
            0.0f
        );

        _slime.Draw(SpriteBatch, Vector2.One);
        _bat.Draw(SpriteBatch, new Vector2(300f, 300f));
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
