using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using DungeonSlime.Engine.Audio;
using DungeonSlime.Engine.Input;
using DungeonSlime.Engine.Utils.Logging;
using DungeonSlime.Engine.Scenes;
using DungeonSlime.Engine.UI;

namespace DungeonSlime.Engine;

public abstract class Core : Game
{
    internal static Core s_instance;
    public static Core Instance => s_instance;

    // TODO: Why not?: public static Core Ins { get; internal set; }

    public static GraphicsDeviceManager Graphics { get; private set; }
    public static new GraphicsDevice GraphicsDevice { get; private set; }
    public static SpriteBatch SpriteBatch { get; private set; }
    public static new ContentManager Content { get; private set; }

    public static InputManager Input { get; private set; }
    public static AudioController Audio { get; private set; }
    public static SceneDirector Scenes { get; private set; }
    public static GumUi UI { get; private set; }

    public bool ExitOnEscape { get; }

    public Core(string title, int width = 800, int height = 600, bool fullScreen = false)
    {
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
        Audio = new AudioController();
        Scenes = new SceneDirector();
        UI = new GumUi(4.0f);

        RegisterDefaultCommands();
    }

    protected virtual void RegisterDefaultCommands() { }

    override protected void Update(GameTime gameTime)
    {
        Input.Update(gameTime);
        Audio.Update();
        Scenes.Update(gameTime);
        UI.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Scenes.Draw(gameTime);
        UI.Draw();
        base.Draw(gameTime);
    }


    protected override void UnloadContent()
    {
        Audio.Dispose();
        Scenes.Dispose();
        base.UnloadContent();
    }

}