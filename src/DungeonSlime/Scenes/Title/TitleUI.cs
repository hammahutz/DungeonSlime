namespace DungeonSlime.Scenes.Title;

using System;
using DungeonSlime.Engine;
using DungeonSlime.Engine.Scenes;
using DungeonSlime.Scenes.Game;
using Gum.Forms.Controls;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using MonoGameGum;
using MonoGameGum.GueDeriving;

public class TitleUI: ISceneUI
{
    private Panel _titleScreenButtonsPanel;
    private Button _optionsButton;
    private SoundEffect _uiSoundEffect;

    private Panel _optionsPanel;
    private Button _optionsBackButton;

    public ContentManager Content { get; private set;  }


    public void Load(ContentManager content)
    {
        Content = content;

        _uiSoundEffect = Content.Load<SoundEffect>("audio/ui");
        CreateTitlePanel();
        CreateOptionsPanel();
    }

    private void CreateTitlePanel()
    {
        _titleScreenButtonsPanel = new Panel();
        _titleScreenButtonsPanel.Dock(Gum.Wireframe.Dock.Fill);
        _titleScreenButtonsPanel.AddToRoot();

        var startButton = new Button();
        startButton.Anchor(Gum.Wireframe.Anchor.BottomLeft);
        startButton.Visual.X = 50;
        startButton.Visual.Y = -12;
        startButton.Visual.Width = 70;
        startButton.Text = "Start";
        startButton.Click += HandleStartClicked;
        _titleScreenButtonsPanel.AddChild(startButton);

        _optionsButton = new Button();
        _optionsButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        _optionsButton.Visual.X = -50;
        _optionsButton.Visual.Y = -12;
        _optionsButton.Visual.Width = 70;
        _optionsButton.Text = "Options";
        _optionsButton.Click += HandleOptionsClicked;
        _titleScreenButtonsPanel.AddChild(_optionsButton);

        startButton.IsFocused = true;
    }


    private void HandleStartClicked(object sender, EventArgs e)
    {
        Core.Audio.PlaySoundEffect(_uiSoundEffect);
        Core.Scenes.ChangeScene(new GameScene(Content));
    }

    private void HandleOptionsClicked(object sender, EventArgs e)
    {
        Core.Audio.PlaySoundEffect(_uiSoundEffect);
        _titleScreenButtonsPanel.IsVisible = false;
        _optionsPanel.IsVisible = true;
        _optionsBackButton.IsFocused = true;
    }


    private void CreateOptionsPanel()
    {
        _optionsPanel = new Panel();
        _optionsPanel.Dock(Gum.Wireframe.Dock.Fill);
        _optionsPanel.IsVisible = false;
        _optionsPanel.AddToRoot();

        var optionsText = new TextRuntime();
        optionsText.X = 10;
        optionsText.Y = 10;
        optionsText.Text = "OPTIONS";
        _optionsPanel.AddChild(optionsText);

        var musicSlider = new Slider();
        musicSlider.Anchor(Gum.Wireframe.Anchor.Top);
        musicSlider.Visual.Y = 30f;
        musicSlider.Minimum = 0;
        musicSlider.Maximum = 1;
        musicSlider.Value = Core.Audio.SongVolume;
        musicSlider.SmallChange = .1;
        musicSlider.LargeChange = .2;
        musicSlider.ValueChanged += HandleMusicSliderValueChanged;
        musicSlider.ValueChangeCompleted += HandleMusicSliderValueChangeCompleted;
        _optionsPanel.AddChild(musicSlider);

        var sfxSlider = new Slider();
        sfxSlider.Anchor(Gum.Wireframe.Anchor.Top);
        sfxSlider.Visual.Y = 93;
        sfxSlider.Minimum = 0;
        sfxSlider.Maximum = 1;
        sfxSlider.Value = Core.Audio.SoundEffectVolume;
        sfxSlider.SmallChange = .1;
        sfxSlider.LargeChange = .2;
        sfxSlider.ValueChanged += HandleSfxSliderChanged;
        sfxSlider.ValueChangeCompleted += HandleSfxSliderChangeCompleted;
        _optionsPanel.AddChild(sfxSlider);

        _optionsBackButton = new Button();
        _optionsBackButton.Text = "BACK";
        _optionsBackButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        _optionsBackButton.X = -28f;
        _optionsBackButton.Y = -10f;
        _optionsBackButton.Click += HandleOptionsButtonBack;
        _optionsPanel.AddChild(_optionsBackButton);
    }


    private void HandleSfxSliderChanged(object sender, EventArgs e) => Core.Audio.SoundEffectVolume = (float)((Slider)sender).Value;
    private void HandleSfxSliderChangeCompleted(object sender, EventArgs e) => Core.Audio.PlaySoundEffect(_uiSoundEffect);

    private void HandleMusicSliderValueChanged(object sender, EventArgs e) => Core.Audio.SongVolume = (float)((Slider)sender).Value;
    private void HandleMusicSliderValueChangeCompleted(object sender, EventArgs e) => Core.Audio.PlaySoundEffect(_uiSoundEffect);

    private void HandleOptionsButtonBack(object sender, EventArgs e)
    {
        Core.Audio.PlaySoundEffect(_uiSoundEffect);

        _titleScreenButtonsPanel.IsVisible = true;
        _optionsPanel.IsVisible = false;

        _optionsButton.IsFocused = true;
    }

}