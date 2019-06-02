using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PushBox : MonoBehaviour
{
    [SerializeField]
    string pushInput = "Fire1";
    [SerializeField]
    Camera cam;

    RawImage crosshair;
    
    public Color crosshairCanMove;
    public Color crosshairBlocked;
    public Texture normalCrosshair;
    public Texture pushCrosshair;
    public Texture blockedCrosshair;

    // Start is called before the first frame update
    void Start()
    {
        crosshair = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        
        // Perform raycast to check if player is aiming at box within 1 units
        if (Physics.Raycast(ray, out hit, 1.5f)) {
            if (hit.transform.tag == "Box") {

                Box box = hit.transform.gameObject.GetComponent<Box>();
                // Direction of movement is opposite of the normal of the collider
                Vector3 direction = -hit.normal;

                if (box.CanMove(direction)) {
                    // TODO: Toggle a shader that indicates block can be pushed or not
                    crosshair.color = crosshairCanMove;
                    crosshair.texture = pushCrosshair;

                    // If we press pushInput and box can move
                    if (Input.GetButtonDown(pushInput)) {
                        box.Move(direction);
                    }

                } else {
                    crosshair.color = crosshairBlocked;
                    crosshair.texture = blockedCrosshair;
                }

            }
        } else {
            crosshair.color = Color.white;
            crosshair.texture = normalCrosshair;
        }
    }
}
