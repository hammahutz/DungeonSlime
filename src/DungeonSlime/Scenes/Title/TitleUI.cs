using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace DungeonSlime.Scenes.Title;

public partial class TitleUI : Engine.UI.BaseUI
{
    private SoundEffect _uiSoundEffect;
    public TitleUI(ContentManager content) : base(content) {}
    protected override void LoadContent() => _uiSoundEffect = Content.Load<SoundEffect>("audio/ui");
}