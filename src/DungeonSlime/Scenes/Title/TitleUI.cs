using DungeonSlime.Engine.Graphics;
using DungeonSlime.Engine.UI;
using DungeonSlime.UI;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonSlime.Scenes.Title;

public partial class TitleUI : BaseUI
{
    private SoundEffect _uiSoundEffect;
    private TextureAtlas _textureAtlas;

    private AnimatedButtonFactory _animatedButtonFactory;
    public TitleUI(ContentManager content) : base(content) { }

    protected override void LoadContent()
    {
        _uiSoundEffect = Content.Load<SoundEffect>("audio/ui");
        _textureAtlas = TextureAtlas.FromFile(Content, "data", "atlas-definition.xml");

        RegionAnimationChain enableButton = new RegionAnimationChain("unfocused-button");
        AnimationAnimationChain focusButton = new AnimationAnimationChain("focused-button-animation");

        _animatedButtonFactory = new AnimatedButtonFactory(_textureAtlas, enableButton, focusButton);
    }
}