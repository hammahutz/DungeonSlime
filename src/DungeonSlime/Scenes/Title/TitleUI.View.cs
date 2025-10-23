namespace DungeonSlime.Scenes.Title;

using DungeonSlime.Engine;
using DungeonSlime.Engine.Graphics;
using DungeonSlime.UI;

using Gum.Forms.Controls;

using MonoGameGum;
using MonoGameGum.GueDeriving;

public partial class TitleUI
{
    private Panel _titleScreenButtonsPanel;
    private Button _startButton;
    private Button _optionsButton;

    private Panel _optionsPanel;
    private TextRuntime _optionsText;
    private Button _optionsBackButton;
    private OptionsSlider _musicSlider;
    private OptionsSlider _sfxSlider;


    protected override void AddViews()
    {
        CreateTitlePanel();
        CreateOptionsPanel();
    }

    private void CreateTitlePanel()
    {
        _titleScreenButtonsPanel = new Panel();
        _titleScreenButtonsPanel.Dock(Gum.Wireframe.Dock.Fill);
        _titleScreenButtonsPanel.AddToRoot();

        _startButton = _animatedButtonFactory.Build();
        _startButton.Anchor(Gum.Wireframe.Anchor.BottomLeft);
        _startButton.Visual.X = 50;
        _startButton.Visual.Y = -12;
        _startButton.Visual.Width = 70;
        _startButton.Text = "Start";
        _titleScreenButtonsPanel.AddChild(_startButton);

        _optionsButton = _animatedButtonFactory.Build();
        _optionsButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        _optionsButton.Visual.X = -50;
        _optionsButton.Visual.Y = -12;
        _optionsButton.Visual.Width = 70;
        _optionsButton.Text = "Options";
        _titleScreenButtonsPanel.AddChild(_optionsButton);

        _startButton.IsFocused = true;
    }


    private void CreateOptionsPanel()
    {
        _optionsPanel = new Panel();
        _optionsPanel.Dock(Gum.Wireframe.Dock.Fill);
        _optionsPanel.IsVisible = false;
        _optionsPanel.AddToRoot();

        _optionsText = new TextRuntime()
        {
            X = 10,
            Y = 10,
            Text = "OPTIONS",
            UseCustomFont = true,
            CustomFontFile = "fonts/04b_30.fnt",
        };
        _optionsPanel.AddChild(_optionsText);

        _musicSlider = new OptionsSlider(_textureAtlas)
        {
            Minimum = 0,
            Maximum = 1,
            Value = Core.Audio.SongVolume,
            SmallChange = .1,
            LargeChange = .2
        };
        _musicSlider.Visual.Y = 30f;
        _musicSlider.Anchor(Gum.Wireframe.Anchor.Top);
        _optionsPanel.AddChild(_musicSlider);

        _sfxSlider = new OptionsSlider(_textureAtlas);
        _sfxSlider.Name = "SfxSlider";
        _sfxSlider.Text = "SFX";
        _sfxSlider.Anchor(Gum.Wireframe.Anchor.Top);
        _sfxSlider.Visual.Y = 93;
        _sfxSlider.Minimum = 0;
        _sfxSlider.Maximum = 1;
        _sfxSlider.Value = Core.Audio.SoundEffectVolume;
        _sfxSlider.SmallChange = .1;
        _sfxSlider.LargeChange = .2;
        _optionsPanel.AddChild(_sfxSlider);

        _optionsBackButton = _animatedButtonFactory.Build();
        _optionsBackButton.Text = "BACK";
        _optionsBackButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        _optionsBackButton.X = -28f;
        _optionsBackButton.Y = -10f;
        _optionsPanel.AddChild(_optionsBackButton);
    }



}