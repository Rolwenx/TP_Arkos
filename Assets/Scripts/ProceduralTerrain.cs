using UnityEngine;

public class ProceduralTerrain : MonoBehaviour
{

    [SerializeField] private int WIDTH;
    [SerializeField] private int HEIGHT;

    private Vector3[] vertices;

    void GenerateGrid()
    {
        vertices = new Vector3[WIDTH * HEIGHT];
        for (int z = 0; z < HEIGHT; z++) {
            for(int x = 0; x < WIDTH; x++) {
                int index = z * WIDTH + x; 
                vertices[index] = new Vector3(x, 0, z);
            }
        }
    }



}
