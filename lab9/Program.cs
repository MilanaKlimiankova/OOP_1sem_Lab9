using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab9
{
    public class Game
    {
        public delegate void Message(string message); //делегат

        public event Message Damage; //событие
        public event Message Restore;

        public int HP { get; set; }
        public string Name { get; set; }

        public void Heal(int val)
        {
            HP += val;
            Restore($"Восстановлено {val} единиц здоровья");
        }

        public void Attack(int val) 
        {
            HP -= val;
            Damage.Invoke($"-{val} единиц здоровья");
        }
        public Game(string Name, int HP)
        {
            this.Name = Name;
            this.HP = HP;
            Show();
        }
        public void Show()
        {
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("HP: " + HP);
            Console.WriteLine();
        }
    }

    public static class StringUpgrader
    {
        public static Func<string, string> StrFunc; //возвращает результат действия
        public static Action<string> action; //является обобщенным, принимает параметры и возвращает значение void

        private static string AddDot(string str)//добавить точку вконце
        {
            str += ".";
            return str;
        }
        private static void Out(string str)//вывод строки
        {
            Console.WriteLine(str);
        }
        private static string RemoveDoubleSpaces(string str)//убрать двойные пробелы
        {

            return str.Replace("  ", " ");
        }
        private static string FirstLetterToUpperCase(string str)//замена первой буквы на заглавную
        {
            char a = str[0];
            return str.Replace(str[0], char.ToUpper(str[0]));
        }
        private static string RemoveCom(string str) //удаление запятых
        {
            return str.Replace(", ", " ");
        }

        public static void Upgrade(string str)//Собираем цепочку и меняем строку 
        {

            StrFunc = RemoveCom;
            string temp = StrFunc.Invoke(str);
            StrFunc += FirstLetterToUpperCase;
            temp = StrFunc.Invoke(temp);
            StrFunc += RemoveDoubleSpaces;
            temp = StrFunc.Invoke(temp);
            StrFunc += AddDot;
            temp = StrFunc.Invoke(temp);
            action = Out;
            action(str);
            Console.WriteLine(StrFunc(temp));
        }
    }
    class Program
    {
        public static void ShowMessage(string str)
        {
            Console.WriteLine(str);
        }

        public delegate void WriteHP(Game obj);

        static void Main(string[] args)
        {
            Game w1 = new Game("Witch", 1000);
            w1.Restore += ShowMessage;
            w1.Damage += ShowMessage;
            w1.Heal(30);
            w1.Attack(100);
            WriteHP WriteHP1 = (obj) => Console.WriteLine($"Уровень здоровья: {obj.HP}"); //лямбда-выражение
            WriteHP1(w1);
            Console.WriteLine("____________________");

            Game w2 = new Game("Pristess", 500);
            w2.Restore += ShowMessage;
            w2.Damage += ShowMessage;
            w2.Heal(300);
            w2.Attack(10);
            WriteHP WriteHP2 = (obj) => Console.WriteLine($"Уровень здоровья: {obj.HP}");
            WriteHP2(w2);
            Console.WriteLine("____________________");

            Game w3 = new Game("Golum", 2000);
            w3.Restore += ShowMessage;
            w3.Damage += ShowMessage;
            w3.Heal(300);
            w3.Attack(100);
            WriteHP WriteHP3 = (obj) => Console.WriteLine($"Уровень здоровья: {obj.HP}");
            WriteHP3(w3);
            Console.WriteLine("____________________");

            string str = "варкалось,, шорьки  пырялись,, зелюки, хрюкотали";
            StringUpgrader.Upgrade(str);

        }
    }
}
