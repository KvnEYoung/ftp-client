using System;
using Renci.SshNet;

namespace ftp_client
{
  class Program
  {
    static string ReadPassword()
    {
      var password = "";
      ConsoleKeyInfo keyInfo;

      do
      {
        keyInfo = Console.ReadKey(true);

        if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter)
        {
          password += keyInfo.KeyChar;
          Console.Write("*");
        }
        else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
        {
          password = password.Substring(0, (password.Length - 1));
          Console.Write("\b \b");
        }
      } while (keyInfo.Key != ConsoleKey.Enter);

      return password;
    }

    static void Main(string[] args)
    {
      Console.Write("host: ");
      var host = Console.ReadLine();

      Console.Write("username: ");
      var username = Console.ReadLine();

      Console.Write("Password: ");
      var password = ReadPassword();

      using (var client = new SftpClient(host, 22, username, password))
      {
        client.Connect();

        foreach (var file in client.ListDirectory("."))
        {
          Console.WriteLine(file.FullName);
        }
      }
    }
  }
}


