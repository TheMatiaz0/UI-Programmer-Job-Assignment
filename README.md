# UI Programmer Job Assignment

Unity version: 2022.2.18f1

UI design has been provided by the company for this job assignment as UI programmer.

![mouse_window](https://github.com/TheMatiaz0/UI-Programmer-Job-Assignment/blob/main/public/mouse_window.gif)
![windows](https://github.com/TheMatiaz0/UI-Programmer-Job-Assignment/blob/main/public/windows.png)

Featuring:
- Buttons with sfx and color tint.
- Ability to use both keyboard and/or gamepad.
- Smooth scroll view.
- Volume slider.
- Checkbox field.
- Popup windows.
- Own navigation system (bypassing Unity navigation).
- Post-processing effects.

Download: https://github.com/TheMatiaz0/UI-Programmer-Job-Assignment/releases/tag/1.0.0

Known Bugs:
- Scroll snapping does not include vertical layout group paddings top and bottom
- Snapping needs offset when detecting elements inside viewport
- Raycasting UI does not work properly with Lens Distortion effect (URP), this needs to be probably replaced by solution outside of postprocessing stack: https://gamedev.stackexchange.com/questions/140334/how-to-make-curved-gui-in-unity
- Popup tweening has complex and rare bug that leaves it in the half state (Schrodinger's Popup)
