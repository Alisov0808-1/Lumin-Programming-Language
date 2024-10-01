namespace Lumin
{
    public static class array
    {
        static public void ParsArray(string command, string file, int last)
        {
            if (command.TrimStart().StartsWith("arrayadd"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                string[] t = parts[1].Split(new char[] { ',' }, 2, StringSplitOptions.RemoveEmptyEntries);
                string[] t2 = t[1].Split(new char[] { ',' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (char.IsLetter(t[0].Trim()[0]) && char.IsLetter(t2[0].Trim()[0]))
                {
                    File.AppendAllText(file, "\n" + $"   lea ebx, [{t[0]} + {t2[0]}*{t2[1]}]\r\n  mov ecx, [{t2[0]}]\r\n  mov [ebx], ecx          ");
                    last++;
                }
                else if (char.IsLetter(t[0].Trim()[0]) && !char.IsLetter(t2[0].Trim()[0]))
                {
                    File.AppendAllText(file, "\n" + $"  lea ebx, [{t[0]} + {t2[0]}*{t2[1]}]\r\n  mov ecx, {t2[0]}\r\n  mov [ebx], ecx          ");
                    last++;
                }
            }
            else if (command.TrimStart().StartsWith("arraydelete"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2);
                string[] t = parts[1].Split(new char[] { ',' }, 2, StringSplitOptions.RemoveEmptyEntries);
                string[] t2 = t[1].Split(new char[] { ',' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (char.IsLetter(t[0].Trim()[0]) && char.IsLetter(t[1].Trim()[0]))
                {
                    File.AppendAllText(file, "\n" + $"  \nmov  [{t[0]} + {t2[0]}*{t2[1]}],0          ");
                    last++;
                }
                else if (char.IsLetter(t[0].Trim()[0]) && !char.IsLetter(t[1].Trim()[0]))
                {
                    File.AppendAllText(file, "\n" + $"mov [{t[0]}+{t2[0]}*{t2[1]}] ");
                    last++;
                }
            }
            if (command.TrimStart().StartsWith("cprintarray") && !command.TrimStart().StartsWith("cprintl"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2);
                string[] t = parts[1].Split(new char[] { ',' }, 2, StringSplitOptions.RemoveEmptyEntries);
                string[] t2 = t[1].Split(new char[] { ',' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (char.IsLetter(t[0].Trim()[0]) && char.IsLetter(t2[1].Trim()[0]))
                {
                    File.AppendAllText(file, $"\nmov eax, [{t2[0]}]\r\n  shl eax, [{t2[1]}]\r\n  add eax,{t[0]}\r\n  push eax\r\n  call [printf]    ");
                }
                else if (char.IsLetter(t[0].Trim()[0]) && !char.IsLetter(t2[1].Trim()[0]))
                {
                    File.AppendAllText(file, $"\nmov eax, [{t2[0]}]\r\n  shl eax, {t2[1]}\r\n  add eax,{t[0]}\r\n  push eax\r\n  call [printf]    ");
                }
            }
        }
    }
}
