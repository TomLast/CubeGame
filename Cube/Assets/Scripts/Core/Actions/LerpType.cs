public abstract class LerpType
{
    public float Time;
}

public class Lerp : LerpType
{
}

public class Tick : LerpType
{
    public float TickIntervall;
}
