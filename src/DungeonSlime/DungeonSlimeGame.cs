using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using DungeonSlime.Engine;
using DungeonSlime.Engine.Graphics;
using DungeonSlime.Engine.Utils.Logging;
using DungeonSlime.Engine.Input.Commands;
using DungeonSlime.Engine.Input;
using DungeonSlime.Engine.Collision;

namespace DungeonSlime;

public class DungeonSlimeGame : Core
{
    private SpriteFont _font;
    private Texture2D _logo;
    private Rectangle iconSourceRect = new(0, 0, 128, 128);
    private Rectangle wordmarkSourceRect = new(150, 34, 458, 58);

    private AnimatedSprite _slime;
    private AnimatedSprite _bat;

    private Vector2 _slimePosition = new(100, 200);
    private Vector2 _slimeVelocity = new(0, 0);

    private Tilemap _tilemap;


    public DungeonSlimeGame()
        : base("DungeonSlime", width: 1920, height: 1080) { }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _font = Content.Load<SpriteFont>("fonts/font");
        _logo = Content.Load<Texture2D>("images/logo");

        TextureAtlas atlas = TextureAtlas.FromFile(Content, "data", "atlas-definition.xml");

        _slime = atlas.CreateAnimatedSprite("slime-animation");
        _slime.Scale = new Vector2(4.0f, 4.0f);

        _bat = atlas.CreateAnimatedSprite("bat-animation");
        _bat.Scale = new Vector2(4.0f, 4.0f);

        _tilemap = Tilemap.FromFile(Content, "data/tilemap-definition.xml");
        _tilemap.Scale = new Vector2(4.0f);
    }

    protected override void Update(GameTime gameTime)
    {
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

        _tilemap.Draw(SpriteBatch);

        _slime.Draw(SpriteBatch, _slimePosition);
        _bat.Draw(SpriteBatch, new Vector2(300f, 300f));
        SpriteBatch.End();

        base.Draw(gameTime);
    }

    protected override void RegisterDefaultCommands()
    {
        Input.Commands.RegisterKeyboardCommand(new Command<KeyboardState, Keys>(Keys.Left, InputTrigger.JustPressed, () =>
        {
            Logger.Info("Left pressed");
            _slimeVelocity = Vector2.UnitX * -10;
        }));

        Input.Commands.RegisterKeyboardCommand(new Command<KeyboardState, Keys>(Keys.Right, InputTrigger.JustPressed, () =>
        {
            Logger.Info("Right pressed");
            _slimeVelocity = Vector2.UnitX * 10;
        }));
        Input.Commands.RegisterKeyboardCommand(new Command<KeyboardState, Keys>(Keys.Up, InputTrigger.JustPressed, () =>
        {
            Logger.Info("Up pressed");
            _slimeVelocity = Vector2.UnitY * -10;
        }));
        Input.Commands.RegisterKeyboardCommand(new Command<KeyboardState, Keys>(Keys.Down, InputTrigger.JustPressed, () =>
        {
            Logger.Info("Down pressed");
            _slimeVelocity = Vector2.UnitY * 10;
        }));
        Input.Commands.RegisterMouseCommand(new Command<MouseState, MouseButton>(MouseButton.Left, InputTrigger.JustReleased, () =>
        {
            Logger.Info("Mouse Left Released");
            _slimePosition = Input.Mouse.Position.ToVector2();
        }));
    }
}