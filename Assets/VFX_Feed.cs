using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
[RequireComponent(typeof(VisualEffect))]
public class VFX_Feed : MonoBehaviour
{
    private VisualEffect vfx;
    private Mesh Mesh;
    [SerializeField] private Transform MeshTransform;
    [SerializeField] string PositionPropertyName;
    [SerializeField] string AnglesPropertyName;
    [SerializeField] string ScalePropertyName;
    [SerializeField] string meshPropertyName;
    void Awake()
    {
        Mesh = MeshTransform.GetComponent<Mesh>();
        vfx = GetComponent<VisualEffect>();
    }
    void Update()
    {
        vfx.SetVector3 (PositionPropertyName, MeshTransform.position);
        vfx.SetVector3 (AnglesPropertyName, MeshTransform.eulerAngles);
        vfx.SetVector3 (ScalePropertyName, MeshTransform.lossyScale); 

        
    }
}