// Run this single file with .NET 10 or later:
//     dotnet run Yoda.cs
//
// The package reference is kept in the source file so no .csproj is required
#:package CAPCOM.REDox@1.0.0

using REDox.Json;

var yoda = new Jedi
{
    Name = "Yoda",
    Age = 900,
    Species = "Unknown",
    Homeworld = "Unknown",
    Affiliation = "Jedi Order",
    Rank = "Grand Master",
    LightsaberColor = "Green",
    ForceAbilities = ["Telekinesis", "Force healing", "Force lightning resistance"],
    Traits = ["Wise", "Patient", "Short", "Powerful"],
    IsAlive = false
};

// Serialize and deserialize the Jedi
var json = JsonSerializer.Serialize(yoda);
var restored = JsonSerializer.Deserialize<Jedi>(json);

Console.WriteLine("Serialized Yoda:");
Console.WriteLine(json);
Console.WriteLine();
Console.WriteLine($"Restored Jedi: {restored?.Name}, age {restored?.Age}, rank {restored?.Rank}");
Console.WriteLine();

// Parse the JSON into REDox's editable token DOM and change it in place
using var doc = JsonDocument.Parse(json);

var root = doc.RootElement.AsObject();
root["Age"] = 901;                       
root["Rank"] = "Legendary Grand Master";   
root.Add("SpeciesNote", "A mysterious long-lived species");
root.Remove("IsAlive");                  

var traits = root["Traits"].AsArray();
traits.Add("Calm under pressure");         
traits.Insert(0, "Ancient"); 
traits.RemoveAt(3);

var editedJson = doc.RootElement.ToJsonString();

Console.WriteLine("Edited Yoda document:");
Console.WriteLine(editedJson);

public sealed class Jedi
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? Species { get; set; }
    public string? Homeworld { get; set; }
    public string? Affiliation { get; set; }
    public string? Rank { get; set; }
    public string? LightsaberColor { get; set; }
    public string[] ForceAbilities { get; set; } = [];
    public string[] Traits { get; set; } = [];
    public bool IsAlive { get; set; }
}
