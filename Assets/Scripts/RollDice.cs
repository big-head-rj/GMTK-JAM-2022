using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDice : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float speedRoll = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rigidbody.AddForce(Vector3.forward * 15 * Time.deltaTime);
        rigidbody.transform.position += Vector3.forward * 5 * Time.deltaTime;
        transform.Rotate(speedRoll, 0.0f, 0.0f);
        //Testando Github Guto
    }
}
