using UnityEngine;

public class ModulatedNoise : Noise
{
    public readonly Noise startNoise;
    public readonly Noise endNoise;
    public readonly Noise modulationNoise;

    public ModulatedNoise()
    {
        this.startNoise = new SimplexNoise();
        this.endNoise = new RidgeNoise();
        this.modulationNoise = new InverseNoise();
    }

    public ModulatedNoise(Noise startNoise, Noise endNoise, Noise modulationNoise)
    {
        this.startNoise = startNoise;
        this.endNoise = endNoise;
        this.modulationNoise = modulationNoise;
    }

    public override float Sample(Vector3 pos)
    {
        var start = startNoise.Sample(pos);
        var end = endNoise.Sample(pos);
        var modulation = modulationNoise.Sample(pos);
        return Mathf.Lerp(start, end, modulation);
    }
}
