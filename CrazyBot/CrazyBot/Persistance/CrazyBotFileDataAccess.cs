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
        public CrazyBotInfo Load(String path)
        {
            try

            {
                using (StreamReader reader = new StreamReader(path)) // fájl megnyitása
                {
                    
                    String line = reader.ReadLine();
                    int size = Int32.Parse(line);

                    line = reader.ReadLine();
                    Position RobotPozition = new Position(Int32.Parse(line.Split(" ")[0]), Int32.Parse(line.Split(" ")[1]));

                    line = reader.ReadLine();
                    RobotDirection RobotDir = (RobotDirection)(Int32.Parse(line));

                    line = reader.ReadLine();
                    FieldType fieldTypeOnRobot = (FieldType)Int32.Parse(line);

                    line = reader.ReadLine();
                    ulong time = Convert.ToUInt64(line);

                    line = reader.ReadLine();
                    int crazyTime = Int32.Parse(line);

                    CrazyBotInfo table = new CrazyBotInfo(size, RobotPozition, time, RobotDir, fieldTypeOnRobot, crazyTime);

                    for (Int32 i = 0; i < size; i++)
                    {
                        line = reader.ReadLine();
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

        public void Save(String path, CrazyBotInfo gameInfo)
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
                            writer.Write((int)gameInfo.board[i, j] + " "); // kiírjuk az értékeket
                        }
                        writer.WriteLine();
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






