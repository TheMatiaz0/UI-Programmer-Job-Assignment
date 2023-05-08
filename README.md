# UI Programmer Job Assignment
Unity version: 2022.2.18f1
UI design created by company for this job assignment as UI programmer.

Known Bugs:
- Scroll snapping does not include vertical layout group paddings top and bottom
- Snapping needs offset when detecting elements inside viewport
- Raycasting UI does not work properly with Lens Distortion effect (URP), this needs to be probably replaced by solution outside of postprocessing stack: https://gamedev.stackexchange.com/questions/140334/how-to-make-curved-gui-in-unity
- Popup tweening has complex and rare bug that leaves it in the half state (Schrodingen's Popup)
