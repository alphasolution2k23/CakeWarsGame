using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the balloon moves upward
    public float rotationSpeedX = 50f; // Speed at which the balloon rotates around the X axis
    public float explosionForce = 300f;
    public float explosionRadius = 5f;
    public float explosionUpward = 1f;
    public float pieceLifetime = 2f;

    private MeshFilter meshFilter;
    private Mesh originalMesh;
    public GameObject ExplodePArticle;

    void Start()
    {
        // Start the coroutine to destroy the balloon after 5 seconds
    //    StartCoroutine(DestroyAfterTime(5f));
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            originalMesh = meshFilter.mesh;
        }
    }

    void Update()
    {
        // Move the balloon upward
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // Rotate the balloon around its pivot on the x, y, and z axes
        transform.Rotate(new Vector3(rotationSpeedX, 0, 0) * Time.deltaTime);

    }
    public void Explode()
    {
        if (originalMesh == null) return;

        // Get the original mesh's vertices and triangles
        Vector3[] vertices = originalMesh.vertices;
        int[] triangles = originalMesh.triangles;

        List<GameObject> pieces = new List<GameObject>();

        // Create pieces for each triangle
        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3[] pieceVertices = new Vector3[3];
            int[] pieceTriangles = new int[3];

            for (int j = 0; j < 3; j++)
            {
                pieceVertices[j] = vertices[triangles[i + j]];
                pieceTriangles[j] = j;
            }

            Mesh pieceMesh = new Mesh();
            pieceMesh.vertices = pieceVertices;
            pieceMesh.triangles = pieceTriangles;

            GameObject piece = new GameObject("Piece " + (i / 3));
            piece.transform.position = transform.position;
            piece.transform.rotation = transform.rotation;

            MeshFilter pieceMeshFilter = piece.AddComponent<MeshFilter>();
            pieceMeshFilter.mesh = pieceMesh;

            MeshRenderer pieceMeshRenderer = piece.AddComponent<MeshRenderer>();
            pieceMeshRenderer.material = GetComponent<MeshRenderer>().material;

            Rigidbody pieceRigidbody = piece.AddComponent<Rigidbody>();
            pieceRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);

            Destroy(piece, pieceLifetime);

            pieces.Add(piece);
        }

        // Disable the original mesh renderer
        GetComponent<MeshRenderer>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            GameObject DestroyedObj = Instantiate(ExplodePArticle, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    private IEnumerator DestroyAfterTime(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Destroy the balloon
        Destroy(gameObject);
    }
}