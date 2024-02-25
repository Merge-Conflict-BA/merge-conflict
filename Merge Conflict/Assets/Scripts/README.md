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