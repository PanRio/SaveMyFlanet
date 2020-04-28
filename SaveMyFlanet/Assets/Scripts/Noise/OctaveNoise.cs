using UnityEngine;

public class OctaveNoise : Noise
{
    public readonly Noise slave;
    public readonly int numOctaves;
    public readonly float persistence;

    public OctaveNoise(int numOctaves = 4, float persistence = 0.75f)
    {
        this.slave = new SimplexNoise();
        this.numOctaves = numOctaves;
        this.persistence = persistence;
    }

    public OctaveNoise(Noise slave, int numOctaves = 4, float persistence = 0.75f)
    {
        this.slave = slave;
        this.numOctaves = numOctaves;
        this.persistence = persistence;
    }

    public override float Sample(Vector3 pos)
    {
        var totalValue = 0f;
        var totalWeight = 0f;
        for (var i = 0; i < numOctaves; i++)
        {
            var weight = Mathf.Pow(persistence, i);
            totalValue += slave.Sample(pos * Mathf.Pow(2f, i)) * weight;
            totalWeight += weight;
        }
        return totalValue / totalWeight;
    }
}
