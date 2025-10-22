using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


using DungeonSlime.Engine;
using DungeonSlime.Engine.Input.Commands;
using DungeonSlime.Engine.Scenes;

using DungeonSlime.Scenes.Game;
using DungeonSlime.Engine.UI;


namespace DungeonSlime.Scenes.Title;

public class TitleScene : Scene
{
    private const string DUNGEON_TEXT = "Dungeon";
    private const string SLIME_TEXT = "Slime";
    private const string PRESS_ENTER_TEXT = "Press Enter To Start";

    private SpriteFont _font;
    private SpriteFont _fontTitle;
    private Color _dropShadowColor = Color.Black * 0.5f;


    private Vector2 _dungeonTextPosition;
    private Vector2 _dungeonTextOrigin;

    private Vector2 _slimeTextPosition;
    private Vector2 _slimeTextOrigin;

    private Vector2 _pressEnterTextPosition;
    private Vector2 _pressEnterTextOrigin;

    private Texture2D _backgroundPattern;
    private Rectangle _backgroundDestination;
    private Vector2 _backgroundOffset;
    private float _scollSpeed = 50.0f;

    protected override BaseUI UI => new TitleUI(Content);

    public TitleScene(ContentManager content) : base(content) { }

    public override void Initialize()
    {
        base.Initialize();

        float padding = 100;
        float gap = 10;

        Vector2 sizeDugeonText = _fontTitle.MeasureString(DUNGEON_TEXT);
        _dungeonTextPosition = new Vector2(640, padding + sizeDugeonText.Y / 2);
        _dungeonTextOrigin = sizeDugeonText * 0.5f;

        Vector2 sizeSlimeText = _fontTitle.MeasureString(SLIME_TEXT);
        _slimeTextPosition = new Vector2(640, _dungeonTextPosition.Y + sizeDugeonText.Y / 2 + sizeSlimeText.Y / 2 + gap);
        _slimeTextOrigin = sizeSlimeText * 0.5f;

        _backgroundOffset = Vector2.Zero;
        _backgroundDestination = Core.GraphicsDevice.PresentationParameters.Bounds;
    }

    public override void RegisterCommands()
    {
        Core.Input.Commands.RegisterKeyboardCommand(new Command<KeyboardState, Keys>(Keys.Enter, InputTrigger.JustPressed, () =>
        {
            Core.Scenes.ChangeScene(new GameScene(Content));
        }));
    }

    public override void LoadContent()
    {
        base.LoadContent();
        _font = Content.Load<SpriteFont>("fonts/font");
        _fontTitle = Content.Load<SpriteFont>("fonts/fontTitle");
        _backgroundPattern = Content.Load<Texture2D>("images/background-pattern");
    }

    public override void Update(GameTime gameTime)
    {
        float offset = _scollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        _backgroundOffset.X -= offset;
        _backgroundOffset.Y -= offset;
        _backgroundOffset.X %= _backgroundPattern.Width;
        _backgroundOffset.Y %= _backgroundPattern.Height;

    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));

        Core.SpriteBatch.Begin(samplerState: SamplerState.PointWrap);
        Core.SpriteBatch.Draw(_backgroundPattern, _backgroundDestination, new Rectangle(_backgroundOffset.ToPoint(), _backgroundDestination.Size), Color.White * 0.5f);
        Core.SpriteBatch.End();

        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        Core.SpriteBatch.DrawString(_fontTitle, DUNGEON_TEXT, _dungeonTextPosition + new Vector2(10, 10), _dropShadowColor, 0.0f, _dungeonTextOrigin, 1.0f, SpriteEffects.None, 1.0f);
        Core.SpriteBatch.DrawString(_fontTitle, DUNGEON_TEXT, _dungeonTextPosition, Color.White, 0.0f, _dungeonTextOrigin, 1.0f, SpriteEffects.None, 1.0f);


        Core.SpriteBatch.DrawString(_fontTitle, SLIME_TEXT, _slimeTextPosition + new Vector2(10, 10), _dropShadowColor, 0.0f, _slimeTextOrigin, 1.0f, SpriteEffects.None, 1.0f);
        Core.SpriteBatch.DrawString(_fontTitle, SLIME_TEXT, _slimeTextPosition, Color.White, 0.0f, _slimeTextOrigin, 1.0f, SpriteEffects.None, 1.0f);

        Core.SpriteBatch.DrawString(_font, PRESS_ENTER_TEXT, _pressEnterTextPosition, Color.White, 0.0f, _pressEnterTextOrigin, 1.0f, SpriteEffects.None, 1.0f);
        Core.SpriteBatch.End();
    }


}