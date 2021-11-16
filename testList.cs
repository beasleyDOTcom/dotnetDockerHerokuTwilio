       using System;
       using System.Collections.Generic;
       Dictionary<string, List<string>> roomCodes = new Dictionary<string, List<string>>();
       string room = "hello";
       roomCodes[room] = new List<string>();
       Console.WriteLine(roomCodes[room]);
       roomCodes[room].Add("1234343434");
       Console.WriteLine(roomCodes[room][0]);

