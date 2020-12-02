﻿using System;
using System.IO;
using System.Threading.Tasks;

namespace ELTE.Sudoku.Persistence
{
	/// <summary>
	/// Sudoku fájlkezelő típusa.
	/// </summary>
	public class SudokuFileDataAccess : ISudokuDataAccess
	{
		/// <summary>
		/// Fájl betöltése.
		/// </summary>
		/// <param name="path">Elérési útvonal.</param>
		/// <returns>A fájlból beolvasott játéktábla.</returns>
		public async Task<SudokuTable> LoadAsync(String path)
		{
			try
			{
				using (StreamReader reader = new StreamReader(path)) // fájl megnyitása
				{
					String line = await reader.ReadLineAsync();
					String[] numbers = line.Split(' '); // beolvasunk egy sort, és a szóköz mentén széttöredezzük
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
			}
		}

		/// <summary>
		/// Fájl mentése.
		/// </summary>
		/// <param name="path">Elérési útvonal.</param>
		/// <param name="table">A fájlba kiírandó játéktábla.</param>
		public async Task SaveAsync(String path, SudokuTable table)
		{
			try
			{
				using (StreamWriter writer = new StreamWriter(path)) // fájl megnyitása
				{
					writer.Write(table.Size); // kiírjuk a méreteket
					await writer.WriteLineAsync(" " + table.RegionSize);
					for (Int32 i = 0; i < table.Size; i++)
					{
						for (Int32 j = 0; j < table.Size; j++)
						{
							await writer.WriteAsync(table[i, j] + " "); // kiírjuk az értékeket
						}
						await writer.WriteLineAsync();
					}

					for (Int32 i = 0; i < table.Size; i++)
					{
						for (Int32 j = 0; j < table.Size; j++)
						{
							await writer.WriteAsync((table.IsLocked(i, j) ? "1" : "0") + " "); // kiírjuk a zárolásokat
						}
						await writer.WriteLineAsync();
					}
				}
			}
			catch
			{
				throw new SudokuDataException();
			}
		}
	}
}
