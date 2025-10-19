
using Microsoft.Xna.Framework.Content;

namespace DungeonSlime.Engine.UI;

public abstract class BaseUI 
{
    public ContentManager Content { get; private set; }

    public BaseUI(ContentManager content)
    {
        Content = content;
    }
    public void Initliaze()
    {
        LoadContent();
        AddViews();
        AddLogic();
    }

    protected virtual void LoadContent() {}
    public abstract void AddViews();
    public abstract void AddLogic();
}