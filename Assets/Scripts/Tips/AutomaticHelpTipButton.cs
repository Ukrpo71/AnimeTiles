using System;
using UnityEngine;

public class AutomaticHelpTipButton : TipButtonBase
{
    [SerializeField] private Board _board;

    protected override bool BoardConditionCheck()
    {
        return InGameQueue.CanAutomaticHelp();
    }

    protected override void TipAction()
    {
        _board.AutomaticHelp();
    }
}
