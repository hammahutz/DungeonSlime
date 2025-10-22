using DungeonSlime.Engine.Graphics;
using DungeonSlime.UI;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace DungeonSlime.Scenes.Game;

public partial class GameUI : Engine.UI.BaseUI
{
    private SoundEffect _uiSoundEffect;

    private AnimatedButtonFactory _animatedButtonFactory;
    private readonly TextureAtlas _textureAtlas;

    public bool IsPaused { get; private set; }

    public GameUI(ContentManager content, TextureAtlas textureAtlas) : base(content)
    {
        _textureAtlas = textureAtlas;
    }

    protected override void LoadContent()
    {
        _uiSoundEffect = Content.Load<SoundEffect>("audio/ui");

        RegionAnimationChain enableButton = new RegionAnimationChain("unfocused-button");
        AnimationAnimationChain focusButton = new AnimationAnimationChain("focused-button-animation");

        _animatedButtonFactory = new AnimatedButtonFactory(_textureAtlas, enableButton, focusButton);
    }

}