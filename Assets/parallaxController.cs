using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private Transform cam; // Reference to the camera
    private Vector3 camStartPos; // Initial camera position
    private float distance; // Distance moved by the camera

    private GameObject[] backgrounds; // Background layers
    private Material[] mat; // Materials for each background layer
    private float[] backSpeed; // Speed for each background layer

    private float farthestBack = float.MinValue; // Furthest background for depth effect (avoid division by zero)

    [Range(0.01f, .05f)]
    public float parallaxSpeed; // Speed modifier for parallax effect

    void Start()
    {
        cam = Camera.main.transform; // Get main camera transform
        camStartPos = cam.position; // Store initial position

        int backCount = transform.childCount; // Fix typo: 'ChildCount' to 'childCount'
        mat = new Material[backCount]; // Fix missing semicolon
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        // Find the farthest background layer
        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
        }

        BackSpeedCalc(backCount); // Fix missing semicolon
    }

    void BackSpeedCalc(int backCount)
    {
        for (int i = 0; i < backCount; i++) // Find the farthest background
        {
            float zDist = backgrounds[i].transform.position.z - cam.position.z;
            if (zDist > farthestBack)
            {
                farthestBack = zDist;
            }
        }

        for (int i = 0; i < backCount; i++) // Set speeds of background
        {
            float zDist = backgrounds[i].transform.position.z - cam.position.z;
            backSpeed[i] = 1 - (zDist / farthestBack); // Fix missing semicolon
        }
    }

    void LateUpdate()
    {
        distance = cam.position.x - camStartPos.x; // Calculate movement distance
        
        transform.position = new Vector3(cam.position.x,transform.position.y,0);
        // Move each background independently
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed); // Fix: 'setTextureOFFest' → 'SetTextureOffset', '_main' → '_MainTex', 'vector2' → 'new Vector2()'
        }
    }
}
