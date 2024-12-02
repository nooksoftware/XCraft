
# GUI Assessment
## GUIElement (GUIE)

children (sub guielements)
children handling (add, get, erase)
    add, get, erase, handles sub-methods of the guielement, these, **must be protected**

access to d and a
parent (GUIE)
rendering (GUIR)
activity (GUIA)
type (GUIT) (should be protected)

isUniversal = true;
clickState (unless additionally specified in concrete)
normal, hover, and clicked color

Ti navigationArea (if applicable)
    Implement Ti

Navigation (lx, ly, lw, lh, lox, loy)
Navigation applying (no custom APi-Software-enginering editin)

id snapshot from Dictionary of the parent of guie (**must be protected**)
Full path

cWidth, cHeight (constant width/height)

protected subfunctions
    (connecting to parent)
    (id, fullpath)

public subfunctions
    (render appropriate coordinates, transforms, as simple rectangles, lines, etc.)
    other misc outputting

## Code Programming Principles
### GUI Element Writing
 - Question 1: Assess the requirements of GUIElement, take into account - ***purpose (behavior)***, ***data (structure)*** of the GUI Element, 
 - Question 2: Prepare the labeling conventions (how the methods; functions, variables, classes, and other symbols, are going to be named)
   - Names of the labels
   - Conventionality of the labels
 - Question 3: Prepare the requirements on markdown files, along with prepared labels
 - Question 4: Prepare the Acceptance Test for the GUIElements, with all required testing
 - Question 5: Prepare the algorithms to be implemented
   - Question 5.B: What is required from the GUIE, GUIA, GUIG, or D, A, or Game1 classes perspectives?
   - Question 5.B.2: If there is something, pre-list, pre-assess, and possibly implement
 - Question 6: Write down everything (speed typing)

## Progression (GUI)

1. 
 - Prepare all GUIElement Concepts
 - Prepare all assessments for them
 - Implement all acceptance tests for them
 - Implement its code (speed typing)

2.
 - Create GUI System
   - Main Menu
   - Editor
   - Game menu

