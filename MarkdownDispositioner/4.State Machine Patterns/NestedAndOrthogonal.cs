// State Interface
public interface IAuthState
{
    void Authenticate(UserAuthContext context); // Method to handle authentication
}

// Concrete States
public class LoggedOutState : IAuthState
{
    public void Authenticate(UserAuthContext context)
    {
        Console.WriteLine("Logging in...");
        context.SetState(new LoggedInState()); // Transition to LoggedInState
    }
}

public class LoggedInState : IAuthState
{
    public void Authenticate(UserAuthContext context)
    {
        Console.WriteLine("Already logged in. Performing two-factor authentication...");
        context.SetState(new TwoFactorAuthState()); // Transition to TwoFactorAuthState
    }
}

public class TwoFactorAuthState : IAuthState
{
    public void Authenticate(UserAuthContext context)
    {
        Console.WriteLine("Two-factor authentication complete. Validating session...");
        context.SetState(new SessionValidatedState()); // Transition to SessionValidatedState
    }
}

public class SessionValidatedState : IAuthState
{
    public void Authenticate(UserAuthContext context)
    {
        Console.WriteLine("Session already validated.");
    }
}

// Context
public class UserAuthContext
{
    private IAuthState _state; // Current state of the user authentication

    public UserAuthContext()
    {
        _state = new LoggedOutState(); // Initial state
    }

    public void SetState(IAuthState state)
    {
        _state = state; // Change the current state
    }

    public void Authenticate()
    {
        _state.Authenticate(this); // Delegate the Authenticate action to the current state
    }
}

class Program
{
    static void Main()
    {
        UserAuthContext authContext = new UserAuthContext();

        authContext.Authenticate();   // Output: Logging in...
        authContext.Authenticate();   // Output: Already logged in. Performing two-factor authentication...
        authContext.Authenticate();   // Output: Two-factor authentication complete. Validating session...
        authContext.Authenticate();   // Output: Session already validated.
    }
}
