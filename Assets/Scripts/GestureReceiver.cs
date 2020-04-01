using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript;
using TouchScript.Layers;


public class GestureReceiver : MonoBehaviour
{
    protected Camera arCamera;


    protected void OnEnable()
    {
        var tc = TouchManager.Instance;
        if( tc != null ){
            tc.PointersPressed += pointersPressedHandler;
            tc.PointersUpdated += pointersUpdatedHandler;
            tc.PointersReleased += pointersReleasedHandler;
        }

        var obj = GameObject.Find("AR Camera");
        if( obj != null ){
            arCamera = obj.GetComponent<Camera>();
        }

        var layer = GetComponent<StandardLayer>();
        if(layer!=null){
            // layer.UseHitFilters = true;
        }
    }

    protected void OnDisable()
    {
        var tc = TouchManager.Instance;
        if( tc != null ){
            tc.PointersPressed -= pointersPressedHandler;
            tc.PointersUpdated -= pointersUpdatedHandler;
            tc.PointersReleased -= pointersReleasedHandler;
        }
    }

    protected void pointersPressedHandler(object sender, PointerEventArgs e)
    {
        onPressed(e);
    }

    protected void pointersUpdatedHandler(object sender, PointerEventArgs e)
    {
        onUpdated(e);
    }

    protected void pointersReleasedHandler(object sender, PointerEventArgs e)
    {
        onReleased(e);
    }

    protected virtual void onPressed(PointerEventArgs e)
    {
    }

    protected virtual void onUpdated(PointerEventArgs e)
    {
    }

    protected virtual void onReleased(PointerEventArgs e)
    {
    }

}
