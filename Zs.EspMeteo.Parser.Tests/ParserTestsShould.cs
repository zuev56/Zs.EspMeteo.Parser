using System.IO;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Zs.EspMeteo.Parser.Tests;

public sealed class ParserTestsShould
{
    [Theory]
    [InlineData("Assets/EspMeteoPage_Dht_1.html", 1)]
    [InlineData("Assets/EspMeteoPage_Dht_2.html", 1)]
    [InlineData("Assets/EspMeteoPage_Dht_1_&_Dht_2.html", 2)]
    public void ParseHtmlToEspMeteoInstance(string partialPath, int externalSensorsAmount)
    {
        var html = GetFileContentByPartialPath(partialPath);
        var parser = new Parser();
        var espMeteo = parser.ParseInternal(html);

        espMeteo.Sensors.Count.Should().Be(1 + externalSensorsAmount);
        espMeteo.Sensors.Should().Contain(s => s.Name == "BMP085/180");
    }

    private string GetFileContentByPartialPath(string partialPath)
    {
        var fullPath = GetFullPath(partialPath);
        var content = File.ReadAllText(fullPath);
        return content;
    }

    private static string GetFullPath(string relativePath)
    {
        var assemblyLocation = Assembly.GetExecutingAssembly().Location;
        var root = Path.GetDirectoryName(assemblyLocation)!;
        var fullPath = $"{root}{Path.DirectorySeparatorChar}{relativePath}";
        return fullPath;
    }
}