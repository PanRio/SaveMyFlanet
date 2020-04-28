using UnityEngine;

public class SwirlNoise : Noise
{
    public readonly Noise valueNoise;
    public readonly Noise deltaNoise;
    public readonly float swirliness;

    public SwirlNoise(float swirliness)
    {
        this.valueNoise = new SimplexNoise();
        this.deltaNoise = new SimplexNoise();
        this.swirliness = swirliness;
    }

    public SwirlNoise(Noise valueNoise, Noise deltaNoise, float swirliness)
    {
        this.valueNoise = valueNoise;
        this.deltaNoise = deltaNoise;
        this.swirliness = swirliness;
    }

    public override float Sample(Vector3 pos)
    {
        var dx = deltaNoise.Sample(pos);
        var dy = deltaNoise.Sample(pos + Vector3.one);
        var dz = deltaNoise.Sample(pos - Vector3.one);
        var samplePos = pos + new Vector3(dx, dy, dz) * swirliness;
        return valueNoise.Sample(samplePos);
    }
}
