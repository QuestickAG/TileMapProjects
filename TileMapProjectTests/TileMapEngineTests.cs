using System.Diagnostics;
using TileMapEngine.Enums;
using TileMapEngine.Models;

namespace TileMapProjectTests;

public class Tests
{
    private const int Width = 1000;
    private const int Height = 1000;
    private TileLayer _layer;
    
    [SetUp]
    public void Setup()
    {
        _layer = new TileLayer(Width, Height);
    }

    [Test]
    public void GetTileType_DefaultIsPlain()
    {
        Assert.AreEqual(SurfaceType.Plain, _layer.GetTile(0, 0));
        Assert.AreEqual(SurfaceType.Plain, _layer.GetTile(999, 999));
    }
    
    [Test]
    public void GetTileType_OutOfBounds_Throws()
    {
        Assert.Catch(typeof(ArgumentOutOfRangeException), () => _layer.GetTile(-1, 999));
    }
    
    [Test]
    public void SetAndGetTileType_CorrectlyUpdatesValue()
    {
        _layer.SetTile(10, 20, SurfaceType.Mountain);
        Assert.AreEqual(SurfaceType.Mountain, _layer.GetTile(10, 20));

        _layer.SetTile(10, 20, SurfaceType.Plain);
        Assert.AreEqual(SurfaceType.Plain, _layer.GetTile(10, 20));
    }
    
    [Test]
    public void FromEnumerable_CreatesCorrectLayer()
    {
        var tiles = new List<(int, int, SurfaceType)>
        {
            (1, 1, SurfaceType.Mountain),
            (2, 2, SurfaceType.Plain)
        };

        var layer = TileLayer.FromEnumerable(tiles, 3, 3);

        Assert.AreEqual(SurfaceType.Mountain, layer.GetTile(1, 1));
        Assert.AreEqual(SurfaceType.Plain, layer.GetTile(2, 2));
        Assert.AreEqual(3, layer.Width);
        Assert.AreEqual(3, layer.Height);
    }
    
    [Test]
    public void Performance_AccessTime([Values(0, 999, 500)] int x, [Values(0,999, 500)] int y)
    {
        var sw = Stopwatch.StartNew();
        for (int i = 0; i < 1000000; i++)
        {
            _layer.GetTile(x, y);
        }
        sw.Stop();

        Assert.IsTrue(sw.ElapsedMilliseconds < 50,
            $"Access time too slow: {sw.ElapsedMilliseconds}ms");
    }
}