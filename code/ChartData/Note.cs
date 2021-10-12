using System.Text.Json.Serialization;
using System.Collections.Generic;

public class Note
{
	/// <summary>
	/// The length of the section in steps
	/// </summary>
	[JsonPropertyName("lengthInSteps")]
	public float LengthInSteps { get; set; }

    /// <summary>
	/// Whether the section should be hit by the right side
	/// </summary>
	[JsonPropertyName("mustHitSection")]
	public bool MustHitSection { get; set; }

    /// <summary>
	/// This is an array of Note objects for the chart
	/// </summary>
	[JsonPropertyName("sectionNotes")]
	public List<List<float>> Notes { get; set; }

}
