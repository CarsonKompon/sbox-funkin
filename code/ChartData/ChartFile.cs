using System.Collections.Generic;
using System.Text.Json.Serialization;

public class ChartFile
{
    /// <summary>
	/// The object that contains all of the chart's data
	/// </summary>
	[JsonPropertyName("song")]
	public Song Song { get; set; }
}