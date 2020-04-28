using UnityEngine;

public class InverseNoise : Noise
{
    public readonly Noise slave;

    public InverseNoise()
    {
        this.slave = new SimplexNoise();
    }

    public InverseNoise(Noise slave)
    {
        this.slave = slave;
    }

    public override float Sample(Vector3 pos)
    {
        return 1f - this.slave.Sample(pos);
    }
}
