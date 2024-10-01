using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumin
{
    static public class types
    {
        static public void ParsTypes(string command,string file,List<string>peremen)
        {
              if (command.TrimStart().StartsWith("dword"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2);
                if (parts.Length > 1 && parts[1].Contains("="))
                {
                    string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                    if (a2.Length > 1)
                    {
                        if (a2[1] == "null") { File.AppendAllText(file, "\n" + $"{a2[0]} dd ?"); }
                        else
                        {
                            File.AppendAllText(file, "\n" + $"{a2[0]} dd {a2[1]}");
                        }
                    }
                    if (a2.Length > 0)
                    {
                        peremen.Add(a2[0].Trim());
                    }
                }
            }
            else if (command.TrimStart().StartsWith("word"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2);
                if (parts.Length > 1 && parts[1].Contains("="))
                {
                    string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                    if (a2.Length > 1)
                    {
                        if (a2[1] == "null") { File.AppendAllText(file, "\n" + $"{a2[0]} dw ?"); }
                        else
                        {
                            File.AppendAllText(file, "\n" + $"{a2[0]} dw {a2[1]}");
                        }
                    }
                    if (a2.Length > 0)
                    {
                        peremen.Add(a2[0].Trim());
                    }
                }
            }
            else if (command.TrimStart().StartsWith("tword"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2);
                if (parts.Length > 1 && parts[1].Contains("="))
                {
                    string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                    if (a2.Length > 1)
                    {
                        if (a2[1] == "null") { File.AppendAllText(file, "\n" + $"{a2[0]} dt ?"); }
                        else
                        {
                            File.AppendAllText(file, "\n" + $"{a2[0]} dt {a2[1]}");
                        }
                    }
                    if (a2.Length > 0)
                    {
                        peremen.Add(a2[0].Trim());
                    }
                }
            }
            else if (command.TrimStart().StartsWith("byte"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1 && parts[1].Contains("="))
                {
                    string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                    if (a2.Length > 1)
                    {
                        if (a2[1].Trim() == "null") { File.AppendAllText(file, "\n" + $"{a2[0]} db ?"); }
                        else
                        {
                            File.AppendAllText(file, "\n" + $"{a2[0]} db {a2[1]}");
                        }
                    }
                    if (a2.Length > 0)
                    {
                        peremen.Add(a2[0].Trim());
                    }
                }
            }
            else if (command.TrimStart().StartsWith("qword"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1 && parts[1].Contains("="))
                {
                    string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                    if (a2.Length > 1)
                    {
                        if (a2[1].Trim() == "null") { File.AppendAllText(file, "\n" + $"{a2[0]} dq ?"); }
                        else
                        {
                            File.AppendAllText(file, "\n" + $"{a2[0]} dq {a2[1]}");
                        }
                    }
                    if (a2.Length > 0)
                    {
                        peremen.Add(a2[0].Trim());
                    }
                }
            }
        }
    }
}
