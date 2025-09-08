using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using DungeonSlime.Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonSlime.Library;

public class TextureAtlas
{
    private Dictionary<string, TextureRegion> _regions;
    public Texture2D Texture { get; set; }

    public TextureAtlas()
    {
        _regions = new Dictionary<string, TextureRegion>();
    }

    public TextureAtlas(Texture2D texture)
    {
        Texture = texture;
        _regions = new Dictionary<string, TextureRegion>();
    }

    public void AddRegion(string name, int x, int y, int width, int height) =>
        _regions.Add(name, new TextureRegion(Texture, x, y, width, height));

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

    public void Clear() => _regions.Clear();

    public static TextureAtlas FromFile(ContentManager content, string fileName)
    {
        TextureAtlas atlas = new TextureAtlas();

        string filePath = Path.Combine(content.RootDirectory, fileName);

        using (Stream stream = TitleContainer.OpenStream(fileName))
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
                        int widht = int.Parse(region.Attribute("widht")?.Value ?? "0");
                        int height = int.Parse(region.Attribute("height")?.Value ?? "0");

                        atlas.AddRegion(name, x, y, widht, height);
                    }
                }
            }
        }

        return atlas;
    }
}
