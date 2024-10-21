namespace csharp_cartographer._01.Configuration.ArtifactTypes
{
    public class ArtifactType
    {
        public string Type { get; set; } = string.Empty;

        public List<string> Facts { get; set; } = [];
    }

    public class ArtifactTypeConfig
    {
        public static readonly List<ArtifactType> ArtifactTypes = new()
        {
            new ArtifactType
            {
                Type = "Model Definition",
                Facts =
                [
                    "A model definition represents the data structure for an object. It typically contains properties that define the data fields and sometimes includes methods for data manipulation or validation.",
                    "It typically contains properties that define the data fields and sometimes includes methods for data manipulation or validation.",
                    "Model classes are often used in frameworks like ASP.NET Core MVC or Entity Framework to define how data is stored, transmitted, or displayed."
                ],
            },
            new ArtifactType
            {
                Type = "Service Class",
                Facts =
                [
                    "A service is a class that contains business logic or operations to process data, interact with external systems (e.g., APIs or databases), or perform complex computations.",
                    "Services typically serves as a middle layer between the controllers and data models, encapsulating the application's core functionality.",
                    "In frameworks like ASP.NET Core, service classes are often registered for dependency injection and provide reusable methods for different parts of the application."
                ],
            },
            new ArtifactType
            {
                Type = "Workflow Class",
                Facts =
                [
                    "A workflow class is responsible for orchestrating and coordinating a sequence of steps or tasks that make up a business process.",
                    "It defines the flow of execution, including the logic for handling various conditions, decisions, and transitions between tasks.",
                    "Workflows are often used in systems that manage complex business processes, ensuring that actions occur in the correct order and under the right conditions."
                ],
            },
            new ArtifactType
            {
                Type = "DataAccess Class",
                Facts =
                [
                    "A data access class is responsible for interacting with the database or other data storage systems to perform create, read, update, & delete (CRUD) operations.",
                    "This keeps data access logic separate from business logic and UI layers, promoting maintainability and testability.",
                    "This class is often referred to as a repository when used with the Repository Pattern.",
                ],
            },
            new ArtifactType
            {
                Type = "Helper Class",
                Facts =
                [
                    "A helper class is a utility class that often consists of static methods designed to perform common or repetitive tasks.",
                    "Static methods are common so so the class doesn't have to be instantiated to use the methods.",
                    "The actions these methods perform are often small and not tied to any specific process so they can be used from anywhere in the application."
                ],
            },
            new ArtifactType
            {
                Type = "Config Class",
                Facts =
                [
                    "A configuration class is designed to hold and manage application settings and configuration data, often coming from external sources like configuration files or environment variables.",
                    "This class centralizes the configuration values, making them accessible throughout the application in a clean and organized way.",
                    "Configuration classes can be registered in the dependency injection (DI) container, making them available throughout the application."
                ],
            },
            new ArtifactType
            {
                Type = "API Controller",
                Facts =
                [
                    "An API controller is a specialized class in ASP.NET Core that handles HTTP requests and responses in a Web API.",
                    "They are designed to expose endpoints that provide or consume data, typically in JSON or XML format.",
                    "They perform actions like creating, reading, updating, and deleting (CRUD) resources, and they are commonly used for building RESTful web services."
                ],
            },
            new ArtifactType
            {
                Type = "Mapping Class",
                Facts =
                [
                    "A mapping class is responsible for converting data between different object models or data structures.",
                    "Mapping classes help ensure separation of concerns by abstracting away the logic required to translate between different object structures.",
                    "The mapping can be manual (writing the code explicitly) or automated using libraries like AutoMapper."
                ],
            },
        };
    }
}
