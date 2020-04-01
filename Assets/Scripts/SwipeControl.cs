using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TouchScript;
using TouchScript.Hit;
using TouchScript.InputSources;
using TouchScript.Pointers;
using TouchScript.Gestures;

public class SwipeControl : GestureReceiver
{
    private Vector2 prevPos;

    public GameObject targetObject;

    bool dragging;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnButtonReset()
    {
        targetObject.transform.rotation = Quaternion.identity;
    }

    protected override void onPressed(PointerEventArgs e)
    {
        foreach (var pointer in e.Pointers){
            HitData hit = pointer.GetPressData();
            string name;
            if( hit.Target != null ){
                name = hit.Target.name;
            }
            else{
                name = "No Object";
            }
            Debug.Log($"[{name}]Pressed[{pointer.Position.x},{pointer.Position.y}]");
            //  1つだけ見る
            prevPos.x = pointer.Position.x;
            prevPos.y = pointer.Position.y;
            
            dragging = true;
            break;
        }
    }

    protected override void onUpdated(PointerEventArgs e)
    {
        foreach (var pointer in e.Pointers){
            HitData hit = pointer.GetPressData();
            string name;
            if( hit.Target != null ){
                name = hit.Target.name;
            }
            else{
                name = "No Object";
            }
            var diff = pointer.Position - prevPos;
            float delta = 0;
            if( name == "PanelRollControl" ){
                delta = diff.x / 10;
                targetObject.transform.rotation *= Quaternion.AngleAxis(delta, -arCamera.transform.forward);
            }
            else if( name == "PanelPitchControl" ){
                delta = diff.y / 10;
                targetObject.transform.rotation *= Quaternion.AngleAxis(delta, arCamera.transform.right);
            }
            else if(dragging) {
                Vector3 start = new Vector3(prevPos.x, prevPos.y, 1);
                Vector3 end = new Vector3(pointer.Position.x, pointer.Position.y, 1);
                Vector3 sPos = arCamera.ScreenToWorldPoint(start);
                Vector3 ePos = arCamera.ScreenToWorldPoint(end);
                var dir = (ePos - sPos).normalized;

                var d = Vector3.Dot(-targetObject.transform.right, dir);
                targetObject.transform.rotation *= Quaternion.AngleAxis(d*2, targetObject.transform.up);
            }

            prevPos = pointer.Position;
        }
    }

    protected override void onReleased(PointerEventArgs e)
    {
        foreach (var pointer in e.Pointers){
            HitData hit = pointer.GetPressData();
            string name;
            if( hit.Target != null ){
                name = hit.Target.name;
            }
            else{
                name = "No Object";
            }
            Debug.Log($"[{name}]Released[{pointer.Position.x},{pointer.Position.y}]");

            dragging = false;
            break;
        }
    }
}
