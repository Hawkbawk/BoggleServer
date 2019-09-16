# A Server for Playing Boggle
## Objective
While the primary objective of this project was for myself and my fellow students to learn how to design, manage, and expand an ever growing codebase, we were also able to learn a great deal about RESTful API's, in addition to learning how to transmit data between machines using HTTP and JSON.
## Running Locally
The easiest way to run any C# program is through Microsoft's C#/C++ IDE, Visual Studio. That is what this application was written in, and that is where the most success will be had. The link below leads to Microsoft's website, where you can download their free version of Visual Studio, Visual Studio Community. Make sure you download the C# Development Environment when configuring your download in the Visual Studio Installer.

`https://visualstudio.microsoft.com/vs/community/`

You'll also need git installed locally on your machine in order to clone this repo. The link below leads to their website. Any version should do.

`https://git-scm.com/downloads`

After installing Visual Studio, run the following commands to clone the repo locally on your machine. The project should be cloned into a folder located in your current working directory called "BoggleServer". However, if you don't like the command line, you can also follow Visual Studio's built-in repo cloner and clone it that way.

`git clone https://github.com/Hawkbawk/BoggleServer`

Then, simply open up Visual Studio, navigate to wherever you cloned the project, open it up and voila! You should be able to load up the solution and see all of the projects and files. To actually startup the server, just make sure Visual Studio is running the BoggleService project, then hit the run button. This will start the server on http://localhost:60000 using IIS Express, to which any client can then connect. 

## About
This BoggleServer project was written by myself, Ryan Hawkins, and a fellow student at the University of Utah, Curtis Lin. The original application was written during the spring of 2019, in the CS 3500 Software Practice class at the University of Utah, taught by Joe Zachary. Some of the code in this repo was written by him, and accordingly has a header crediting him. This application was built over the course of 3 to 4 weeks, with each week building the codebase. Eventually, we had created a server that followed the API outlined at http://ice.eng.utah.edu/api.html. Any client that follows the API listed there should be able to communicate with this server.

