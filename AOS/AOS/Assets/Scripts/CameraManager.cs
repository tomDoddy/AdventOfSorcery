using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject PlayerObj;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        transform.position = new Vector3(PlayerObj.transform.position.x, PlayerObj.transform.position.y, transform.position.z);
    }
}