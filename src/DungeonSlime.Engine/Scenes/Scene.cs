using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DungeonSlime.Engine.Scenes;

public abstract class Scene : IDisposable
{
    protected ContentManager Content { get; }
    public bool IsDisposed { get; private set; }

    public Scene(ContentManager content)
    {
        Content = new ContentManager(content.ServiceProvider);
        Content.RootDirectory = content.RootDirectory;
    }
    ~Scene() => Dispose();


    public virtual void Initialize()
    {
        RegisterCommands();
        LoadContent();
    }
    public virtual void RegisterCommands() { }
    public virtual void LoadContent() { }
    public virtual void UnloadContent() => Content.Unload();
    public virtual void Update(GameTime gameTime) { }
    public virtual void Draw(GameTime gameTime) { }
    public virtual void UnregisterCommands() => Core.Input.Flush();


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }
        if (disposing)
        {
            IsDisposed = true;
            UnloadContent();
            Content.Dispose();
            UnregisterCommands();
        }
    }
}