using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Structure" , menuName = "Structure" , order =1)]
public class Structure : ScriptableObject
{
    public Mesh mesh;
    public Collider collider;
    [Space(20f)]
    public StructureType type;
    [Space(20f)]
    public float cost;
    [Range ( 0f , 100f)]
    public float range;
    
}
