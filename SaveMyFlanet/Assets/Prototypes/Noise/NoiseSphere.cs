using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseSphere : MonoBehaviour
{
    public float border = 0.5f;

    private Mesh mesh;
    private List<Vector3> baseVertices;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = new List<Vector3>(mesh.vertices);
    }

    void Update()
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
                            swirliness: 0.5f
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
                                swirliness: 0.5f
                            )
                        ),
                        scale: 4f
                    )
                ),
                border: border
            ),
            numOctaves: 4,
            persistence: 0.75f
        );
        
        Vector3[] vertices = new Vector3[baseVertices.Count];

        for (int i = 0; i < vertices.Length; i++)
        {
            var vertexPos = baseVertices[i];
            var sampled = noise.Sample(vertexPos);

            vertices[i] = vertexPos * sampled;
        }

        mesh.vertices = vertices;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
