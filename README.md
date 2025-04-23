With the Micro:Bit-Unity Serial Interface it is possible to connect a Micro:Bit to Unity and use the sensors from the Micro:Bit as inputs in unity. This project includes the necessary classes for Unity and the code for the Micro:Bit. It supports reading the majority of sensors on the Micro:Bit. Output is not supported at the time.

# Compatibility 

Micro:Bit Unity Serial Interface has currently only been tested on Mac. It should theoretically also run on Windows. Please log issues if you encounter any. Mobile platforms are not currently supported.

The Micro:Bit-Unity Serial Interface only supports *wired* connections. A setup for *wireless* connections using two Micro:Bit-units is in preparation.

This project was developed und tested on Unity 6, version 6000.0.39f1. It may also run on later versions, no guarantee however.

# Installation, Configuration and Usage

To use the Micro:Bit-Unity Serial Interface, you must install the code on the Micro:Bit, and then prepare your Unity project to receive the signals from the Micro:Bit. 

## Installing the Code on Micro:Bit

1) Download the file microbit-UnityMicrobitController.hex from [0-Packaged Versions](https://github.com/sutee9/MicroBit-Unity-SerialInterface/blob/master/0-PackagedVersions/)
2) Connect your micro:bit to your computer with a USB cable
3) Open Finder (Mac) / File explorer (Windows) and notice that your micro:bit is listed like a USB drive called MICROBIT.
4) Find the downloaded program hex file (e.g. in your local downloads folder) and drag and drop this on to the MICROBIT drive.

For more detailed instructions, please consult the article on the Micro:Bit support website [https://microbit.org/get-started/user-guide/transfer-code-to-the-microbit/]

## Prepare your Unity Project

There are two ways to prepare your Unity Project to receive signals from the Micro:Bit. After adding the package, continue with Configuration below.

### Variant 1: Start with this project

If you have not yet started with your own project, simply download this project from Github by pressing the green "Code" button and downloading the .zip File. Unpack it to the location of your choice. Then, add the project to Unity Hub by choosing Add Project and selecting the project folder you just created.

### Variant 2: Add the Unity Package to your 

Choose this version if you have already started a Unity Project. Simply install add the Unity Package 

1) Download the file MicroBit-Unity-SerialInterface.unitypackage from [0-Packaged Versions](https://github.com/sutee9/MicroBit-Unity-SerialInterface/blob/master/0-PackagedVersions/)
2) In your Unity Project, import the package using the menu *Assets > Import Package > Custom Package*.
3) Select the file you just downloaded, and import everything in the package. You will most likely see errors in the console, ignore them for now, they will disappear with the next steps.
4) Open the menu *Edit > Project Settings*. In the *Project Settings*, choose Player > Other Settings. Set the *Api Compatibility Level* to **.NET Framework**
5) The project expects TextMeshPro to be installed. If it isn't already in your project, install it using *Window > Package Manager* and then searching for *TextMesh Pro* in the Unity Registry. Install the package.

### Configuration

Before Micro:Bit-Unity Serial Interface can receive signals from the Micro:Bit you must configure it.

1) Connect the Micro:Bit to your computer using a USB-Cable.
2) Determine the serial port.

   **Mac:**
   Open the Terminal. In the Terminal, type
   ```
   ls /dev/cu.*
   ```

   There should be a device with *usbmodem* in its name, such as */dev/cu.usbmodem102*. If you have several such devices, unplug other USB devices until you determine which one of those is the Micro:Bit. Copy its name.

    **Windows**
    Currently untested. However, the name should be COM1, COM2, etc.

3)  In Unity, add the Prefab *MicroBitInput* which is located in *Assets/MicroBitInput/Prefabs* to your scene.
4)  On the GameObject you just added, you will find the SerialReader component. Paste the name you previously determined into the field *Port Name*.
5)  Test the connection: Check the "Log" property and press Play on your project. You should be seeing 

**Troubleshooting:**
