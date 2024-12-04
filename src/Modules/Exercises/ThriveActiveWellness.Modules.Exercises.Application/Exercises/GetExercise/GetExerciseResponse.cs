namespace ThriveActiveWellness.Modules.Exercises.Application.Exercises.GetExercise;

public class GetExerciseResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Instructions { get; set; }
    public string Notes { get; set; }
    public string Difficulty { get; set; }
    public string Category { get; set; }
    public string Equipment { get; set; }
    public string MuscleGroup { get; set; }
    public ICollection<MediaDto> Media { get; set; }

    public class MediaDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
