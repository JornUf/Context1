using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class ui_edit : MonoBehaviour
{
    private Button button;
    private Vector3 scale;
    private Vector3 baseScale;
    private Vector3 bigScale;
    private RectTransform holder;
    [SerializeField] private float hoverScale;
    [SerializeField] private float scaleSpeed;
    bool scaled;
    void Awake()
    {
        button = GetComponent<Button>();
        holder = button.GetComponent<RectTransform>();
        scale = gameObject.transform.localScale;
        baseScale = scale;
        bigScale = scale * hoverScale;

    }

    public void MouseEnter()
    {
        scaled = true;
    }

    public void MouseExit()
    {
        scaled = false;
    }


    void Update()
    {
        scale = Vector3.Lerp(scale, scaled ? bigScale : baseScale, scaleSpeed * Time.deltaTime);
        holder.localScale = scale;
    }
}
