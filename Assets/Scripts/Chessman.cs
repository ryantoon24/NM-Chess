using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{

    // References
    public GameObject controller;
    public GameObject movePlate;
    public GameObject moveCancel;

    private int pieceGroup;

    // Positions
    private int xBoard = -1;
    private int yBoard = -1;

    // Variable to keep track of group because there can only be so many tags at once
    private bool selectedGroup;
    private bool groupOne;

    // Variable to keep track of side
    private string player;

    // Variable to keep track if the piece has moved yet, important for pawn double first move rule.
    private bool hasMoved;

    // Refrences for all the possible sprites
    public Sprite black_queen, black_knight, black_bishop, black_king, black_rook, black_pawn;
    public Sprite white_queen, white_knight, white_bishop, white_king, white_rook, white_pawn;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");


        // Take instantiated location and adjust the transformation
        SetCoords();

        switch (this.name)
        {
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "black"; pieceGroup = 1; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = black_knight; player = "black"; pieceGroup = 1; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; player = "black"; pieceGroup = 1; break;
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; player = "black"; pieceGroup = 1; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "black"; pieceGroup = 1; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "black"; pieceGroup = 0; hasMoved = false; break;

            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; player = "white"; pieceGroup = 1; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = white_knight; player = "white"; pieceGroup = 1; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = "white"; pieceGroup = 1; break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; player = "white"; pieceGroup = 1; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = "white"; pieceGroup = 1; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = "white"; pieceGroup = 0; hasMoved = false; break;


        }
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }


    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }
    

    private void OnMouseUp()
    {
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            DestroyMovePlates();

            InitiateMovePlates();

            GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>().DisablePieces();
        }
    }
    

    public void OnFocus()
    {
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            DestroyMovePlates();

            InitiateMovePlates();

            GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>().DisablePieces();
        }
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "black_queen":
            case "white_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "black_knight":
            case "white_knight":
                LMovePlate();
                break;
            case "black_bishop":
            case "white_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "black_king":
            case "white_king":
                SurroundMovePlate();
                break;
            case "black_rook":
            case "white_rook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "black_pawn":
                PawnMovePlate(xBoard, yBoard - 1);
                if (hasMoved == false) // mine
                {
                    PawnMovePlate(xBoard, yBoard - 2);
                }
                //PawnFirstMovePlate(xBoard, yBoard - 2);
                break;
            case "white_pawn":
                PawnMovePlate(xBoard, yBoard + 1);
                if (hasMoved == false) // mine 
                {
                    PawnMovePlate(xBoard, yBoard + 2); 
                }
                //PawnFirstMovePlate(xBoard, yBoard + 2);
                break;

        }

    }

    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x,y) && sc.GetPosition(x,y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if (sc.PositionOnBoard(x,y) && sc.GetPosition(x,y).GetComponent<Chessman>().player != player)
        {
            MovePlateAttackSpawn(x, y);
        }

        
    }

    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard);
        PointMovePlate(xBoard - 1, yBoard);
        PointMovePlate(xBoard + 1, yBoard + 1);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 1);
    }

    public void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x,y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                MovePlateSpawn(x, y);
            }
            else if (cp.GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public void PawnMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }

            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y);
            }

            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }

    }

    /* was mine but I realized this code was dumb and more difficult
    public void PawnFirstMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();

        if (sc.PositionOnBoard(x, y))
        {
            if (hasMoved == false)
            {
                if (sc.GetPosition(x, y) == null)
                {
                    MovePlateSpawn(x, y);
                }
            }
        }
    }
    */

    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }


    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void CheckComponents()
    {
        if (controller.GetComponent<Game>().GetCurrentPlayer() == player && controller.GetComponent<Game>().GetCurrentGroup() == pieceGroup && controller.GetComponent<Game>().GetEnabled())
        {
            this.GetComponent<NextMind.NeuroTags.NeuroTag>().enabled = true;
            this.transform.GetChild(0).transform.gameObject.SetActive(true);
        }
        else
        {
            this.GetComponent<NextMind.NeuroTags.NeuroTag>().enabled = false;
            this.transform.GetChild(0).transform.gameObject.SetActive(false);
        }
    }

    

    // mine
    public void SetHasMoved()
    {
        hasMoved = true;
    }

    //also mine
    private void Update()
    {
        CheckComponents();   
    }

    public void CancelMove()
    {
        DestroyMovePlates();
    }

    public void SetNMActive()
    {
        this.GetComponent<NextMind.NeuroTags.NeuroTag>().enabled = false;
    }

}
