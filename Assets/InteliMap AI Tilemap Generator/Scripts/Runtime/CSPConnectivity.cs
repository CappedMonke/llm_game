using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteliMap
{
    public enum Connectivity
    {
        FourWay,
        EightWay,
        Hexagonal,
    }

    [Serializable]
    public class CSPConnectivity
    {
        public CSPConnectivity(Connectivity con, int uniqueCount, DirectionalBools enforceBorderConnectivity)
        {
            this.con = con;
            this.uniqueCount = uniqueCount;
            this.enforceBorderConnectivity = enforceBorderConnectivity;

            switch (con)
            {
                case Connectivity.FourWay:
                    topConnectivity = new bool[uniqueCount * uniqueCount];
                    bottomConnectivity = new bool[uniqueCount * uniqueCount];
                    leftConnectivity = new bool[uniqueCount * uniqueCount];
                    rightConnectivity = new bool[uniqueCount * uniqueCount];
                    break;
                case Connectivity.EightWay:
                    topConnectivity = new bool[uniqueCount * uniqueCount];
                    bottomConnectivity = new bool[uniqueCount * uniqueCount];
                    leftConnectivity = new bool[uniqueCount * uniqueCount];
                    rightConnectivity = new bool[uniqueCount * uniqueCount];

                    topLeftConnectivity = new bool[uniqueCount * uniqueCount];
                    topRightConnectivity = new bool[uniqueCount * uniqueCount];
                    bottomLeftConnectivity = new bool[uniqueCount * uniqueCount];
                    bottomRightConnectivity = new bool[uniqueCount * uniqueCount];
                    break;
                case Connectivity.Hexagonal:
                    leftConnectivity = new bool[uniqueCount * uniqueCount];
                    rightConnectivity = new bool[uniqueCount * uniqueCount];

                    topLeftConnectivity = new bool[uniqueCount * uniqueCount];
                    topRightConnectivity = new bool[uniqueCount * uniqueCount];
                    bottomLeftConnectivity = new bool[uniqueCount * uniqueCount];
                    bottomRightConnectivity = new bool[uniqueCount * uniqueCount];
                    break;
            }

            if (enforceBorderConnectivity.top)
            {
                topBorderConnectivity = new bool[uniqueCount];
            }
            if (enforceBorderConnectivity.bottom)
            {
                bottomBorderConnectivity = new bool[uniqueCount];
            }
            if (enforceBorderConnectivity.left)
            {
                leftBorderConnectivity = new bool[uniqueCount];
            }
            if (enforceBorderConnectivity.right)
            {
                rightBorderConnectivity = new bool[uniqueCount];
            }
        }

        [SerializeField] public Connectivity con;

        [SerializeField] private int uniqueCount;

        [SerializeField] private bool[] topConnectivity;
        [SerializeField] private bool[] bottomConnectivity;
        [SerializeField] private bool[] leftConnectivity;
        [SerializeField] private bool[] rightConnectivity;

        [SerializeField] private bool[] topLeftConnectivity;
        [SerializeField] private bool[] topRightConnectivity;
        [SerializeField] private bool[] bottomLeftConnectivity;
        [SerializeField] private bool[] bottomRightConnectivity;

        [SerializeField] public DirectionalBools enforceBorderConnectivity;
        [SerializeField] private bool[] topBorderConnectivity;
        [SerializeField] private bool[] bottomBorderConnectivity;
        [SerializeField] private bool[] leftBorderConnectivity;
        [SerializeField] private bool[] rightBorderConnectivity;

        public bool GetTopConnectivity(int mainIdx, int topIdx)
        {
            return topConnectivity[mainIdx * uniqueCount + topIdx];
        }

        public bool GetBottomConnectivity(int mainIdx, int bottomIdx)
        {
            return bottomConnectivity[mainIdx * uniqueCount + bottomIdx];
        }

        public bool GetLeftConnectivity(int mainIdx, int leftIdx)
        {
            return leftConnectivity[mainIdx * uniqueCount + leftIdx];
        }

        public bool GetRightConnectivity(int mainIdx, int rightIdx)
        {
            return rightConnectivity[mainIdx * uniqueCount + rightIdx];
        }

        public bool GetTopLeftConnectivity(int mainIdx, int topLeftIdx)
        {
            return topLeftConnectivity[mainIdx * uniqueCount + topLeftIdx];
        }

        public bool GetTopRightConnectivity(int mainIdx, int topRightIdx)
        {
            return topRightConnectivity[mainIdx * uniqueCount + topRightIdx];
        }

        public bool GetBottomLeftConnectivity(int mainIdx, int bottomLeftIdx)
        {
            return bottomLeftConnectivity[mainIdx * uniqueCount + bottomLeftIdx];
        }

        public bool GetBottomRightConnectivity(int mainIdx, int bottomRightIdx)
        {
            return bottomRightConnectivity[mainIdx * uniqueCount + bottomRightIdx];
        }

        public bool GetTopBorderConnectivity(int idx)
        {
            return topBorderConnectivity[idx];
        }

        public bool GetBottomBorderConnectivity(int idx)
        {
            return bottomBorderConnectivity[idx];
        }

        public bool GetLeftBorderConnectivity(int idx)
        {
            return leftBorderConnectivity[idx];
        }

        public bool GetRightBorderConnectivity(int idx)
        {
            return rightBorderConnectivity[idx];
        }

        public int GetLCVHeuristic(Vector2Int pos, int startY, SparseSet[,] domains, BoundsInt bounds, int index)
        {
            int size = 0;
            switch (con)
            {
                case Connectivity.FourWay:
                    if (pos.y > 0) // bottom
                    {
                        foreach (int bottom in domains[pos.x, pos.y - 1])
                        {
                            if (!bottomConnectivity[index * uniqueCount + bottom])
                            {
                                size++;
                            }
                        }
                    }

                    if (pos.y < bounds.size.y - 1) // top
                    {
                        foreach (int top in domains[pos.x, pos.y + 1])
                        {
                            if (!topConnectivity[index * uniqueCount + top])
                            {
                                size++;
                            }
                        }
                    }

                    if (pos.x > 0) // left
                    {
                        foreach (int left in domains[pos.x - 1, pos.y])
                        {
                            if (!leftConnectivity[index * uniqueCount + left])
                            {
                                size++;
                            }
                        }
                    }

                    if (pos.x < bounds.size.x - 1) // right
                    {
                        foreach (int right in domains[pos.x + 1, pos.y])
                        {
                            if (!rightConnectivity[index * uniqueCount + right])
                            {
                                size++;
                            }
                        }
                    }
                    break;
                case Connectivity.EightWay:
                    if (pos.y > 0) // bottom
                    {
                        foreach (int bottom in domains[pos.x, pos.y - 1])
                        {
                            if (!bottomConnectivity[index * uniqueCount + bottom])
                            {
                                size++;
                            }
                        }
                    }

                    if (pos.y < bounds.size.y - 1) // top
                    {
                        foreach (int top in domains[pos.x, pos.y + 1])
                        {
                            if (!topConnectivity[index * uniqueCount + top])
                            {
                                size++;
                            }
                        }
                    }

                    if (pos.x > 0) // left
                    {
                        foreach (int left in domains[pos.x - 1, pos.y])
                        {
                            if (!leftConnectivity[index * uniqueCount + left])
                            {
                                size++;
                            }
                        }
                    }

                    if (pos.x < bounds.size.x - 1) // right
                    {
                        foreach (int right in domains[pos.x + 1, pos.y])
                        {
                            if (!rightConnectivity[index * uniqueCount + right])
                            {
                                size++;
                            }
                        }
                    }

                    if (pos.y > 0 && pos.x > 0) // bottom left
                    {
                        foreach (int bottomLeft in domains[pos.x - 1, pos.y - 1])
                        {
                            if (!bottomLeftConnectivity[index * uniqueCount + bottomLeft])
                            {
                                size++;
                            }
                        }
                    }

                    if (pos.y > 0 && pos.x < bounds.size.x - 1) // bottom right
                    {
                        foreach (int bottomRight in domains[pos.x + 1, pos.y - 1])
                        {
                            if (!bottomRightConnectivity[index * uniqueCount + bottomRight])
                            {
                                size++;
                            }
                        }
                    }

                    if (pos.y < bounds.size.y - 1 && pos.x > 0) // top left
                    {
                        foreach (int topLeft in domains[pos.x - 1, pos.y + 1])
                        {
                            if (!topLeftConnectivity[index * uniqueCount + topLeft])
                            {
                                size++;
                            }
                        }
                    }

                    if (pos.y < bounds.size.y - 1 && pos.x < bounds.size.x - 1) // top right
                    {
                        foreach (int topRight in domains[pos.x + 1, pos.y + 1])
                        {
                            if (!topRightConnectivity[index * uniqueCount + topRight])
                            {
                                size++;
                            }
                        }
                    }
                    break;
                case Connectivity.Hexagonal:
                    if (pos.x > 0) // left
                    {
                        foreach (int left in domains[pos.x - 1, pos.y])
                        {
                            if (!leftConnectivity[index * uniqueCount + left])
                            {
                                size++;
                            }
                        }
                    }

                    if (pos.x < bounds.size.x - 1) // right
                    {
                        foreach (int right in domains[pos.x + 1, pos.y])
                        {
                            if (!rightConnectivity[index * uniqueCount + right])
                            {
                                size++;
                            }
                        }
                    }

                    if (Math.Abs(pos.y - startY) % 2 == 0) // even
                    {
                        if (pos.y > 0)
                        {
                            // bottom right
                            foreach (int bottomRight in domains[pos.x, pos.y - 1])
                            {
                                if (!bottomRightConnectivity[index * uniqueCount + bottomRight])
                                {
                                    size++;
                                }
                            }

                            if (pos.x > 0)
                            {
                                // bottom left
                                foreach (int bottomLeft in domains[pos.x - 1, pos.y - 1])
                                {
                                    if (!bottomLeftConnectivity[index * uniqueCount + bottomLeft])
                                    {
                                        size++;
                                    }
                                }
                            }
                        }

                        if (pos.y < bounds.size.y - 1)
                        {
                            // top right
                            foreach (int topRight in domains[pos.x, pos.y + 1])
                            {
                                if (!topRightConnectivity[index * uniqueCount + topRight])
                                {
                                    size++;
                                }
                            }

                            if (pos.x > 0)
                            {
                                // top left
                                foreach (int topLeft in domains[pos.x - 1, pos.y + 1])
                                {
                                    if (!topLeftConnectivity[index * uniqueCount + topLeft])
                                    {
                                        size++;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (pos.y > 0)
                        {
                            // bottom right
                            if (pos.x < bounds.size.x - 1)
                            {
                                foreach (int bottomRight in domains[pos.x + 1, pos.y - 1])
                                {
                                    if (!bottomRightConnectivity[index * uniqueCount + bottomRight])
                                    {
                                        size++;
                                    }
                                }
                            }

                            // bottom left
                            foreach (int bottomLeft in domains[pos.x, pos.y - 1])
                            {
                                if (!bottomLeftConnectivity[index * uniqueCount + bottomLeft])
                                {
                                    size++;
                                }
                            }
                        }

                        if (pos.y < bounds.size.y - 1)
                        {
                            // top right
                            if (pos.x < bounds.size.x - 1)
                            {
                                foreach (int topRight in domains[pos.x + 1, pos.y + 1])
                                {
                                    if (!topRightConnectivity[index * uniqueCount + topRight])
                                    {
                                        size++;
                                    }
                                }
                            }

                            // top left
                            foreach (int topLeft in domains[pos.x, pos.y + 1])
                            {
                                if (!topLeftConnectivity[index * uniqueCount + topLeft])
                                {
                                    size++;
                                }
                            }
                        }
                    }
                    break;
            }

            return size;
        }

        public void AddToConnectivity(int x, int y, int startY, int tileIdx, BoundsInt bounds, int[] indicies, int emptyIndex, InteliMapBuilder.InteliMapBuilderAdvanced advanced)
        {
            AddBorderConnectivity(x, y, tileIdx, bounds, advanced);

            switch (con)
            {
                case Connectivity.FourWay:
                    AddFourWayConnectivity(x, y, tileIdx, bounds, indicies, emptyIndex, advanced);
                    break;
                case Connectivity.EightWay:
                    AddEightWayConnectivity(x, y, tileIdx, bounds, indicies, emptyIndex, advanced);
                    break;
                case Connectivity.Hexagonal:
                    AddHexagonalConnectivity(x, y, startY, tileIdx, bounds, indicies, emptyIndex, advanced);
                    break;
            }
        }

        private void AddBorderConnectivity(int x, int y, int tileIdx, BoundsInt bounds, InteliMapBuilder.InteliMapBuilderAdvanced advanced)
        {
            if (advanced.enforceBorderConnectivity.bottom && y == 0)
            {
                bottomBorderConnectivity[tileIdx] = true;
            }

            if (advanced.enforceBorderConnectivity.top && y == bounds.size.y - 1)
            {
                topBorderConnectivity[tileIdx] = true;
            }

            if (advanced.enforceBorderConnectivity.left && x == 0)
            {
                leftBorderConnectivity[tileIdx] = true;
            }

            if (advanced.enforceBorderConnectivity.right && x == bounds.size.x - 1)
            {
                rightBorderConnectivity[tileIdx] = true;
            }
        }

        private void AddFourWayConnectivity(int x, int y, int tileIdx, BoundsInt bounds, int[] indicies, int emptyIndex, InteliMapBuilder.InteliMapBuilderAdvanced advanced)
        {
            if (y > 0) // bottom
            {
                int tb = indicies[x + (y - 1) * bounds.size.x];
                if (tb != -1)
                {
                    bottomConnectivity[tileIdx * uniqueCount + tb] = true;
                }
                else if (advanced.interpretEmptyAsTile)
                {
                    bottomConnectivity[tileIdx * uniqueCount + emptyIndex] = true;
                }
            }

            if (y < bounds.size.y - 1) // top
            {
                int tt = indicies[x + (y + 1) * bounds.size.x];
                if (tt != -1)
                {
                    topConnectivity[tileIdx * uniqueCount + tt] = true;
                }
                else if (advanced.interpretEmptyAsTile)
                {
                    topConnectivity[tileIdx * uniqueCount + emptyIndex] = true;
                }
            }

            if (x > 0) // left
            {
                int tl = indicies[x - 1 + y * bounds.size.x];
                if (tl != -1)
                {
                    leftConnectivity[tileIdx * uniqueCount + tl] = true;
                }
                else if (advanced.interpretEmptyAsTile)
                {
                    leftConnectivity[tileIdx * uniqueCount + emptyIndex] = true;
                }
            }

            if (x < bounds.size.x - 1) // right
            {
                int tr = indicies[x + 1 + y * bounds.size.x];
                if (tr != -1)
                {
                    rightConnectivity[tileIdx * uniqueCount + tr] = true;
                }
                else if (advanced.interpretEmptyAsTile)
                {
                    rightConnectivity[tileIdx * uniqueCount + emptyIndex] = true;
                }
            }
        }

        private void AddEightWayConnectivity(int x, int y, int tileIdx, BoundsInt bounds, int[] indicies, int emptyIndex, InteliMapBuilder.InteliMapBuilderAdvanced advanced)
        {
            AddFourWayConnectivity(x, y, tileIdx, bounds, indicies, emptyIndex, advanced);

            if (y > 0 && x > 0) // bottom left
            {
                int tbl = indicies[(x - 1) + (y - 1) * bounds.size.x];
                if (tbl != -1)
                {
                    bottomLeftConnectivity[tileIdx * uniqueCount + tbl] = true;
                }
                else if (advanced.interpretEmptyAsTile)
                {
                    bottomLeftConnectivity[tileIdx * uniqueCount + emptyIndex] = true;
                }
            }
            if (y > 0 && x < bounds.size.x - 1) // bottom right
            {
                int tbr = indicies[(x + 1) + (y - 1) * bounds.size.x];
                if (tbr != -1)
                {
                    bottomRightConnectivity[tileIdx * uniqueCount + tbr] = true;
                }
                else if (advanced.interpretEmptyAsTile)
                {
                    bottomRightConnectivity[tileIdx * uniqueCount + emptyIndex] = true;
                }
            }

            if (y < bounds.size.y - 1 && x > 0) // top left
            {
                int ttl = indicies[(x - 1) + (y + 1) * bounds.size.x];
                if (ttl != -1)
                {
                    topLeftConnectivity[tileIdx* uniqueCount + ttl] = true;
                }
                else if (advanced.interpretEmptyAsTile)
                {
                    topLeftConnectivity[tileIdx * uniqueCount + emptyIndex] = true;
                }
            }
            if (y < bounds.size.y - 1 && x < bounds.size.x - 1) // top right
            {
                int ttr = indicies[(x + 1) + (y + 1) * bounds.size.x];
                if (ttr != -1)
                {
                    topRightConnectivity[tileIdx* uniqueCount + ttr] = true;
                }
                else if (advanced.interpretEmptyAsTile)
                {
                    topRightConnectivity[tileIdx * uniqueCount + emptyIndex] = true;
                }
            }
        }

        private void AddHexagonalConnectivity(int x, int y, int startY, int tileIdx, BoundsInt bounds, int[] indicies, int emptyIndex, InteliMapBuilder.InteliMapBuilderAdvanced advanced)
        {
            if (x > 0) // left
            {
                int tl = indicies[x - 1 + y * bounds.size.x];
                if (tl != -1)
                {
                    leftConnectivity[tileIdx * uniqueCount + tl] = true;
                }
                else if (advanced.interpretEmptyAsTile)
                {
                    leftConnectivity[tileIdx * uniqueCount + emptyIndex] = true;
                }
            }

            if (x < bounds.size.x - 1) // right
            {
                int tr = indicies[x + 1 + y * bounds.size.x];
                if (tr != -1)
                {
                    rightConnectivity[tileIdx * uniqueCount + tr] = true;
                }
                else if (advanced.interpretEmptyAsTile)
                {
                    rightConnectivity[tileIdx * uniqueCount + emptyIndex] = true;
                }
            }

            if (Math.Abs(y - startY) % 2 == 0) // even
            {
                if (y > 0)
                {
                    // bottom right
                    int tbr = indicies[x + (y - 1) * bounds.size.x];
                    if (tbr != -1)
                    {
                        bottomRightConnectivity[tileIdx * uniqueCount + tbr] = true;
                    }
                    else if (advanced.interpretEmptyAsTile)
                    {
                        bottomRightConnectivity[tileIdx * uniqueCount + emptyIndex] = true;
                    }

                    if (x > 0)
                    {
                        // bottom left
                        int tbl = indicies[x - 1 + (y - 1) * bounds.size.x];
                        if (tbl != -1)
                        {
                            bottomLeftConnectivity[tileIdx * uniqueCount + tbl] = true;
                        }
                        else if (advanced.interpretEmptyAsTile)
                        {
                            bottomLeftConnectivity[tileIdx * uniqueCount + emptyIndex] = true;
                        }
                    }
                }

                if (y < bounds.size.y - 1)
                {
                    // top right
                    int ttr = indicies[x + (y + 1) * bounds.size.x];
                    if (ttr != -1)
                    {
                        topRightConnectivity[tileIdx * uniqueCount + ttr] = true;
                    }
                    else if (advanced.interpretEmptyAsTile)
                    {
                        topRightConnectivity[tileIdx * uniqueCount + emptyIndex] = true;
                    }

                    if (x > 0)
                    {
                        // top left
                        int ttl = indicies[x - 1 + (y + 1) * bounds.size.x];
                        if (ttl != -1)
                        {
                            topLeftConnectivity[tileIdx * uniqueCount + ttl] = true;
                        }
                        else if (advanced.interpretEmptyAsTile)
                        {
                            topLeftConnectivity[tileIdx * uniqueCount + emptyIndex] = true;
                        }
                    }
                }
            }
            else
            {
                if (y > 0)
                {
                    // bottom right
                    if (x < bounds.size.x - 1)
                    {
                        int tbr = indicies[x + 1 + (y - 1) * bounds.size.x];
                        if (tbr != -1)
                        {
                            bottomRightConnectivity[tileIdx * uniqueCount + tbr] = true;
                        }
                        else if (advanced.interpretEmptyAsTile)
                        {
                            bottomRightConnectivity[tileIdx * uniqueCount + emptyIndex] = true;
                        }
                    }

                    // bottom left
                    int tbl = indicies[x + (y - 1) * bounds.size.x];
                    if (tbl != -1)
                    {
                        bottomLeftConnectivity[tileIdx * uniqueCount + tbl] = true;
                    }
                    else if (advanced.interpretEmptyAsTile)
                    {
                        bottomLeftConnectivity[tileIdx * uniqueCount + emptyIndex] = true;
                    }
                }

                if (y < bounds.size.y - 1)
                {
                    // top right
                    if (x < bounds.size.x - 1)
                    {
                        int ttr = indicies[x + 1 + (y + 1) * bounds.size.x];
                        if (ttr != -1)
                        {
                            topRightConnectivity[tileIdx * uniqueCount + ttr] = true;
                        }
                        else if (advanced.interpretEmptyAsTile)
                        {
                            topRightConnectivity[tileIdx * uniqueCount + emptyIndex] = true;
                        }
                    }

                    // top left
                    int ttl = indicies[x + (y + 1) * bounds.size.x];
                    if (ttl != -1)
                    {
                        topLeftConnectivity[tileIdx * uniqueCount + ttl] = true;
                    }
                    else if (advanced.interpretEmptyAsTile)
                    {
                        topLeftConnectivity[tileIdx * uniqueCount + emptyIndex] = true;
                    }
                }
            }
        }
    }
}