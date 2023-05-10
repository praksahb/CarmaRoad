namespace CarmaRoad.Enum
{

    // for use in calling different animation according to state
    public enum AnimalAnimationClip
    {
        Idle,
        Walk,
        Run,
        Dead,
    }

    // only enum following the none protocol 
    // will be used for animal state machine behaviour
    public enum AnimalType
    {
        None,
        Small,
        Medium,
        Large,
        Human,
    }

    // Create more accordingly, used in AnimalSO
    public enum AllAnimals
    {
        Cat,
        Horse,
        OldMan,
        Hyena,
        Dog
    }

    // Car/ vehicle types 
    public enum CarType
    {
        Ambulance,
        Taxi,
    }

    // spawn position left or right
    public enum AnimalSpawnPosition
    {
        Left,
        Right,
    }

    public enum LevelDifficulty
    {
        None,
        Easy,
        Medium,
        Hard,
        Impossible,
    }
}