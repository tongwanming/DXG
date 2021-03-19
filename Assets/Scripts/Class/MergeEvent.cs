public class MergeEvent
{
    public LevelSphere SphereA { get; set; }
    public LevelSphere SphereB { get; set; }

    public MergeEvent(LevelSphere sphereA, LevelSphere sphereB)
    {
        SphereA = sphereA;
        SphereB = sphereB;
    }

    private MergeEvent()
    {
    }
}