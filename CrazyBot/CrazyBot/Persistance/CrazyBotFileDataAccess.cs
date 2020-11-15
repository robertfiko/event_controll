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
            try

            {
                using (StreamReader reader = new StreamReader(path)) // fájl megnyitása
                {
                    
                    String line = reader.ReadLineAsync().Result;
                    int size = Int32.Parse(line);

                    line = reader.ReadLineAsync().Result;
                    Position RobotPozition = new Position(Int32.Parse(line.Split(" ")[0]), Int32.Parse(line.Split(" ")[1]));

                    line = reader.ReadLineAsync().Result;
                    RobotDirection RobotDir = (RobotDirection)(Int32.Parse(line));

                    line = reader.ReadLineAsync().Result;
                    FieldType fieldTypeOnRobot = (FieldType)Int32.Parse(line);

                    line = reader.ReadLineAsync().Result;
                    ulong time = Convert.ToUInt64(line);

                    line = reader.ReadLineAsync().Result;
                    int crazyTime = Int32.Parse(line);

                    CrazyBotInfo table = new CrazyBotInfo(size, RobotPozition, time, RobotDir, fieldTypeOnRobot, crazyTime);

                    for (Int32 i = 0; i < size; i++)
                    {
                        line = reader.ReadLineAsync().Result;
                        var numbers = line.Split(' ');

                        for (Int32 j = 0; j < size; j++)
                        {
                            table.board[i, j] = (FieldType)(Int32.Parse(numbers[j]));
                        }
                    }
                    return table;
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException();
            }
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
                            await writer.WriteAsync((int)gameInfo.board[i, j] + " "); // kiírjuk az értékeket
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






