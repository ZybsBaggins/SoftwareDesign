using System;
using System.Collections.Generic;
using System.Linq;

namespace LayeredArchitectureExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setting up dependencies
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            var userController = new UserController(userService);

            // Creating a new user
            userController.CreateUser("john_doe", "password123");

            // Retrieving and displaying user details
            var user = userController.GetUser(1);
            if (user != null)
            {
                Console.WriteLine($"User: {user.Username}");
            }
            else
            {
                Console.WriteLine("User not found");
            }
        }
    }

    // Presentation Layer: Handles user input and output
    public class UserController
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // Method to create a new user by passing the request to the service layer
        public void CreateUser(string username, string password)
        {
            _userService.CreateUser(username, password);
        }

        // Method to retrieve user details by calling the service layer
        public User GetUser(int userId)
        {
            return _userService.GetUser(userId);
        }
    }

    // Business Logic Layer: Contains core business logic
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Method to create a user and add it to the repository
        public void CreateUser(string username, string password)
        {
            var user = new User { Username = username, Password = password };
            _userRepository.AddUser(user);
        }

        // Method to get user details from the repository
        public User GetUser(int userId)
        {
            return _userRepository.GetUser(userId);
        }
    }

    // Data Access Layer: Interacts with the data source
    public class UserRepository
    {
        private readonly List<User> _users = new List<User>();

        // Method to add a user to the in-memory list
        public void AddUser(User user)
        {
            user.Id = _users.Count + 1;
            _users.Add(user);
        }

        // Method to retrieve a user by ID from the in-memory list
        public User GetUser(int userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }
    }

    // Domain Model: Represents the user entity
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
