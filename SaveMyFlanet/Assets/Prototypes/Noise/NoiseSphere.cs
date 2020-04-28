using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseSphere : MonoBehaviour
{
    public float scale = 16f;

    public int numOctaves = 8;
    public float valleyPersistence = 0.5f;
    public float mountainPersistence = 0.9f;
    
    public float groundLevel = 0.5f;

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
        var noise = new MountainNoise
        (
            decisionNoise: new SimplexNoise(),
            valleyNoise: new OctaveNoise
            (
                slave: new SimplexNoise(),
                numOctaves: numOctaves,
                persistence: valleyPersistence
            ),
            peakNoise: new OctaveNoise
            (
                slave: new RidgeNoise(),
                numOctaves: numOctaves,
                persistence: mountainPersistence
            )
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
