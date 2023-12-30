using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace TicTacToe
{
    internal class Program
    {
        /// <summary>
        /// Печать названия игры
        /// </summary>
        static void PrintNameGame()
        {
            string textToEnter = "игра Крестики-Нолики";

            PrintTextCenter(textToEnter);

            Console.WriteLine("");
        }
        /// <summary>
        /// создание пустого поля для игры
        /// </summary>
        /// <param name="emptyField"></param>
        /// <returns></returns>
        static string[,] MakeField(out string[,] emptyField)
        {
            string[,] field = new string[3, 3];//определяем размеры игрового поля, как размеры двумерного массива

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = "";//задаём двумерный массив 3*3 и заполняем каждую ячейку пустой строкой
                }
            }

            emptyField = field;
            return emptyField;
        }
        /// <summary>
        /// Печать поля
        /// </summary>
        /// <param name="field"></param>
        static void PrintField(string[,] field)
        {

            string numbersColumn = " 1  2  3";

            PrintTextCenter(numbersColumn);//пишем номера столбцов

            for (int i = 0; i < field.GetLength(0); i++)
            {
                string fieldRow = $"{i + 1} ";//задаём пустую строку, которую потом заполним тремя элементами одной строки массива, с номером строки

                for (int j = 0; j < field.GetLength(1); j++)
                {
                    string fieldCell = $"[{field[i, j]}] ";
                    fieldRow += fieldCell;//заполняем строку
                }
                PrintTextCenter(fieldRow);//выписываем строку
                Console.WriteLine();
            }
        }
        /// <summary>
        /// печать текста по центру
        /// </summary>
        /// <param name="text"></param>
        static void PrintTextCenter(string text)
        {
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text));
        }
        /// <summary>
        /// изменение ячейки поля
        /// </summary>
        /// <param name="field"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        static string[,] ChangeCellField(ref string[,] field, int x, int y, string sign)
        {
            field[x - 1, y - 1] = sign;
            return field;
        }
        /// <summary>
        /// делаем ход
        /// </summary>
        /// <param name="field"></param>
        static void DoTurn(string[,] field, string lastPointSign, out string lastSign)
        {
            PrintTextCenter("Введите через пробел номер строки, номер столбца и знак 'X' или 'O', используя цифры и английские буквы");

            Console.CursorLeft = Console.WindowWidth / 2;//ставим курсор в центр экрана

            string sign, enteredString = "";
            bool isCorrect, isEmptyString, isThree;
            int numberX, numberY;

            do
            {
                do
                {
                    Console.CursorLeft = Console.WindowWidth / 2;//ставим курсос в центр экрана

                    string inputUser = Console.ReadLine();//вводим строку из двух цифр и крестика или нолика
                    string[] symbolsLocal = inputUser.Split(" ");

                    isEmptyString = IsEmptyString(inputUser);
                    isThree = (symbolsLocal.Length == 3);

                    if (isEmptyString && isThree)
                    {
                        enteredString = inputUser;

                    }
                    else
                    {
                        PrintTextCenter("Неправильный ввод, попробуйте снова");
                    }

                } while (!(isEmptyString&& isThree));

                string[] symbols = enteredString.Split(" ");//разделяем строку на 2 числа и символ в массив

                string stringX = symbols[0];//считываем первую цифру
                string stringY = symbols[1];//считываем вторую цифру
                string symbolSign = symbols[2];//считываем крестик или нолик

                sign = symbolSign.ToUpper();//делаем крестик или нолик в любом случае большим

                bool firstSymbol = (int.TryParse(stringX, out numberX) && numberX > 0 && numberX <= 3);//входит ли первое число в диапазон от 1 до 3
                bool secondSymbol = (int.TryParse(stringY, out numberY) && numberY > 0 && numberY <= 3);//входим ли второе число в диапазон от 1 до 3
                bool thirdSymbol = (sign == "O" || sign == "X");//является ли вводимый символ игровым
                bool isLastSignRepeat = (lastPointSign == sign);
                bool isCellEmpty = IsCellEmpty(field, numberX, numberY);

                isCorrect = (firstSymbol && secondSymbol && thirdSymbol && !isLastSignRepeat && isCellEmpty);//проверка всех условий

                if (!isCorrect)
                {
                    PrintTextCenter("Неправильный ввод, попробуйте снова");//вывод в случае неправильного ввода
                }

            } while (!isCorrect);//проверка на ввод

            ChangeCellField(ref field, numberX, numberY, sign);//меняем игровое поле

            Console.Clear();//очищаем консоль для красоты

            PrintField(field);//выводим поле на экран

            lastSign = sign;
        }
        /// <summary>
        /// проверка строки на выйгрыш
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        static bool IsRow(string[,] field)
        {
            if (field[0, 0] == field[0,1] && field[0,1] == field[0,2] && field[0,0] != "")
            {
                return false;
            }
            if (field[1, 0] == field[1, 1] && field[1, 1] == field[1, 2] && field[1, 0] != "")
            {
                return false;
            }
            if (field[2, 0] == field[2, 1] && field[2, 1] == field[2, 2] && field[2, 0] != "")
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// проверка столбца на выйгрыш
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        static bool IsColumn(string[,] field)
        {
            if (field[0, 0] == field[1,0] && field[1,0] == field[2,0] && field[0, 0] != "")
            {
                return false;
            }
            if (field[0, 1] == field[1, 1] && field[1, 1] == field[2, 1] && field[0, 1] != "")
            {
                return false;
            }
            if (field[0, 2] == field[1, 2] && field[1, 2] == field[2, 2] && field[0, 2] != "")
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// проверка ячейки на заполненность 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="numberX"></param>
        /// <param name="numberY"></param>
        /// <returns></returns>
        static bool IsCellEmpty(string[,] field, int numberX, int numberY)
        {
            if (field[numberX - 1, numberY - 1] == "")
            { 
                return true;
            }
            
            return false;
        }
        /// <summary>
        /// заполнение поля в 9 ходов
        /// </summary>
        /// <param name="countTurn"></param>
        /// <returns></returns>
        static bool IsFieldFill(int countTurn)
        {
            if (countTurn > 0)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// проверка диагонали на выйгрыш
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        static bool IsDiagonal(string[,] field)
        {
            if (field[0, 0] == field[1, 1] && field[1, 1] == field[2, 2] && field[0, 0] != "")
            {
                return false;
            }
            if (field[2, 0] == field[1, 1] && field[1, 1] == field[0, 2] && field[2, 0] != "")
            {
                return false;
            }

            return true;
        } 
        /// <summary>
        /// проверка строки на пустоты
        /// </summary>
        /// <param name="inputUser"></param>
        /// <returns></returns>
        static bool IsEmptyString(string inputUser)
        {
            int countEmpty = 0;
            string test = inputUser.Replace(" ", "%");

            foreach (char symbol in test)
            {
                if (symbol == '%')
                    countEmpty++;
            }

            if (inputUser.Length == 0 || inputUser.Contains("\t") || inputUser == null || (countEmpty == inputUser.Length))
            {
                PrintTextCenter("Неправильный ввод, попробуйте снова");
                return false;
            }

            return true;
        }
        /// <summary>
        /// все условия конца игры
        /// </summary>
        /// <param name="countTurn"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        static bool AllConditions(int countTurn, string[,] field)
        {
            bool isRow = IsRow(field);
            bool isFieldFill = IsFieldFill(countTurn);
            bool isColumn = IsColumn(field);
            bool isDiagonal = IsDiagonal(field);
            bool allCondition = isColumn && isRow && isColumn && isDiagonal && isFieldFill;

            if (allCondition)
            {
                return true;
            }

            return false;
        }

        static void Main(string[] args)
        {
            int countTurn = 9;
            string lastPointSign = "anything";
            
            PrintNameGame();

            MakeField(out string[,] field);
            PrintField(field);
            PrintTextCenter("Начинайте игру");
            
            Console.WriteLine();

            while (AllConditions(countTurn,field))
            {
                DoTurn(field,lastPointSign, out string lastSign);
                countTurn--;
                lastPointSign = lastSign;
            }

            PrintTextCenter($"Игра закончена, победили {lastPointSign}");
        }
    }
}
