using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CrazyBot.Model;

namespace CrazyBot.Persistance
{
    class CrazyBotFileDataAccess : ICrazyBotDataModel
    {
        public async Task<CrazyBotInfo> LoadAsync(String path)
        {
            /*
            try

            {
                using (StreamReader reader = new StreamReader(path)) // fájl megnyitása
                {
                    String line = await reader.ReadLineAsync();
                    String[] numbers = line.Split(' '); 
                    Int32 tableSize = Int32.Parse(numbers[0]); // beolvassuk a tábla méretét
                    Int32 regionSize = Int32.Parse(numbers[1]); // beolvassuk a házak méretét
                    SudokuTable table = new SudokuTable(tableSize, regionSize); // létrehozzuk a táblát

                    for (Int32 i = 0; i < tableSize; i++)
                    {
                        line = await reader.ReadLineAsync();
                        numbers = line.Split(' ');

                        for (Int32 j = 0; j < tableSize; j++)
                        {
                            table.SetValue(i, j, Int32.Parse(numbers[j]), false);
                        }
                    }

                    for (Int32 i = 0; i < tableSize; i++)
                    {
                        line = await reader.ReadLineAsync();
                        String[] locks = line.Split(' ');

                        for (Int32 j = 0; j < tableSize; j++)
                        {
                            if (locks[j] == "1")
                            {
                                table.SetLock(i, j);
                            }
                        }
                    }

                    return table;
                }
            }
            catch
            {
                throw new SudokuDataException();
            }*/
            return null;
        }


        public async Task SaveAsync(String path, CrazyBotInfo gameInfo)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path)) 
                {
                    writer.WriteLine(gameInfo.size); 
                    writer.WriteLine(gameInfo.robot.X + " " + gameInfo.robot.Y);
                    writer.WriteLine((int)gameInfo.robotDir);
                    writer.WriteLine((int)gameInfo.fieldTypeOnRobot);
                    writer.WriteLine(gameInfo.time);
                    writer.WriteLine(gameInfo.timeLeftUntilCrazy);

                    for (Int32 i = 0; i < gameInfo.size; i++)
                    {
                        for (Int32 j = 0; j < gameInfo.size; j++)
                        {
                            await writer.WriteAsync(gameInfo.board[i, j] + " "); // kiírjuk az értékeket
                        }
                        await writer.WriteLineAsync();
                    }
                }
            }
            catch
            {
                throw new InvalidOperationException("There was an error saving your game!");
            }
        }
    }



}






