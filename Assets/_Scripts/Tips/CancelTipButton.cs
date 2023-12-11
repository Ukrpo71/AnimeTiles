using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelTipButton : TipButtonBase
{
    protected override bool BoardConditionCheck()
    {
        return InGameQueue.CanCancelLastMove();
    }

    protected override void TipAction()
    {
        InGameQueue.CancelLastMove();
    }
}
