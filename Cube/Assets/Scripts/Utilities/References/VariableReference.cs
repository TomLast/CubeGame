﻿public class VariableReference<T, VT> where VT : Variable<T>
{
    public bool UseConstant = true;
    public T ConstantValue;
    public VT Variable;

    public VariableReference()
    { }

    public VariableReference(T value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public T Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
        set
        {
            if (UseConstant) ConstantValue = value;
            else
                Variable.Value = value;
        }
    }
}