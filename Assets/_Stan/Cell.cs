using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public new string name;
    public Mesh mesh;
    public new Collider collider;
    [Range(0, 1000)] public float cost;
    [Range(0, 1000)] public float timeToEvolve = 100;
    public List<Cell> evolutions;
    public bool canEvolve = false;
    private float lifeTime = 0;


    private void Start()
    {
        if (name == null)
            name = "Default Cell";     

        if (mesh == null)
            mesh = GetComponent<MeshFilter>().mesh;

        if (collider == null)
            collider = GetComponent<Collider>();
    }


    private void Update()
    {
        lifeTime += Time.deltaTime;

        if(lifeTime > timeToEvolve)
        {
            canEvolve = true;
        }
    }


    public virtual void Evolve()
    {
        if (evolutions.Count == 0)
            return;
    }

}
