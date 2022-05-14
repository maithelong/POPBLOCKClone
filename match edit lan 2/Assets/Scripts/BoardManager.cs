using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static BoardManager instance;
    // 1
    private int count = 0;
   private List<GameObject> numberTile = new List<GameObject>();
    public List<GameObject> characters = new List<GameObject>();     // 2
    public GameObject tile;      // 3
    public int xSize, ySize;     // 4
    private bool canAdd=false;
    private GameObject[,] tiles;      // 5
   [SerializeField] public UImanager m_ui;
    private bool isGameOver;
    private int point = 0;
   // private int countPoitn = 0;
    private int hightScore = 0;
    public bool IsShifting { get; set; }     // 6

    void Start()
    {
        instance = GetComponent<BoardManager>();     // 7
        m_ui.setScoretext("Score :" + point);

        Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
        CreateBoard(offset.x, offset.y);     // 8
    }

    private void CreateBoard(float xOffset, float yOffset)
    {
        tiles = new GameObject[xSize, ySize];     // 9

        float startX = transform.position.x;     // 10
        float startY = transform.position.y;

        for (int x = 0; x < xSize; x++)
        {      // 11
            for (int y = 0; y < ySize; y++)
            {
                        GameObject newTile = Instantiate(tile, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), tile.transform.rotation);
                        newTile.transform.parent = transform; // 1                   
                        GameObject newSprite = characters[Random.Range(0, characters.Count)]; // 2
                        if(y<3)
                            {
                                newTile.GetComponent<SpriteRenderer>().sprite = newSprite.GetComponent<SpriteRenderer>().sprite;
                                
                            }
                        else
                        {
                            newTile.GetComponent<SpriteRenderer>().sprite = null;

                        }
                               // 3                   
                                newTile.transform.position = new Vector2(x, y);
                               newTile.gameObject.tag = newSprite.gameObject.tag;
                                newTile.name = "(" + x + ")" + "(" + y + ")";
                                tiles[x, y] = newTile;
                
            }
        }
    }
   
    
    public  IEnumerator Flood(int x, int y,  Color newColor,GameObject CurrentSprite)
    {
       
        WaitForSeconds wait = new WaitForSeconds(.001f);
        if (x >= 0 && x < xSize && y >= 0 && y < ySize && count <= 162)
        { 
            Debug.Log(point);
            ++count;
            if (tiles[x, y].tag == CurrentSprite.tag)
            {
                
                   yield return wait;
                    tiles[x, y].GetComponent<SpriteRenderer>().sprite = null;
                //countPoitn = point;
                   // tiles[x, y].tag = "NullSprites";
                   
                    StartCoroutine(Flood(x + 1, y, newColor, CurrentSprite));
                    StartCoroutine(Flood(x - 1, y, newColor, CurrentSprite));
                    StartCoroutine(Flood(x, y + 1, newColor, CurrentSprite));
                    StartCoroutine(Flood(x, y - 1, newColor, CurrentSprite));
               
                  
            }  
              
        }
        else if (count > 162)
        {
            yield return wait;
           
            count = 0;
            
        }
       


        // oldColor = Color.white;

        //Queue<GameObject> queue = new Queue<GameObject>();
        // queue.Enqueue(tiles[x,y]);
        // if(queue.Count>0||queue.Count<10)


    }
    private bool checkIsGameOver()
    {
        for (int x = 0; x < xSize; x++)
        {      // 11
            for (int y = 0; y < ySize; y++)
            {
                if (tiles[x, ySize - 1].GetComponent<SpriteRenderer>().sprite != null)
                {
                    isGameOver = true;
                }
                else
                {
                    isGameOver=false;
                }

            }
        }
        return isGameOver;
    }    
    public IEnumerator DownTile()
    {
        yield return new WaitForSeconds(.5f);
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                if (y > 0)
                {
                    if (tiles[x, y].GetComponent<SpriteRenderer>().sprite != null && tiles[x, y - 1].GetComponent<SpriteRenderer>().sprite == null)
                    {
                        while (y > 0 && tiles[x, y - 1].GetComponent<SpriteRenderer>().sprite == null)
                        {
                            //thay tag name
                            tiles[x, y - 1].tag = tiles[x, y].tag;
                            // tiles[x, y].tag = "NullSprites";
                            //thay sprite
                            tiles[x, y - 1].GetComponent<SpriteRenderer>().sprite = tiles[x, y].GetComponent<SpriteRenderer>().sprite;
                            tiles[x, y].GetComponent<SpriteRenderer>().sprite = null;
                            point += 2;
                           
                           
                            if (y > 0)
                            {
                                y--;
                                m_ui.setScoretext("Score :" + point);
                                hightScore = point;
                            } 
                        }
                    }
                }
            }
        }
       
    }
   public IEnumerator createAtFirstRow()
    {
        yield return new WaitForSeconds(.3f);
       
       for(int x=0; x<xSize; x++)
        {    
          
            
                if (tiles[x,0].GetComponent<SpriteRenderer>().sprite == null)
                {
                    GameObject newSprite = characters[Random.Range(0, characters.Count)];
                    tiles[x, 0].GetComponent<SpriteRenderer>().sprite = newSprite.GetComponent<SpriteRenderer>().sprite;
                    tiles[x, 0].tag=newSprite.tag;
                    //characters[Random.Range(0, characters.Count)]
                }

        }
    }    
    public IEnumerator upTile()
    {
        yield return new WaitForSeconds(0.1f);
        for (int x = 0; x < xSize; x++)
        {
            for (int y = ySize - 1; y >= 0; y--)
            {
                if (tiles[x, y].GetComponent<SpriteRenderer>().sprite != null && tiles[x, y + 1].GetComponent<SpriteRenderer>().sprite == null)
                {
                   
                    tiles[x, y + 1].tag = tiles[x, y].tag;
                   // tiles[x, y].tag = "NullSprites";
                    tiles[x, y + 1].GetComponent<SpriteRenderer>().sprite = tiles[x, y].GetComponent<SpriteRenderer>().sprite;
                    tiles[x, y].GetComponent<SpriteRenderer>().sprite = null;
                    //StartCoroutine(CreateFirstRow());
                }
            }

        }
        if(checkIsGameOver())
        {
            //Debug.Log("fail");
            m_ui.setgameoverpanel(true);
            m_ui.setHighScoretext("Your High Score :"+hightScore);
            m_ui.setScoretext(null);
        }    
    }



    }
