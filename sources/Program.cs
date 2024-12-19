System.Console.WriteLine("Client-Server (0), Client (1), or Offline (2)? .:");
string line = System.Console.ReadLine();
int choise = System.Convert.ToInt32(line);

using var game = new XCraft.Game1(
    choise == 0
);
game.Run();
