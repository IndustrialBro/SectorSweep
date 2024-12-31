using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objective
{
    public abstract void PrepareObjective(); // Pøipraví co je potøeba (oznaèí nepøítele na zabití, spawne tu vìc co musí ukrást, bla bla bvla)
    public virtual bool CheckForSuccess(GameObject go) { return true; } // Zkontroluje jestli hráè splnil úkol
}
