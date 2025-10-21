using System;
using DungeonSlime.Engine;
using DungeonSlime.Scenes.Title;

namespace DungeonSlime.Scenes.Game;


public partial class GameUI
{

    protected override void AddLogic()
    {
        _resumeButton.Click += (object _, EventArgs _) => {
            Core.IsPaused = false;
            Core.Audio.PlaySoundEffect(_uiSoundEffect);
            _pausePanel.IsVisible = false;
        };
        _quitButton.Click += (object _, EventArgs _) => {
            Core.IsPaused = false; Core.Audio.PlaySoundEffect(_uiSoundEffect);
            Core.Scenes.ChangeScene(new TitleScene(Core.Content));
        };
    }
    public void PauseGame()
    {
        Core.IsPaused = true;
        _pausePanel.IsVisible = true;
        _resumeButton.IsFocused = true;
    }
}