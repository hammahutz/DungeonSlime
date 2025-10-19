using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace DungeonSlime.Scenes.Game;

public partial class GameUI : Engine.UI.BaseUI
{
    private SoundEffect _uiSoundEffect;

    public GameUI(ContentManager content) : base(content) {}

    protected override void LoadContent()
    {
        _uiSoundEffect = Content.Load<SoundEffect>("audio/ui");
    }

}