using UnityEngine;

public class Cursors : MonoBehaviour 
{
    public static Cursors CursorsInst;
    public Vector3 HitPoint;
    private void Start()
    {
        if (CursorsInst == null)
        {
            CursorsInst = this;
        }
    }
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            // Create a LayerMask 
            int layerMask = (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("npc"));
            if (Physics.Raycast(ray, out hit, float.MaxValue, layerMask))
            {
                transform.position = hit.point;
                HitPoint = hit.point;
            }
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Ground")))
            {
                transform.position = hit.point;
            }
        }
#endif
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //    // Get the touch position
        //    Touch touch = Input.GetTouch(0);
        //    Vector3 touchPosition = touch.position;
        //    Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Ground")))
        //    {
        //        transform.position = hit.point;
        //    }
        //}
    }
}
