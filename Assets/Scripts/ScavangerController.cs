using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScavangerController: NetworkBehaviour
{
    Transform camera;
    CharacterController characterController;

    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private Vector3 cameraOffset;

    [SerializeField]
    private ScavangerHealthManager healthManager;

    private Animator animator;

    private Vector3 aimDirection;
    public Vector3 AimDirection { get { return aimDirection; } }

    // Use this for initialization
    void Start () {
        if (!isLocalPlayer)
            return;

        camera = Camera.main.transform;
        CameraFollowScript followScript = camera.GetComponent<CameraFollowScript>();
        followScript.Target = transform;
        followScript.Offset = cameraOffset;

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forwardDirection = camera.forward;
        Vector3 rightDirection = camera.right;
        forwardDirection.Scale(new Vector3(1, 0, 1));
        rightDirection.Scale(new Vector3(1, 0, 1));

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask, QueryTriggerInteraction.Collide))
        {
            aimDirection = (hit.point - transform.position);
            aimDirection.Scale(new Vector3(1, 0, 1));
            Debug.DrawRay(transform.position, hit.point - transform.position, Color.red);
            transform.forward = aimDirection.normalized;
        }

        Vector3 direction = (vertical * forwardDirection + horizontal * rightDirection).normalized;
        Debug.DrawRay(transform.position, direction, Color.blue);
        characterController.Move(direction * speed * Time.deltaTime);
        animator.SetFloat("Speed", (direction * speed).magnitude);

        float angle = Vector3.Angle(direction, aimDirection);
        Vector3 cross = Vector3.Cross(direction, aimDirection);
        angle *= cross.y < 0 ? -1 : 1;
        animator.SetFloat("Angle", angle);
    }
}
