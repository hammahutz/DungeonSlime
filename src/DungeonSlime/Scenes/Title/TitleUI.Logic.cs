using System;

using DungeonSlime.Engine;
using DungeonSlime.Scenes.Game;

using Gum.Forms.Controls;

namespace DungeonSlime.Scenes.Title;


public partial class TitleUI
{
    protected override void AddLogic()
    {
        _startButton.Click += HandleStartClicked;
        _optionsButton.Click += HandleOptionsClicked;

        _musicSlider.ValueChanged += HandleMusicSliderValueChanged;
        _musicSlider.ValueChangeCompleted += HandleMusicSliderValueChangeCompleted;

        _sfxSlider.ValueChanged += HandleSfxSliderChanged;
        _sfxSlider.ValueChangeCompleted += HandleSfxSliderChangeCompleted;

        _optionsBackButton.Click += HandleOptionsButtonBack;
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