using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneratedLevel : Level
{
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject[] _layers1;
    [SerializeField] private GameObject[] _layers2;
    [SerializeField] private GameObject[] _layers3;
    [SerializeField] private GameObject[] _layers4;

    [SerializeField] private bool _keepGenerating;

    public override void Init()
    {
        if (_keepGenerating == false)
        {
            base.Init();
            return;
        }

        Instantiate(_layers1[Random.Range(0, _layers1.Length)], _parent);
        Instantiate(_layers2[Random.Range(0, _layers2.Length)], _parent);
        Instantiate(_layers3[Random.Range(0, _layers3.Length)], _parent);
        Instantiate(_layers4[Random.Range(0, _layers4.Length)], _parent);

        List<PuzzleTile> tiles = new List<PuzzleTile>();
        foreach (Transform g in transform.GetComponentsInChildren<Transform>())
        {
            if (g.TryGetComponent(out PuzzleTile tile))
                tiles.Add(tile);
        }
        SetTiles(tiles);
        
        base.Init();
    }

    public void SetKeepGenerating(bool valu)
    {
        _keepGenerating = valu;
    }
}
