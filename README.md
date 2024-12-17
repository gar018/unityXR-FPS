# unityXR-FPS

## What is this?
This project is the culmination of my first experiences working with Unity and OpenXR in Unity 
via Bucknell's 'Design and Development for XR' course. Our final project was to focus on something we wanted to create
in Unity- applying the UX concepts we had learned throughout the class. Because I have always been a fan of shooter games, and wanted to explore how one would create a gun of their own, the design I went with was to create an interactable firearm with behaviors that resemble a real-life firearm.

## Demo Video

[![Watch the demo video here!](https://img.youtube.com/vi/1CRLHornsws/maxresdefault.jpg)](https://www.youtube.com/watch?v=1CRLHornsws)

## Implemented Features

- Teleportation and Continuous Move Locomotion (with tunneling vignette to help with motion sickness)
- An interactable Firearm, with fully-functional reloading and firing mechanics
- Audio, visual, and haptic feedback for user actions such as inserting or ejecting the magazine, racking the slide, etc.
- Dynamic ammo indicator on the magazines
- Reloading affordances, such as a glowing visual on the gun magazine or slide to provide instructions on how to successfully reload.
- Magazine respawning system

## Planned Features

For this project I was being lent a school headset which I no longer have with me. However when I learn how to develop on my personal headset there are some features I intend to implement:

- New scene environment based in a gun range
- Improved bullet projectiles for more engaging user experience
- Target practice game (following better projectiles)
- Code cleanup, allowing the GunAffordanceProvider component to be a gun framework for any weapon model

## Installation and Setup

The project was created inside of a Demo Scene for the XR Interaction Toolkit, and I never got to making a separate scene after I began working. Unfortunately, some steps are a bit unusual after booting up Unity. This is a first priority in Planned Features and this installation guide will change.

1. Clone this repository using 'git clone https://github.com/gar018/unityXR-FPS.git'
2. Open Unity Hub and add project from disk. Select the root folder of the repo.
3. Once the project is loaded on Unity, follow this path from the Project window to the scene: 'Samples/XR Interaction Toolkit/2.6.3/Starter Assets/DemoScene'.
4. Select DemoScene (not to be confused with the folder) to pull up the scene. Go to File -> Build Settings to Build & Run.