using UnityEngine;

public class ContrastNoise : Noise
{
    public readonly Noise slave;
    public readonly float contrast;
    private readonly float multiplier;

    public ContrastNoise(float contrast)
    {
        this.slave = new SimplexNoise();
        this.contrast = contrast;
        this.multiplier = Mathf.Pow(2f, contrast - 1f);
    }

    public ContrastNoise(Noise slave, float contrast)
    {
        this.slave = slave;
        this.contrast = contrast;
        this.multiplier = Mathf.Pow(2f, contrast - 1f);
    }

    public override float Sample(Vector3 pos)
    {
        var sampled = this.slave.Sample(pos);
        if (sampled < 0.5f)
        {
            return Mathf.Pow(sampled, contrast) * multiplier;
        }
        else
        {
            return 1f - Mathf.Pow(1f - sampled, contrast) * multiplier;
        }
    }
}
