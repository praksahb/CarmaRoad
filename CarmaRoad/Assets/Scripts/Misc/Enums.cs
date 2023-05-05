
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
    Large,
    Human,
}

// Create more accordingly, used in AnimalSO
public enum AllAnimals
{
    BlackCat,
}

// Car/ vehicle types 
public enum CarType
{
    Ambulance,
}

// spawn position left or right
public enum AnimalSpawnPosition
{
    Left,
    Right,
}