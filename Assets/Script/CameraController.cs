using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [Header("move")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform coner1;
    [SerializeField] private Transform coner2;

    [Header("Zoom")]
    [SerializeField] private float zoomModifier;


    [SerializeField] private float xInput;
    [SerializeField] private float zInput;

    public static CameraController instance;

     void Awake()
    {
        instance = this;
        _camera = Camera.main;
    }

     void Start()
    {
        moveSpeed = 50;
    }

    private void Update()
    {
        MovebyKeyBoard();
        Zoom();
    }

    private void Zoom()
    {
        zoomModifier = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetKey(KeyCode.Z))
            zoomModifier = 0.1f;
        if (Input.GetKey(KeyCode.X))
            zoomModifier = -0.1f;

        _camera.orthographicSize += zoomModifier;
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, 4, 10);
    }

    private void MovebyKeyBoard()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        Vector3 dir = (transform.forward * zInput) + (transform.right * xInput);
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.position = Clamp(coner1.position, coner2.position);
    }

    private void MovebyKeyMouse()
    {
       
    }

    private Vector3 Clamp(Vector3 lowerLeft , Vector3 topRight)
    {
        Vector3 pos = new Vector3(Mathf.Clamp(transform.position.x, lowerLeft.x, topRight.x), transform.position.y, Mathf.Clamp(transform.position.z, lowerLeft.z, topRight.z));
        return pos;
    }
}
