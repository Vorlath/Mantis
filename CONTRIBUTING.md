# Mantis Contribution Guide

## Coding Practices
**1. Class and Type Names**
  - Use `PascalCase` for all names.
  - All types should be public. 
    - Exceptions can be made but must have a defined reason.
    - Public types make testing easier
  - **Examples:**
    ```cs
    // Acceptable type names
    public class UserService : IUserService {}
    public struct Point3D {}

    // Unacceptable type names
    internal class UserService : IUserService {}
    public class vector2d {}
    ``` 
**2. Enums**
  - End all enums with the `Enum` suffix.
  - Follow all existing type name conventions
  - **Examples:**
    ```cs
    // Acceptable enum names
    public enum UserStatusEnum {}
    public enum BallStateEnum {}

    // Unacceptable enum names
    public enum UserStatus {}
    public enum BallStateEnum {}
    ``` 

**3. Interfaces**
  - Begin all interfaces with `I`
  - Follow all existing type name conventions
  - **Examples:**
    ```cs
    // Acceptable enum names
    public interface IUserService {}

    // Unacceptable enum names
    public interface UserService {}
    public interface UserServiceInterface {}
    ``` 

**4. Fields and Properties**
  - Use `readonly` as much as possible.
  - Use `this` keyword for accessing instance values.
  - Use `ClassName` for accessing static values

**5. Public Fields and Properties**
  - Use `PascalCase` for naming.
  - **Examples:**
    ```cs
    // Acceptable declaration
    public string FirstName { get; set; }
    public static int Age;

    // Unacceptable declaration
    public string firstName { get; set; }
    public static int age;
    ``` 

    ```cs
    // Acceptable accessing
    Console.WriteLine(this.FirstName);
    Person.Age = 5;

    // Unacceptable accessing
    Console.WriteLine(FirstName);
    Age = 5;
    ``` 

**6. Protected Fields and Properties**
  - Use `CamelCase` for naming.
  - **Examples:**
    ```cs
    // Acceptable declaration
    protected string firstName { get; set; }
    protected static int age;

    // Unacceptable declaration
    protected string FirstName { get; set; }
    protected static int Age;
    ``` 

    ```cs
    // Acceptable accessing
    Console.WriteLine(this.firstName);
    Person.age = 5;

    // Unacceptable accessing
    Console.WriteLine(firstName);
    age = 5;
    ``` 

**7. Private Fields and Properties**
  - Use `_` prefix.
  - Use `CamelCase` for naming.

  - **Examples:**
    ```cs
    // Acceptable declaration
    private string _firstName { get; set; }
    private static int _age;

    // Unacceptable declaration
    private string FirstName { get; set; }
    private static int age;
    ``` 

    ```cs
    // Acceptable accessing
    Console.WriteLine(this._firstName);
    Person._age = 5;

    // Unacceptable accessing
    Console.WriteLine(_firstName);
    _age = 5;
    ``` 

**8. Special Types**
  - End all `Systems` with the `System` suffix.
  - End all `Services` with the `Service` suffix.
  - **Examples:**
      ```cs
    // Acceptable type names
    public class UserService : IUserService {}
    public struct RenderSystem : ISystem, IDrawSystem {}

    // Unacceptable type names
    public class Users : IUserService {}
    public class Rendering : ISystem, IDrawSystem {}
    ``` 

**9. Project folder layout**
  - Commonly seen project folders include: 
    - `./Services` - IService interfaces or their concrete implementations
    - `./Utilities` - Common utility or helper classes. Generally static by nature.
    - `./Extensions` - Contains helpful extension methods
    - `./Systems` - ISystem interfaces or their concrete implementations

## Folder Structure
Below is an overview of the key directories and their purposes:

### **1. [`./scripts/`](scripts)**
First time setup and install scripts to help configure a developers local dev envirnment. Scripts should either be written in [PowerShell](https://learn.microsoft.com/en-us/powershell/) or [Bash](https://en.wikipedia.org/wiki/Bash_(Unix_shell))

### **2. [`./src/`](src)**
Primary folder holding most of all Mantis code. Contains the following sub-folders:
  - **[`./src/engine`](src/engine)** - Holds engine specific code where engine code is defined as code that implements game loop specific functionality. Examples Include:
    - Scenes
    - Systems
    - Drawables
    - Updateables
    - UI
    - Input

  - **[`./src/core`](src/core)** - Holds core code as defined by code that can exists outside of a game engine context and does not require a game loop to be developed or consumed. Examples include:
    - Logging
    - Networking
    - Collections
    - Serialization
    - File management
    - Asset management
    - ECS?? (more discussion to be had to determin where this belongs)

For a more concise overview of projects, project layout, and their purpose see [Project Layout](#project-layout)

### **3. [`./libraries/`](libraries)**
Folder to hold required library code. As of `2025-06-01` this folder includes the following submodules:
  - [`cpt-max/MonoGame`](https://github.com/cpt-max/MonoGame/tree/5f96e607263109ffc1e8c54e3868c5cf134a9c2d)
  - [`sebas77/Svelto.ECS`](https://github.com/sebas77/Svelto.ECS/tree/a106768f1b583f10df1e0af24c001fa87d9b3f55)

  
### **4. [`./examples/`](examples)**
Folder to hold all example and demo code for Mantis engine. Projects added here should be small self contained demos that clearly illustrate best practices for Mantis usage.

### **5. [`./tests/`](tests)**
Holds test projects for [./src/](src) code.

---

## Project Layout
We will try to follow a [Clean Architecture](https://www.dandoescode.com/blog/clean-architecture-an-introduction) layerd namespacing pattern. Due to the nature of a game engine our layer names may differ somewhat from traditional layers found in a typical Clean Arhitecture project. So far we have the following namespaces:

### [`Mantis.Core`](src/core)
Holds core code as defined by code that can exists outside of a game engine context and does not require a game loop to be developed or consumed. This may be synonymous with the [domain layer](https://www.dandoescode.com/blog/clean-architecture-an-introduction#domain-layer) found in a typical Clean Architecture environment.

Projects currently found in this namespace include:
  - [`Mantis.Core.ECS.Common`](src/core/Mantis.Core.ECS.Common/Mantis.Core.ECS.Common) - Defines common interfaces for ECS integration.
  - [`Mantis.Core.ECS`](src/core/Mantis.Core.ECS/Mantis.Core.ECS) - Contains [sebas77/Svelto.ECS](https://github.com/sebas77/Svelto.ECS) implementations for common ECS integration


### [`Mantis.Engine`](src/engine)
Holds engine specific code as defined by code that implements game loop specific functionality. This may be synonymous with the [application layer](https://www.dandoescode.com/blog/clean-architecture-an-introduction#application-layer) found in a typical Clean Architecture environment.

Projects currently found in this namespace include:
  - [`Mantis.Engine.Common`](src/engine/Mantis.Engine.Common/) - Defines common engine interfaces
  - [`Mantis.Engine`](src/engine/Mantis.Engine/) - Contains MonoGame specific implementations for common engine interfaces 

---