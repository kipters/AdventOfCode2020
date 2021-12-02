using System.Text.RegularExpressions;

internal class DayEight
{
    internal record Instruction(string Opcode, int Operand);
    internal static async Task RunFirst()
    {
        var assembly = await File.ReadAllTextAsync("day8.txt");
        var regex = new Regex(@"([a-z]{3})\s([+|-]\d+)", RegexOptions.Compiled);
        var code = regex.Matches(assembly)
            .Select(x => new Instruction(x.Groups[1].Value, int.Parse(x.Groups[2].Value)))
            .ToArray();
        
        var (_, ac) = ExecuteInstructions(code);
        Console.WriteLine($"Machine stuck with accumulator {ac}");
    }

    private static (bool completed, int accumulator) ExecuteInstructions(Instruction[] code, int? patchOffset = null)
    {
        var pc = 0;
        var ac = 0;
        var c = 0;
        HashSet<int> visitmap = new();

        while (pc < code.Length)
        {
            if (visitmap.Contains(pc))
            {
                return (false, ac);
            }

            visitmap.Add(pc);
            var (opcode, operand) = code[pc];
            if (patchOffset == pc)
            {
                opcode = opcode switch
                {
                    "jmp" => "nop",
                    "nop" => "jmp",
                    _ => opcode
                };
            }
            c++;
            //Console.WriteLine($"({ac}) {opcode} {operand}");
            switch (opcode)
            {
                case "acc":
                    ac += operand;
                    pc++;
                    break;

                case "jmp":
                    pc += operand;
                    break;

                default:
                    pc++;
                    break;
            }
        }

        return (true, ac);
    }

    internal static async Task RunSecond()
    {
        var assembly = await File.ReadAllTextAsync("day8.txt");
        var regex = new Regex(@"([a-z]{3})\s([+|-]\d+)", RegexOptions.Compiled);
        var code = regex.Matches(assembly)
            .Select(x => new Instruction(x.Groups[1].Value, int.Parse(x.Groups[2].Value)))
            .ToArray();

        var accumulator = Enumerable.Range(0, code.Length)
            .Where(i => code[i].Opcode == "jmp" || code[i].Opcode == "nop")
            .Select(p => ExecuteInstructions(code, p))
            .First(p => p.completed)
            .accumulator;

        Console.WriteLine($"Code corruption detected at offset {accumulator}");
    }
}