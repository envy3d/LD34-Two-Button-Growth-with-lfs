
public class TurningFlipFlopSelection : ITurningSelection
{
    public int GetNewTurningDirection(int currentDir)
    {
        return currentDir * -1;
    }
}
