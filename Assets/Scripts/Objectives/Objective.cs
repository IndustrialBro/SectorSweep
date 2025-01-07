using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objective
{
    public abstract void PrepareObjective(); // Pøipraví co je potøeba (oznaèí nepøítele na zabití, spawne tu vìc co musí ukrást, bla bla bvla)
    protected abstract void EndObjective();
}
