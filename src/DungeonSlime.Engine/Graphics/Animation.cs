
using System;
using System.Collections.Generic;

namespace DungeonSlime.Engine.Graphics;

public class Animation
{
    public List<TextureRegion> Frames { get; private set; }
    public TimeSpan Delay { get; private set; }

    public Animation()
    {
        Frames = new List<TextureRegion>();
        Delay = TimeSpan.FromMilliseconds(100);
    }

    public Animation(List<TextureRegion> frames, TimeSpan delay)
    {
        Frames = frames ?? throw new ArgumentNullException(nameof(frames));
        Delay = delay;
    }
}