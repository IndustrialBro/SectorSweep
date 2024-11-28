using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objective : ScriptableObject
{
    public abstract void PrepareObjective(); // Pøipraví co je potøeba (oznaèí nepøítele na zabití, spawne tu vìc co musí ukrást, bla bla bvla)
    public abstract bool CheckForSuccess(); // Zkontroluje jestli hráè splnil úkol
}
