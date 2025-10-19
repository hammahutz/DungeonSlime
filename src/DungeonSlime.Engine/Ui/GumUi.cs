using Gum.Forms;
using Gum.Forms.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;

namespace DungeonSlime.Engine.Ui;

public class GumUi
{
    public GumUi(float scale = 1.0f)
    {
        GumService.Default.Initialize(Core.Instance, DefaultVisualsVersion.V2);
        GumService.Default.ContentLoader.XnaContentManager = Core.Content; //Core.Content; ????

        FrameworkElement.KeyboardsForUiControl.Add(GumService.Default.Keyboard);
        FrameworkElement.GamePadsForUiControl.AddRange(GumService.Default.Gamepads);

        FrameworkElement.TabReverseKeyCombos.Add(new KeyCombo() {PushedKey = Keys.Up});
        FrameworkElement.TabKeyCombos.Add(new KeyCombo() {PushedKey = Keys.Down});

        GumService.Default.CanvasWidth = Core.GraphicsDevice.PresentationParameters.BackBufferWidth / scale;
        GumService.Default.CanvasHeight = Core.GraphicsDevice.PresentationParameters.BackBufferHeight / scale;
        GumService.Default.Renderer.Camera.Zoom = scale;
    }

    public void Update(GameTime gameTime) => GumService.Default.Update(gameTime);
    public void Draw() => GumService.Default.Draw();

    public void Clear() => GumService.Default.Root.Children.Clear();
}