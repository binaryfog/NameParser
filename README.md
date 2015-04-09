NameParser
====
Human name parsing. Parses names using English conventions for persons names. 

```csharp
string fullName = "Jack Johnson"; 
FullNameParser target = new FullNameParser(fullName); 
target.Parse();
string firstName = target.FirstName;
string lastName = target.LastName;
string displayName = target.DisplayName;
```
