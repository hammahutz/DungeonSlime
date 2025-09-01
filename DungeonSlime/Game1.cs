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

    public Game1()
        : base("DungeonSlime") { }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Console.WriteLine("Hello");

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

        SpriteBatch.Draw(_logo, Vector2.Zero, Color.White);
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
