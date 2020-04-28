using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueNoise : Noise
{
    public ValueNoise(Vector3Int size)
    {
        var rng = new System.Random();
        this.seed = rng.Next();
        this.size = size;
    }

    public ValueNoise(Vector3Int size, int seed)
    {
        this.seed = seed;
        this.size = size;
    }

    public readonly int seed;
    public readonly Vector3Int size;

    public override float Sample(Vector3 pos)
    {
        var posFloor = new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z);
        var result = 0f;
        for (var dx = 0; dx < 2; dx++)
        {
            for (var dy = 0; dy < 2; dy++)
            {
                for (var dz = 0; dz < 2; dz++)
                {
                    var samplePos = posFloor + new Vector3Int(dx, dy, dz);
                    var sampleValue = this.Sample(samplePos);
                    var sampleWeights = Vector3.one - Utils.Abs(pos - samplePos);
                    var sampleWeight = sampleWeights.x * sampleWeights.y * sampleWeights.z;
                    result += sampleValue * sampleWeight;
                }
            }
        }
        return result;
    }

    public float Sample(Vector3Int pos)
    {
        var posMod = Utils.Mod(pos, this.size);

        var randInt = Utils.RandInt(this.seed);
        randInt = Utils.RandInt(randInt + posMod.x);
        randInt = Utils.RandInt(randInt + posMod.y);
        return Utils.RandFloat(randInt + posMod.z);
    }
}
