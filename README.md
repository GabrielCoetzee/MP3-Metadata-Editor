# MP3_Metadata_Editor
C# - WPF - MVVM Project to modify MP3 metadata such as album art, lyrics, comments, song title etc.

This is a project I made in my free time, in order to apply a few design patterns and technologies that I have learned. While I did keep the SOLID principles in mind while coding, and try to follow them, whether this project is an violation of any of them, I`m not yet sure, but I am never truly done with any of my personal projects. As I improve, and become a better developer, I will come back to each of my projects, and improve upon them, or start over entirely.

This project consists both of a Client and Server.

Technologies Used : 

- C# WPF with .NET Framework 4.6.2
- SQL
- Entity Framework 6
- C# Windows Service
- C# WCF Rest Service
- WPF FontAwesome
- Newtonsoft.JSON

Some notes about the Client : 

- It is a C# WPF project that uses .NET Framework 4.6.2
- I used the MVVM design pattern 
- There are Unit Tests against the ViewModel. I used vanilla Visual Studio unit testing capabilities.
- You can modify an MP3`s metadata, and save it to the MP3, without the Windows Service / Server Code running. This was one of my goals.
- You may also choose custom album art to add to an MP3. This is achievable without any server code running, this was also part of the goal.
- I made use both of the Factory and Singleton patterns. This was in order to allow extensibility as to how MP3 metadata is read. (Open-Closed Principle)

Some notes about the Server : 

- The server code, consists of three solutions : Windows Service, WCF Rest API and a Server library, the latter of which is my Data Access layer.
- The WCF Rest service is hosted within the Windows Service, therefore the Windows Service is the start-up project.
- I mentioned above, you may modify an MP3s metadata without the server code / Windows Service running. When the Windows service is running though, you can download album art for a song through my WCF Rest API. My     WCF Rest API uses LAST FM`s API to get the album art. 
- When you have modified an MP3`s metadata, and you press 'Save Changes' on the client, the client will only save the changes to the MP3 file if the Windows Service is not running, but if the Windows Service is        running, it will also POST the MP3 details to the WCF Rest API, which will add the MP3 details to a SQL database via Entity Framework. 
- The above mentioned database, is currently only used to save MP3 details to. I did this mostly as an Entity Framework exercise, but I may decide to extend it at a later stage, or use the database in another          project.

Please run both solutions in Debug mode. I still have to make an installer to deploy this application. 

Sorry for the extensive reading! I hope you enjoy going through my code. 



