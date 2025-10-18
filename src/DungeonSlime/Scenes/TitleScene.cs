using DungeonSlime.Engine;
using DungeonSlime.Engine.Input.Commands;
using DungeonSlime.Engine.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DungeonSlime.Scenes;

public class TitleScene : Scene
{
    private const string DUNGEON_TEXT = "Dungeon";
    private const string SLIME_TEXT = "Slime";
    private const string PRESS_ENTER_TEXT = "Press Enter To Start";

    private SpriteFont _font;
    private SpriteFont _fontTitle;


    private Vector2 _dungeonTextPosition;
    private Vector2 _dungeonTextOrigin;

    private Vector2 _slimeTextPosition;
    private Vector2 _slimeTextOrigin;

    private Vector2 _pressEnterTextPosition;
    private Vector2 _pressEnterTextOrigin;

    private Color _dropShadowColor = Color.Black * 0.5f;

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

        Vector2 sizeEnterText = _font.MeasureString(PRESS_ENTER_TEXT);
        _pressEnterTextPosition = new Vector2(640, 720 - padding - sizeEnterText.Y / 2);
        _pressEnterTextOrigin = sizeEnterText * 0.5f;

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
    }

    public override void Draw(GameTime gameTime)
    {
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));

        Core.SpriteBatch.DrawString(_fontTitle, DUNGEON_TEXT, _dungeonTextPosition + new Vector2(10, 10), _dropShadowColor, 0.0f, _dungeonTextOrigin, 1.0f, SpriteEffects.None, 1.0f);
        Core.SpriteBatch.DrawString(_fontTitle, DUNGEON_TEXT, _dungeonTextPosition, Color.White, 0.0f, _dungeonTextOrigin, 1.0f, SpriteEffects.None, 1.0f);


        Core.SpriteBatch.DrawString(_fontTitle, SLIME_TEXT, _slimeTextPosition + new Vector2(10, 10), _dropShadowColor, 0.0f, _slimeTextOrigin, 1.0f, SpriteEffects.None, 1.0f);
        Core.SpriteBatch.DrawString(_fontTitle, SLIME_TEXT, _slimeTextPosition, Color.White, 0.0f, _slimeTextOrigin, 1.0f, SpriteEffects.None, 1.0f);

        Core.SpriteBatch.DrawString(_font, PRESS_ENTER_TEXT, _pressEnterTextPosition, Color.White, 0.0f, _pressEnterTextOrigin, 1.0f, SpriteEffects.None, 1.0f);
        Core.SpriteBatch.End();
    }
}