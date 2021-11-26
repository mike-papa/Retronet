using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{

    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
        
    }

    public Vector2 size; //size of grid
    public int startPos = 0; //start position
    public GameObject room;
    public Vector2 offset; //distance between each room

    List<Cell> board;
    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateDungeon()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[Mathf.FloorToInt(i + j * size.x)];
                if (currentCell.visited)
                {
                    var newRoom =
                        Instantiate(room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform)
                            .GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(currentCell.status);

                    newRoom.name += " " + i + "-" + j;
                }
            }
        }
    }
    
    void MazeGenerator()
    {
        //create board with all the cells that it must have
        board = new List<Cell>();
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
            
        }
        
        // currentCell is going to be keeping track of which position we are at
        int currentCell = startPos; 
        Stack<int> path = new Stack<int>();
        
        //actual algorithm
        while (true)
        {
            
            board[currentCell].visited = true;
            if (currentCell == board.Count - 1)
            {
                break;
            }
            //Check the Cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);
            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
               path.Push(currentCell);

               int newCell = neighbors[Random.Range(0, neighbors.Count)];
               if (newCell > currentCell)
               {
                   //down or right
                   if (newCell - 1 == currentCell)//right
                   {
                       board[currentCell].status[2] = true;
                       currentCell = newCell;
                       board[currentCell].status[3] = true;
                   }
                   else///down
                   {
                       board[currentCell].status[1] = true;
                       currentCell = newCell;
                       board[currentCell].status[0] = true;
                   }
               }
               else
               {
                   //down or left
                   if (newCell + 1 == currentCell)//left
                   {
                       board[currentCell].status[3] = true;
                       currentCell = newCell;
                       board[currentCell].status[2] = true;
                   }
                   else//right
                   {
                       board[currentCell].status[0] = true;
                       currentCell = newCell;
                       board[currentCell].status[1] = true;
                   }
               }
            }
        }
        GenerateDungeon();
    }

    List<int>  CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();
        
        //check up neighbor of cell
        //first condition is checking if it's not on the first row therefore if it has an up neighbor 
        //second condition is checking if this up neighbor has not been visited and is available for us
        if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell - size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - size.x));
        }
        //check down neighbor of cell 
        //analogically
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + size.x));
        }
        //check right neighbor of cell
        //first condition is checking if it's not on the last column therefore if it has an right neighbor
        //if % is equal 0, the cell's column is the rightmost column
        //second condition is checking if this up neighbor has not been visited and is available for us
        
        if ((cell+1) % size.x != 0 && !board[Mathf.FloorToInt(cell + 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }
        //check left neighbor of cell
        if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }
        return neighbors;
    }
}
