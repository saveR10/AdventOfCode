using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Utilities.Dynamic
{
    internal class Movement
    {
        public Dictionary<int, string> TileMove(ref Dictionary<int, string> oldplatform,int n)
        {
            Dictionary<int, string> newplatform = new Dictionary<int, string>();
            newplatform = oldplatform;
            for (int c = 0; c < n; c++)
            {
                int freespace = 0;
                for (int r = n; r > 0; r--)
                {
                    if (oldplatform[r][c] == '.')
                    {
                        freespace++;
                    }

                    if (oldplatform[r][c] == 'O' && freespace > 0)
                    {
                        string line = newplatform[r + freespace];
                        line = line.Remove(c, 1);
                        line = line.Insert(c, "O");
                        newplatform[r + freespace] = line;

                        line = newplatform[r];
                        line = line.Remove(c, 1);
                        line = line.Insert(c, ".");
                        newplatform[r] = line;
                    }

                    if (oldplatform[r][c] == '#')
                    {
                        freespace = 0;
                    }
                }
            }

            return newplatform;
        }
    }
}
