namespace ProductValidation.Core.Exceptions;

public class NullValidatorException<T> : Exception
{
    public NullValidatorException() : base(
        $"A validator associated with the provided type '{typeof(T)}' is not injected to the application, therefore this operation is terminated. Please add necessary validator(s) and try again."
        )
    {
    }
}