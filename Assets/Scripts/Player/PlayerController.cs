using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity = 3.0f;
    public GameObject model;
    public Vector3 ModelDefaultRotation;
    private Vector3 movingVec;

    private Rigidbody rigid;
    [SerializeField]
    private bool isThrust = false;
    private void Awake()
    {
        // the dafault rotation is 180
        model.transform.rotation = Quaternion.Euler(0, 90, 0);
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Using Transform to Translate.
        float movingVecH = Vector3.Dot(movingVec, Vector3.right);
        float movingVecV = Vector3.Dot(movingVec, Vector3.forward);

        if (Mathf.Abs(movingVecH) >= Mathf.Abs(movingVecV))
        {
            movingVec = movingVecH * Vector3.right;
        }
        else
        {
            movingVec = movingVecV * Vector3.forward;
        }

        if (movingVec.magnitude > 0.1f)
        {
            // rotate from default direction with movingVec

            model.transform.forward = movingVec;
            model.transform.rotation = model.transform.rotation * Quaternion.Euler(ModelDefaultRotation);
        }
        Vector3 newVelocity = movingVec * velocity;
        newVelocity.y = rigid.velocity.y;
        rigid.velocity = newVelocity;
    }

    public void Move(Vector3 vector)
    {
        movingVec = vector;
    }
}
