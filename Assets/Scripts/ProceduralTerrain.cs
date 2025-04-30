using UnityEngine;

public class ProceduralTerrain : MonoBehaviour
{

    [SerializeField] private int WIDTH;
    [SerializeField] private int HEIGHT;
    [SerializeField] private float scale = 0.1f;
    [SerializeField] private float amplitude = 5f;

    private Vector3[] vertices;
    private int[] triangles;

    private void Start()
    {
    
        GenerateMesh();
    }
    private void GenerateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.name = "TerrainProcedural";
        GetComponent<MeshFilter>().mesh = mesh;

        vertices = new Vector3[WIDTH * HEIGHT];
        triangles = new int[(WIDTH-1)*(HEIGHT-1) *6];

        // generate vertices
        for (int z = 0; z < HEIGHT; z++) {
            for(int x = 0; x < WIDTH; x++) {
                int index = z * WIDTH + x; 
                vertices[index] = new Vector3(x, 0, z);
                vertices[index].y = Mathf.PerlinNoise(vertices[index].x * scale, vertices[index].z * scale) * amplitude;
            }
        }

        // generate triangles
        int t = 0;
        for (int z = 0; z < HEIGHT-1; z++)
        {
            for (int x = 0; x < WIDTH-1; x++)
            {
                int index = z * WIDTH + x;

                triangles[t++] = index;
                triangles[t++] = index + WIDTH; 
                triangles[t++] = index + + WIDTH + 1 ;

                triangles[t++] = index;
                triangles[t++] = index + WIDTH + 1; 
                triangles[t++] = index + + 1;

            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles; 
        mesh.RecalculateNormals();


    }



}
