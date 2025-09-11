using Microsoft.Xna.Framework.Content.Pipeline;

using TImport = System.String;

namespace DungeonSlime.Library.PipeLine;

[ContentImporter(".txt", DisplayName = "Importer1", DefaultProcessor = "Processor1")]
public class Importer1 : ContentImporter<TImport>
{
    public override TImport Import(string filename, ContentImporterContext context)
    {
        return default(TImport);
    }
}
