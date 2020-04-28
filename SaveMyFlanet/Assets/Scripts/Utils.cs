using UnityEngine;

public static class Utils
{
    public static float Mod(float a, float b)
    {
        return (a % b + b) % b;
    }

    public static int Mod(int a, int b)
    {
        return (a % b + b) % b;
    }

    public static Vector3 Mod(Vector3 a, Vector3 b)
    {
        return new Vector3(Mod(a.x, b.x), Mod(a.y, b.y), Mod(a.z, b.z));
    }

    public static Vector3Int Mod(Vector3Int a, Vector3Int b)
    {
        return new Vector3Int(Mod(a.x, b.x), Mod(a.y, b.y), Mod(a.z, b.z));
    }

    public static Vector3 Abs(Vector3 v)
    {
        return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    }

    public static float RandFloat(int seed)
    {
        var rng = new System.Random(seed);
        return (float)rng.NextDouble();
    }

    public static int RandInt(int seed)
    {
        var rng = new System.Random(seed);
        return rng.Next();
    }
}
