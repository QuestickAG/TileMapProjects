namespace TileMapEngine.Models;
using TileMapEngine.Enums;

internal interface ITileLayer
{
    int Width { get; }
    int Height { get; }

    SurfaceType GetTile(int x, int y);
    void SetTile(int x, int y, SurfaceType tile);
}

