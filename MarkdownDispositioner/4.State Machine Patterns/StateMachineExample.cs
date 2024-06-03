// State Interface
public interface ITCPState
{
    void Open(TCPConnection connection); // Method to open the connection
    void Close(TCPConnection connection); // Method to close the connection
    void Send(TCPConnection connection, string data); // Method to send data
}

// Concrete States
public class ListeningState : ITCPState
{
    public void Open(TCPConnection connection)
    {
        Console.WriteLine("Already listening.");
    }

    public void Close(TCPConnection connection)
    {
        Console.WriteLine("Closing the connection.");
        connection.SetState(new ClosedState()); // Transition to ClosedState
    }

    public void Send(TCPConnection connection, string data)
    {
        Console.WriteLine("Cannot send data. Connection is in listening state.");
    }
}

public class EstablishedState : ITCPState
{
    public void Open(TCPConnection connection)
    {
        Console.WriteLine("Connection already established.");
    }

    public void Close(TCPConnection connection)
    {
        Console.WriteLine("Closing the connection.");
        connection.SetState(new ClosedState()); // Transition to ClosedState
    }

    public void Send(TCPConnection connection, string data)
    {
        Console.WriteLine($"Sending data: {data}");
    }
}

public class ClosedState : ITCPState
{
    public void Open(TCPConnection connection)
    {
        Console.WriteLine("Opening the connection.");
        connection.SetState(new EstablishedState()); // Transition to EstablishedState
    }

    public void Close(TCPConnection connection)
    {
        Console.WriteLine("Connection already closed.");
    }

    public void Send(TCPConnection connection, string data)
    {
        Console.WriteLine("Cannot send data. Connection is closed.");
    }
}

// Context
public class TCPConnection
{
    private ITCPState _state; // Current state of the TCP connection

    public TCPConnection()
    {
        _state = new ClosedState(); // Initial state
    }

    public void SetState(ITCPState state)
    {
        _state = state; // Change the current state
    }

    public void Open()
    {
        _state.Open(this); // Delegate the Open action to the current state
    }

    public void Close()
    {
        _state.Close(this); // Delegate the Close action to the current state
    }

    public void Send(string data)
    {
        _state.Send(this, data); // Delegate the Send action to the current state
    }
}

class Program
{
    static void Main()
    {
        TCPConnection connection = new TCPConnection();

        connection.Open();   // Output: Opening the connection.
        connection.Send("Hello");   // Output: Sending data: Hello
        connection.Close();  // Output: Closing the connection.
        connection.Send("Hello");   // Output: Cannot send data. Connection is closed.
    }
}
