using UnityEngine;

public class RidgeNoise : Noise
{
    public readonly Noise slave;

    public RidgeNoise()
    {
        this.slave = new SimplexNoise();
    }

    public RidgeNoise(Noise slave)
    {
        this.slave = slave;
    }

    public override float Sample(Vector3 pos)
    {
        return Mathf.Abs(this.slave.Sample(pos) * 2f - 1f);
    }
}
