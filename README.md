NameParser
====
Person name parser for english names. 

```csharp
string fullName = "Jack Johnson"; 
FullNameParser target = new FullNameParser(fullName); 
target.Parse();
string firstName = target.FirstName;
string lastName = target.LastName;
string displayName = target.DisplayName;
```
