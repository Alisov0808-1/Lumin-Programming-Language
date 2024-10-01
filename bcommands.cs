using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lumin
{
    static public class bcommands
    {
        static public void ParsBCommands(string command, string file,int last) 
        {
             if (command.Trim().StartsWith("bbeep")) 
            {
                File.AppendAllText(file, "\n mov ah, 0x0E \r\n    mov al, 0x07\r\n    int 0x10 ");
            }
            else if (command.TrimStart().StartsWith("bsetcurpoz"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                string[] t = parts[1].Split(new char[] { ',' }, 2, StringSplitOptions.RemoveEmptyEntries);
                string pattern = @"\d+";
                bool containsNumbers3 = Regex.IsMatch(t[1], pattern);
                bool containsNumbers2 = Regex.IsMatch(t[0], pattern);
                if (!char.IsLetter(t[0][0]) && !char.IsLetter(t[1][0]))
                {
                    File.AppendAllText(file, "\n" + $"  mov ah, 0x02\r\nmov bh, 0x00\r\nmov al,{t[0]}\nmov dh,al \r\nmov al,{t[1]}\nmov dl,al\r\nint 0x10");
                }
                else if (char.IsLetter(t[0][0]) && char.IsLetter(t[1][0]))
                {
                    File.AppendAllText(file, "\n" + $"  mov ah, 0x02\r\nmov bh, 0x00\r\nmov al, byte [{t[0]}]\nmov dh,al \r\nmov al, byte[{t[1]}]\nmov dl,al\r\nint 0x10");
                }
                else if (!char.IsLetter(t[0][0]) && char.IsLetter(t[1][0]))
                {
                    File.AppendAllText(file, "\n" + $" mov ah, 0x02\r\nmov bh, 0x00\r\nmov al, byte {t[0]}\nmov dh,al \r\nmov al, byte[{t[1]}]\nmov dl,al\r\nint 0x10");
                }
                else if (char.IsLetter(t[0][0]) && !char.IsLetter(t[1][0]))
                {
                    File.AppendAllText(file, "\n" + $"  mov ah, 0x02\r\nmov bh, 0x00\r\nmov al,byte [{t[0]}]\nmov dh,al \r\nmov al, byte{t[1]}\nmov dl,al\r\nint 0x10");
                }
            }
            else if (command.TrimStart().StartsWith("bsetcursize"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                string[] t = parts[1].Split(new char[] { ',' }, 2, StringSplitOptions.RemoveEmptyEntries);
                string pattern = @"\d+";
                bool containsNumbers3 = Regex.IsMatch(t[1], pattern);
                bool containsNumbers2 = Regex.IsMatch(t[0], pattern);
                if (!char.IsLetter(t[0][0]) && !char.IsLetter(t[1][0]))
                {
                    File.AppendAllText(file, "\n" + $" mov ah, 0x01\r\nmov ch, {t[0]}\r\nmov cl, {t[1]}\r\nint 0x10");
                }
                else if (char.IsLetter(t[0][0]) && char.IsLetter(t[1][0]))
                {
                    File.AppendAllText(file, "\n" + $"  mov ah, 0x01\r\nmov ch, [{t[0]}]\r\nmov cl, [{t[1]}]\r\nint 0x10");
                }
                else if (!char.IsLetter(t[0][0]) && char.IsLetter(t[1][0]))
                {
                    File.AppendAllText(file, "\n" + $"  mov ah, 0x01\r\nmov ch, {t[0]}\r\nmov cl, [{t[1]}]\r\nint 0x10");
                }
                else if (char.IsLetter(t[0][0]) && !char.IsLetter(t[1][0]))
                {
                    File.AppendAllText(file, "\n" + $"  mov ah, 0x01\r\nmov ch, [{t[0]}]\r\nmov cl, {t[1]}\r\nint 0x10");
                }
            }
            ////////////////////////////////////////
            else if (command.TrimStart().StartsWith("btextcolor"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                string pattern = @"\d+";
                bool containsNumbers = false;
                try
                {
                    containsNumbers = Regex.IsMatch(parts[1], pattern);
                }
                catch { }
                if (!containsNumbers && parts[1].Contains("'") == false)
                {
                    File.AppendAllText(file, "\n" + $"mov ax, 0x0700  \r\n    mov bh,[{parts[1]}]  \n mov bl,[{parts[1]}]\n mov bl,00[{parts[1]}]  \r\n    mov cx, 0x0000       \n     mov cx, 0x0000 \r\n    mov dx, 0x314F  \r\n    int 0x10    ");
                }
                else { File.AppendAllText(file, "\n" + $"mov ax, 0x0700  \r\n    mov bh,{parts[1]}\n mov bl,{parts[1]}\n mov bl,00{parts[1]}   \r\n     mov cx, 0x0000        \n     mov cx, 0x0000 \r\n    mov dx, 0x314F \r\n    int 0x10  "); }
            }
            else if (command.TrimStart().StartsWith("binput"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                string[] t = parts[1].Split(new char[] { ',' }, 2, StringSplitOptions.RemoveEmptyEntries);
                File.AppendAllText(file, "\n" + $"mov si,0\n Command0{last}: \r\n    mov ah,10h\r\n        int 16h\r\n        cmp ah, 0Eh    \r\n        jz Delete_symbol0{last}\r\n        cmp al, 0Dh\r\n        jz {t[1]}\r\n        mov [{t[0]}+si],al\r\n        inc si\r\n        mov ah,09h\r\n               mov cx,1\r\n        int 10h\r\n   add dl,1\r\n  mov ah,2h\r\n        xor bh,bh\r\n        int 10h \r\n    jmp Command0{last} \nDelete_symbol0{last}:\r\n    cmp dl,0\r\n    jz Command0{last} \r\n    sub dl,1     \r\n    mov ah,2h\r\n        xor bh,bh\r\n        int 10h \r\n    mov al,20h     \r\n    mov [{t[0]} + si],al \r\n    mov ah,09h\r\n            mov cx,1\r\n        int 10h\r\n        dec si       \r\n    jmp Command0{last}        ");
                last++;
                last++;
            }
            else if (command.TrimStart().StartsWith("bprint"))
            {
                bool chec = false;
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (parts[1].Contains("'"))
                {
                    parts[1] = parts[1].Replace("'", "");
                    parts[1] = parts[1].Replace("'", "");

                }
                else if (parts[1].Contains("\""))
                {
                    parts[1] = parts[1].Replace("\"", "\"");
                    parts[1] = parts[1].Replace("\"", "\"");
                }
                else { chec = true; }
                for (int i = 0; i < parts[1].Length; i++)
                {
                    if (chec)
                    {
                        string[] t = parts[1].Split(',', 2);
                        if (char.IsLetter(t[1][0]))
                        {
                            File.AppendAllText(file, "\n" + $" mov bp,[{t[0]}]    \r\n   mov cx,[{t[1]}]\r\n        mov ax,1301h      \r\n        int 10h");
                        }
                        else { File.AppendAllText(file, "\n" + $" mov bp,[{t[0]}]    \r\n   mov cx,{t[1]}\r\n        mov ax,1301h      \r\n        int 10h"); }
                        break;
                    }
                    else
                    {
                        if (parts[1][i] == '\\' && parts[1][i + 1] == 'n') { File.AppendAllText(file, "\n" + $"mov al, 0x0D \r\nint 0x10     \r\nmov al, 0x0A \r\nint 0x10 "); i++; }
                        else
                        {
                            File.AppendAllText(file, "\n" + $"mov ah, 0x0E \r\nmov al, '{parts[1][i]}'  \r\nint 0x10  ");
                        }
                    }
                }
            }
        }
    }
}
