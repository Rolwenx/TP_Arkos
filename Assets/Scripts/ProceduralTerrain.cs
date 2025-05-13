using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

public class ProceduralTerrain : MonoBehaviour
{

    [SerializeField] private int WIDTH = 50;
    [SerializeField] private int HEIGHT = 50;
    [SerializeField] private float scale = 0.1f;
    [SerializeField] private float amplitude = 5f;
    [SerializeField] private float LODDistance = 30f;

    private Vector3[] vertices;
    private Vector3[] highResVertices;
    private Vector3[] lowResVertices;

    private int[] triangles;
    private int[] highResTriangles;
    private int[] lowResTriangles;


    private Mesh highResMesh;
    private Mesh lowResMesh;
    private Mesh currentMesh;


    private MeshCollider meshCollider;

    private MeshFilter meshFilter;

    private float _radius = 2f;
    private float deformationStrength = 0.5f;
    

    [BurstCompile]
    public struct DeformJob : IJobParallelFor {
        public NativeArray<Vector3> vertices;
        public Vector3 hitPoint;
        public float radius;
        public float strength;
        public void Execute(int index) {
            if (Vector3.Distance(vertices[index], hitPoint) < radius) {
                Vector3 vertex = vertices[index];
                vertex.y += strength ; 
                vertices[index] = vertex;
            }
        }
    }
    private void Start()
    {

        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        highResMesh = GenerateMesh(WIDTH, HEIGHT, 1, out highResVertices, out highResTriangles);
        lowResMesh = GenerateMesh(WIDTH, HEIGHT, 2, out lowResVertices, out lowResTriangles);

        currentMesh = highResMesh;
        meshFilter.mesh = highResMesh;
        meshCollider.sharedMesh = highResMesh;

    }

    private void Update(){

        float distance = Vector3.Distance(Camera.main.transform.position, transform.position);

        if (distance > LODDistance) {
            meshFilter.mesh = lowResMesh;
            meshCollider.sharedMesh = lowResMesh;
            currentMesh = lowResMesh;
        } else if (distance <= LODDistance) {
            meshFilter.mesh = highResMesh;
            meshCollider.sharedMesh = highResMesh;
            currentMesh = highResMesh;

        }
        
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {

                NativeArray<Vector3> highResNative = new NativeArray<Vector3>(highResVertices, Allocator.TempJob);
                            
                // Apply to high res
                DeformJob highjob = new DeformJob
                {
                    vertices = highResNative,
                    hitPoint = hit.point,
                    radius = _radius,
                    strength = deformationStrength
                };

                // this line means : 
                // Schedule this job to process all vertices,
                //  splitting the work into chunks of 64 elements per thread
                JobHandle handleHigh = highjob.Schedule(highResNative.Length, 64);

                // Apply to low res
                NativeArray<Vector3> lowResNative = new NativeArray<Vector3>(lowResVertices, Allocator.TempJob);
                DeformJob lowJob = new DeformJob
                {
                    vertices = lowResNative,
                    hitPoint = hit.point,
                    radius = _radius,
                    strength = deformationStrength
                };
                JobHandle handleLow = lowJob.Schedule(lowResNative.Length, 64);

                // waits for the job to finish before continuing
                JobHandle.CompleteAll(ref handleHigh, ref handleLow);

                // Copy back results
                highResNative.CopyTo(highResVertices);
                lowResNative.CopyTo(lowResVertices);
                highResNative.Dispose();
                lowResNative.Dispose();

                // Update both meshes
                highResMesh.vertices = highResVertices;
                highResMesh.RecalculateNormals();

                lowResMesh.vertices = lowResVertices;
                lowResMesh.RecalculateNormals();

                // Update collider to ensure it reflects the new shape
                meshCollider.sharedMesh = null;
                meshCollider.sharedMesh = currentMesh;
                
            }
        }
    }
    private Mesh GenerateMesh(int width, int height, int LOD_STEP, out Vector3[] vertices, out int[] triangles)
    {
        width = width/LOD_STEP;
        height = height/LOD_STEP;
        Mesh generatedMesh = new Mesh();
        generatedMesh.name = $"TerrainProcedural_{width}x{height}";
        GetComponent<MeshFilter>().mesh = generatedMesh;

        vertices = new Vector3[width * height];
        triangles = new int[(width-1)*(height-1) *6];

        // generate vertices
        for (int z = 0; z < height; z++) {
            for(int x = 0; x < width; x++) {
                int index = z * width + x; 
                vertices[index] = new Vector3(x*LOD_STEP, 0, z*LOD_STEP);
                vertices[index].y = Mathf.PerlinNoise(vertices[index].x * scale, vertices[index].z *  scale) * amplitude;
            }
        }

        // generate triangles
        int t = 0;
        for (int z = 0; z < height-1; z++)
        {
            for (int x = 0; x < width-1; x++)
            {
                int index = z * width + x;

                triangles[t++] = index;
                triangles[t++] = index + width; 
                triangles[t++] = index + + width + 1 ;

                triangles[t++] = index;
                triangles[t++] = index + width + 1; 
                triangles[t++] = index + + 1;

            }
        }

        generatedMesh.vertices = vertices;
        generatedMesh.triangles = triangles; 
        generatedMesh.RecalculateNormals();

        return generatedMesh;

    }
    
}
