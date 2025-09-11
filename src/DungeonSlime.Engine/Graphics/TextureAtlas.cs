using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonSlime.Engine.Graphics;

public class TextureAtlas
{
    private readonly Dictionary<string, TextureRegion> _regions;
    private readonly Dictionary<string, Animation> _animations;
    public Texture2D Texture { get; set; }

    public TextureAtlas()
    {
        _regions = new Dictionary<string, TextureRegion>();
        _animations = new Dictionary<string, Animation>();
    }

    public TextureAtlas(Texture2D texture)
    {
        Texture = texture;
        _regions = new Dictionary<string, TextureRegion>();
        _animations = new Dictionary<string, Animation>();
    }

    public void AddRegion(string name, int x, int y, int width, int height) => _regions.Add(name, new TextureRegion(Texture, x, y, width, height));

    public void AddAnimation(string animationName, Animation animation) => _animations.Add(animationName, animation);

    public TextureRegion GetRegion(string name)
    {
        try
        {
            return _regions[name];
        }
        catch (System.Exception)
        {
            throw new KeyNotFoundException($"Can not find region with name {name}");
        }
    }

    public Animation GetAnimation(string name)
    {
        try
        {
            return _animations[name];
        }
        catch (System.Exception)
        {
            throw new KeyNotFoundException($"Can not find region with name {name}");
        }
    }


    public void RemoveRegion(string name)
    {
        try
        {
            _regions.Remove(name);
        }
        catch (System.Exception)
        {
            throw new KeyNotFoundException($"Can not delete the region with name {name}");
        }
    }
    public void RemoveAnimation(string name)
    {
        try
        {
            _animations.Remove(name);
        }
        catch (System.Exception)
        {
            throw new KeyNotFoundException($"Can not delete the region with name {name}");
        }
    }

    public void ClearRegion() => _regions.Clear();
    public void ClearAnimation() => _animations.Clear();

    public static TextureAtlas FromFile(ContentManager content, params string[] relativeFilePath)
    {
        TextureAtlas atlas = new TextureAtlas();

        string filePath = Path.Combine([content.RootDirectory, .. relativeFilePath]);

        try
        {
            using (Stream stream = TitleContainer.OpenStream(filePath))
            {
                using (XmlReader reader = XmlReader.Create(stream))
                {
                    XDocument document = XDocument.Load(reader);
                    XElement root = document.Root;

                    string texturePath = root.Element("Texture").Value;
                    atlas.Texture = content.Load<Texture2D>(texturePath);

                    var regions = root.Element("Regions")?.Elements("Region");

                    if (regions is not null)
                    {
                        foreach (var region in regions)
                        {
                            string name = region.Attribute("name")?.Value;
                            if (string.IsNullOrEmpty(name))
                            {
                                continue;
                            }

                            int x = int.Parse(region.Attribute("x")?.Value ?? "0");
                            int y = int.Parse(region.Attribute("y")?.Value ?? "0");
                            int width = int.Parse(region.Attribute("width")?.Value ?? "0");
                            int height = int.Parse(region.Attribute("height")?.Value ?? "0");

                            atlas.AddRegion(name, x, y, width, height);
                        }
                    }

                    var animations = root.Element("Animations")?.Elements("Animation");
                    if (animations is not null)
                    {
                        foreach (var animationElement in animations)
                        {
                            var frames = new List<TextureRegion>();
                            var frameElements = animationElement.Elements("Frame");

                            if (frameElements is not null)
                            {
                                foreach (var frameElement in frameElements)
                                {
                                    var regionName = frameElement.Attribute("region").Value;
                                    var region = atlas.GetRegion(regionName);
                                    frames.Add(region);
                                }
                            }

                            var name = animationElement.Attribute("name")?.Value;
                            var delay = TimeSpan.FromMilliseconds(float.Parse(animationElement.Attribute("delay")?.Value ?? "100"));

                            atlas.AddAnimation(name, new Animation(frames, delay));
                        }
                    }
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            throw new FileNotFoundException(
                $"Could not find the atlas definition file: {filePath}",
                ex
            );
        }
        catch (XmlException ex)
        {
            throw new XmlException($"Error parsing the atlas definition file: {filePath}", ex);
        }
        catch (System.Exception ex)
        {
            throw new System.Exception(
                $"An error occurred while loading the texture atlas from file: {filePath}",
                ex
            );
        }

        return atlas;
    }

    public Sprite CreateSprite(string regionName) => new Sprite(GetRegion(regionName));
}