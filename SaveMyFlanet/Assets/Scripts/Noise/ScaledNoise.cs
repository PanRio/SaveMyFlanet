using UnityEngine;

public class ScaledNoise : Noise
{
    public readonly Noise slave;
    public readonly float scale;

    public ScaledNoise(float scale)
    {
        this.slave = new SimplexNoise();
        this.scale = scale;
    }

    public ScaledNoise(Noise slave, float scale)
    {
        this.slave = slave;
        this.scale = scale;
    }

    public override float Sample(Vector3 pos)
    {
        return this.slave.Sample(pos * scale);
    }
}
