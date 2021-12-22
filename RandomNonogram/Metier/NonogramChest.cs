using System;
using System.Collections.Generic;

namespace Logic
{
    /// <summary>
    /// Class that manages the nonogram array
    /// </summary>
    public class NonogramChest
    {
        private readonly int x;
        private readonly int y;
        private readonly int numberOfCase;

        /// <summary>
        /// Constructor of the nonogramChest class
        /// </summary>
        /// <param name="x">X Size of the table</param>
        /// <param name="y">Y Size of the table</param>
        /// <param name="noc">number of colored cells</param>
        public NonogramChest(int x, int y, int noc)
        {
            this.x = x;
            this.y = y;
            numberOfCase = noc;
            Table = new bool[x, y];
            TableFilling(); // Call of the table filling
        }

        /// <summary>
        /// Getter and Setter of the cells table
        /// </summary>
        public bool[,] Table { get; set; }

        /// <summary>
        /// Method to fill the table with the number of colored cells "numberOfCase
        /// </summary>
        private void TableFilling()
        {
            for (int i = 0; i < numberOfCase; i++) // For each cells to be colored
            {
                int randX;
                int randY;
                do
                {
                    Random random = new Random();
                    randX = random.Next(1, x);
                    randY = random.Next(1, y);
                }
                while (Table[randX, randY]);

                Table[randX, randY] = true; // We fill in a box with the random X and Y coordinates with the cells to be colored
            }
        }

        /// <summary>
        /// Method to indicate on each column the number of boxes to color
        /// </summary>
        /// <returns>The table of indications of the columns</returns>
        public Dictionary<int, string> ListColumns()
        {
            Dictionary<int, string> listColumns = new Dictionary<int, string>();
            for (int i = 0; i < x; i++) // For each columns
            {
                int count = 0; // Number of cells
                listColumns.Add(i, null);
                for (int j = 0; j < y; j++) // For each rows
                {
                    if (Table[i, j]) // If the cell is coloried
                    {
                        count++; // Increase the counter
                    }
                    else
                    {
                        if (count != 0) // Otherwise we put in listColumns the number(if it is not null)
                        {
                            listColumns[i] += count + " ";
                            count = 0;
                        }
                    }
                }
                if (count != 0) // Manages the cells at the end of the board
                {
                    listColumns[i] += count;
                }
                if (listColumns[i] == null && i != 0) // Manages the column at the end of the tray
                {
                    listColumns[i] = 0 + " ";
                }
            }
            return listColumns;
        }

        /// <summary>
        /// Method to indicate on each rows the number of boxes to color
        /// </summary>
        /// <returns>The table of indications of the rows</returns>
        public Dictionary<int, string> ListRows()
        {
            Dictionary<int, string> listRows = new Dictionary<int, string>();
            for (int j = 0; j < x; j++) // For each rows
            {
                int count = 0; // Number of cells
                listRows.Add(j, null);
                for (int i = 0; i < y; i++) // For each columns
                {
                    if (Table[i, j]) // If the cell is coloried
                    {
                        count++; // Increase the counter
                    }
                    else
                    {
                        if (count != 0) // Otherwise we put in listRows the number(if it is not null)
                        {
                            listRows[j] += count + "\n";
                            count = 0;
                        }
                    }
                }
                if (count != 0) // Manages the cells at the end of the board
                {
                    listRows[j] += count;
                }

                if (listRows[j] != null) 
                {
                    char lastCharacter = listRows[j][^1];
                    if (lastCharacter == '\n')
                    {
                        listRows[j] = listRows[j][..^1];
                    }
                }


                if (listRows[j] == null && j != 0) // Manages the rows at the end of the tray
                {
                    listRows[j] = 0 + " ";
                }
            }
            return listRows;
        }
    }
}
