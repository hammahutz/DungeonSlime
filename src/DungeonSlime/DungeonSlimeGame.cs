using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using DungeonSlime.Engine;
using DungeonSlime.Engine.Input.Commands;
using DungeonSlime.Scenes;


namespace DungeonSlime;

public class DungeonSlimeGame : Core
{
    private Song _themeSong;

    public DungeonSlimeGame() : base("DungeonSlime", width: 1280, height: 720) { }

    protected override void Initialize()
    {
        base.Initialize();
        Audio.PlaySong(_themeSong);
        Scenes.ChangeScene(new TitleScene(Content));
    }

    protected override void LoadContent()
    {
        _themeSong = Content.Load<Song>("audio/theme");
    }

    protected override void RegisterDefaultCommands()
    {
        Input.GlobalCommands.RegisterKeyboardCommand(new Command<KeyboardState, Keys>(Keys.Escape, InputTrigger.JustPressed, () =>
        {
            if (ExitOnEscape && Input.Keyboard.IsDown(Keys.Escape))
            {
                Exit();
            }
        }));

    }
}