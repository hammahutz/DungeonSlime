using System;
using Microsoft.Xna.Framework;

namespace DungeonSlime.Engine.Scenes;


public class SceneDirector : IDisposable
{
    private static Scene s_activeScene;
    private static Scene s_nextScene;

    public bool IsDisposed { get; private set; }

    public void Update(GameTime gameTime)
    {
        if (s_nextScene != null)
        {
            TransitionScene();
        }

        s_activeScene?.Update(gameTime);
    }

    public void Draw(GameTime gameTime)
    {
        s_activeScene?.Draw(gameTime);
    }

    public void ChangeScene(Scene next)
    {
        if (s_activeScene != next)
        {
            s_nextScene = next;
        }
    }

    private void TransitionScene()
    {
        s_activeScene?.Dispose();
        GC.Collect();

        s_activeScene = s_nextScene;
        s_nextScene = null;

        s_activeScene?.Initialize();
    }

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
            s_activeScene?.Dispose();
            s_nextScene?.Dispose();
        }
    }
}