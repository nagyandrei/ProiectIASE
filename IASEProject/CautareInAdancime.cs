using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IASEProject
{
    public class CautareInAdancime
    {
        private static CautareInAdancime instance;
 

        private int numberOfVertices;
        private int numberOfClosedVertices;
        private int visitNumber = 1;

        private CautareInAdancime()
        {

        }

        public CautareInAdancime CreateCautareInAdancime()
        {
            if (instance == null)
            {
                instance = new CautareInAdancime();
            }
            return instance;
        }

        //private void AddSuccessors(int SearchTreeNode, int CurrentNode)
        //{
        //    SearchTreeNode left, right, up, down;
        //}


    }
}
