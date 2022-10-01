using Proxx.Entity;
using Proxx.Manager;
using Proxx.Repositiry;
using System;
using System.Collections.Generic;

namespace Proxx
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Part1
            //conditions from the first task you can find in the BoardEntity class
            var board = new BoardEntity(5, 5);

           
            IGameRepository gameRepo = new GameRepository();


            //Part2
            board.BlackHoles = gameRepo.GetBlackHoles(board.Rows, board.Columns, 5);
        
            //Part3
            gameRepo.CalculateAdjacentBlackHoles(board);

            Draw(board);

            int totalOpenedCellCount = 0;
            int rowNemberBuf, colNumberBuf = 0;

            while (totalOpenedCellCount < board.Cells.Count)
            {
                Console.WriteLine("enter row number");
                rowNemberBuf = Int32.Parse(Console.ReadLine());

                Console.WriteLine("enter col number");
                colNumberBuf = Int32.Parse(Console.ReadLine());

                if ( rowNemberBuf > board.Rows - 1 || rowNemberBuf<0 || colNumberBuf > board.Columns - 1 || colNumberBuf < 0)
                {
                    Console.WriteLine("this cell is incorect");
                    continue;
                }

                var currentCell = new IdObject(rowNemberBuf, colNumberBuf);

                if (board.Cells.TryGetValue(currentCell, out CellEntity cell) && cell.IsOpen)
                {
                    Console.WriteLine("this cell is open");
                    continue;
                }
             

                //Part4
                var openedCells = gameRepo.GetNewOpenedCells(currentCell, board);
              
                
                totalOpenedCellCount += openedCells.Count;

                Draw(board, openedCells.Count == 0 || totalOpenedCellCount == board.Cells.Count);
              
                if (openedCells.Count == 0)
                {
                    Console.WriteLine("You lose");
                    Console.ReadLine();
                    break;
                }

               
            }
            if (totalOpenedCellCount == board.Cells.Count)
            {
                Console.WriteLine("You win");
                Console.ReadLine();
            }



        }


        private static void Draw(BoardEntity board, bool last = false)
        {
            for (int row = 0; row < board.Rows; row++)
            {
                Console.WriteLine(Environment.NewLine);
                for (int col = 0; col < board.Columns; col++)
                {
                    var cellId = new IdObject(row, col);
                    string val = "";
                    ConsoleColor color = ConsoleColor.Green;
                    if (last)
                    {
                        if (board.BlackHoles.Contains(cellId))
                        {
                            val = $"H ";
                            color = ConsoleColor.Red;
                        }
                        else
                        {
                            val = $"{board.Cells[cellId].BlackHolesCount} ";
                        }
                    }
                    else
                    {
                        if (board.Cells.TryGetValue(cellId, out CellEntity cell) && cell.IsOpen)
                        {
                            val = $"{cell.BlackHolesCount} ";
                        }
                        else
                        {
                            val = $"C ";
                        }
                    }
                    Console.ForegroundColor = color;
                    Console.Write(val);

                }
            }
            Console.WriteLine(Environment.NewLine);
            Console.ResetColor();
        }
    }
}
