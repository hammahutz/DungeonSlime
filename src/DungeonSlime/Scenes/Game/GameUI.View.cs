using Gum.Forms.Controls;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using MonoGameGum;
using MonoGameGum.GueDeriving;

namespace DungeonSlime.Scenes.Game;

public partial class GameUI
{
    private Panel _pausePanel;
    private Button _resumeButton;
    private ColoredRectangleRuntime _background;
    private TextRuntime _text;
    private Button _quitButton;

    protected override void AddViews()
    {
        _pausePanel = new Panel();
        _resumeButton = _animatedButtonFactory.Build();
        _background = new ColoredRectangleRuntime();
        _text = new TextRuntime();
        _quitButton = _animatedButtonFactory.Build();

        _pausePanel.Anchor(Anchor.Center);
        _pausePanel.Visual.WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute;
        _pausePanel.Visual.HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute;
        _pausePanel.Visual.Width = 264;
        _pausePanel.Visual.Height = 70;
        _pausePanel.IsVisible = false;
        _pausePanel.AddToRoot();

        _background.Dock(Dock.Fill);
        _background.Color = Color.DarkBlue;
        _pausePanel.AddChild(_background);

        _text.Text = "PAUSED";
        _text.X = 10f;
        _text.Y = 10f;
        _pausePanel.AddChild(_text);

        _resumeButton.Text = "RESUME";
        _resumeButton.Anchor(Anchor.BottomLeft);
        _resumeButton.Visual.X = 9f;
        _resumeButton.Visual.Y = -9f;
        _resumeButton.Visual.Width = 80;
        _pausePanel.AddChild(_resumeButton);

        _quitButton.Text = "QUIT";
        _quitButton.Anchor(Anchor.BottomRight);
        _quitButton.Visual.X = -9f;
        _quitButton.Visual.Y = -9f;
        _quitButton.Width = 80;

        _pausePanel.AddChild(_quitButton);

    }
}