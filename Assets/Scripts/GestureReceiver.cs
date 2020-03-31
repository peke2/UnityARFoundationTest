using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript;
using TouchScript.Pointers;
using TouchScript.Gestures;
using TouchScript.InputSources;


public class GestureReceiver : InputSource
{
    private MetaGesture gesture;

    private Vector2 startPos;

    Camera arCamera;

    Material material;

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

        material = new Material(Shader.Find("Custom/VertexColorShader"));

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


    override protected void pressPointer(Pointer pointer)
    {
        // Debug.Log($"Press[{pointer.Position.x},{pointer.Position.y}]");
    }

    override protected void releasePointer(Pointer pointer)
    {
        // Debug.Log($"Release[{pointer.Position.x},{pointer.Position.y}]");
    }

    private void pointersPressedHandler(object sender, PointerEventArgs e)
    {
        foreach (var pointer in e.Pointers){
            Debug.Log($"Pressed[{pointer.Position.x},{pointer.Position.y}]");
            //  1つだけ見る
            startPos.x = pointer.Position.x;
            startPos.y = pointer.Position.y;
            break;
        }
    }


    private void pointersReleasedHandler(object sender, PointerEventArgs e)
    {
        foreach (var pointer in e.Pointers){
            Debug.Log($"Released[{pointer.Position.x},{pointer.Position.y}]");

            Vector3 start = new Vector3(startPos.x, startPos.y, 10);
            Vector3 end = new Vector3(pointer.Position.x, pointer.Position.y, 1);
            Vector3 sPos = arCamera.ScreenToWorldPoint(start);
            Vector3 ePos = arCamera.ScreenToWorldPoint(end);

            // GameObject lineObj = new GameObject("Line Object");

            // LineRenderer line = lineObj.AddComponent<LineRenderer>();
            // line.material = new Material(Shader.Find("Sprites/Default"));
            // line.positionCount = 2;
            // line.SetPosition(0, sPos);
            // line.SetPosition(1, ePos);
            // line.widthMultiplier = 0.1f;
            // Gradient gradient = new Gradient();
            // gradient.SetKeys(
            //     new GradientColorKey[] {new GradientColorKey(Color.red, 0), new GradientColorKey(Color.white, 1)},
            //     new GradientAlphaKey[] {new GradientAlphaKey(1,0), new GradientAlphaKey(1,1)}
            // );
            // line.colorGradient = gradient;

            var dir = (ePos - sPos).normalized;
            var side = Vector3.Cross(dir, arCamera.transform.forward);

            var obj = new GameObject("Triangle Object");
            var renderer = obj.AddComponent<MeshRenderer>();
            var filter = obj.AddComponent<MeshFilter>();
            var mesh = new Mesh();
            float width = 0.1f;
            filter.mesh = mesh;
            mesh.vertices = new Vector3[]{sPos + side*width, ePos, sPos-side*width};
            mesh.colors = new Color[]{Color.red, Color.green, Color.blue};
            mesh.triangles = new int[]{0,1,2};
            renderer.material = material;
            break;
        }
    }

}
