using DungeonSlime.Engine;
using DungeonSlime.Engine.Graphics;
using DungeonSlime.Engine.Input;
using DungeonSlime.Engine.Input.Commands;
using DungeonSlime.Engine.Scenes;
using DungeonSlime.Engine.UI;
using DungeonSlime.Engine.Utils.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DungeonSlime.Scenes.Game;

public class GameScene : Scene
{
    private AnimatedSprite _slime;
    private AnimatedSprite _bat;
    private Vector2 _slimePosition = new(100, 200);
    private Vector2 _slimeVelocity = new(0, 0);

    private GameUI _ui;

    private Tilemap _tilemap;
    protected override BaseUI UI => _ui;

    public GameScene(ContentManager content) : base(content) {}

    public override void LoadContent()
    {
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "data", "atlas-definition.xml");

        _slime = atlas.CreateAnimatedSprite("slime-animation");
        _slime.Scale = new Vector2(4.0f, 4.0f);

        _bat = atlas.CreateAnimatedSprite("bat-animation");
        _bat.Scale = new Vector2(4.0f, 4.0f);

        _tilemap = Tilemap.FromFile(Content, "data/tilemap-definition.xml");
        _tilemap.Scale = new Vector2(4.0f);

        _ui = new GameUI(Content, atlas);
    }

    public override void Update(GameTime gameTime)
    {
        _bat.Update(gameTime);
        _slime.Update(gameTime);

        base.Update(gameTime);
    }

    public override void Draw(GameTime gametime)
    {
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _tilemap.Draw(Core.SpriteBatch);
        _slime.Draw(Core.SpriteBatch, _slimePosition);
        _bat.Draw(Core.SpriteBatch, new Vector2(300f, 300f));
        Core.SpriteBatch.End();
    }

    public override void RegisterCommands()
    {
        Core.Input.Commands.RegisterKeyboardCommand(new Command<KeyboardState, Keys>(Keys.Left, InputTrigger.JustPressed, () =>
        {
            Logger.Info("Left pressed");
            _slimeVelocity = Vector2.UnitX * -10;
        }));

        Core.Input.Commands.RegisterKeyboardCommand(new Command<KeyboardState, Keys>(Keys.Right, InputTrigger.JustPressed, () =>
        {
            Logger.Info("Right pressed");
            _slimeVelocity = Vector2.UnitX * 10;
        }));
        Core.Input.Commands.RegisterKeyboardCommand(new Command<KeyboardState, Keys>(Keys.Up, InputTrigger.JustPressed, () =>
        {
            Logger.Info("Up pressed");
            _slimeVelocity = Vector2.UnitY * -10;
        }));
        Core.Input.Commands.RegisterKeyboardCommand(new Command<KeyboardState, Keys>(Keys.Down, InputTrigger.JustPressed, () =>
        {
            Logger.Info("Down pressed");
            _slimeVelocity = Vector2.UnitY * 10;
        }));
        Core.Input.Commands.RegisterMouseCommand(new Command<MouseState, MouseButton>(MouseButton.Left, InputTrigger.JustReleased, () =>
        {
            Logger.Info("Mouse Left Released");
            _slimePosition = Core.Input.Mouse.Position.ToVector2();
        }));

        Core.Input.Commands.RegisterKeyboardCommand(new Command<KeyboardState, Keys>(Keys.Q, InputTrigger.JustPressed, () =>
        {
            _ui.PauseGame();
        }));
        Core.Input.Commands.RegisterGamePadCommand(new Command<GamePadState, Buttons>(Buttons.Start, InputTrigger.JustPressed, () =>
        {
            _ui.PauseGame();
        }));
    }
}