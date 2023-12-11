using UnityEngine;

public class ShuffleTipButton : TipButtonBase
{
    [SerializeField] private Board _board;
    protected override bool BoardConditionCheck()
    {
        // Always true
        return true;
    }

    protected override void TipAction()
    {
        _board.ShuffleTiles();
    }
}
