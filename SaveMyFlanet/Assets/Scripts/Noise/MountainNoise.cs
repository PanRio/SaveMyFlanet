using UnityEngine;

public class MountainNoise : Noise
{
    public readonly Noise decisionNoise;
    public readonly Noise valleyNoise;
    public readonly Noise peakNoise;
    public readonly float border;

    public MountainNoise(float border = 0.5f)
    {
        this.decisionNoise = new SimplexNoise();
        this.valleyNoise = new RidgeNoise();
        this.peakNoise = new InverseNoise();
        this.border = border;
    }

    public MountainNoise(Noise decisionNoise, Noise valleyNoise, Noise peakNoise, float border = 0.5f)
    {
        this.decisionNoise = decisionNoise;
        this.valleyNoise = valleyNoise;
        this.peakNoise = peakNoise;
        this.border = border;
    }

    public override float Sample(Vector3 pos)
    {
        var decision = decisionNoise.Sample(pos);
        var low = valleyNoise.Sample(pos) * border;
        var high = peakNoise.Sample(pos) * (1f - border) + border;
        return Mathf.Lerp(low, high, decision);
    }
}
