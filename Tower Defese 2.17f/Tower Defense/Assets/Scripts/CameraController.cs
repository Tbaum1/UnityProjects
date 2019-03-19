using UnityEngine;

public class CameraController : MonoBehaviour {

    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;

    private float y;
    private float x;
    private Vector3 rotateValue;
    
    // Update is called once per frame
    void Update()
    {

        if (GameMan.GameIsOver)
        {
            this.enabled = false;
            return;
        }
               
        //checks for user input with the w,a,s,d keys or by moving mouse to top, bottom, or sides of screen
        //then it moves the MainCamera in the direction specified
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);            
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        //rotates the camera on the Y axis when the player holds down the left mouse button
        //if (Input.GetMouseButton(0))
        //{
        //    x = Input.GetAxis("Mouse Y");
        //    y = Input.GetAxis("Mouse X");
        //    rotateValue = new Vector3(x, y * 90, 0);
        //    transform.eulerAngles = transform.eulerAngles - rotateValue;
        //    //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotateValue), 1f);
        //}

        float scroll = Input.GetAxis("Mouse ScrollWheel");  //instantiates the Mouse ScrollWheel

        Vector3 pos = transform.position; //sets pos to camera position

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;  //scrolls in or out on the scene
        pos.y = Mathf.Clamp(pos.y, minY, maxY); //sets a max zoom in and zoom out

        transform.position = pos;

    }
}
