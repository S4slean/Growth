using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    //[Header("Tweaking")]
    //public float yAddWhileDragging;

    private bool isDragging = false;
    private bool asAnObject;

    public GameObject Marker; 

    //REF
    private Camera mainCamera;
    // Debug
    [SerializeField]
    private GameObject currentObject;
    [SerializeField]
    private Collider col;




    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Get();
        }
        if (isDragging && Input.GetMouseButtonUp(0))
        {
            Drop();
        }
    }

    private void FixedUpdate()
    {
        if (isDragging)
        {
            Drag();
        }
    }


    #region Fonctions
    // quand le joueur veut placer une infrastructure
    private void Drop()
    {
 
        isDragging = false;
        currentObject = null;
        col.enabled = true;
        col = null;

        print("drop");

    }
    // quand le joueur selectionne une infrastructure
    private void Get()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.GetComponent<IsDragable>() != null)
            {
                currentObject = hit.transform.gameObject;
                isDragging = true;
                col = currentObject.GetComponent<Collider>();
                col.enabled = false;
            }
        }

    }
    // quand le joueur a une infrastructure sélectionnée
    private void Drag()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        Debug.DrawLine(mainCamera.transform.position, hit.point, Color.yellow);
        Vector3 pos = hit.point;
        //pos.y += yAddWhileDragging;

        currentObject.transform.rotation = hit.transform.rotation;
        currentObject.transform.position = pos;
    }
    #endregion





}
