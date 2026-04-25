using UnityEngine;

public class InputBlocker
{
    private int _blockCount;
    public bool IsBlocked => _blockCount > 0;
    public void Block()
    {
        _blockCount++;
    }

    public void Unblock()
    {
        _blockCount--;
    }
}
