using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFloor : MonoBehaviour
{
    // Start is called before the first frame update
    private Grid floorGrid;
    private ContactFilter2D filter; // Collider Detect Tools.
    private List<Collider2D> results;// Collider Detect Tools.
    public int level;
    public int status;
    void Start()
    {
        floorGrid = GameObject.Find("Grid").GetComponent<Grid>();
        filter = new ContactFilter2D().NoFilter(); //initiate the Collider Detect Tools.
        results = new List<Collider2D>(); //initiate the Collider Detect Tools.
        transform.position = floorGrid.GetCellCenterWorld(floorGrid.WorldToCell(transform.position));
        status=GameObject.Find("HomeCanvas").GetComponent<HomeCanvas>().levels[level];
        if(status==1){
            GetComponent<SpriteRenderer>().color=new Color(0f,1f,0f,1f);
        }else if(status==2){
            GetComponent<SpriteRenderer>().color=new Color(1f,1f,0f,1f);
        }
    }

    // Update is called once per frame
    void Update()
    {   
        // cheat
        if(status == 0 && Input.GetKeyDown(KeyCode.P)){
            status = 1;
            GetComponent<SpriteRenderer>().color=new Color(1f,1f,0f,1f);
        }

        if(status==0){
            return;
        }
        Physics2D.OverlapCircle(transform.position, 0.1f,filter,results);

        foreach( Collider2D result in results)
        {   
            if(result.isTrigger){
                continue;
            }
            if(result.gameObject.TryGetComponent<PlayerControl>(out PlayerControl playerControl)){
                SceneManager.LoadScene( level.ToString());
                break;
            }
               
        }
    }
}
