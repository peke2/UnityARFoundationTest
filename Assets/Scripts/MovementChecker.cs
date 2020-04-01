using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TouchScript;
using TouchScript.Layers;
// using TouchScript.InputSources;
// using TouchScript.Pointers;
// using TouchScript.Gestures;

public class MovementChecker : GestureReceiver
{
    Material material;
    private Vector2 startPos;

    List<GameObject> objectList = new List<GameObject>();
    const int MAX_OBJECTS = 20;

    // Start is called before the first frame update
    void Start()
    {
        material = new Material(Shader.Find("Custom/VertexColorShader"));
        var layer = GetComponent<StandardLayer>();
        layer.UseHitFilters = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void onPressed(PointerEventArgs e)
    {
        foreach (var pointer in e.Pointers){
            Debug.Log($"Pressed[{pointer.Position.x},{pointer.Position.y}]");
            //  1つだけ見る
            startPos.x = pointer.Position.x;
            startPos.y = pointer.Position.y;
            break;
        }
    }


    protected override void onReleased(PointerEventArgs e)
    {
        foreach (var pointer in e.Pointers){
            Debug.Log($"Released[{pointer.Position.x},{pointer.Position.y}]");

            Vector3 start = new Vector3(startPos.x, startPos.y, 1);
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
            var meshRenderer = obj.AddComponent<MeshRenderer>();
            var filter = obj.AddComponent<MeshFilter>();
            var mesh = new Mesh();
            float width = 0.1f;
            filter.mesh = mesh;
            mesh.vertices = new Vector3[]{sPos + side*width, ePos, sPos-side*width};
            mesh.colors = new Color[]{Color.red, Color.green, Color.blue};
            mesh.triangles = new int[]{0,1,2};
            meshRenderer.material = material;

            if( objectList.Count >= MAX_OBJECTS ){
                var removeObj = objectList[0];
                objectList.RemoveAt(0);
                GameObject.Destroy(removeObj);
            }
            objectList.Add(obj);

            break;
        }
    }
}
