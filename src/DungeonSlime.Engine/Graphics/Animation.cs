
using System;
using System.Collections.Generic;

namespace DungeonSlime.Engine.Graphics;

public class Animation
{
    public List<TextureRegion> Frames { get; private set; }
    public TimeSpan Delay { get; private set; }
    public string Name { get; }

    public Animation(string name)
    {
        Name = name;
        Frames = new List<TextureRegion>();
        Delay = TimeSpan.FromMilliseconds(100);
    }

    public Animation(string name, List<TextureRegion> frames, TimeSpan delay)
    {
        Name = name;
        Frames = frames ?? throw new ArgumentNullException(nameof(frames));
        Delay = delay;
    }
}