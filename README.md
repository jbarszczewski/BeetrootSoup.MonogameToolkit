# BeetrootSoup.MonogameToolkit
Lightweight library containing small helpers for monogame.

The main part of the library is the layout system which let you organize renderable objects into scenes->layers->grouped objects. The library is in very early version and currently targeting win32 projects, although could be easily compiled against different monogame library).

##Layout
The base class for the layout is Node. Apart from collection of children Nodes it contains basic properties like position, rotation and speed. It also contais IMovementPattern object to create custom movement logic. There are several classes that extend Node providing more functionality. 
