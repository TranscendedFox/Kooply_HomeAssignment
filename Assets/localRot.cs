using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class localRot : MonoBehaviour
{
    [SerializeField] private float speed;
    public float sineSpeed;
    public float sineAmptitude;    
    void Update()
    {
        speed = Mathf.Sin(Time.time * sineSpeed) * sineAmptitude;
        transform.Rotate(Vector3.right * speed);
    }
}
