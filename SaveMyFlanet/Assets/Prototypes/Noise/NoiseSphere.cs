using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseSphere : MonoBehaviour
{
    public float scale = 16f;

    public int numOctaves = 4;
    public float persistence = 0.5f;

    public float valleySwirliness = 0.5f;
    public float peakSwirliness = 0.5f;
    public float groundLevel = 0.5f;
    public float border = 0.5f;

    private Mesh mesh;
    private List<Vector3> baseVertices;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = new List<Vector3>(mesh.vertices);
    }

    [EasyButtons.Button]
    void UpdateMesh()
    {
        var noise = new OctaveNoise
        (
            slave: new MountainNoise
            (
                decisionNoise: new SimplexNoise(seed: 0),
                valleyNoise: new ScaledNoise
                (
                    slave: new RidgeNoise
                    (
                        new SwirlNoise
                        (
                            valueNoise: new SimplexNoise(seed: 1),
                            deltaNoise: new SimplexNoise(seed: 2),
                            swirliness: valleySwirliness
                        )
                    ),
                    scale: 2f
                ),
                peakNoise: new InverseNoise
                (
                    new ScaledNoise
                    (
                        slave: new RidgeNoise
                        (
                            new SwirlNoise
                            (
                                valueNoise: new SimplexNoise(seed: 3),
                                deltaNoise: new SimplexNoise(seed: 4),
                                swirliness: peakSwirliness
                            )
                        ),
                        scale: 4f
                    )
                ),
                border: border
            ),
            numOctaves: numOctaves,
            persistence: persistence
        );
        
        Vector3[] vertices = new Vector3[baseVertices.Count];

        for (int i = 0; i < vertices.Length; i++)
        {
            var vertexPos = baseVertices[i];
            var samplePos = vertexPos * scale;
            var sampled = noise.Sample(samplePos);
            var height = sampled * (1f - groundLevel) + groundLevel;

            vertices[i] = vertexPos * height;
        }

        mesh.vertices = vertices;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
