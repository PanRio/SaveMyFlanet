using UnityEngine;

public class SwirlNoise : Noise
{
    public readonly SimplexNoise valueNoise;
    public readonly SimplexNoise deltaNoise;
    public readonly float swirliness;

    public SwirlNoise(float swirliness, int seed)
    {
        this.swirliness = swirliness;
        this.valueNoise = new SimplexNoise(seed);
        this.deltaNoise = new SimplexNoise(Utils.RandInt(seed) + 1);
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
