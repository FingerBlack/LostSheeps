using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCombine : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject turret;
    private Turret turretComponent;
    void Start()
    {
        turretComponent=turret.GetComponent<Turret>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(turretComponent.gridPosition==new Vector3Int(-13,-4,0)){
            Destroy(gameObject);
        }
    }
}
