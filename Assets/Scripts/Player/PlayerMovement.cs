using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Para
    public float Speed = 6f;

    private Animator _anim;
    private Rigidbody _rigidbody;
    private Vector3 _movement;
    private int _floorMask;
    private float _camRayLength = 100f;

    #endregion

    #region UnityInterCallFunc

    void Awake()
    {
        _floorMask = LayerMask.GetMask("Floor");
        _anim = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
        Animating(h, v);
    }

    #endregion


    #region IntercallFunc

    private void Move(float h, float v)
    {
        _movement.Set(h, 0f, v);
        _movement = _movement.normalized * Speed * Time.deltaTime;
        _rigidbody.MovePosition(transform.position + _movement);
    }

    private void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, _camRayLength, _floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            _rigidbody.MoveRotation(newRotation);
        }
    }

    private void Animating(float h, float v)
    {
        bool walking = (h != 0f || v != 0f);
        _anim.SetBool("IsWalking", walking);
    }

    #endregion
}
