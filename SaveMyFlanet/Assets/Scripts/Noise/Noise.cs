using UnityEngine;

public abstract class Noise
{
    public abstract float Sample(Vector3 pos);

    private static int seed_ = 0;
    public static int seed
    {
        get
        {
            return seed_++;
        }
    }
}
