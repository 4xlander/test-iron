using UnityEngine;

public class BulletItem : MonoBehaviour
{
    public GameObject visualGO;
    public Rigidbody2D rb;
    public float moveSpeed = 8f;

    public void Init(Vector3 targetDir)
    {
        rb.linearVelocity = targetDir.normalized * moveSpeed;

        if (targetDir.x > 0)
            visualGO.transform.localScale = new Vector3(-1, 1, 1);
    }
}