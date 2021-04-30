using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;


    // Board positions! Not world positions!
    int matrixX;
    int matrixY;


    // false is movement, true means it's attacking
    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            // Change to different color
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }
    
    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        


        if (attack)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            Destroy(cp);
        }

        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(),
            reference.GetComponent<Chessman>().GetYBoard());


        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();
        reference.GetComponent<Chessman>().SetHasMoved(); //setter

        controller.GetComponent<Game>().SetPosition(reference);

        controller.GetComponent<Game>().NextTurn();

        controller.GetComponent<Game>().EnablePieces();

        reference.GetComponent<Chessman>().DestroyMovePlates();
    }



    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
