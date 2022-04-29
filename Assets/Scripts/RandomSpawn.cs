using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public Transform one;
    public Transform two;
    public Transform three;

    public Vector3[] Positions;
    // Start is called before the first frame update
    void Start()
    {
        Positions = new Vector3[3];
        Positions[0] = one.transform.position;
        Positions[1] = two.transform.position;
        Positions[2] = three.transform.position;
        this.transform.position = Positions[Random.Range(0, 2)];
        Debug.Log(Positions[Random.Range(0, 2)]);
    }
}
