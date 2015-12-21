
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(MeshCollider))]
public class PolyColToMeshCol : MonoBehaviour
{

    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        Mesh collisionMesh = new Mesh();

        Vector3[] verts = new Vector3[sprite.vertices.Length];
        for (int i = 0; i < verts.Length; i++)
        {
            verts[i] = sprite.vertices[i];
        }
        int spriteTrisLength = sprite.triangles.Length;
        int[] tris = new int[spriteTrisLength];// * 2];
        for (int i = 0; i < spriteTrisLength - 1; i++)
        {
            tris[i] = sprite.triangles[i];
        }

        /*for (int j = 0; j < spriteTrisLength - 3; j += 3)
        {
            tris[spriteTrisLength + j] = sprite.triangles[j];
            tris[spriteTrisLength + j + 1] = sprite.triangles[j + 2];
            tris[spriteTrisLength + j + 2] = sprite.triangles[j + 3];
        }*/

        collisionMesh.vertices = verts;
        collisionMesh.triangles = tris;
        collisionMesh.RecalculateBounds();

        meshCollider.sharedMesh = collisionMesh;
	}
}
