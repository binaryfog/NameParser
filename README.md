NameParser
====
Human name parsing. Parses names using English conventions for persons names. 
Intended to be extendable, the library can be extended just by implement IPattern interface and assign a score to the returned result.

If you have a person name , that is not parsed correctly, let me know. harapu@gmail.com  . I'll see what I can do.

The cases handled are:
```csharp
    /// <summary>
    /// Parse a person full name 
    /// </summary>
    /// <example>
    /// 1. Mr Jack Johnson  => Title = "Mr", First Name = "Jack" Last Name = "Johnson"
    /// 2. Jack Johnson  => First Name = "Jack" Last Name = "Johnson"
    /// 3. Jack => First Name = "Jack"
    /// 4. Jack Johnson Enterprises => ignored
    /// 5. Pasquale (Pat) Vacoturo  =>  First Name = "Pasquale" Last Name = "Vacoturo" Nickname = Pat 
    /// 6. Mr Giovanni Van Der Hutte  => Title = "Mr", First Name = "Giovanni" Last Name = "Van Der Hutte"
    /// 7. Giovanni Van Der Hutte  => First Name = "Giovanni" Last Name = "Van Der Hutte"
    /// </example>
    /// <remarks>
    /// 1. The prefix "ATTN:" is removed if exists and the parsing proceeds on the new string
    /// </remarks>
```

```csharp
//Usage
string fullName = "Mr. Jack Johnson"; 
FullNameParser target = new FullNameParser(fullName); 
target.Parse();
string firstName = target.FirstName;
string lastName = target.LastName;
string displayName = target.DisplayName;
string title = target.Title;

```


