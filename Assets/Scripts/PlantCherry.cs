using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCherry : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public bool ifmove;
    public float speed;
    public Grid map;
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;
    public GameObject upRight;
    public GameObject downRight;
    public GameObject upLeft;
    public GameObject downLeft;
    private ContactFilter2D filter; // Collider Detect Tools.
    private List<Collider2D> results;// Collider Detect Tools.
    public Vector3Int gridPosition;
    void Start()
    {   
        target=null;
        speed=1f;
        StartCoroutine(CheckNeighbors());
        filter = new ContactFilter2D().NoFilter(); //initiate the Collider Detect Tools.
        results = new List<Collider2D>(); //initiate the Collider Detect Tools.
    }

    // Update is called once per frame
    void Update()
    {
        if(target){
            float dis=Vector3.Distance(transform.parent.transform.position,target.transform.position);
            if(dis<0.1f){
                target.GetComponent<Box>().HP=100f;
                target.transform.GetChild(0).gameObject.GetComponent<PlantPea>().HP+=100f;
                target.transform.GetChild(0).gameObject.GetComponent<PlantPea>().basicShootPeriod/=2f;
                //target.transform.GetChild(0).gameObject.GetComponent<PlantPea>().bulletSpeed*=2f;
                Destroy(transform.parent.gameObject);
            }
            transform.parent.position=Vector3.MoveTowards(transform.parent.position,target.transform.position,speed*Time.deltaTime);
            
        }else{

        }
    }
    void AddBuff(GameObject g){
        if(g&&g.transform.childCount==1){
            if(g.transform.GetChild(0).gameObject.TryGetComponent<PlantPea>(out PlantPea plantPea)){
                plantPea.bulletPeriodBuff=0.3f;

                
            }
        }
    }
    private IEnumerator CheckNeighbors(){   
        
        while(true){
            gridPosition=map.WorldToCell(transform.position);
            upRight = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(1,1,0)));
            downRight = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(1,-1,0)));
            upLeft = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(-1,1,0)));
            downLeft = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(-1,-1,0)));
            up = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(0,1,0)));
            down = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(0,-1,0)));
            left = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(-1,0,0)));
            right = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(1,0,0)));
            AddBuff(upRight);
            AddBuff(downRight);
            AddBuff(upLeft);
            AddBuff(downLeft);
            AddBuff(up);
            AddBuff(down);
            AddBuff(left);
            AddBuff(right);
            yield return null; 
        }
    }
    private GameObject GetBox(Vector3 position){
        Physics2D.OverlapCircle(position, 0.1f,filter, results);
        foreach( Collider2D result in results)
        {
            if(result.gameObject.TryGetComponent<Box>(out Box box)){
                return result.gameObject;
            }
        }
        return null;
    }
}
