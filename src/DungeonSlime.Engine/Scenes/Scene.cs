using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DungeonSlime.Engine.Scenes;

public abstract class Scene : IDisposable
{
    protected ContentManager Content { get; }
    public bool IsDisposed { get; private set; }

    public Scene()
    {
        Content = new ContentManager(Core.Content.ServiceProvider);
        Content.RootDirectory = Core.Content.RootDirectory;
    }
    ~Scene() => Dispose();


    public virtual void Initialize()
    {
        RegisterCommands();
        LoadContent();
    }
    public virtual void RegisterCommands() { }
    public virtual void LoadContent() { }
    public virtual void UnLoadContent() => Content.Unload();
    public virtual void Update(GameTime gametime) { }
    public virtual void Draw(GameTime gametime) { }

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
            UnLoadContent();
            Content.Dispose();
        }
    }
}