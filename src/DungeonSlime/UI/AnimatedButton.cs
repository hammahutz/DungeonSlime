using DungeonSlime.Engine.Graphics;

using Gum.Forms.Controls;
using Gum.Forms.DefaultVisuals;

namespace DungeonSlime.UI;

class AnimatedButton : Button
{
    public AnimatedButton(TextureAtlas textureAtlas)
    {
        ButtonVisual buttonVisual = SetVisual();
    }

    private void SetVisual()
    {
        ButtonVisual buttonVisual = (ButtonVisual)Visual;

        buttonVisual.WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToChildren;
        buttonVisual.Width = 21f;

        buttonVisual.HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute;
        buttonVisual.Height = 14f;
    }
}