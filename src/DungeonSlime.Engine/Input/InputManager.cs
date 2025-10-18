using DungeonSlime.Engine.Input.Commands;

using Microsoft.Xna.Framework;

namespace DungeonSlime.Engine.Input;

public class InputManager
{
    public KeyboardInfo Keyboard { get; private set; }
    public MouseInfo Mouse { get; private set; }
    public GamePadInfo[] GamePads { get; private set; }


    public CommandHandler Commands { get; private set; }

    public InputManager()
    {
        Keyboard = new KeyboardInfo();
        Mouse = new MouseInfo();
        GamePads = new GamePadInfo[4];

        Commands = new CommandHandler();
    }
    public InputManager(bool includeKeyboard, bool includeMouse, int gamePadCount)
    {
        if (includeKeyboard)
            Keyboard = new KeyboardInfo();
        if (includeMouse)
            Mouse = new MouseInfo();
        if (gamePadCount > 0)
        {
            GamePads = new GamePadInfo[gamePadCount];
            for (int i = 0; i < gamePadCount && i < 4; i++)
            {
                GamePads[i] = new GamePadInfo((PlayerIndex)i);
            }
        }
    }

    public void Update(GameTime gameTime)
    {
        Keyboard?.Update(gameTime);
        Mouse?.Update(gameTime);

        if (GamePads is not null)
        {
            for (int i = 0; i < GamePads.Length; i++)
            {
                GamePads[i]?.Update(gameTime);
            }
        }
        Commands.Update(Keyboard, Mouse, GamePads);
    }
}