using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity = 3.0f;
    public float jumpThrust = 3.0f;
    public GameObject model;
    public Vector3 ModelDefaultRotation;
    private Vector3 movingVec;

    private Rigidbody rigid;
    [SerializeField]
    private bool isThrust = false;
    private void Awake()
    {
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
            model.transform.forward = Vector3.Slerp(
                    model.transform.forward, movingVec, 0.1f);
            //model.transform.rotation = model.transform.rotation * Quaternion.Euler(ModelDefaultRotation);
        }
        Vector3 newVelocity = movingVec * velocity;
        newVelocity.y = rigid.velocity.y + (isThrust ? 1.0f : 0.0f) * jumpThrust;
        rigid.velocity = newVelocity;
        isThrust = false;
    }

    public void Move(Vector3 vector)
    {
        movingVec = vector;
    }

    public void Jump(bool _isThrust)
    {
        isThrust = _isThrust;
    }
}
