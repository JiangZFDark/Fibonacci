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
            public int weight { get; set; } = int.MaxValue;//距离权重
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
                int weight = int.Parse(strArr[i, 2]);
                AdjacencyType adj = new AdjacencyType();
                adj.verNum = posOne;
                adj.headVex = posTwo;
                adj.weight = weight;
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

        /// <summary>
        /// 迪杰斯特拉算法
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public DijkstraEntity Dijkstra(string vertex)
        {
            DijkstraEntity ret = new DijkstraEntity();
            //创建距离表，存储从起点到每一个顶点的临时距离
            Dictionary<string, int> distanceMap = new Dictionary<string, int>();
            Dictionary<string, string> preVexMap = new Dictionary<string, string>();
            //起始点
            int startIndex = getPosition(vertex);
            //初始化距离表
            for (int i = 0; i < this.MAXVEX; i++)
            {
                if (i != startIndex)
                {
                    distanceMap.Add(this.verArr[i].vertexe, int.MaxValue);
                    preVexMap.Add(this.verArr[i].vertexe, string.Empty);
                }
            }
            this.verArr[startIndex].visisted = true;
            List<AdjacencyType> edges = GetEdgeList(this.verArr[startIndex]);
            edges.ForEach(e =>
            {
                var index = e.headVex;
                var tempVertex = this.verArr[index];
                if (distanceMap.ContainsKey(tempVertex.vertexe))
                {
                    distanceMap[tempVertex.vertexe] = e.weight;
                    preVexMap[tempVertex.vertexe] = vertex;
                }
            });
            //主循环，重复遍历最短距离顶点和刷新距离表的操作
            //除起始点外，每个顶点需要遍历一次(外层循环N-1次)
            for(int i = 1; i < this.MAXVEX; i++)
            {
                int minDistanceFromStart = int.MaxValue;
                int minDistanceIndex = -1;
                //找到当前顶点距离最短顶点，为获得距离表的最小值
                for(int j = 0; j < this.MAXVEX; j++)
                {
                    if (!this.verArr[j].visisted && distanceMap[this.verArr[j].vertexe] < minDistanceFromStart)
                    {
                        minDistanceFromStart = distanceMap[this.verArr[j].vertexe];
                        minDistanceIndex = j;
                    }
                }
                if (minDistanceIndex == -1)
                {
                    break;
                }
                this.verArr[minDistanceIndex].visisted = true;
                var secondaryEdges = GetEdgeList(this.verArr[minDistanceIndex]);
                secondaryEdges.ForEach(s =>
                {
                    var index = s.headVex;
                    var tempVertex = this.verArr[index];
                    if (!tempVertex.visisted)
                    {
                        int weight = s.weight;
                        int preDistance = distanceMap[tempVertex.vertexe];
                        if (weight != int.MaxValue && (minDistanceFromStart + weight < preDistance))
                        {
                            distanceMap[tempVertex.vertexe] = minDistanceFromStart + weight;
                            preVexMap[tempVertex.vertexe] = this.verArr[minDistanceIndex].vertexe;
                        }
                    }
                });
            }
            ret.distanceMap = distanceMap;
            ret.preVexMap = preVexMap;
            return ret;
        }

        private List<AdjacencyType> GetEdgeList(VertexeType vertexe)
        {
            List<AdjacencyType> list = new List<AdjacencyType>();
            var temp = vertexe.fristout;
            if (temp != null)
            {
                while (true)
                {
                    if (temp != null)
                    {
                        list.Add(temp);
                        temp = temp.adjLink;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            
            return list;
        }

        /// <summary>
        /// 弗洛伊德算法
        /// </summary>
        /// <returns></returns>
        public List<VertexDistance> Floyd()
        {
            List<VertexDistance> distanceList = new List<VertexDistance>();
            //初始化邻接矩阵
            for(int i = 0; i < this.MAXVEX; i++)
            {
                distanceList.Add(new VertexDistance
                {
                    vertex = this.verArr[i].vertexe,
                    headVex = this.verArr[i].vertexe,
                    distance = 0
                });
                var temp = this.verArr[i].fristout;
                if (temp == null)
                {
                    continue;
                }
                while (true)
                {
                    if (temp != null)
                    {
                        var vertexType = this.verArr[temp.headVex];
                        distanceList.Add(new VertexDistance
                        {
                            vertex = this.verArr[i].vertexe,
                            headVex = vertexType.vertexe,
                            distance = temp.weight
                        });
                        temp = temp.adjLink;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            //Distance(i,j)=Min(Distance(i,j),(Distance(i,k)+Distance(k,j)))
            for(int k = 0; k < this.MAXVEX; k++)
            {
                for(int i = 0; i < this.MAXVEX; i++)
                {
                    for(int j = 0; j < this.MAXVEX; j++)
                    {
                        var startVertex = this.verArr[i];
                        var endVertex = this.verArr[j];
                        var relayVertex = this.verArr[k];
                        var edgeFromStartToRelay = distanceList.Find(x => x.vertex == startVertex.vertexe && x.headVex == relayVertex.vertexe);
                        var edgeFromRealyToEnd = distanceList.Find(x => x.vertex == relayVertex.vertexe && x.headVex == endVertex.vertexe);
                        var edgeFromStartToEnd = distanceList.Find(x => x.vertex == startVertex.vertexe && x.headVex == endVertex.vertexe);
                        if (edgeFromStartToRelay == null || edgeFromStartToRelay.distance == int.MaxValue || edgeFromRealyToEnd == null || edgeFromRealyToEnd.distance == int.MaxValue)
                        {
                            continue;
                        }
                        VertexDistance vertexDistanceFromStartToEnd = new VertexDistance();
                        vertexDistanceFromStartToEnd.vertex = startVertex.vertexe;
                        vertexDistanceFromStartToEnd.headVex = endVertex.vertexe;
                        vertexDistanceFromStartToEnd.distance = Math.Min(edgeFromStartToEnd == null ? int.MaxValue : edgeFromStartToEnd.distance, (edgeFromStartToRelay.distance + edgeFromRealyToEnd.distance));
                        if (edgeFromStartToEnd != null)
                        {
                            edgeFromStartToEnd.distance = vertexDistanceFromStartToEnd.distance;
                        }
                        else
                        {
                            distanceList.Add(vertexDistanceFromStartToEnd);
                        }
                    }
                }
            }
            distanceList.RemoveAll(d => d.headVex == d.vertex);
            return distanceList;
        }

        /// <summary>
        /// 贪心算法
        /// </summary>
        /// <param name="vertex">父节点</param>
        /// <returns></returns>
        public Dictionary<string, string> Prim(string vertex)
        {
            Dictionary<string, string> reachedVertexs = new Dictionary<string, string>();
            reachedVertexs.Add(vertex, "null");
            //边的权重
            int weight = 0;
            //源顶点下标
            int fromIndex = getPosition(vertex);
            //目标顶点下标
            int toIndex = fromIndex;
            this.verArr[fromIndex].visisted = true;
            while (reachedVertexs.Count < this.verArr.Length)
            {
                weight = int.MaxValue;
                //在已触达的顶点中，寻找到达新顶点的最短边
                foreach (var reachedVertex in reachedVertexs.Keys)
                {
                    var vertexe = this.verArr[getPosition(reachedVertex)];
                    var firstout = vertexe.fristout;
                    if (firstout == null)
                    {
                        continue;
                    }
                    while (true)
                    {
                        if (firstout != null)
                        {
                            var vertexType = this.verArr[firstout.headVex];
                            if (!vertexType.visisted)
                            {
                                if (firstout.weight < weight)
                                {
                                    weight = firstout.weight;
                                    fromIndex = firstout.verNum;
                                    toIndex = firstout.headVex;
                                }
                            }
                            firstout = firstout.adjLink;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                this.verArr[toIndex].visisted = true;
                reachedVertexs.Add(this.verArr[toIndex].vertexe, this.verArr[fromIndex].vertexe);
            }

            return reachedVertexs;
        }
    }

    public class VertexDistance
    {
        public string vertex { get; set; }

        public string headVex { get; set; }

        public int distance { get; set; } = int.MaxValue;
    }

    public class DijkstraEntity
    {
        public Dictionary<string,int> distanceMap { get; set; }

        public Dictionary<string,string> preVexMap { get; set; }
    }
}
