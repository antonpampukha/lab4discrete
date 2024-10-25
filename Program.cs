using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string path = "matrix1dir.txt";
        int[,] incidenceMatrix = ReadMatrixFromFile(path);
        if (!IsCorrectMatrix(incidenceMatrix))
        {
            Console.WriteLine("Not correct matrix");
            return;
        }
        //ShowMatrix(incidenceMatrix, "Incidence matrix");

        #region Choice type of graph  
        bool isDirGraph = false;
        Console.Write("Your matrix is dir? (y/n): ");
        string? buff = Console.ReadLine();
        if (buff == "y")
            isDirGraph = true;
        else if (buff == "n")
            isDirGraph = false;
        else
        {
            Console.WriteLine("Not correct input");
            return;
        }
        #endregion

        int[,] adjacencyMatrix;
        try
        {
            adjacencyMatrix = isDirGraph ?
                ConvertIncidenceToAdjacencyMatrixDirGraph(incidenceMatrix) :
                ConvertIncidenceToAdjacencyMatrixUndirGraph(incidenceMatrix);
        }
        catch
        {
            Console.WriteLine("Not matching matrix type");
            return;
        }

        ShowMatrix(adjacencyMatrix, "Adjacency matrix:");

        if (isDirGraph)
        {
            if (IsCompleteDirGraph(adjacencyMatrix))
                Console.WriteLine("Complete graph");
            else
                Console.WriteLine("Not complete graph");
        }
        else
        {
            if (IsCompleteUndirGraph(adjacencyMatrix))
                Console.WriteLine("Complete graph");
            else
                Console.WriteLine("Not complete graph");
        }

        if (isDirGraph)
        {
            if (IsEulerianDirGraph(adjacencyMatrix))
                Console.WriteLine("Eulerian graph");
            else
                Console.WriteLine("Not Eulerian graph");
        }
        else
        {
            if (IsEulerianUndirGraph(adjacencyMatrix))   //easily using func "IsEulerianDirGraph"
                Console.WriteLine("Eulerian graph");     //to avoid copy code
            else
                Console.WriteLine("Not Eulerian graph");
        }

    }

    static int[,] ReadMatrixFromFile(string path)
    {
        string[] lines = File.ReadAllLines(path);
        int edges = lines.Length;
        int vertices = lines[0].Split(' ').Length;

        int[,] matrix = new int[edges, vertices];

        for (int i = 0; i < edges; i++)
        {
            string[] values = lines[i].Split(' ');
            for (int j = 0; j < vertices; j++)
            {
                matrix[i, j] = int.Parse(values[j]);
            }
        }

        return matrix;
    }
    static public bool IsCorrectMatrix(int[,] matrix)
    {
        int vertices = matrix.GetLength(0);
        int edges = matrix.GetLength(1);
        for (int j = 0; j < edges; j++)
        {
            int incidentVertices = 0;
            for (int i = 0; i < vertices; i++)
            {
                if (matrix[i, j] != 0)
                {
                    incidentVertices++;
                }
            }

            if (incidentVertices != 2 && incidentVertices != 1) // 1 бо петлі
            {
                return false;
            }
        }
        return true;
    }
    static public void ShowMatrix(int[,] matrix, string message = "Matrix")
    {
        Console.WriteLine(message);
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write($"{matrix[i, j]}  ");
            }
            Console.WriteLine();
        }
    }

    static public int[,] ConvertIncidenceToAdjacencyMatrixUndirGraph(int[,] matrix)
    {
        int[,] newMatrix = new int[matrix.GetLength(0), matrix.GetLength(0)];


        for (int j = 0; j < matrix.GetLength(1); j++)
        {

            List<int> index = new() { Capacity = 2 };
            //Search index
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, j] == 1 || matrix[i, j] == 2)
                    index.Add(i);
            }

            //Put index in new matrix
            //Edges
            if (index.Count == 2)
            {
                newMatrix[index[0], index[1]]++;
                newMatrix[index[1], index[0]]++;
            }
            //Loop
            if (index.Count == 1)
            {
                newMatrix[index[0], index[0]] += 2;
            }

        }
        return newMatrix;
    }
    static public int[,] ConvertIncidenceToAdjacencyMatrixDirGraph(int[,] matrix)
    {

        int[,] newMatrix = new int[matrix.GetLength(0), matrix.GetLength(0)];


        for (int j = 0; j < matrix.GetLength(1); j++)
        {

            //List<int> index = new() { Capacity = 2 };
            Dictionary<int, int> index = new();

            //Search index
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, j] == 1) index.Add(1, i);

                if (matrix[i, j] == -1) index.Add(-1, i);

                if (matrix[i, j] == 2) index.Add(2, i);
            }

            //Put index in new matrix
            //Edges
            if (index.Count == 2)
            {
                if (index.Keys.FirstOrDefault(x => x == 1) == 1)
                    newMatrix[index[-1], index[1]]++;
                else if (index.Keys.FirstOrDefault(x => x == -1) == -1)
                    newMatrix[index[1], index[-1]]++;
            }
            //Loop
            if (index.Count == 1)
            {
                newMatrix[index[2], index[2]] += 2;
            }

        }
        return newMatrix;
    }


    static public bool IsCompleteUndirGraph(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = i + 1; j < matrix.GetLength(0); j++)
            {
                if (matrix[i, j] == 0)
                    return false;
            }
        }
        return true;
    }
    static public bool IsCompleteDirGraph(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                if (matrix[i, j] == 0 && i != j)
                    return false;
            }
        }
        return true;
    }
    static public bool IsEulerianUndirGraph(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            int sum = 0;
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                sum += matrix[i, j];
            }
            if (sum % 2 != 0)
                return false;
        }
        return true;
    }
    static public bool IsEulerianDirGraph(int[,] matrix)
    {
        int length = matrix.GetLength(0);
        for (int i = 0; i < length; i++)
        {
            int sumRow = 0;
            int sumCol = 0;

            for (int j = 0; j < length; j++)
            {
                sumRow += matrix[i, j];
            }

            for (int k = 0; k < length; k++)
            {
                sumCol += matrix[k, i];
            }
            if (sumCol != sumRow)
                return false;
        }
        return true;
    }
}