using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lumin
{
    public static class ccommands
    {
        static public void Parsccom(string command,string file,string inputFilePath,int last) 
        {
            if (command.TrimStart().StartsWith("entrypoint"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                File.AppendAllText(file, "\n" + "include 'fasm/include/win32a.inc' \nentry " + parts[1]);
            }
            if (command.TrimStart().StartsWith("section")) 
            {
                File.AppendAllText(file, "\n" + command.TrimStart());
            }
            if (command.TrimStart().StartsWith("cexit"))
            {
                File.AppendAllText(file, "\n" + "invoke ExitProcess, 0 ");
            }

            if (command.TrimStart().StartsWith("cprint") && !command.TrimStart().StartsWith("cprintl") && !command.TrimStart().StartsWith("cprintarray"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                File.AppendAllText(file, "\n" + $"\ninvoke printf,{parts[1]}");
            }
            if (command.TrimStart().StartsWith("cprintl") && !command.TrimStart().StartsWith("cprintarray"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                File.AppendAllText(file, "\n" + "invoke printf, " + parts[1] + ",0Ah  ");
            }
            if (command.TrimStart().StartsWith("cwaitforkeyexit"))
            {
                File.AppendAllText(file, "\n" + " invoke getch\r\n  \r\n  invoke ExitProcess, 0        ");
            }
            if (command.TrimStart().StartsWith("csetencoding"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                File.AppendAllText(file, "\n" + $" invoke SetConsoleOutputCP,{parts[1]}");
            }
            if (command.TrimStart().StartsWith("cstrcmp"))
            {
                
                    string command21 = command.Replace("cstrcmp ", "");
                    string isp1 = null;
                    string pattern = @"\d+";
                    string[] h = command21.Split("then");
                    string[] partsw = h;
                    string[] nums = partsw[0].Split(',', 2);
                    string[] nums2 = nums[1].Split(',', 2);
                    string[] nums3 = nums2[1].Split(',', 2);
                string[] nums4 = nums3[1].Split(',', 2);
                bool was1 = false;
                    string[] lines = File.ReadAllLines(inputFilePath);//////////////////////
                    string a = null;
                    if (char.IsLetter(nums3[0][0])) { File.AppendAllText(file, $"\n mov esi, [{nums[0]}]\r\n    lea edi, [{nums3[0]}]\r\n    mov ecx, [{nums4[0]}]    \r\n    rep movsb                    \r\n\r\n    invoke lstrcmpA, [{nums2[0]}], [{nums2[1]}] \r\n    cmp eax, 0\r\n    je {h[1]} "); }
                    else
                    {
                    File.AppendAllText(file, $"\n mov esi, [{nums[0]}]\r\n    lea edi, [{nums3[0]}]\r\n    mov ecx, {nums4[0]}                    \r\n    rep movsb                    \r\n\r\n    invoke lstrcmpA, [{nums2[0]}], [{nums2[1]}] \r\n    cmp eax, 0\r\n    je {h[1]}\n lea edi, [{nums3[0]}]  \n  mov ecx, [{nums4[0]}]                  \n    rep movsb                  \n     invoke lstrcmpA, [{nums2[0]}], [{nums2[1]}]  \ncmp eax, 0 je {h[1]} ");
                }
                    last++;
               
            }
            if (command.TrimStart().StartsWith("csystem"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                string pattern = @"\d+";
                bool containsNumbers = false;
                try
                {
                    containsNumbers = Regex.IsMatch(parts[1], pattern);
                }
                catch { }
                File.AppendAllText(file, "\n" + $"invoke system,{parts[1]}  ");
            }
            if (command.TrimStart().StartsWith("csleep"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                string pattern = @"\d+";
                bool containsNumbers = false;
                try
                {
                    containsNumbers = Regex.IsMatch(parts[1], pattern);
                }
                catch { }
                if (char.IsLetter(parts[1][0]) && parts[1].Contains("'") == false)
                {
                    File.AppendAllText(file, "\n" + $"  invoke Sleep,[{parts[1]}]   ");
                }
                else { File.AppendAllText(file, "\n" + $"invoke Sleep,{parts[1]} "); }
            }
        }
    }
}
