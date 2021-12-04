using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game : MonoBehaviour
{

}
public class Letter : MonoBehaviour
{
    private char letter;
    Vector2 position;
    public Letter(char l, double x, double y)
    {
        letter = l;
        position = new Vector2((float)x, (float)y);
    }
}
