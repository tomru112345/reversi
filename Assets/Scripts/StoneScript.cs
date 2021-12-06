using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    bool isBlack_;

    public bool IsBlack()
    {
        return isBlack_;
    }

    public void IsBlack(bool isBlack)
    {
        isBlack_ = isBlack;
    }
}
