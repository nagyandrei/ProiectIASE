using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IASEProject
{
    public class SearchTreeNode
    {
        private SearchTreeNode parrentNode = null;
        private int state;
        private int depth;
        private int[,] stareInitiala;
        private int[,] stareFinala;
        private int n;
        private Queue nodesToExplore = new Queue();

        public SearchTreeNode(SearchTreeNode pn, int s, int d)
        {
            parrentNode = pn;
            state = s;
            depth = d;
          //  n = _n;
        }

        public SearchTreeNode GetParrentNode()
        {
            return parrentNode;
        }

        public int GetState()
        {
            return state;
        }

        public int GetDepth()
        {
            return depth;
        }

        private byte[] DecodeState(int state)
        {
            byte[] dstate = new byte[n*n];
            int i = 0;
            for (i = 0; i < n*n; i++)
            {
                dstate[n*n-1 - i] = (byte)(state % 10);
                state = state / 10;
            }
            return dstate;
        }

        private int EncodeState(byte[] state)
        {
            int estate = 0, i = 0;
            for (i = 0; i < n * n; i++)
            {
                estate = estate * 10 + state[i];
            }
            return estate;
        }

        //Trebuie modificata
        private byte[] GenerateState(byte[] state, int pos, int up, int down, int left, int right)
        {
            int i = 0;
            byte[] newstate = new byte[n * n];
            for (i = 0; i < n * n; i++)
            {
                newstate[i] = state[i];
            }
            newstate[(pos / n) * n + pos % n] = newstate[((pos / n) + up * (-1) + down * 1) * n + (pos % n) + left * (-1) + right * 1];
            newstate[((pos / n) + up * (-1) + down * 1) * n + (pos % n) + left * (-1) + right * 1] = 0;
            return newstate;
        }

        //Trebuie modificata
        private void AddSuccessors(SearchTreeNode currentNode)
        {
            SearchTreeNode left, right, up, down;
            int i = 0;
            while ((i < n * n) && (DecodeState(currentNode.GetState())[i] != 0))
                i++;
            if ((i / n) != 0)
            {
                up = new SearchTreeNode(currentNode, EncodeState(GenerateState(DecodeState(currentNode.GetState()), i, 1, 0, 0, 0)), currentNode.GetDepth() + 1);
                nodesToExplore.Enqueue(up);
            }
            if ((i / 3) != 2)
            //nu este pe linia de jos deci se poate muta jos
            {
                down = new SearchTreeNode(currentNode, EncodeState(GenerateState(DecodeState(currentNode.GetState()), i, 0, 1, 0, 0)), currentNode.GetDepth() + 1);
                nodesToExplore.Enqueue(down);
            }
            if ((i % 3) != 0)
            //nu este pe coloana din stanga deci se poate muta stanga
            {
                left = new SearchTreeNode(currentNode, EncodeState(GenerateState(DecodeState(currentNode.GetState()), i, 0, 0, 1, 0)), currentNode.GetDepth() + 1);
                nodesToExplore.Enqueue(left);
            }
            if ((i % 3) != 2)
            //nu este pe coloana din dreapta deci se poate muta

            {
                right = new SearchTreeNode(currentNode, EncodeState(GenerateState(DecodeState(currentNode.GetState()), i, 0, 0, 0, 1)), currentNode.GetDepth() + 1);
                nodesToExplore.Enqueue(right);
            }
        }
    }

}