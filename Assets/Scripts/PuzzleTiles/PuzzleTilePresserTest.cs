using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTilePresserTest : MonoBehaviour
{
    [SerializeField] private PuzzleTilePresser _puzzleTilePresser;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _puzzleTilePresser.CheckIfCanBePressed();
        }
    }
}
