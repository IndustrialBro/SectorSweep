using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objective
{
    public abstract void PrepareObjective(); // P�iprav� co je pot�eba (ozna�� nep��tele na zabit�, spawne tu v�c co mus� ukr�st, bla bla bvla)
    public virtual bool CheckForSuccess(GameObject go) { return true; } // Zkontroluje jestli hr�� splnil �kol
}
