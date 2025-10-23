using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Gum.DataTypes;
using Gum.DataTypes.Variables;
using Gum.Forms.Controls;
using Gum.Forms.DefaultVisuals;
using Gum.Graphics.Animation;
using Gum.Managers;
using Gum.Wireframe;

using DungeonSlime.Engine.Graphics;

using MonoGameGum.GueDeriving;

namespace DungeonSlime.UI;

class AnimatedButton : Button
{

    public TextureAtlas TextureAtlas { get; }
    public BaseAnimationChain Enable { get; }
    public BaseAnimationChain Focus { get; }

    public AnimatedButton(TextureAtlas textureAtlas, BaseAnimationChain enable, BaseAnimationChain focus)
    {
        TextureAtlas = textureAtlas;
        Enable = enable;
        Focus = focus;

        ButtonVisual button = (ButtonVisual)Visual;
        button
            .SetDimension()
            .SetBackground(textureAtlas)
            .SetText("START")
            .AddAnimationChain(textureAtlas, enable)
            .AddAnimationChain(textureAtlas, focus)
            .InitState(enable, focus)
            .HandleKeyDown(this)
            .HandleRollOn(this);
    }


}
class AnimatedButtonFactory
{
    public TextureAtlas TextureAtlas { get; private set; }
    public BaseAnimationChain Enable { get; private set; }
    public BaseAnimationChain Focus { get; private set; }

    public AnimatedButtonFactory(TextureAtlas textureAtlas, BaseAnimationChain enable, BaseAnimationChain focus)
    {
        TextureAtlas = textureAtlas;
        Enable = enable;
        Focus = focus;
    }

    public AnimatedButton Build() => new AnimatedButton(TextureAtlas, Enable, Focus);
}


static class ButtonVisualExtensions
{
    public static ButtonVisual SetDimension(this ButtonVisual button, ButtonDimensions dimensions = null)
    {
        dimensions = dimensions ?? new ButtonDimensions();
        button.WidthUnits = dimensions.WidthUnits;
        button.Width = dimensions.Width;

        button.HeightUnits = dimensions.HeightUnits;
        button.Height = dimensions.Height;

        return button;
    }
    public static ButtonVisual SetBackground(this ButtonVisual button, TextureAtlas textureAtlas, Color? color = null)
    {
        Color newColor = color ?? Color.White;

        button.Background.Texture = textureAtlas.Texture;
        button.Background.TextureAddress = TextureAddress.Custom;
        button.Background.Color = newColor;
        return button;
    }

    public static ButtonVisual SetText(this ButtonVisual button, string textValue)
    {
        TextRuntime text = button.TextInstance;
        text.Text = textValue;
        text.Red = 70;
        text.Green = 86;
        text.Blue = 130;
        text.UseCustomFont = true;
        text.CustomFontFile = "fonts/04b_30.fnt";
        text.FontScale = 0.25f;
        text.Anchor(Anchor.Center);
        text.Width = 0;
        text.WidthUnits = DimensionUnitType.RelativeToChildren;
        return button;
    }

    public static ButtonVisual AddAnimationChain(this ButtonVisual button, TextureAtlas textureAtlas, BaseAnimationChain animationChain)
    {
        button.Background.AnimationChains = button.Background.AnimationChains ?? new AnimationChainList();
        button.Background.AnimationChains.Add(animationChain.Get(textureAtlas));
        return button;
    }

    public static ButtonVisual InitState(this ButtonVisual button, BaseAnimationChain enable, BaseAnimationChain focus)
    {
        button.ButtonCategory.ResetAllStates();
        StateSave enableState = button.States.Enabled;

        button.States.Enabled.Apply = () =>
        {
            button.Background.CurrentChainName = enable.Name;
            if (enable is AnimationAnimationChain)
            {
                button.Background.Animate = true;
            }
        };

        button.States.Focused.Apply = () =>
        {
            button.Background.CurrentChainName = focus.Name;
            if (focus is AnimationAnimationChain)
            {
                button.Background.Animate = true;
            }
        };

        button.States.Highlighted.Apply = button.States.Enabled.Apply;
        button.States.HighlightedFocused.Apply = button.States.Focused.Apply;


        return button;
    }
    public static ButtonVisual HandleKeyDown(this ButtonVisual button, AnimatedButton animatedButton, KeyEventHandler action = null)
    {
        if (action is not null)
        {
            animatedButton.KeyDown += action;
            return button;
        }

        animatedButton.KeyDown += (_, e) =>
        {
            if (e.Key == Keys.Left)
                animatedButton.HandleTab(TabDirection.Up, loop: true);

            if (e.Key == Keys.Right)
                animatedButton.HandleTab(TabDirection.Down, loop: true);
        };

        return button;
    }

    public static ButtonVisual HandleRollOn(this ButtonVisual button, AnimatedButton animatedButton, EventHandler handler = null)
    {
        if (handler is not null)
        {
            button.RollOn += handler;
            return button;
        }

        button.RollOn += (_, e) => animatedButton.IsFocused = true;
        return button;
    }
}

class ButtonDimensions(float width = 21f, float height = 14f, DimensionUnitType widthUnits = DimensionUnitType.RelativeToChildren, DimensionUnitType heightUnits = DimensionUnitType.Absolute)
{
    public float Width { get; } = width;
    public float Height { get; } = height;
    public DimensionUnitType WidthUnits { get; } = widthUnits;
    public DimensionUnitType HeightUnits { get; } = heightUnits;
}

abstract class BaseAnimationChain
{
    public string Name { get; }
    public BaseAnimationChain(string name)
    {
        Name = name;
    }
    public abstract AnimationChain Get(TextureAtlas textureAtlas);
}

class RegionAnimationChain : BaseAnimationChain
{
    public RegionAnimationChain(string name) : base(name) { }

    public override AnimationChain Get(TextureAtlas textureAtlas)
    {
        AnimationChain animations = new AnimationChain();
        animations.Name = Name;

        TextureRegion textureRegion = textureAtlas.GetRegion(Name);

        AnimationFrame frames = new AnimationFrame
        {
            TopCoordinate = textureRegion.TopTextureCoordinate,
            BottomCoordinate = textureRegion.BottomTextureCoordinate,
            LeftCoordinate = textureRegion.LeftTextureCoordinate,
            RightCoordinate = textureRegion.RightTextureCoordinate,
            FrameLength = 0.3f,
            Texture = textureRegion.Texture
        };

        animations.Add(frames);
        return animations;
    }

}

class AnimationAnimationChain : BaseAnimationChain
{
    public AnimationAnimationChain(string name) : base(name) { }

    public override AnimationChain Get(TextureAtlas textureAtlas)
    {

        AnimationChain animations = new AnimationChain();
        animations.Name = Name;

        Animation animation = textureAtlas.GetAnimation(Name);
        foreach (TextureRegion region in animation.Frames)
        {
            AnimationFrame frames = new AnimationFrame
            {
                TopCoordinate = region.TopTextureCoordinate,
                BottomCoordinate = region.BottomTextureCoordinate,
                LeftCoordinate = region.LeftTextureCoordinate,
                RightCoordinate = region.RightTextureCoordinate,
                FrameLength = (float)animation.Delay.TotalSeconds,
                Texture = region.Texture
            };
            animations.Add(frames);
        }
        return animations;
    }
}

