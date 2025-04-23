With the Micro:Bit-Unity Serial Interface it is possible to connect a Micro:Bit to Unity and use the sensors from the Micro:Bit as inputs in unity. This project includes the necessary classes for Unity and the code for the Micro:Bit. It supports reading the majority of sensors on the Micro:Bit. Output is not supported at the time.

# Compatibility 

Micro:Bit Unity Serial Interface has currently only been tested on Mac. It should theoretically also run on Windows. Please log issues if you encounter any. Mobile platforms are not currently supported, but as the 

# Installation and Usage

To use the Micro:Bit-Unity Serial Interface, you must install the code on the Micro:Bit, and then prepare your Unity project to receive the signals from the Micro:Bit. 

## Installing the Code on Micro:Bit

1) Download the .hex file from [../0-PackagedVersions/microbit-UnityMicrobitController.hex](../0-PackagedVersions/microbit-UnityMicrobitController.hex)
2) Connect your micro:bit to your computer with a USB cable
3) Open Finder (Mac) / File explorer (Windows) and notice that your micro:bit is listed like a USB drive called MICROBIT.
4) Find the downloaded program hex file (e.g. in your local downloads folder) and drag and drop this on to the MICROBIT drive.

For more detailed instructions, please consult the article on the Micro:Bit support website [https://microbit.org/get-started/user-guide/transfer-code-to-the-microbit/]

## Prepare your Unity Project

There are two ways to prepare your Unity Project to receive signals from the Micro:Bit. 

### Variant 1: Start with this project

If you have not yet started with your own project, simply download this project from Github by pressing the green "Code" button and downloading the .zip File. Unpack it to the location of your choice. Then, add the project to Unity Hub by choosing Add Project and selecting the project folder you just created.

### Variant 2: Add the Unity Package to your 
