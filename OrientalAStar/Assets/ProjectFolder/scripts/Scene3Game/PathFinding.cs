using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
	public floor                                               Start_AStar=null;
	public floor                                                 End_AStar=null;

	public Material                                          startMaterial=null;
	public Material                                            endMaterial=null;

	public bool                                              test_Color = true;


	public Node                                                      startNode; 
    public Node                                                       goalNode;

	Vector3                                        Start_AStar_pos=Vector3.zero;
	Vector3                                          End_AStar_pos=Vector3.zero;

	public ArrayList                                                  pathArray;

 
	public Vector3 []                                          _wayPoints=null;
 
	 
	public float test_line_width=-6.0f;
	public float test_line_gap=1.0f;


 
	public floor []                                                 PathTiles;
	public Material                                             path_material;


    



	// Use this for initialization
	void Start () 
	{

#if TEST_COLOR_ASTAR
		if(test_Color)
		{
			Start_AStar.transform.GetChild(0).GetComponent<Renderer>().material= startMaterial;
			End_AStar.transform.GetChild(2).transform.GetChild(0).GetComponent<Renderer>().material= endMaterial;  
        }
#endif
	}



	// Use this for initialization
	void Update () 
	{

#if !TEST_COLOR_ASTAR
		if(test_Color)
		{
			Start_AStar.GetComponent<Renderer>().material= startMaterial;
			End_AStar.GetComponent<Renderer>().material= endMaterial;  
        }

      
#endif

#if  ASTAR_TEST
		if(Input.GetKeyDown(KeyCode.A))
		{
			ApplyAStar();
        }
#endif
     

       
	}

	public void setTarget(floor start,floor end)
	{
		this.Start_AStar=start;
		this.End_AStar=end;
	}

	public void ApplyAStar()
	{

		pathArray = new ArrayList();
		Start_AStar_pos = Start_AStar.transform.GetChild(0).transform.position;
		End_AStar_pos   = End_AStar.transform.GetChild(2).transform.position;

		//Assign StartNode and Goal Node
		startNode = new Node(Start_AStar_pos);
		startNode.tile = Start_AStar;

		goalNode  = new Node(End_AStar_pos );
		goalNode.tile = End_AStar;

#if ASTAR_TEST

		Debug.Log("Start Position"+Start_AStar_pos);
		Debug.Log("End Position"+End_AStar_pos);


#endif


		//pathArray = floorbase.GetComponent<AstarCarRace>().FindPath(startNode, goalNode);
		 pathArray = this.GetComponent<AstarCarRace>().FindPath(startNode, goalNode);


		if(pathArray.Count ==0)
		{

#if ASTAR_TEST

		   Debug.Log("Size is zero");
#endif
		   return;
		}



#if ASTAR_TEST
		Debug.Log("#######################");
		Debug.Log("Way point Size ::" + pathArray.Count);
		Debug.Log("@@@@@@@@@@@@@@@@@@@@@@@");
#endif
		_wayPoints = new Vector3[pathArray.Count];
		PathTiles = new floor[ pathArray.Count];
		for(int i=0;i<pathArray.Count;i++)
		{
			 Node node = (Node)pathArray[i];
			_wayPoints[i]=node.position;
			 PathTiles[i]= node.tile;
			int tile_Child = PathTiles[i].ChildIDSelected;
			 PathTiles[i].transform.GetChild(tile_Child).GetComponent<Renderer>().material=path_material;

#if ASTAR_TEST
			Debug.Log(" TILE  "+PathTiles[i]);
			Debug.Log(" Position Empty "+_wayPoints[i]);
			Debug.Log("@@@@@@@@@@@@@@@@@@@@@@@" );
#endif
			//node.tile.gameObject.GetComponent<Renderer>().material=path_material;
		}

		    Start_AStar.GetComponent<Renderer>().material= startMaterial;
			End_AStar.GetComponent<Renderer>().material= endMaterial;  


		 this.GetComponent<AI>().run_Ai=true;

		Game_Controller.Instance.playerA_Star = Game_Controller.Instance.playerB_Star = true;



    }

    void OnDrawGizmos()
    {
        if (pathArray == null)
            return;
#if  !BLOCK_COMMEND

        if (pathArray.Count > 0)
        {
            int index = 1;
            foreach (Node node in pathArray)
            {
                if (index < pathArray.Count)
                {
					
                    Node nextNode = (Node)pathArray[index];

				 

					for(float i=-test_line_width;i<=test_line_width;i=i+test_line_gap)
                    {
						Debug.DrawLine(node.position+node.tile.gameObject.transform.forward*i, nextNode.position, Color.green);

						Debug.Log(i);
                    }

					//Debug.DrawLine(node.position+node.tile.transform.forward*, nextNode.position, Color.green);
                 
					//Debug.DrawLine(node.position, nextNode.position, Color.green);
                    index++;
                }
            };
        }
#endif
    }

    }