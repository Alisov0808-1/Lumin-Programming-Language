using Lumin;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
public class Sas
{
    private Dictionary<string, int> registers;

    static string format = null;
    string executionStartAddress = "";
    public string Interpret(string command)
    {
        return command;
        Console.WriteLine("Intepreting finished!");
    }
    static string RemoveBrackets(string input)//////////////////////////////
    {
        return Regex.Replace(input.Trim(), @"\[(.*?)@.*?\]", m => m.Groups[1].Value);
        //string pattern = @"\[(.*?)@?\]";
        //string result = Regex.Replace(input, pattern, "$1");
        //return result;
    }
    static void Main(string[] args)
    {
      
        if (args.Length > 0)
        {
            Main2("0.lum");
        }
        else
        {
            Main2();
        }
    }
    static void Main2(string args = null)
    {
        string input = "mov opo,[ pop@ ]";
        string result = RemoveBrackets(input);
        //string[] p = File.ReadAllLines("Program.cs");

        int lastMatchIndex = 0;
        string text2 = null;
        bool boot = false;
        bool console = false;
        int poz = 0;
        string inputFilePath = "";
        //if (args == null)
        //{
        //    inputFilePath = Console.ReadLine();
        //}
        //else
        //{
            inputFilePath = "0.lum";
      //  }
        string[] commands = File.ReadAllLines(inputFilePath);
        string[] reserv = commands;
        string file = inputFilePath.Replace(".lum", ".asm");
        int mat = 0;
        bool was = false;
        int last = 0;
        int errorcount = 0;
        List<string> func = new List<string>();
        List<string> struc = new List<string>();
        List<string> peremen = new List<string>();
        File.WriteAllText(file, "");
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Console.WriteLine("Интерпретация...");
        for (int i = 0; i < commands.Count(); i++)
        {
            int count = commands[i].Count(f2 => f2 == ';');
            if (count > 1)
            {
                int index2 = commands[i].IndexOf(';');
                commands[i] = commands[i].Remove(index2, 1).Insert(index2, "\n");
                Console.WriteLine();
            }
            else
            {
                commands[i] = commands[i].Replace(";", "");
            }
            int index = commands[i].LastIndexOf('#');
            if (index != -1)
            {
                commands[i] = commands[i].Substring(0, index);
            }
            if (commands[i].TrimStart().StartsWith("else ")) { commands[i] = commands[i].Replace("else ", string.Empty); }
            if (commands[i].TrimStart().StartsWith("struct "))
            {
                var lines = File.ReadAllLines(inputFilePath);
                string isp = null;
                string[] s = commands[i].TrimStart().Split(' ');
                struc.Add(s[1].Replace(" ", "").Trim());
            }
            if (commands[i].TrimStart().StartsWith("func"))
            {
                string command2 = commands[i].Replace("func ", "");
                string isp = null;
                if (command2.Contains("{")) { command2 = command2.Replace("{", ""); }
                int currentPosition = poz;
                string fileContent = File.ReadAllText(file);
                string[] lines2 = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                func.Add(command2.Replace(" ", "").Trim());
            }
        }
        func.Add("cinput");
        foreach (string command in commands)
        {
            if (command.TrimStart().StartsWith("initialize"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (parts[1].Trim() == "console") //мышку еще
                {
                    File.AppendAllText(file, "\n cli\r\n        xor ax,ax\r\n        mov ds,ax\r\n        mov es,ax\r\n        mov ss,ax\r\n        mov sp,07C00h\r\n        sti\r\n     mov ah, 0x00    \r\n    mov al, 0x03   \r\n    int 0x10       \r\n\r\n   \r\n    mov ax, 0x1112\r\n    int 0x10               ");
                }


            }
           else if (command.TrimStart().StartsWith("import"))
            {
                string command2 = command.Replace("import ", ""); command2 = command2.Replace("'", ""); command2 = command2.Replace("'", "");
                string name = command2;
                command2 = File.ReadAllText(command2.TrimStart());
                File.AppendAllText(file, "\n" + command2);
                string[] commands2 = File.ReadAllLines(name.TrimStart());
                for (int i = 0; i < commands2.Length; i++)
                {
                    if (commands2[i].EndsWith(':')) { func.Add(commands2[i].Replace(":", "").Trim()); }
                }
            }

            else if (command.TrimStart().StartsWith("struct"))
            {
                var lines = File.ReadAllLines(inputFilePath);
                string isp = null;
                File.AppendAllText(file, "\n" + command.Replace("struct ".Trim(), "struc ".Trim()));
            }
            else if (command.TrimStart().StartsWith("arrayadd") || command.TrimStart().StartsWith("arraydelete") || command.TrimStart().StartsWith("cprintarray"))
            {
                array.ParsArray(command, file, last);
            }
            else if (command.TrimStart().StartsWith("}") || command.TrimStart().StartsWith("{"))
            {
                int currentPosition = poz;
                var lines = File.ReadAllLines(inputFilePath);
                string fileContent = File.ReadAllText(file);
                string[] lines2 = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                using (StreamWriter outputFile = new StreamWriter(file, true))
                {
                    for (int i = poz - 1; i >= 0; i--)
                    {
                        string line = lines[i].TrimStart();
                        if (line.TrimStart().StartsWith("func"))
                        {
                            if (command.TrimStart().StartsWith("}"))
                            {
                                outputFile.WriteLine("\njmp endOasm");
                                break;
                            }
                            else { break; }
                        }
                        else
                        {
                            if (line.TrimStart().StartsWith("struc"))
                            {
                                if (command.TrimStart().StartsWith("}"))
                                {
                                    outputFile.WriteLine("\n}");
                                    break;
                                }
                                else if (command.TrimStart().StartsWith("{"))
                                {
                                    outputFile.WriteLine("\n{");
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else if (command.TrimStart().StartsWith("func"))
            {
                string command2 = command.Replace("func ", "");
                string isp = null;
                if (command2.Contains("{")) { command2 = command2.Replace("{", ""); }
                File.AppendAllText(file, "\n" + command2 + ":");
                int currentPosition = poz;
                string fileContent = File.ReadAllText(file);
                string[] lines2 = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                File.WriteAllLines(file, lines2);
                func.Add(command2.Replace(" ", "").Trim());
            }
            else if (command.TrimStart().StartsWith("format"))
            {
                string[] k = command.TrimStart().Split(" ");
                if (k[1].Trim() == "winconsole".Trim()) { File.AppendAllText(file, "\nformat PE Console"); console = true; }
                else if (k[1].Trim() == "GUI".Trim())
                {
                }
                else if (k[1].Trim() == "boot".Trim()) { File.AppendAllText(file, "\n cli\r\n        xor ax,ax\r\n        mov ds,ax\r\n        mov es,ax\r\n        mov ss,ax\r\n        mov sp,07C00h\r\n        sti\r\n      "); }
            }
            else if (command.Trim().StartsWith("bbeep") || command.Trim().StartsWith("binput") || command.Trim().StartsWith("bprint") || command.TrimStart().StartsWith("bsetcursize") || command.TrimStart().StartsWith("bsetcurpoz") || command.Trim().StartsWith("bbeep"))
            {
                bcommands.ParsBCommands(command, file, last);
            }
            else if (command.TrimStart().StartsWith("comparestr"))
            {
                string command21 = command.Replace("comparestr ", "");
                string isp1 = null;
                string pattern = @"\d+";
                string[] h = command21.Split("then");
                string[] partsw = h;
                string[] nums = partsw[0].Split(',', 2);
                string[] nums2 = nums[1].Split(',', 2);
                string[] nums3 = nums2[1].Split(',', 2);
                bool was1 = false;
                string[] lines = File.ReadAllLines(inputFilePath);
                string a = null;
                if (char.IsLetter(nums3[0][0])) { File.AppendAllText(file, $"\n   mov si,[{nums[0]}] \r\n    mov di,[{nums2[0]}]\r\n    mov cx,[{nums3[0]}]\r\n   push si\r\n    push di\r\n    push cx\ncycle{last}:\r\n    mov al, [si]\r\n    cmp al, [di]\r\n    jnz {nums3[1]}\r\n    inc si\r\n    inc di\r\n    loop cycle{last}\r\n  pop cx\r\n    pop di\r\n    pop si\n   jmp {h[1]}  "); }
                else
                {
                    File.AppendAllText(file, $"\n   mov si,[{nums[0]}] \r\n    mov di,[{nums2[0]}]\r\n    mov cx,{nums3[0]}\r\n   push si\r\n    push di\r\n    push cx\ncycle{last}:\r\n    mov al, [si]\r\n    cmp al, [di]\r\n    jnz {nums3[1]}\r\n    inc si\r\n    inc di\r\n    loop cycle{last}\r\n  pop cx\r\n    pop di\r\n    pop si\n   jmp {h[1]}  ");
                }
                last++;
            }
            else if (command.TrimStart().StartsWith("if"))
            {
                If_and_loop.ParsIf(command, poz, file, inputFilePath, func);
            }
            else if (command.TrimStart().StartsWith("sector") && !command.TrimStart().StartsWith("sector end"))
            {
                string[] parts = command.TrimStart().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    File.AppendAllText(file, "\n" + "org " + parts[1]);
                }
                else
                {
                }
            }
            else if (command.TrimStart().StartsWith("longgoto"))////////////
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                File.AppendAllText(file, $"");
            }


            else if (command.TrimStart().StartsWith("sector end"))
            {
                boot = true;
            }
            else if (command.TrimStart().StartsWith("entrypoint") || command.TrimStart().StartsWith("csetencoding") || command.TrimStart().StartsWith("cstrcmp") || command.TrimStart().StartsWith("section")|| command.TrimStart().StartsWith("csleep") || command.TrimStart().StartsWith("cexit") || command.TrimStart().StartsWith("cprintl") || command.TrimStart().StartsWith("cprint") || command.TrimStart().StartsWith("cwaitforkeyexit") || command.TrimStart().StartsWith("csystem"))
            {
                ccommands.Parsccom(command, file,inputFilePath,last);
            }
            else if (command.TrimStart().StartsWith("dword") ||command.TrimStart().StartsWith("word") ||  command.TrimStart().StartsWith("tword") || command.TrimStart().StartsWith("qword") || command.TrimStart().StartsWith("byte")) 
            {
                types.ParsTypes(command,file,peremen);
            }
            else if (command.TrimStart().StartsWith("reserve"))
            {
                string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                string[] t = parts[1].Split(new char[] { ',' }, 2, StringSplitOptions.RemoveEmptyEntries);
                string pattern = @"\d+";
                string parsed = null;
                if (t[0].Contains("byte")) { parsed = "db 0"; }
                if (t[0].Contains("word")) { parsed = "dw 0"; }
                if (t[0].Contains("dword")) { parsed = "dd 0"; }
                if (t[0].Contains("qword")) { parsed = "dq 0"; }
                if (t[0].Contains("tword")) { parsed = "dt 0"; }
                parsed = $"\ntimes {t[1]} " + parsed;
                File.AppendAllText(file, parsed);
            }

            else if (command.TrimStart().StartsWith("save"))
            {
                string[] prt = command.TrimStart().Split(" ");
                bool a = false;
                for (int i = 0; i < peremen.Count; i++)
                {
                    if (prt[1].Replace(" ", "") == peremen[i].Replace(" ", ""))
                    {
                        File.AppendAllText(file, $"\npush [{prt[1]}]");
                        a = true;
                        break;
                    }
                }
                if (a == false)
                {
                    File.AppendAllText(file, $"\npush {prt[1]}");
                }
            }
            else if (command.TrimStart().StartsWith("restore"))
            {
                string[] prt = command.TrimStart().Split(" ");
                bool a = false;
                for (int i = 0; i < peremen.Count; i++)
                {
                    if (prt[1].Replace(" ", "") == peremen[i].Replace(" ", ""))
                    {
                        File.AppendAllText(file, $"\npop [{prt[1]}]");
                        a = true;
                        break;
                    }
                }
                if (a == false)
                {
                    File.AppendAllText(file, $"\npush {prt[1]}");
                }
            }
            else if (command.TrimStart().StartsWith("ret"))
            {
                string pattern = @"\d+";
                if (command.Trim() == "ret")
                {
                    File.AppendAllText(file, "\n" + $"ret");
                }
                else
                {
                    string[] prt = command.Split(" ", 2, StringSplitOptions.RemoveEmptyEntries);
                    bool containsNumbers = false;
                    try
                    {
                        containsNumbers = Regex.IsMatch(prt[1], pattern);
                    }
                    catch { }
                    if (!containsNumbers && prt[1].Contains("'") == false && prt[1].TrimStart().StartsWith("rand") == false)
                    {
                        File.AppendAllText(file, "\n" + $"mov eax,[{prt[1]}]");
                    }
                    else
                    {
                        for (int ia = 0; ia < func.Count; ia++)
                        {
                            string com = command.Replace(" ", null);
                            if (prt[1].StartsWith(func[ia]))
                            {
                                File.AppendAllText(file, "\n" + $"call {prt[1]}");
                                break;
                            }
                            else { File.AppendAllText(file, "\n" + $"mov eax,{prt[1]}"); break; }
                        }
                    }
                }
            }
            else if (command.TrimStart().StartsWith("stop"))
            {
                File.AppendAllText(file, "\n" + $"jmp endOasm");
            }
          else  if (command.TrimStart().StartsWith("loop"))
            {
                If_and_loop.ParsLoop(command,poz,file,inputFilePath,func);
            }

            else if (command.TrimStart().StartsWith("*asms"))
            {
                string text = File.ReadAllText(inputFilePath);
                string pattern = @"\*asms(.*?)\*asmn";
                RegexOptions options = RegexOptions.Singleline | RegexOptions.Multiline;
                MatchCollection matches = Regex.Matches(text, pattern, options);
                if (lastMatchIndex < matches.Count)
                {
                    Match match = matches[lastMatchIndex];
                    string extractedText = match.Groups[1].Value;
                    File.AppendAllText(file, "\n" + extractedText);
                    Console.WriteLine(extractedText);
                    lastMatchIndex++;
                }
                else
                {
                    Console.WriteLine("Все совпадения были обработаны.");
                }
            }
            else if (!command.TrimStart().StartsWith("*")&& !command.TrimStart().StartsWith("bprint")&& !command.TrimStart().StartsWith("cprint"))
            {
                bool ch = Parser.Pars(command, file, func);
                if (!ch)
                {
                    string[] srtc = command.TrimStart().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < struc.Count; i++)
                    {
                        if (srtc.Length > 1 && srtc[0].TrimStart().StartsWith(struc[i].TrimStart()))
                        {
                            File.AppendAllText(file, "\n" + srtc[1] + " " + srtc[0]);
                            break;
                        }
                    }
                    for (int ia = 0; ia < func.Count; ia++)
                    {
                        string com = command.Replace(" ", null);
                        if (com.TrimStart().StartsWith(func[ia]))
                        {
                            File.AppendAllText(file, "\n" + "call " + func[ia]);
                            break;
                        }
                        else if (com.Contains(func[ia]) && com.TrimStart().StartsWith("goto"))
                        {
                            ;
                            File.AppendAllText(file, "\n" + "jmp " + func[ia]);
                            break;
                        }
                        else
                        {
                        }
                    }
                    string pattern = @"\d+";
                }
            }
            poz++;
          

        }
        if (console)
        {
            File.AppendAllText(file, "\n" + $"section '.idata' data import readable ;\r\n        library kernel, 'kernel32.dll',\\\r\n                msvcrt, 'msvcrt.dll'\r\n  \r\n  import kernel,\\\r\n                           " +
            $"      GetStdHandle, 'GetStdHandle',\\\r\n     SetConsoleOutputCP, 'SetConsoleOutputCP',\\\n   lstrcmpA, 'lstrcmpA',\\\n   FillConsoleOutputCharacter, 'FillConsoleOutputCharacterA',\\\r\n       FillConsoleOutputAttribute, 'FillConsoleOutputAttribute',\\\r\n       SetConsoleCursorPosition, 'SetConsoleCursorPosition',\\\r\n " +
            $"      ExitProcess, 'ExitProcess',\\\r\n SetConsoleTextAttribute, 'SetConsoleTextAttribute',\\\nSleep, 'Sleep' \r\n          \r\n  import msvcrt,\\\r\n                                printf, 'printf',\\\r\n        scanf, 'scanf',\\\r\n          getch, '_getch',\\\r\n   system, 'system'          ");
        }
        File.AppendAllText(file, "\nendOasm:\njmp $");
        if (boot) { File.AppendAllText(file, "\n" + $"times(512-2-($-07C00h)) db 0\r\ndb 055h,0AAh "); }
        string filePath = file;
        string[] lines4 = File.ReadAllLines(filePath);
        bool foundClosingBrace = false;
        bool foundClosingBrace2 = false;
        for (int i = 0; i < lines4.Count() - 1; i++)
        {
          
            lines4[i] = RemoveBrackets(lines4[i]);
            if (lines4[i].TrimStart().StartsWith("struc"))
            {
                foundClosingBrace = true;
            }
            if (foundClosingBrace && lines4[i].Contains(" d"))
            {
                string j = '.' + lines4[i];
                lines4[i] = j;
            }
            if (lines4[i].TrimStart().StartsWith("}"))
            {
                foundClosingBrace = false;
            }
        }
        File.WriteAllLines(filePath, lines4);
        string[] lines5 = File.ReadAllLines(filePath);
        for (int i = 0; i < File.ReadAllLines(filePath).Count()-1; i++)
        {
            lines5[i] = RemovePd(lines5[i].Trim());
           

        }
        File.WriteAllLines(filePath, lines5);


        stopwatch.Stop();
        if (errorcount != 0) { Console.WriteLine("Ошибки:"); }
        Console.WriteLine($"Ошибок при интерпретации: {errorcount}");
        Console.WriteLine("Интерпретация завершена: " + stopwatch.ElapsedMilliseconds + " мс\nКомпиляция...");
        Process process = new Process();
        process.StartInfo.FileName = "fasm/fasm.exe";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.Arguments = file;
        process.Start();
        process.WaitForExit();
        Console.WriteLine("Компиляция завершена!");
        Console.ReadKey();
        Environment.Exit(0);
    }
 
    static string RemovePd(string line)
    {
        bool inSingleQuote = false;
        bool inDoubleQuote = false;

     
        MatchCollection matches = Regex.Matches(line, @"\((.*?)\)");

        foreach (Match match in matches)
        {
            string content = match.Groups[1].Value.Trim();
            int startIndex = match.Index;

         
            for (int j = 0; j < startIndex; j++)
            {
                char c = line[j];
                if (c == '\"') inDoubleQuote = !inDoubleQuote;
                if (c == '\'') inSingleQuote = !inSingleQuote;
            }


            if (!inDoubleQuote && !inSingleQuote)
            {
                if (content.Trim().Equals("dword", StringComparison.OrdinalIgnoreCase))
                {
                    line = line.Remove(match.Index, match.Length);
                    line = line.Insert(match.Index, "dword["); 
                   // line = line.Trim().Replace(" ", "");

                    line = line.Trim().Replace(" ", "").Replace("[dword[", "dword[");
                    line = line.Insert(3, " ");
                }
                if (content.Trim().Equals("word", StringComparison.OrdinalIgnoreCase))
                {
                    line = line.Remove(match.Index, match.Length);
                    line = line.Insert(match.Index, "word["); 
                   

                    line = line.Trim().Replace(" ","").Replace("[word[", "word[");
                    line = line.Insert(3, " ");
                }
                if (content.Trim().Equals("tword", StringComparison.OrdinalIgnoreCase))
                {
                    line = line.Remove(match.Index, match.Length);
                    line = line.Insert(match.Index, "tword[");
                    line = line.Trim().Replace(" ", "");

                    line = line.Trim().Replace("[tword[", "tword[");
                    line = line.Insert(4, " ");
                    line = line.Insert(3, " ");
                }
                if (content.Trim().Equals("qword", StringComparison.OrdinalIgnoreCase))
                {
                    line = line.Remove(match.Index, match.Length);
                    line = line.Insert(match.Index, "qword[");
                    line = line.Trim().Replace(" ", "");

                    line = line.Trim().Replace("[qword[", "qword[");
                    line = line.Insert(3, " ");
                }
                if (content.Trim().Equals("byte", StringComparison.OrdinalIgnoreCase))
                {
                    line = line.Remove(match.Index, match.Length);
                    line = line.Insert(match.Index, "byte["); line = line.Trim().Replace(" ", "");

                    line = line.Trim().Replace("[byte[", "byte[");
                    line = line.Insert(3, " ");
                }
            }
        }

        return line;
    }
   
    
}