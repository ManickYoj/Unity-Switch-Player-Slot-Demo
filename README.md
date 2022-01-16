# Switching Player Control Demo
The code to support a pair of articles:
- [Switching Character Control in Unity: Part I, Design](https://medium.com/@nickfrancisci/switching-character-control-in-unity-part-i-design-57f3bd42e071)
- [Switching Character Control in Unity: Part II, Implementation](https://medium.com/@nickfrancisci/switching-character-control-in-unity-part-ii-implementation-a05432f7d7be)


### Dependencies
- [Gridbox Prototype Materials](https://assetstore.unity.com/packages/2d/textures-materials/gridbox-prototype-materials-129127)
- [M4A3E2 Tank Model](https://assetstore.unity.com/packages/3d/vehicles/land/m4a3e2-84358)

Steps to repliacate scene:
1. Import the tank and prototype material packages.
2. Add the tank to the scene. Attach a camera to the turret and one to the body. Position them appropriately.
3. Add colliders to the track_L and track_R.
4. Put down a quad for the ground. Apply a prototype grid texture to the mesh renderer and tile it to taste.
4. Convert to New Unity Input System
5. Add driving script
6. Add turret rotate script
7. Add Receiver, Transmitter, and Registry scripts
