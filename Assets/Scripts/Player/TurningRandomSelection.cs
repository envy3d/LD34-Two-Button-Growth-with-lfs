using UnityEngine;

public class TurningRandomSelection : ITurningSelection
{

    public int GetNewTurningDirection(int currentDir)
    {
        return (Random.Range(0, 2) * 2) - 1;
    }
}

