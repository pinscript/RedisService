What?
============
Do you always forget to start the redis server when booting your computer? Are you annoyed by that unnecessary console window
taking up valuable taskbar space?

RedisService creates a Windows Service that wraps redis-server.exe.
This means that redis-server.exe is ran under LOCAL SYSTEM and no console window needs to be open. Redis-server.exe is now also started automatically.

How?
============

1. Clone and build the project.
2. Set the path to your redis-server.exe in App.config.
3. Install the service by invoking `RedisServer.exe install`.
4. Start the service by invoking `net start RedisServer`.

That's it?
============
Yep. Note that this is pretty much thrown together in 15 minutes. I won't guarantee that it's bug-free. Please report any bugs you may find.
