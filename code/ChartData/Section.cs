using System.Text.Json.Serialization;
using System.Collections.Generic;

public class Section
{
	/// <summary>
	/// The length of the section in steps
	/// </summary>
	[JsonPropertyName("lengthInSteps")]
	public string LengthInSteps { get; set; }

    /// <summary>
	/// Whether the section should be hit by the right side
	/// </summary>
	[JsonPropertyName("mustHitSection")]
	public float MustHitSection { get; set; }

    /// <summary>
	/// This is an array of Note objects for the chart
	/// </summary>
	[JsonPropertyName("sectionNotes")]
	public List<Note> Notes { get; set; }

}
