using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using RoboChase.Model;

namespace RoboChase.Persistance
{
    public class RoboChaseFileDataAccess : IRoboChaseData
    {
        public async Task<RoboChaseInfo> LoadAsync(String path)
        {
            try

            {
                using (StreamReader reader = new StreamReader(path))
                {
                    
                    String line = await reader.ReadLineAsync();
                    int size = Int32.Parse(line);

                    line = await reader.ReadLineAsync();
                    Position RobotPozition = new Position(Int32.Parse(line.Split(" ")[0]), Int32.Parse(line.Split(" ")[1]));

                    line = await reader.ReadLineAsync();
                    RobotDirection RobotDir = (RobotDirection)(Int32.Parse(line));

                    line = await reader.ReadLineAsync();
                    FieldType fieldTypeOnRobot = (FieldType)Int32.Parse(line);

                    line = await reader.ReadLineAsync();
                    ulong time = Convert.ToUInt64(line);

                    line = await reader.ReadLineAsync();
                    int crazyTime = Int32.Parse(line);

                    RoboChaseInfo table = new RoboChaseInfo(size, RobotPozition, time, RobotDir, fieldTypeOnRobot, crazyTime);

                    for (Int32 i = 0; i < size; i++)
                    {
                        line = await reader.ReadLineAsync();
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
                throw new FileLoadException();
            }
        }

        public async Task<bool> SaveAsync(String path, RoboChaseInfo gameInfo)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path)) 
                {
                    await writer.WriteLineAsync(gameInfo.size.ToString()); 
                    await writer.WriteLineAsync(gameInfo.robot.X + " " + gameInfo.robot.Y);
                    await writer.WriteLineAsync(((int)gameInfo.robotDir).ToString());
                    await writer.WriteLineAsync(((int)gameInfo.fieldTypeOnRobot).ToString());
                    await writer.WriteLineAsync(gameInfo.time.ToString());
                    await writer.WriteLineAsync(gameInfo.timeLeftUntilCrazy.ToString());

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
                return false;
            }
            return true;
        }
    }



}






