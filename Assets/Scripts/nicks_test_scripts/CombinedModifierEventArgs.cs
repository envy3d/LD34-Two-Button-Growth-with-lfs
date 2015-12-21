using System;
using System.Collections.Generic;

public class CombinedModifierEventArgs : EventArgs
{
    private List<float> modifiers;

    public CombinedModifierEventArgs()
    {
        modifiers = new List<float>();
    }

    public List<float> GetModifiers()
    {
        return modifiers;
    }

    public void AddModifier(float value)
    {
        if (modifiers == null)
        {
            modifiers = new List<float>();
        }
        modifiers.Add(value);
    }

    public void ResetModifiers()
    {
        if (modifiers != null)
        {
            modifiers.Clear();
        }
    }
}