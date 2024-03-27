# Some helpful infos and guidlines

## Debugging
For **Debuging** please use the Debugger class. It logs only if the programm runs in unity editor. 

There are the 3methods:
- logMessage(string message)
- logWarning(string message)
- logError(string message)

Feel free to add some more methods if needed.

---

## Component handler and spawning
- the component-prefab calls the ComponentHandler
    - ComponentHandler contains methods for the dragging and merging logic
- the conveyorBelt object calls the ComponentSpawner
    - it contains the methods for the spawning at a custom postion and a method to spawn direct on  the belt
 
---
## Compare GameObject tag
To compare the tage from an GameObject, pls. use the static class [Tag](/Assets/Scripts/Tag.cs)! If there is a new tag upcoming, add it to the PossibleTags.<br><br>
**Usage:**
```
/* Pseudo: 
bool tagCheckResult = Tag.CompareTags(<GameObjectWhichShouldCheckedOnTag>, Tag.PossibleTags.<theTagWhichShouldItBe>)
*/

if (Tag.CompareTags(col.gameObject, Tag.PossibleTags.ConveyorBelt))
{
    Debug.Log("col.gameObject has the tag: ConveyorBelt")
}
```


---

## [C# Coding Standards and Naming Conventions](https://github.com/ktaranov/naming-convention/blob/master/C%23%20Coding%20Standards%20and%20Naming%20Conventions.md#c-coding-standards-and-naming-conventions)
| Object    | Name | Notation |
| -------- | ------- | ------- |
| Namespace | name | PascalCase |
| Class | name | PascalCase |
| Constructor | name | PascalCase |
| Method | name | PascalCase |
| Method | arguments | camelCase |
| Local | variables | camelCase |
| Local | constant | PascalCase |
| Field | name Public | PascalCase |
| Field | name Private | _camelCase |
| Properties | name | PascalCase |
| Delegate | name | PascalCase |
| Enum | type name | PascalCase |





