Console.WriteLine("Введите первое число:");
if (!int.TryParse(Console.ReadLine(), out int a))
{
    Console.WriteLine("Ошибка!Введено не число.");
    return;
}

Console.WriteLine("Введите второе число:");
if (!int.TryParse(Console.ReadLine(), out int b))
{
    Console.WriteLine("Ошибка!Введено не число.");
    return;
}

Console.WriteLine("Введите оператор (&, |, ^):");
string op = Console.ReadLine();

if (op !="&" && op != "|" && op != "^")
{
    Console.WriteLine("Ошибка!Не верно введен оператор.");
    return;
}

int result;

switch (op)

{
    case "&":
        result = a & b;
        break;
    case "|":
        result = a | b;
        break;
    case "^":
        result = a ^ b;
        break;
    default:
        Console.WriteLine("Ошибка!Оператор неопределен.");
        return;

}

Console.WriteLine($"Результат (10): {result}");
Console.WriteLine($"Результат (2): {Convert.ToString(result,2)}");
Console.WriteLine($"Результат (16): {Convert.ToString(result, 16)}");

