using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    [Space(5)]
    [Header("TWEAKING")]
    [Range(-10f, 10f)]
    public float moveSpeed = 10f;
    public float accelerationAmount = 5f;
    [Tooltip("en seconde")]
    public float accelerationTime = 2f;
    [Range(1f, 5f)]
    public float decelerationMultiplier = 4;
    // ration accéleration par seconde; 
    private float accelerationRatio
    {
        get
        {
            return (accelerationAmount * Time.deltaTime) / accelerationTime;
        }
    }
    private float maxSpeed;
    private float baseSpeed;

    [Header("Setup")]
    [Space(5)]
    [Range (20f , 50f)]
    public float startDistanceFromGround = 35f;

    #region Rotation
    /*
    [Header("Rotation")]
    [Space(5)]
    public float sensitivityX = 10f;
    public float sensitivityY = 10f;
    [Range(-360f, 360f)]
    public int minimumX, maximumX;
    [Range(-180f, 180f)]
    public int minimumY, maximumY;

    private float rotationY = 0f;
    */
    #endregion

    //REF
    public Transform ground; 
    private Camera cam;



    //Boolean
    [Space(5)]
    [SerializeField]
    private bool inMovement = false;
    #endregion

    private void Awake()
    {
        transform.position = ground.transform.position + new Vector3(0f, startDistanceFromGround, 0f);
        transform.LookAt(ground);
        baseSpeed = moveSpeed;
        maxSpeed = moveSpeed + accelerationAmount;
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        #region Rotation //disable

        //rotation
        /*
        if (Input.GetMouseButton(1))
        {
            AdjustRotation();
        }
        */
        #endregion 
        #region Deplacement

        float xDelta =Input.GetAxis("Horizontal");
        float yDelta = Input.GetAxis("Vertical");
        float zDelta = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetAxis("Mouse ScrollWheel") == 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                zDelta = 1;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                zDelta = -1;
            }
        }
        if (xDelta != 0f || zDelta != 0f || yDelta != 0)
        {
            inMovement = true;
            AdjustPosition(xDelta, yDelta, zDelta);
        }
        else
        {
            inMovement = false;
            if (moveSpeed > baseSpeed)
            {
                moveSpeed -= accelerationRatio;
            }
        }

        #endregion
    }

    private void AdjustPosition(float xDelta, float yDelta, float zDelta)
    {
        if (inMovement)
        {
            if (moveSpeed < maxSpeed)
            {
                moveSpeed += accelerationRatio;
            }
            if (moveSpeed >= maxSpeed)
            {
                moveSpeed = maxSpeed;
            }
        }
        Vector3 direction = transform.localRotation * new Vector3(xDelta, yDelta, zDelta).normalized;
        // forcement entre 0 et 1 , permet une décélération progressive
        float damping = Mathf.Clamp01(Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(yDelta), Mathf.Abs(zDelta)));
        float distance = moveSpeed * damping * Time.deltaTime;

        Vector3 position = transform.localPosition;
        position += direction * distance;
        transform.localPosition = position;
    }

    //disable
    private void AdjustRotation()
    {
        /*
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        */
    }


}
