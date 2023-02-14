using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    private Grid map; // Grid Map
    private GameObject box; // Grid Map
    public Vector3Int gridPlayerPosition;
    public int basicNumber;
    public int totalNumber;
    private ContactFilter2D filter; // Collider Detect Tools.
    private List<Collider2D> results;// Collider Detect Tools.
    public CanvasManager canvasManager;
    void Start()
    {      
        canvasManager=GameObject.Find("Canvas").GetComponent<CanvasManager>();
        map=GameObject.Find("Grid").GetComponent<Grid>();
        box=Resources.Load("Prefabs/Box") as GameObject;
        basicNumber=8;
        totalNumber=16;
        
        filter = new ContactFilter2D().NoFilter(); //initiate the Collider Detect Tools.
        results = new List<Collider2D>(); //initiate the Collider Detect Tools.
    }

    // Update is called once per frame
    void Update()
    {   
        if(!canvasManager.ifStart){
            return;
        }
        gridPlayerPosition=(GameObject.Find("Player").GetComponent<PlayerControl>().playerGridPos);
        int n=totalNumber-GameObject.Find("Boxes").transform.childCount;
        while(n>0){
            int count=0;
            while(true){
                count+=1;
                if(count>20){
                    break;
                }
                int x= Random.Range(-10,11);
                int y= Random.Range(-10,11);
                Vector3 worldPos=map.GetCellCenterWorld(gridPlayerPosition+new Vector3Int(x,y,0));
                if(GetBox(worldPos)){
                    continue;
                }else{
                    GameObject obj=Instantiate(box, worldPos,Quaternion.identity,GameObject.Find("Boxes").transform);
                    n--;
                    break;
                }
            }
        }
    }
    private GameObject GetBox(Vector3 position){
        Physics2D.OverlapCircle(position, 0.1f,filter, results);
        foreach( Collider2D result in results)
        {
            if(result.gameObject.TryGetComponent<Box>(out Box box)){
                return result.gameObject;
            }
            if(result.gameObject.TryGetComponent<Wall>(out Wall wall)){
                return result.gameObject;
            }
        }
        return null;
    }
}
