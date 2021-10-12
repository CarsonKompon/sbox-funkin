using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Song
{
	/// <summary>
	/// The name of the song
	/// </summary>
	[JsonPropertyName("song")]
	public string Name { get; set; }

    /// <summary>
	/// The bpm of the song
	/// </summary>
	[JsonPropertyName("bpm")]
	public float BPM { get; set; }

    /// <summary>
	/// The scroll speed of the chart
	/// </summary>
	[JsonPropertyName("speed")]
	public float ScrollSpeed { get; set; }


	/// <summary>
	/// This is an array of the chart's Sections
	/// </summary>
	[JsonPropertyName("notes")]
	public List<Section> Sections { get; set; }
}
