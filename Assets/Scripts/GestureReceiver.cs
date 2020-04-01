using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript;
using TouchScript.InputSources;


public class GestureReceiver : InputSource
{
    protected Camera arCamera;


    protected override void OnEnable()
    {
        base.OnEnable();
        // gesture = GetComponent<MetaGesture>();
        // if (gesture)
        if( TouchManager.Instance != null )
        {
            var tc = TouchManager.Instance;
            tc.PointersPressed += pointersPressedHandler;
            tc.PointersReleased += pointersReleasedHandler;
        }

        var obj = GameObject.Find("AR Camera");
        arCamera = obj.GetComponent<Camera>();

    }

    protected override void OnDisable()
    {
        base.OnDisable();

        // if (gesture)
        if( TouchManager.Instance != null )
        {
            var tc = TouchManager.Instance;
            tc.PointersPressed -= pointersPressedHandler;
            tc.PointersReleased -= pointersReleasedHandler;
        }
    }

    protected virtual void pointersPressedHandler(object sender, PointerEventArgs e)
    {
    }


    protected virtual void pointersReleasedHandler(object sender, PointerEventArgs e)
    {
    }

}
