using System.Text.RegularExpressions;

namespace Lumin
{
    static public class If_and_loop
    {
        static public void ParsIf(string command, int poz, string file, string inputFilePath, List<string> func)
        {
            if (command.TrimStart().StartsWith("if"))
            {
                string command2 = command.TrimStart().Replace("if ", "");
                string isp = null;
                if (command2.Contains("=") || command2.Contains("!") || command2.Contains(">") || command2.Contains("<"))
                {
                    if (command2.Contains(">") && !command2.Contains(">="))
                    {
                        string pattern = @"\d+";
                        string[] h = command2.Split("then", 2, StringSplitOptions.RemoveEmptyEntries);
                        string[] partsw = h;
                        string[] nums = partsw[0].Split(">", 2, StringSplitOptions.RemoveEmptyEntries);
                        bool was1 = false;
                        string[] lines = File.ReadAllLines(inputFilePath);
                        string a = null;
                        for (int i = poz; i >= 0; i--)
                        {
                            foreach (var term in func)
                            {
                                if (lines[i].StartsWith("func ") && lines[i].Contains(term) && was1 == false)
                                {
                                    a = lines[i].Replace("func ", "");
                                    a = a.Replace("{", "");
                                    was1 = true;
                                }
                            }
                        }
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(nums[1], pattern);
                        }
                        catch { }
                        bool containsNumbers9 = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(nums[0], pattern);
                        }
                        catch { }
                        bool sas = false;
                        for (int ia = 0; ia < func.Count; ia++)
                        {
                            string com = command.Replace(" ", null);
                            if (nums[1].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(nums[1].Trim()[0]) && char.IsLetter(nums[0].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp eax,[{nums[0].Replace("loop ", "")}] \njg {h[1]}");
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp eax,{nums[0].Replace("loop ", "")} \njg {h[1]}");
                                }
                                sas = true;
                                break;
                            }
                            if (nums[0].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp [{nums[1].Replace("loop ", "")}],eax \njg {h[1]}");
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp eax,{nums[1].Replace("loop ", "")} \njg {h[1]}");
                                }
                                sas = true;
                                break;
                            }
                        }
                        if (!char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("loop ", "")}, {nums[1]}\njg {h[1]}"); }
                        }
                        else if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("loop ", "")}], [{nums[1]}]\njg {h[1]}"); }
                        }
                        else if (!char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("loop ", "")}, [{nums[1]}]\njg {h[1]}"); }
                        }
                        else if (char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("loop ", "")}], {nums[1]}\njg {h[1]}"); }
                        }
                    }
                    if (command2.Contains(">="))
                    {
                        string pattern = @"\d+";
                        string[] h = command2.Split("then", 2, StringSplitOptions.RemoveEmptyEntries);
                        string[] partsw = h;
                        string[] nums = partsw[0].Split(">=", 2, StringSplitOptions.RemoveEmptyEntries);
                        bool was1 = false;
                        string[] lines = File.ReadAllLines(inputFilePath);
                        string a = null;
                        for (int i = poz; i >= 0; i--)
                        {
                            foreach (var term in func)
                            {
                                if (lines[i].StartsWith("func ") && lines[i].Contains(term) && was1 == false)
                                {
                                    a = lines[i].Replace("func ", "");
                                    a = a.Replace("{", "");
                                    was1 = true;
                                }
                            }
                        }
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(nums[1], pattern);
                        }
                        catch { }
                        bool containsNumbers9 = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(nums[0], pattern);
                        }
                        catch { }
                        bool sas = false;
                        for (int ia = 0; ia < func.Count; ia++)
                        {
                            string com = command.Replace(" ", null);
                            if (nums[1].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(nums[1].Trim()[0]) && char.IsLetter(nums[0].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp [{nums[0].Replace("loop ", "")}],eax \njg {h[1]} \n cmp [{nums[0].Replace("loop ", "")}],eax \nje {h[1]}");
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp eax,{nums[0].Replace("loop ", "")} \njg {h[1]}\n cmp eax,{nums[0].Replace("loop ", "")} \nje {h[1]}");
                                }
                                sas = true;
                                break;
                            }
                            if (nums[0].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp [{nums[1].Replace("loop ", "")}],eax \njg {h[1]}\n cmp [{nums[1].Replace("loop ", "")}],eax \nje {h[1]}");
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[0]} \ncmp eax,{nums[1].Replace("loop ", "")} \njg {h[1]}\n cmp eax,{nums[1].Replace("loop ", "")} \nje {h[1]}");
                                }
                                sas = true;
                                break;
                            }
                        }
                        if (!char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("loop ", "")}, {nums[1]}\njg {h[1]}\ncmp {nums[0].Replace("loop ", "")}, {nums[1]}\nje {h[1]}"); }
                        }
                        else if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("loop ", "")}], [{nums[1]}]\njg {h[1]}\ncmp [{nums[0].Replace("loop ", "")}], [{nums[1]}]\nje {h[1]}"); }
                        }
                        else if (!char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("loop ", "")}, [{nums[1]}]\njg {h[1]}\ncmp {nums[0].Replace("loop ", "")}, [{nums[1]}]\nje {h[1]}"); }
                        }
                        else if (char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("loop ", "")}], {nums[1]}\njg {h[1]}\ncmp [{nums[0].Replace("loop ", "")}], {nums[1]}\nje {h[1]}"); }
                        }
                    }
                    if (command2.Contains("<="))
                    {
                        string pattern = @"\d+";
                        string[] h = command2.Split("then", 2, StringSplitOptions.RemoveEmptyEntries);
                        string[] partsw = h;
                        string[] nums = partsw[0].Split("<=", 2, StringSplitOptions.RemoveEmptyEntries);
                        bool was1 = false;
                        string[] lines = File.ReadAllLines(inputFilePath);
                        string a = null;
                        for (int i = poz; i >= 0; i--)
                        {
                            foreach (var term in func)
                            {
                                if (lines[i].StartsWith("func ") && lines[i].Contains(term) && was1 == false)
                                {
                                    a = lines[i].Replace("func ", "");
                                    a = a.Replace("{", "");
                                    was1 = true;
                                }
                            }
                        }
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(nums[1], pattern);
                        }
                        catch { }
                        bool containsNumbers9 = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(nums[0], pattern);
                        }
                        catch { }
                        bool sas = false;
                        for (int ia = 0; ia < func.Count; ia++)
                        {
                            string com = command.Replace(" ", null);
                            if (nums[1].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(nums[1].Trim()[0]) && char.IsLetter(nums[0].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp [{nums[0].Replace("loop ", "")}],eax \njl {h[1]} \n cmp [{nums[0].Replace("loop ", "")}],eax \nje {h[1]}");
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp eax,{nums[0].Replace("loop ", "")}\njl {h[1]}\n cmp eax,{nums[0].Replace("loop ", "")} \nje {h[1]}");
                                }
                                sas = true;
                                break;
                            }
                            if (nums[0].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp [{nums[1].Replace("loop ", "")}],eax \njl {h[1]}\n cmp [{nums[1].Replace("loop ", "")}],eax \nje {h[1]}");
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp eax,{nums[1].Replace("loop ", "")} \njl {h[1]}\n cmp eax,{nums[1].Replace("loop ", "")} \nje {h[1]}");
                                }
                                sas = true;
                                break;
                            }
                        }
                        if (!char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("loop ", "")}, {nums[1]}\njl {h[1]}\ncmp {nums[0].Replace("loop ", "")}, {nums[1]}\nje {h[1]}"); }
                        }
                        else if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("loop ", "")}], [{nums[1]}]\njl {h[1]}\ncmp [{nums[0].Replace("loop ", "")}], [{nums[1]}]\nje {h[1]}"); }
                        }
                        else if (!char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("loop ", "")}, [{nums[1]}]\njl {h[1]}\ncmp {nums[0].Replace("loop ", "")}, [{nums[1]}]\nje {h[1]}"); }
                        }
                        else if (char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("loop ", "")}], {nums[1]}\njl {h[1]}\ncmp [{nums[0].Replace("loop ", "")}], {nums[1]}\nje {h[1]}"); }
                        }
                    }
                    else if (command2.Contains("<") && !command2.Contains("<="))
                    {
                        string pattern = @"\d+";
                        string[] h = command2.Split("then", 2, StringSplitOptions.RemoveEmptyEntries);
                        string[] partsw = h;
                        string[] nums = partsw[0].Split("<", 2, StringSplitOptions.RemoveEmptyEntries);
                        bool was1 = false;
                        string[] lines = File.ReadAllLines(inputFilePath);
                        string a = null;
                        for (int i = poz; i >= 0; i--)
                        {
                            foreach (var term in func)
                            {
                                if (lines[i].StartsWith("func ") && lines[i].Contains(term) && was1 == false)
                                {
                                    a = lines[i].Replace("func ", "");
                                    a = a.Replace("{", "");
                                    was1 = true;
                                }
                            }
                        }
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(nums[1], pattern);
                        }
                        catch { }
                        bool containsNumbers9 = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(nums[0], pattern);
                        }
                        catch { }
                        bool sas = false;
                        for (int ia = 0; ia < func.Count; ia++)
                        {
                            string com = command.Replace(" ", null);
                            if (nums[1].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(nums[1].Trim()[0]) && char.IsLetter(nums[0][0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp [{nums[0].Replace("loop ", "")}],eax \njl {h[1]}");
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp eax,{nums[0].Replace("loop ", "")} \njl {h[1]}");
                                }
                                sas = true;
                                break;
                            }
                            if (nums[0].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1][0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp [{nums[1].Replace("loop ", "")}],eax \njl {h[1]}");
                                    Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp eax,{nums[1].Replace("loop ", "")} \njl {h[1]}");
                                }
                                sas = true;
                                break;
                            }
                        }
                        if (!char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("loop ", "")}, {nums[1]}\njl {h[1]}"); }
                        }
                        else if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("loop ", "")}], [{nums[1]}]\njl {h[1]}"); }
                        }
                        else if (!char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("loop ", "")}, [{nums[1]}]\njl {h[1]}"); }
                        }
                        else if (char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("loop ", "")}], {nums[1]}\njl {h[1]}"); }
                        }
                    }
                    else if (command2.Contains("==") && !command2.Contains("!"))
                    {
                        string pattern = @"\d+";
                        string[] h = command2.Split("then", 2, StringSplitOptions.RemoveEmptyEntries);
                        string[] partsw = h;
                        string[] nums = partsw[0].Split('=', 2);
                        nums[1] = nums[1].Replace("=", "");
                        bool was1 = false;
                        string[] lines = File.ReadAllLines(inputFilePath);
                        string a = null;
                        for (int i = poz; i >= 0; i--)
                        {
                            foreach (var term in func)
                            {
                                if (lines[i].StartsWith("func ") && lines[i].Contains(term) && was1 == false)
                                {
                                    a = lines[i].Replace("func ", "");
                                    a = a.Replace("{", "");
                                    was1 = true;
                                }
                            }
                        }
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(nums[1], pattern);
                        }
                        catch { }
                        bool containsNumbers9 = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(nums[0], pattern);
                        }
                        catch { }
                        bool sas = false;
                        for (int ia = 0; ia < func.Count; ia++)
                        {
                            string com = command.Replace(" ", null);
                            if (nums[1].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(nums[1].Trim()[0]) && char.IsLetter(nums[0][0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp [{nums[0].Replace("loop ", "")}],eax \nje {h[1]}");
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp eax,{nums[0].Replace("loop ", "")} \nje {h[1]}");
                                }
                                sas = true;
                                break;
                            }
                            if (nums[0].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1][0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp [{nums[1].Replace("loop ", "")}],eax \nje {h[1]}");
                                    Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp eax,{nums[1].Replace("loop ", "")} \nje {h[1]}");
                                }
                                sas = true;
                                break;
                            }
                        }
                        if (!char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("loop ", "")}, {nums[1]}\nje {h[1]}"); }
                        }
                        else if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("loop ", "")}], [{nums[1]}]\nje {h[1]}"); }
                        }
                        else if (!char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("loop ", "")}, [{nums[1]}]\nje {h[1]}"); }
                        }
                        else if (char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("loop ", "")}], {nums[1]}\nje {h[1]}"); }
                        }
                    }
                    else if (command2.Contains("!"))
                    {
                        string pattern = @"\d+";
                        string[] h = command2.Split("then", 2, StringSplitOptions.RemoveEmptyEntries);
                        string[] partsw = h;
                        string[] nums = partsw[0].Split("!=", 2, StringSplitOptions.RemoveEmptyEntries);
                        bool was1 = false;
                        string[] lines = File.ReadAllLines(inputFilePath);
                        string a = null;
                        for (int i = poz; i >= 0; i--)
                        {
                            foreach (var term in func)
                            {
                                if (lines[i].StartsWith("func ") && lines[i].Contains(term) && was1 == false)
                                {
                                    a = lines[i].Replace("func ", "");
                                    a = a.Replace("{", "");
                                    was1 = true;
                                }
                            }
                        }
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(nums[1], pattern);
                        }
                        catch { }
                        bool containsNumbers9 = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(nums[0], pattern);
                        }
                        catch { }
                        bool sas = false;
                        for (int ia = 0; ia < func.Count; ia++)
                        {
                            string com = command.Replace(" ", null);
                            if (nums[1].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(nums[1].Trim()[0]) && char.IsLetter(nums[0][0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp [{nums[0].Replace("loop ", "")}],eax \njne {h[1]}");
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp eax,{nums[0].Replace("loop ", "")} \njne {h[1]}");
                                }
                                sas = true;
                                break;
                            }
                            if (nums[0].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1][0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp [{nums[1].Replace("loop ", "")}],eax \njne {h[1]}");
                                    Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp eax,{nums[1].Replace("loop ", "")} \njne {h[1]}");
                                }
                                sas = true;
                                break;
                            }
                        }
                        if (!char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("loop ", "")}, {nums[1]}\njne {h[1]}"); }
                        }
                        else if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("loop ", "")}], [{nums[1]}]\njne {h[1]}"); }
                        }
                        else if (!char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("loop ", "")}, [{nums[1]}]\njne {h[1]}"); }
                        }
                        else if (char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("loop ", "")}], {nums[1]}\njne {h[1]}"); }
                        }
                    }
                }
            }
        }
        static public void ParsLoop(string command, int poz, string file, string inputFilePath, List<string> func)
        {
            if (command.TrimStart().StartsWith("loop"))
            {
                string[] parts = command.TrimStart().Split(' ');
                if (parts[1].Trim() == "inf")
                {
                    string[] lines = File.ReadAllLines(inputFilePath);
                    for (int i = poz - 1; i >= 0; i--)
                    {
                        foreach (var term in func)
                        {
                            if (lines[i].Trim().StartsWith("func ") && lines[i].Trim().Contains(term))
                            {
                                string a = lines[i].Replace("func ", ""); a = a.Replace("{", "");
                                File.AppendAllText(file, "\n" + $"jmp {a}");
                                break;
                            }
                        }
                    }
                }
                else if (command.Contains("=") || command.Contains("!") || command.Contains(">") || command.Contains("<") || command.Contains(">=") || command.Contains("<="))
                {
                    if (command.Contains(">") && !command.Contains(">="))
                    {
                        string pattern = @"\d+";
                        string[] partsw = command.TrimStart().Replace("loop ", "").Split(">");
                        bool was1 = false;
                        string[] lines = File.ReadAllLines(inputFilePath);
                        string a = null;
                        for (int i = poz; i >= 0; i--)
                        {
                            foreach (var term in func)
                            {
                                if (lines[i].StartsWith("func ") && lines[i].Contains(term) && was1 == false)
                                {
                                    Console.WriteLine($"Найдена строка: {lines[i]}");
                                    a = lines[i].Replace("func ", "");
                                    a = a.Replace("{", "");
                                    was1 = true;
                                }
                            }
                        }
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(partsw[1], pattern);
                        }
                        catch { }
                        bool containsNumbers9 = false;
                        try
                        {
                            containsNumbers9 = Regex.IsMatch(partsw[0], pattern);
                        }
                        catch { }
                        bool sas = false;
                        for (int ia = 0; ia < func.Count(); ia++)
                        {
                            string com = command.Replace(" ", null);
                            if (partsw[1].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[1].Trim()[0]) && char.IsLetter(partsw[0].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp [{partsw[0].Replace("loop ", "")}],eax \njg {a}");
                                    Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp eax,{partsw[0].Replace("loop ", "")} \njg {a}");
                                }
                                sas = true;
                                break;
                            }
                            if (partsw[0].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp [{partsw[1].Replace("loop ", "")}],eax \njg {a}");
                                    Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp eax,{partsw[1].Replace("loop ", "")} \njg {a}");
                                }
                                sas = true;
                                break;
                            }
                        }
                        if (!char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("loop ", "")}, {partsw[1]}\njg {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("loop ", "")}], [{partsw[1]}]\njg {a}"); }
                        }
                        else if (!char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("loop ", "")}, [{partsw[1]}]\njg {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("loop ", "")}], {partsw[1]}\njg {a}"); }
                        }
                    }
                    else if (command.Contains("<") && !command.Contains("<="))
                    {
                        string pattern = @"\d+";
                        string[] partsw = command.TrimStart().Replace("loop ", "").Split("<");
                        bool was1 = false;
                        string[] lines = File.ReadAllLines(inputFilePath);
                        string a = null;
                        for (int i = poz; i >= 0; i--)
                        {
                            foreach (var term in func)
                            {
                                if (lines[i].StartsWith("func ") && lines[i].Contains(term) && was1 == false)
                                {
                                    Console.WriteLine($"Найдена строка: {lines[i]}");
                                    a = lines[i].Replace("func ", "");
                                    a = a.Replace("{", "");
                                    was1 = true;
                                }
                            }
                        }
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(partsw[1], pattern);
                        }
                        catch { }
                        bool containsNumbers9 = false;
                        try
                        {
                            containsNumbers9 = Regex.IsMatch(partsw[0], pattern);
                        }
                        catch { }
                        bool sas = false;
                        for (int ia = 0; ia < func.Count(); ia++)
                        {
                            string com = command.Replace(" ", null);
                            if (partsw[1].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[1].Trim()[0]) && char.IsLetter(partsw[0].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp [{partsw[0].Replace("loop ", "")}],eax \njl {a}");
                                    Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp eax,{partsw[0].Replace("loop ", "")} \njl {a}");
                                }
                                sas = true;
                                break;
                            }
                            if (partsw[0].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp [{partsw[1].Replace("loop ", "")}],eax \njl {a}");
                                    Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp {partsw[1].Replace("loop ", "")},eax \njl {a}");
                                }
                                sas = true;
                                break;
                            }
                        }
                        if (!char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("loop ", "")}, {partsw[1]}\njl {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("loop ", "")}], [{partsw[1]}]\njl {a}"); }
                        }
                        else if (!char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("loop ", "")}, [{partsw[1]}]\njl {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("loop ", "")}], {partsw[1]}\njl {a}"); }
                        }
                    }
                    else if (command.Contains("<="))
                    {
                        string pattern = @"\d+";
                        string[] partsw = command.TrimStart().Replace("loop ", "").Split("<=", 2);
                        bool was1 = false;
                        string[] lines = File.ReadAllLines(inputFilePath);
                        string a = null;
                        for (int i = poz; i >= 0; i--)
                        {
                            foreach (var term in func)
                            {
                                if (lines[i].StartsWith("func ") && lines[i].Contains(term) && was1 == false)
                                {
                                    Console.WriteLine($"Найдена строка: {lines[i]}");
                                    a = lines[i].Replace("func ", "");
                                    a = a.Replace("{", "");
                                    was1 = true;
                                }
                            }
                        }
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(partsw[1], pattern);
                        }
                        catch { }
                        bool containsNumbers9 = false;
                        try
                        {
                            containsNumbers9 = Regex.IsMatch(partsw[0], pattern);
                        }
                        catch { }
                        bool sas = false;
                        for (int ia = 0; ia < func.Count(); ia++)
                        {
                            string com = command.Replace(" ", null);
                            if (partsw[1].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[1].Trim()[0]) && char.IsLetter(partsw[0].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp [{partsw[0].Replace("loop ", "")}],eax \njl {a} \n cmp [{partsw[0].Replace("loop ", "")}],eax \nje {a}");
                                    Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp eax,{partsw[0].Replace("loop ", "")} \njl {a}\n cmp eax,{partsw[0].Replace("loop ", "")} \nje {a}");
                                }
                                sas = true;
                                break;
                            }
                            if (partsw[0].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp [{partsw[1].Replace("loop ", "")}],eax \njl {a} \n cmp [{partsw[1].Replace("loop ", "")}],eax \nje {a}");
                                    Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp eax,{partsw[1].Replace("loop ", "")} \njl {a} \ncmp eax,{partsw[1].Replace("loop ", "")} \nje {a}");
                                }
                                sas = true;
                                break;
                            }
                        }
                        if (!char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("loop ", "")}, {partsw[1]}\njl {a}\ncmp {partsw[0].Replace("loop ", "")}, {partsw[1]}\nje {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("loop ", "")}], [{partsw[1]}]\njl {a}\ncmp [{partsw[0].Replace("loop ", "")}], [{partsw[1]}]\nje {a}"); }
                        }
                        else if (!char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("loop ", "")}, [{partsw[1]}]\njl {a}\ncmp {partsw[0].Replace("loop ", "")},[ {partsw[1]}]\nje {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("loop ", "")}], {partsw[1]}\njl {a}\ncmp [{partsw[0].Replace("loop ", "")}], {partsw[1]}\nje {a}"); }
                        }
                    }
                    else if (command.Contains(">="))
                    {
                        string pattern = @"\d+";
                        string[] partsw = command.TrimStart().Replace("loop ", "").Split("<=", 2);
                        bool was1 = false;
                        string[] lines = File.ReadAllLines(inputFilePath);
                        string a = null;
                        for (int i = poz; i >= 0; i--)
                        {
                            foreach (var term in func)
                            {
                                if (lines[i].StartsWith("func ") && lines[i].Contains(term) && was1 == false)
                                {
                                    Console.WriteLine($"Найдена строка: {lines[i]}");
                                    a = lines[i].Replace("func ", "");
                                    a = a.Replace("{", "");
                                    was1 = true;
                                }
                            }
                        }
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(partsw[1], pattern);
                        }
                        catch { }
                        bool containsNumbers9 = false;
                        try
                        {
                            containsNumbers9 = Regex.IsMatch(partsw[0], pattern);
                        }
                        catch { }
                        bool sas = false;
                        for (int ia = 0; ia < func.Count(); ia++)
                        {
                            string com = command.Replace(" ", null);
                            if (partsw[1].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[1].Trim()[0]) && char.IsLetter(partsw[0].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp [{partsw[0].Replace("loop ", "")}],eax \njg {a} \n cmp [{partsw[0].Replace("loop ", "")}],eax \nje {a}");
                                    Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp eax,{partsw[0].Replace("loop ", "")} \njg {a}\n cmp eax,{partsw[1].Replace("loop ", "")} \nje {a}");
                                }
                                sas = true;
                                break;
                            }
                            if (partsw[0].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp [{partsw[1].Replace("loop ", "")}],eax \njg {a} \n cmp [{partsw[1].Replace("loop ", "")}],eax \nje {a}");
                                    Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \ncmp eax,{partsw[1].Replace("loop ", "")} \njg {a} \n cmp eax,{partsw[1].Replace("loop ", "")} \nje {a}");
                                }
                                sas = true;
                                break;
                            }
                        }
                        if (!char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("loop ", "")}, {partsw[1]}\njg {a}\ncmp {partsw[0].Replace("loop ", "")}, {partsw[1]}\nje {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("loop ", "")}], [{partsw[1]}]\njg {a}\ncmp [{partsw[0].Replace("loop ", "")}], [{partsw[1]}]\nje {a}"); }
                        }
                        else if (!char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("loop ", "")}, [{partsw[1]}]\njg {a}\ncmp {partsw[0].Replace("loop ", "")},[ {partsw[1]}]\nje {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("loop ", "")}], {partsw[1]}\njg {a}\ncmp [{partsw[0].Replace("loop ", "")}], {partsw[1]}\nje {a}"); }
                        }
                    }
                    else if (command.Contains("==") && !command.Contains("!"))
                    {
                        string pattern = @"\d+";
                        string[] partsw = command.TrimStart().Replace("loop ", "").Split("==", 2);
                        bool was1 = false;
                        string[] lines = File.ReadAllLines(inputFilePath);
                        string a = null;
                        for (int i = poz; i >= 0; i--)
                        {
                            foreach (var term in func)
                            {
                                if (lines[i].StartsWith("func ") && lines[i].Contains(term) && was1 == false)
                                {
                                    Console.WriteLine($"Найдена строка: {lines[i]}");
                                    a = lines[i].Replace("func ", "");
                                    a = a.Replace("{", "");
                                    was1 = true;
                                }
                            }
                        }
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(partsw[1], pattern);
                        }
                        catch { }
                        bool containsNumbers9 = false;
                        try
                        {
                            containsNumbers9 = Regex.IsMatch(partsw[0], pattern);
                        }
                        catch { }
                        bool sas = false;
                        for (int ia = 0; ia < func.Count(); ia++)
                        {
                            string com = command.Replace(" ", null);
                            if (partsw[1].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[1].Trim()[0]) && char.IsLetter(partsw[0].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp [{partsw[0].Replace("loop ", "")}],eax \nje {a}");
                                    Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \ncmp eax,{partsw[0].Replace("loop ", "")} \nje {a}");
                                }
                                sas = true;
                                break;
                            }
                            if (partsw[0].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp [{partsw[1].Replace("loop ", "")}],eax \nje {a}");
                                    Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \ncmp eax,{partsw[1].Replace("loop ", "")}\nje {a}");
                                }
                                sas = true;
                                break;
                            }
                        }
                        if (!char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("loop ", "")}, {partsw[1]}\nje {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("loop ", "")}], [{partsw[1]}]\nje {a}"); }
                        }
                        else if (!char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("loop ", "")}, [{partsw[1]}]\nje {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("loop ", "")}], {partsw[1]}\nje {a}"); }
                        }
                    }
                    else if (command.Contains("!"))
                    {
                        string pattern = @"\d+";
                        string[] partsw = command.TrimStart().Replace("loop ", "").Split("!=");
                        bool was1 = false;
                        string[] lines = File.ReadAllLines(inputFilePath);
                        string a = null;
                        for (int i = poz; i >= 0; i--)
                        {
                            foreach (var term in func)
                            {
                                if (lines[i].StartsWith("func ") && lines[i].Contains(term) && was1 == false)
                                {
                                    Console.WriteLine($"Найдена строка: {lines[i]}");
                                    a = lines[i].Replace("func ", "");
                                    a = a.Replace("{", "");
                                    was1 = true;
                                }
                            }
                        }
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(partsw[1], pattern);
                        }
                        catch { }
                        bool containsNumbers9 = false;
                        try
                        {
                            containsNumbers9 = Regex.IsMatch(partsw[0], pattern);
                        }
                        catch { }
                        bool sas = false;
                        for (int ia = 0; ia < func.Count(); ia++)
                        {
                            string com = command.Replace(" ", null);
                            if (partsw[1].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[1].Trim()[0]) && char.IsLetter(partsw[0].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp [{partsw[0].Replace("loop ", "")}],eax \njne {a}");
                                    Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \ncmp eax,{partsw[0].Replace("loop ", "")} \njne {a}");
                                }
                                sas = true;
                                break;
                            }
                            if (partsw[0].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp [{partsw[1].Replace("loop ", "")}],eax \njne {a}");
                                    Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp eax,{partsw[1].Replace("loop ", "")}\njne {a}");
                                }
                                sas = true;
                                break;
                            }
                        }
                        if (!char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("loop ", "")}, {partsw[1]}\njne {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("loop ", "")}], [{partsw[1]}]\njne {a}"); }
                        }
                        else if (!char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("loop ", "")}, [{partsw[1]}]\njne {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("loop ", "")}], {partsw[1]}\njne {a}"); }
                        }
                    }
                }
                else
                {
                    bool was1 = false;
                    string[] lines = File.ReadAllLines(inputFilePath);
                    for (int i = poz - 1; i >= 0; i--)
                    {
                        foreach (var term in func)
                        {
                            if (lines[i].StartsWith("func ") && lines[i].Contains(term) && was1 == false)
                            {
                                Console.WriteLine($"Найдена строка: {lines[i]}");
                                string a = lines[i].Replace("func ", "");
                                a = a.Replace("{", "");
                                File.AppendAllText(file, "\n" + $"\ndec [{parts[1]}]\n cmp [{parts[1]}], -1\njg {a}");
                                was1 = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
