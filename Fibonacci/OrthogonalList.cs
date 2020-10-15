using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci
{
    /// <summary>
    /// 十字链表
    /// </summary>
    public class OrthogonalList
    {
        private int MAXVEX;
        private VertexeType[] verArr;

        // 顶点数据
        private class VertexeType
        {
            public string vertexe { get; } = null;
            public bool visisted { get; set; } = false;
            public AdjacencyType fristin { get; set; } = null;// 入边表（顶点为弧尾）
            public AdjacencyType fristout { get; set; } = null;// 出边表（顶点为弧头）

            public VertexeType(string vertexe)
            {
                this.vertexe = vertexe;
            }
        }

        /// <summary>
        /// 邻接点域
        /// </summary>
        private class AdjacencyType
        {
            public int verNum { get; set; } = -1;// 弧的起点在顶点数组中的下标
            public int headVex { get; set; } = -1;// 弧终点在顶点数组中的下标
            public AdjacencyType headLink { get; set; } = null;// 指向下一个终点相同的邻接点(对应入度表)
            public AdjacencyType adjLink { get; set; } = null;// 指向下一个起点相同的邻接点(对应出度表)
        }

        // 顶点数组赋值
        public OrthogonalList(string[] arr)
        {
            this.MAXVEX = arr.Length;
            this.verArr = new VertexeType[this.MAXVEX];
            for (int i = 0; i < arr.Length; i++)
            {
                this.verArr[i] = new VertexeType(arr[i]);
            }
        }

        public void addAdj(string[,] strArr)
        {
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                AdjacencyType temp = null;
                AdjacencyType tempin = null;
                int posOne = getPosition(strArr[i,0]);     //获取弧头在顶点数组中的坐标
                int posTwo = getPosition(strArr[i,1]);     //获取弧尾在顶点数组中的坐标
                AdjacencyType adj = new AdjacencyType();
                adj.verNum = posOne;
                adj.headVex = posTwo;
                temp = this.verArr[posOne].fristout;        //弧头的出度表
                tempin = this.verArr[posTwo].fristin;       //弧尾的入度表
                //弧头出度表处理
                if (temp == null)
                {
                    this.verArr[posOne].fristout = adj;
                }
                else
                {
                    while (true)
                    {
                        if (temp.adjLink != null)
                        {
                            temp = temp.adjLink;
                        }
                        else
                        {
                            temp.adjLink = adj;
                            break;
                        }
                    }
                }
                //弧尾入度表处理
                if (tempin == null)
                {
                    this.verArr[posTwo].fristin = adj;
                }
                else
                {
                    while (true)
                    {
                        if (tempin.headLink != null)
                        {
                            tempin = tempin.headLink;
                        }
                        else
                        {
                            tempin.headLink = adj;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取顶点坐标
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private int getPosition(string str)
        {
            int pos = 0;
            for (int i = 0; i < this.MAXVEX; i++)
            {
                if (this.verArr[i].vertexe.Equals(str))
                {
                    pos = i;
                    break;
                }
            }
            return pos;
        }

        /// <summary>
        /// 获取顶点入度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="vertexList"></param>
        /// <returns></returns>
        public int getIndegree(string str, out List<string> vertexList)
        {
            int i = 0;
            vertexList = new List<string>();
            int pos = getPosition(str);
            AdjacencyType temp = this.verArr[pos].fristin;
            if (temp == null)
                return i;
            while (true)
            {
                if (temp != null)
                {
                    var vertex = this.verArr[temp.verNum];
                    vertexList.Add(vertex.vertexe);
                    i++;
                    temp = temp.headLink;
                }
                else
                {
                    break;
                }
            }
            return i;
        }

        /// <summary>
        /// 获取顶点出度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="vertexList"></param>
        /// <returns></returns>
        public int getOutdegree(string str, out List<string> vertexList)
        {
            int i = 0;
            vertexList = new List<string>();
            int pos = getPosition(str);
            AdjacencyType temp = this.verArr[pos].fristout;
            if (temp == null)
                return i;
            while (true)
            {
                if (temp != null)
                {
                    var vertex = this.verArr[temp.headVex];
                    vertexList.Add(vertex.vertexe);
                    i++;
                    temp = temp.adjLink;
                }
                else
                {
                    break;
                }
            }
            return i;
        }

        public void OverViewAndSetVisitedToFalse()
        {
            for(int i = 0; i < this.MAXVEX; i++)
            {
                this.verArr[i].visisted = false;
            }
        }

        /// <summary>
        /// 广度优先遍历
        /// </summary>
        /// <param name="vertex">起始节点</param>
        /// <returns></returns>
        public string BFS(string vertex)
        {
            StringBuilder message = new StringBuilder();
            Queue<VertexeType> queue = new Queue<VertexeType>();
            int pos = getPosition(vertex);
            queue.Enqueue(this.verArr[pos]);
            while (queue.Count>0)
            {
                VertexeType front = queue.Dequeue();
                var frontPos = getPosition(front.vertexe);
                if (front.visisted)
                {
                    continue;
                }
                message.Append(front.vertexe + ",");
                this.verArr[frontPos].visisted = true;
                AdjacencyType temp = this.verArr[frontPos].fristout;
                if (temp == null)
                    continue;
                while (true)
                {
                    if (temp != null)
                    {
                        var vertexType = this.verArr[temp.headVex];
                        queue.Enqueue(vertexType);
                        temp = temp.adjLink;
                    }
                    else
                    {
                        break;
                    }
                }

            }
            
            return message.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 深度优先遍历
        /// </summary>
        /// <param name="vertex">起始节点</param>
        /// <returns></returns>
        public string DFS(string vertex)
        {
            StringBuilder message = new StringBuilder();
            int pos = getPosition(vertex);
            var first = this.verArr[pos];
            message.Append(first.vertexe + ",");
            this.verArr[pos].visisted = true;
            AdjacencyType temp = this.verArr[pos].fristout;
            if (temp == null)
            {
                return message.ToString().TrimEnd(',');
            }
            while (true)
            {
                if (temp != null)
                {
                    var vertexType = this.verArr[temp.headVex];
                    if (!vertexType.visisted)
                    {
                        var ret = DFS(vertexType.vertexe);
                        message.Append(ret + ","); 
                    }
                    temp = temp.adjLink;
                }
                else
                {
                    break;
                }
            }
            return message.ToString().TrimEnd(',');
        }
    }
}
