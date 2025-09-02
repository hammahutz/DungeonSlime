using System;

using DungeonSlime.Library;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DungeonSlime;

public class Game1 : Core
{
    private SpriteFont _font;
    private Texture2D _logo;
    private Rectangle iconSourceRect = new(0, 0, 128, 128);
    private Rectangle wordmarkSourceRect = new(150, 34, 458, 58);

    public Game1()
        : base("DungeonSlime", width: 1920, height: 1080) { }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _font = Content.Load<SpriteFont>("Fonts/Font");
        _logo = Content.Load<Texture2D>("Images/Logo");
    }

    protected override void Update(GameTime gameTime)
    {
        if (
            GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        )
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        SpriteBatch.Begin();
        SpriteBatch.DrawString(_font, "Hello World", new Vector2(100, 100), Color.White);
#if OPENGL
        SpriteBatch.DrawString(_font, "OpenGL", new Vector2(132, 135), Color.Blue);
#endif

        SpriteBatch.Draw(_logo, new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height) / 2, iconSourceRect, Color.Red * 0.5f, 0, new Vector2(_logo.Width, _logo.Height) / 2, 1.0f, SpriteEffects.None, 0.0f);
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
