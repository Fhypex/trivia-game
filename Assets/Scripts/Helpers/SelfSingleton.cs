
using Unity.VisualScripting;

public class SelfSingleton<T> where T : class, new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T(); // Creates an instance of T, assuming T has a public parameterless constructor.
            }
            return _instance;
        }
    }

    // Protected constructor to prevent direct instantiation, but allows T to be instantiated internally
    protected SelfSingleton()
    {
        // Initialization logic here if needed
    }
}