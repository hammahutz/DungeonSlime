using System;

using DungeonSlime.Engine.Input;
using DungeonSlime.Engine.Input.Commands;
using DungeonSlime.Engine.Utils.Logging;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DungeonSlime.Engine;

public class Core : Game
{
    internal static Core s_instance;
    public static Core Instance => s_instance;

    // TODO: Why not?: public static Core Ins { get; internal set; }

    public static GraphicsDeviceManager Graphics { get; private set; }
    public static new GraphicsDevice GraphicsDevice { get; private set; }
    public SpriteBatch SpriteBatch { get; private set; }
    public static new ContentManager Content { get; private set; }

    public InputManager Input { get; private set; }
    public bool ExitOnEscape { get; }

    public Core(string title, int width = 800, int height = 600, bool fullScreen = false)
    {
        Logger.Info("Init Core");
        SetInstance();
        SetWindow(width, height, fullScreen, title);
        SetContent();

        IsMouseVisible = true;
        ExitOnEscape = true;
    }

    private void SetInstance()
    {
        Logger.Info("Set instance");
        if (s_instance != null)
        {
            throw new InvalidOperationException($"Only a singel Core instance can be created");
        }

        s_instance = this;
    }

    private void SetWindow(int width, int height, bool fullScreen, string title)
    {
        Graphics = new GraphicsDeviceManager(this);

        Graphics.PreferredBackBufferWidth = width;
        Graphics.PreferredBackBufferHeight = height;
        Graphics.IsFullScreen = fullScreen;

        Graphics.ApplyChanges();

        Window.Title = title;
    }

    private void SetContent()
    {
        Content = base.Content;
        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        base.Initialize();

        GraphicsDevice = base.GraphicsDevice;
        SpriteBatch = new SpriteBatch(GraphicsDevice);
        Input = new InputManager();

        Input.Commands.RegisterKeyboardCommand(new Command<KeyboardState, Keys>(Keys.Escape, InputTrigger.JustPressed, () =>
        {
            if (ExitOnEscape && Input.Keyboard.IsDown(Keys.Escape))
            {
                Exit();
            }
        }));
    }

    override protected void Update(GameTime gameTime)
    {
        Input.Update(gameTime);
        base.Update(gameTime);
    }
}
