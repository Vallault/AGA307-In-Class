using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Week1 : MonoBehaviour
{
    public GameObject player;
    
    public int numberOne = 9;
    public int numberTwo = 7;
    public int newNumber;

    void Start()
    {
        player.transform.position = new Vector3(numberTwo, numberOne, numberTwo);
        player.GetComponent<Renderer>().material.color = Color.red;

        AddNumbers();
        AddNumbers(numberOne, numberTwo);
        
        newNumber = AddNumbers2(7, 7);
    }

    int AddNumbers2(int _one, int _two)
    {
        return _one + _two;
    }

    void AddNumbers(int _one, int _two)
    {
        newNumber = _one + _two;
        print(newNumber);
    }

    void AddNumbers()
    {
        newNumber = numberOne + numberTwo;
        print(newNumber);
    }
}
