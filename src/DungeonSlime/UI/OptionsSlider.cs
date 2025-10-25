using DungeonSlime.Engine.Graphics;

using Gum.DataTypes;
using Gum.DataTypes.Variables;
using Gum.Forms.Controls;
using Gum.Managers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGameGum.GueDeriving;

namespace DungeonSlime.UI;


class OptionsSlider : Slider
{
    private ColoredRectangleRuntime _fillRectangle;
    private TextRuntime _textInstance;
    public string Text { get => _textInstance.Text; set => _textInstance.Text = value; }

    public OptionsSlider(TextureAtlas atlas)
    {
        ContainerRuntime topLevelContainer = new ContainerRuntime()
        {
            Height = 55f,
            Width = 265f,
        };


        TextureRegion backgroundRegion = atlas.GetRegion("panel-background");
        NineSliceRuntime background = new NineSliceRuntime();
        background.Texture = atlas.Texture;
        background.TextureAddress = TextureAddress.Custom;
        background.Width = backgroundRegion.Width;
        background.Height = backgroundRegion.Height;
        background.TextureTop = backgroundRegion.SourceRectangle.Top;
        background.TextureLeft = backgroundRegion.SourceRectangle.Left;
        background.Dock(Gum.Wireframe.Dock.Fill);
        topLevelContainer.AddChild(background);

        _textInstance = new TextRuntime();
        _textInstance.CustomFontFile = @"fonts/04b_30.fnt";
        _textInstance.UseCustomFont = true;
        _textInstance.FontScale = 0.5f;
        _textInstance.Text = "Replace Me";
        _textInstance.X = 10f;
        _textInstance.Y = 10f;
        _textInstance.WidthUnits = DimensionUnitType.RelativeToChildren;
        topLevelContainer.AddChild(_textInstance);

        ContainerRuntime innerContainer = new ContainerRuntime();
        innerContainer.Height = 13f;
        innerContainer.Width = 241f;
        innerContainer.X = 10f;
        innerContainer.Y = 33f;
        topLevelContainer.AddChild(innerContainer);

        TextureRegion offBackgroundRegion = atlas.GetRegion("slider-off-background");
        NineSliceRuntime offBackground = new NineSliceRuntime();
        offBackground.Dock(Gum.Wireframe.Dock.Left);
        offBackground.Texture = atlas.Texture;
        offBackground.TextureAddress = TextureAddress.Custom;
        offBackground.TextureHeight = offBackgroundRegion.Height;
        offBackground.TextureLeft = offBackgroundRegion.SourceRectangle.Left;
        offBackground.TextureTop = offBackgroundRegion.SourceRectangle.Top;
        offBackground.TextureWidth = offBackgroundRegion.Width;
        offBackground.Width = 28f;
        offBackground.WidthUnits = DimensionUnitType.Absolute;
        offBackground.Dock(Gum.Wireframe.Dock.Left);
        innerContainer.AddChild(offBackground);

        TextureRegion middleBackgroundRegion = atlas.GetRegion("slider-middle-background");
        NineSliceRuntime middleBackground = new NineSliceRuntime();
        middleBackground.Dock(Gum.Wireframe.Dock.FillVertically);
        middleBackground.Texture = middleBackgroundRegion.Texture;
        middleBackground.TextureAddress = TextureAddress.Custom;
        middleBackground.TextureHeight = middleBackgroundRegion.Height;
        middleBackground.TextureLeft = middleBackgroundRegion.SourceRectangle.Left;
        middleBackground.TextureTop = middleBackgroundRegion.SourceRectangle.Top;
        middleBackground.TextureWidth = middleBackgroundRegion.Width;
        middleBackground.Width = 179f;
        middleBackground.WidthUnits = DimensionUnitType.Absolute;
        middleBackground.Dock(Gum.Wireframe.Dock.Left);
        middleBackground.X = 27f;
        innerContainer.AddChild(middleBackground);

        TextureRegion maxBackgroundRegion = atlas.GetRegion("slider-max-background");
        NineSliceRuntime maxBackground = new NineSliceRuntime();
        maxBackground.Texture = maxBackgroundRegion.Texture;
        maxBackground.TextureAddress = TextureAddress.Custom;
        maxBackground.TextureHeight = maxBackgroundRegion.Height;
        maxBackground.TextureLeft = maxBackgroundRegion.SourceRectangle.Left;
        maxBackground.TextureTop = maxBackgroundRegion.SourceRectangle.Top;
        maxBackground.TextureWidth = maxBackgroundRegion.Width;
        maxBackground.Width = 36f;
        maxBackground.WidthUnits = DimensionUnitType.Absolute;
        maxBackground.Dock(Gum.Wireframe.Dock.Right);
        innerContainer.AddChild(maxBackground);

        ContainerRuntime trackInstance = new ContainerRuntime();
        trackInstance.Name = "TrackInstance";
        trackInstance.Dock(Gum.Wireframe.Dock.Fill);
        trackInstance.Height = -2f;
        trackInstance.Width = -2f;
        middleBackground.AddChild(trackInstance);

        _fillRectangle = new ColoredRectangleRuntime();
        _fillRectangle.Dock(Gum.Wireframe.Dock.Left);
        _fillRectangle.Width = 90f;
        _fillRectangle.WidthUnits = DimensionUnitType.PercentageOfParent;
        trackInstance.AddChild(_fillRectangle);

        TextRuntime offText = new TextRuntime();
        offText.Red = 70;
        offText.Green = 86;
        offText.Blue = 130;
        offText.CustomFontFile = @"fonts/04b_30.fnt";
        offText.FontScale = 0.25f;
        offText.UseCustomFont = true;
        offText.Text = "OFF";
        offText.Anchor(Gum.Wireframe.Anchor.Center);
        offBackground.AddChild(offText);

        TextRuntime maxText = new TextRuntime();
        maxText.Red = 70;
        maxText.Green = 86;
        maxText.Blue = 130;
        maxText.CustomFontFile = @"fonts/04b_30.fnt";
        maxText.FontScale = 0.25f;
        maxText.UseCustomFont = true;
        maxText.Text = "MAX";
        maxText.Anchor(Gum.Wireframe.Anchor.Center);
        maxBackground.AddChild(maxText);

        Color focusedColor = Color.White;
        Color unfocusedColor = Color.Gray;

        StateSaveCategory sliderCategory = new StateSaveCategory();
        sliderCategory.Name = Slider.SliderCategoryName;
        topLevelContainer.AddCategory(sliderCategory);

        StateSave enabled = new StateSave();
        enabled.Name = FrameworkElement.EnabledStateName;
        enabled.Apply = () =>
        {
            // When enabled but not focused, use gray coloring for all elements
            background.Color = unfocusedColor;
            _textInstance.Color = unfocusedColor;
            offBackground.Color = unfocusedColor;
            middleBackground.Color = unfocusedColor;
            maxBackground.Color = unfocusedColor;
            _fillRectangle.Color = unfocusedColor;
        };
        sliderCategory.States.Add(enabled);

        StateSave focused = new StateSave();
        focused.Name = FrameworkElement.FocusedStateName;
        focused.Apply = () =>
        {
            // When focused, use white coloring for all elements
            background.Color = focusedColor;
            _textInstance.Color = focusedColor;
            offBackground.Color = focusedColor;
            middleBackground.Color = focusedColor;
            maxBackground.Color = focusedColor;
            _fillRectangle.Color = focusedColor;
        };
        sliderCategory.States.Add(focused);

        StateSave highlightedFocused = focused.Clone();
        highlightedFocused.Name = FrameworkElement.HighlightedFocusedStateName;
        sliderCategory.States.Add(highlightedFocused);

        StateSave highlighted = enabled.Clone();
        highlighted.Name = FrameworkElement.HighlightedStateName;

        Visual = topLevelContainer;
        IsMoveToPointEnabled = true;

        ValueChanged += (_, _) => IsFocused = true;
        Visual.RollOn += (_, _) => IsFocused = true;
        ValueChangedByUi += (_, _) =>
        {
            double ratio = (Value - Minimum) / (Maximum - Minimum);
            _fillRectangle.Width = 100 * (float)ratio;
        };
    }
}