# Welcome to my Portfolio!
> Please keep in mind that this portfolio is slightly outdated (2024), and very game dev focused.\
> These are mostly my hobby projects and what i'm allowed to share, since my full-stack projects are mostly under NDA.

<p align="center">
This GitHub page is specifically made with the intent to showcase my coding skills to the outside world.<br/>
Here you can find hobby projects i've worked on over the years that I am allowed to share!<br/>
This repo contains code written in C#, JavaScript and C++, .zip files with various game builds, images, videos, sound files, etc.
</p>

## About me
![Portfolio banner](https://user-images.githubusercontent.com/49310996/177117500-a005a8b1-4ced-45f1-8cfc-9e62ef4f327d.png)
<p align="center">
<b>My name is Jonas, I'm a Full-Stack Developer, scrum master & industry game programmer near Frankfurt.</b><br/>
Around 2011 I was fascinated by operating systems, and using <a href="https://www.gocosmos.org/">COSMOS</a> I wrote my own "OS" in C# at age 12.<br/>
When I got to know Unity Engine at Version 3 in 2014, I was enamoured by 3D Game & Software Development.<br/>
I successfully completed a certificate at a private school in Frankfurt and worked for various companies as a Full-Stack Developer.<br/>
By now i create cross-platform applications in .NET, Angular and Vue, including 3D Software for Businesses and Consumers in Unity.<br/>
Next to Game Dev, I'm a hobby artist and frequently use tools like Blender, Clip Studio, Premiere Pro & After Effects.<br/>
<b>Colleagues within the industry consider me a reliable partner in software development. Will you be my next partner?</b><br/>
</p>

# Highlighted & Recent Game Projects

## Parking World: Build & Manage
Parking World is a game made by [Binary Impact GmbH](https://binaryimpact.de) 
published by [Aerosoft GmbH](https://aerosoft.com/) and is available on [Steam](https://store.steampowered.com/app/2551570/Parking_World_Build__Manage/).\
As a Junior Unity Developer I was tasked to design and maintain features for this game.\
Some of the notable features are:
- Title screen video autoplay
- Player onboarding mechanics
- Localization
- Various UI & menu layouts
- Various general bug fixes

## Kit Fix-it! (Ongoing project!)
Did you know that we produce **over 50 million tons of e-waste** every single year?\
Corporations make it harder and harder to repair devices, and as a result repair shops are slowly dying out.\
We can't just sit and watch, independent repair shops are a necessity for our future, and this is where Kit Fix-it! comes in.

Kit Fix-it! is a micro-soldering simulator where you fix retro consoles and manage your own repair shop.\
Development for this game started in late 2023 and we're currently working towards a playable demo.\
If you're interested, check out [our update page](https://kit-fix.it/). Unlike my other projects, Kit Fix-it! is made in Godot (C#).

![Kit Fix-it screenshot](https://cdn.bsky.app/img/feed_fullsize/plain/did:plc:wluuk2hgzb5lghureje5c2ii/bafkreids34bldywt7tdlej72re3rza24r5xgu4tnghazizme5bbpfrrony@jpeg)
 
## "Marble Sorting Game"
Included are the source code files for this unique two-week mobile puzzle game project.\
Sort the marbles within the pipes and achieve the high-score, there's multiple modes such as endless and adventure.\
This is basically a match-3 game, and the core mechanic adds more time pressure as the difficulty advances.\
[The repository for this game can be found here](https://github.com/sidpoke/ball-puzzle-game).

![Marble Screenshot](https://i.imgur.com/y2FShHa.png)

## Rabbeaux Knight
**Multiplayer arena Bomberman but with grappling hooks and boxing gloves.**\
Every asset of this game (except for the music, SFX & VFX) was created by myself **in the span of four weeks**.\
You can play it online with your friends, it has a lobby & matchmaking system and the gameplay is incredibly fun.\
So far this is one of my favorite projects, [make sure to check it out!](https://sidpoke.itch.io/RabbeauxKnight)

![Rabbeaux Knight screenshot](https://img.itch.zone/aW1hZ2UvMTQwMDc5Ni84MTYzNDg4LnBuZw==/original/%2BWqjYt.png)

# C# Related
## Stateless State Machinery
Finite State Machines are often a hassle to write manually and a generic implementation is oftentimes helpful.\
This included FSM class allows you to configure states and their triggers with no specific state behavior.\
The usage could look like this:
```csharp
// Definition of states
public enum States 
{
    CoolState,
    OtherCoolState
};

// Definition of transitional triggers
public enum Triggers
{
    CoolTrigger,
    IgnoredTrigger
};

// Define a fsm (Finite State Machine) with the States and Triggers, and CoolState as the initial state.
StateMachine fsm = new StateMachine<States, Triggers>(CoolState);

fsm.Configure(States.CoolState)
    .Permit(Triggers.CoolTrigger, States.OtherCoolState) // Transitional trigger to new state
    .Ignore(Triggers.IgnoredTrigger) // Ignored trigger
    .OnEntry(() => 
    {
        // Actions to perform on entry.
    })
    .OnExit(() => 
    {
        // Actions to perform on exit.
    });

// Firing a trigger to cause a transition. The fsm is now in OtherCoolState"
fsm.Fire(Triggers.CoolTrigger);
```

This is particularly inspired by stateless, a C# state machine plugin. 
I wrote this as a study from other projects i've worked on, and this is an independent implementation used by Kit Fix-it!

## Godot C# Service Locator
Service locators, while often seen as a "dreaded" anti-pattern are still very useful for many occasions.\
This implementation is specifically written by me for Godot (C#) for "Kit Fix-it!", since Godot Autoloads are too static in my view.\
Recommended usage is the creation of a scene lifecycle script that registers and unregisters services globally.
```csharp
    // ... Somewhere in this project
    Services.Register<MyCoolSystem>(); // this is now globally available
    
    // ... Somewhere else in this project
    _myCoolSystem = Services.Get<MyCoolSystem>(); // now has a reference to the service
```

# Node shenanigans
## Yippie - an Electron.js app that shoots confetti as you type
This is one of my silly projects written in JavaScript & Node.js\
Yippie runs in the background and shoots confetti when you type or click anywhere.\
[The repo can be found here](https://github.com/sidpoke/yippie)

# GamesAcademy Projects
## C++ Direct X Renderer
Task of this assignment was making small Direct X renderer/engine in C++ and drawing a cube with a texture.\
I went past the expectations and delivered a renderer with a Mesh (e.g. the famous Utah Teapot).
The object spins in a basic diffuse light. **To run this program, open the "Build" folder and run "DX_Build.exe" or the solution in the "Source" folder.**

![A textured Utah Teapot](https://user-images.githubusercontent.com/49310996/177090819-0d3915ff-e905-40c1-adcd-717d2cb2c1c4.png)

## C++ Container, Dynamic Array
A simple self-written C++ dynamic array.

- main.cpp - Has test functions for this dynamic array
- DynamicArray.h - Header file for all basic array functions
- DynamicArray.inl - In-Line file which defines the basic operations for the dynamic array

## Server Sabotage
A 2D side scroller where you play some buff guy storming a cyberpunk company building that sells user data.\
Inspired by the Amiga game "Persian Gulf Inferno". I was the lead programmer for this.\
[Here's a trailer.](https://www.youtube.com/watch?v=_OpG-AzkmUY)

![Screenshot Server Sabotage](https://user-images.githubusercontent.com/49310996/177113294-f8f62632-9faf-4cdc-aa67-e25f4eacfa29.png)

## B.O.D.Y.
A 3D third-person puzzle platforming game made in Unity, similar to Portal by Valve. I took care of the character controller and interactions.

![Screenshot B.O.D.Y.](https://user-images.githubusercontent.com/49310996/177113065-ac0f4c3a-9af0-48ad-9d31-c2a424904be2.png)

## Rise of the Beetles
This game was made in GamesAcademy during the pandemic. Unfortunately we had multiple communication issues which led to time restraints.
I took care of the programming, however game design and art direction turned this game pretty much into a flop.

![Screenshot Rise of the Beetles](https://user-images.githubusercontent.com/49310996/177113007-869c5912-31b2-4f36-964f-a33d67073819.png)

# WPF + EntityFramework + Repository Pattern (2021)
This project is part of an assignment for GamesAcademy which required me to send over data through WPF from a client to a server.  
The server then proceeded to store the data on an SQL-Database, which uses procedures to call back certain values from events.
Screenshots

![WPF + EF + Repository Pattern Screenshot](https://user-images.githubusercontent.com/49310996/177123500-3d7dd7f6-570a-47e1-9d35-4ff8c70014c0.png)

# Before Games Academy
## Game: Sounds of War (2016-2018)
One of the major projects I've worked on is the Unity game "Sounds of War"\
[Here's a YouTube channel with game showcases.](https://www.youtube.com/channel/UCAPnQokKL_L2rZyfpjCzTBg/videos)\
The project had was abandoned due to personal time restraints and a general lack of budget.\
![Screenshot Sounds of War](https://user-images.githubusercontent.com/49310996/177116697-f58e3c9e-1386-4962-8cb9-6c53e0d40969.png)

## Game: Ke|ros (2016-2017)
I often just called it my "Ninja Cat Game". It was one of my older passion projects that got me into GamesAcademy.\
The soundtrack is one of my favorite works and one of my first real games made with Unity.\
Every asset was also made by myself, but I've lost most of the source code.\
![KeIros Screenshot](https://user-images.githubusercontent.com/49310996/177118341-804159e9-a257-4e6b-9016-2ea841869400.gif)

# Thank You!
I hope you enjoyed my portfolio!
**~ Jonas**

```
 ▄▀▀▀▀▀▀▀▀▄
 █        █
▄██████████▄      ▄
█░░▀░░▀░░░░░▀▄▄  █░█
█░▄░█▀░▄░░░░░░░▀▀░░█
█░░▀▀▀▀░░░░░░░░░░░░█
█░░░░░░░░░░░░░░░░░░█
█░░░░░░░░░░░░░░░░░░█
 █░░▄▄░░▄▄▄▄░░▄▄░░█  Do not fret-
 █░▄▀█░▄▀░░█░▄▀█░▄▀  I am but a dog made of mere butter.
  ▀   ▀     ▀   ▀    Have a blessed day, dear stranger.
```
