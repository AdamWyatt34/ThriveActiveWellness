using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Exercises.Domain.Equipment;
using ThriveActiveWellness.Modules.Exercises.Domain.Exercises.Exceptions;
using ThriveActiveWellnessAPI.Domain.Shared;

namespace ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

public class Exercise : Entity
{
    public ExerciseId Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Difficulty { get; private set; }
    public EquipmentTableId EquipmentTableId { get; private set; }
    public ExerciseTableId TableId { get; private set; }
    
    private readonly List<Media> _media;
    public IReadOnlyList<Media> Media => [.. _media];

    private readonly List<ExerciseMuscleGroup> _exerciseMuscleGroups;
    public IReadOnlyList<ExerciseMuscleGroup> ExerciseMuscleGroups => [.. _exerciseMuscleGroups];

    private Exercise()
    {
    }
    
    private Exercise(
        ExerciseId id,
        string name,
        string description,
        string difficulty,
        Equipment.Equipment equipment)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Difficulty = difficulty ?? throw new ArgumentNullException(nameof(difficulty));
        EquipmentTableId = equipment.TableId;
        _exerciseMuscleGroups = [];
        _media = [];
    }
    
    public static Exercise Create(
        ExerciseId id,
        string name,
        string description,
        string difficulty,
        Equipment.Equipment equipment)
    {
        return new Exercise(id, name, description, difficulty, equipment);
    }
    
    public void AddMuscleGroup(MuscleGroup muscleGroup, MuscleGroupType type)
    {
        // Business rules and validations here
        var emg = ExerciseMuscleGroup.Create(this, muscleGroup, type);
        _exerciseMuscleGroups.Add(emg);
    }
    
    public void AddMedia(string url, string description, MediaType type)
    {
        // Potential business rules and validations here...
        var newMedia = Exercises.Media.Create(url, description, type);
        _media.Add(newMedia);
    }

    public void AddMedia(Uri url, string description, MediaType type)
    {
        AddMedia(url.AbsoluteUri, description, type);
    }
    
    public void ReplaceMedia(List<Media> media)
    {
        _media.Clear();
        _media.AddRange(media);
    }

    public void Update(
        string name,
        string description,
        string difficulty,
        EquipmentTableId equipmentTableId,
        List<MuscleGroup> muscleGroups)
    {
        Name = name;
        Description = description;
        Difficulty = difficulty;
        EquipmentTableId = equipmentTableId;
        _exerciseMuscleGroups.Clear();
        _exerciseMuscleGroups.AddRange(muscleGroups.Select(mg => ExerciseMuscleGroup.Create(this, mg, MuscleGroupType.Primary)));
    }

    public void AddMedia(Uri url, string fileName, string description, MediaType type)
    {
        var media = Domain.Exercises.Media.Create(url, fileName, type);
        _media.Add(media);
        
        Raise(new MediaUploadedDomainEvent(
            Id.Value,
            url,
            fileName,
            description,
            type));
    }

    public void UpdateMediaUrl(Uri temporaryUrl, Uri permanentUrl)
    {
        Media? media = _media.Find(m => m.Url == temporaryUrl.AbsoluteUri);
        if (media is null)
        {
            throw new MediaNotFoundException(Id.Value);
        }

        media.UpdateUrl(permanentUrl.AbsoluteUri);
    }
}
