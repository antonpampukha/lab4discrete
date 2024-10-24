using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string path = "matrix3.txt";
        int[,] incidenceMatrix = ReadMatrixFromFile(path);
        if (!IsCorrectMatrix(incidenceMatrix))
        {
            Console.WriteLine("Not correct matrix");
            return;
        }
        Console.WriteLine([]);

        int[,] newMat = ConvertToMatrix(incidenceMatrix);
        int k =  newMat.GetLength(0);
        for (int i = 0; i < k; i++)
        {
            for (int j = 0; j < k; j++)
            {
                Console.Write($"{newMat[i, j]}  ");
            }
            Console.WriteLine();
        }

        int isComplete = 0;
        bool isEulerian = false;

        #region switch
        //if (ChoiceUndirectedOrDirectedGraph())
        //{
        //    int type = ChoiceTypeOfGraph();
        //    switch (type)
        //    {
        //        case 1:
        //            isComplete = IsUndirectedSimpleGraphComplete(incidenceMatrix);
        //            isEulerian = IsEulerianUndirectedSimpleGraph(incidenceMatrix);
        //            break;
        //        case 2:
        //            //isComplete = IsUndirectedPseudographComplete(incidenceMatrix);
        //            isComplete = 2;
        //            isEulerian = IsEulerianUndirectedPseudograph(incidenceMatrix);
        //            break;
        //        case 3:
        //            //isComplete = IsUndirectedMultigraphComplete(incidenceMatrix);
        //            isComplete = 2;
        //            isEulerian = IsEulerianUndirectedMultigraph(incidenceMatrix);
        //            break;
        //        default:
        //            Console.WriteLine("False choice.");
        //            return;
        //    }
        //}
        //else
        //{
        //    int type = ChoiceTypeOfGraph();
        //    switch (type)
        //    {
        //        case 1:
        //            isComplete = IsDirectedSimpleGraphComplete(incidenceMatrix);
        //            if (isComplete == 1)
        //                isEulerian = true;
        //            else
        //                isEulerian = IsEulerianDirectedSimpleGraph(incidenceMatrix);
        //            break;
        //        case 2:

        //            break;
        //        default:
        //            Console.WriteLine("Невірний вибір.");
        //            return;
        //    }
        //}
        #endregion

        if (isComplete == 1)
        {
            Console.WriteLine("Граф є повним.");
        }
        else if (isComplete == 0)
        {
            Console.WriteLine("Граф не є повним.");
        }
        else
        {
            Console.WriteLine("Graph can't be completed");
        }

        if (isEulerian)
        {
            Console.WriteLine("Граф є ейлеровим.");
        }
        else
        {
            Console.WriteLine("Граф не є ейлеровим.");
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
    static public bool ChoiceUndirectedOrDirectedGraph()
    {
        Console.WriteLine("Оберіть тип графа:");
        Console.WriteLine("1. Undir");
        Console.WriteLine("2. Dir");
        int num = int.Parse(Console.ReadLine());
        while (true)
        {
            switch (num)
            {
                case 1:
                    return true;
                case 2:
                    return false;
                default:
                    Console.WriteLine("Not correct input");
                    break;
            }
        }
    }
    static public int ChoiceTypeOfGraph()
    {
        while (true)
        {
            Console.WriteLine("Оберіть тип графа:");
            Console.WriteLine("1. Простий граф");
            Console.WriteLine("2. Псевдограф");
            Console.WriteLine("3. Мультиграф");
            int choice = int.Parse(Console.ReadLine());
            if (choice == 1 || choice == 2 || choice == 3)
                return choice;
            else Console.WriteLine("Not correct input");
        }

    }

    static public int[,] ConvertToMatrix(int[,] matrix)
    {
        int[,] newMatrix = new int[matrix.GetLength(0), matrix.GetLength(0)];
        //int length = newMatrix.GetLength(0); = 3

        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            //int firstIndex = -1;
            //int secondIndex = -1;
            //int loop = -1;
            //bool isUsed = false;
            List<int> index = new() { Capacity = 2 };
            //Search index
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                /*if (matrix[i, j] == 1 && firstIndex != 1)
                {
                    firstIndex = i;
                    isUsed = true;
                    continue;
                }
                if (matrix[i, j] == 1 && isUsed)
                {
                    secondIndex = i;
                    isUsed = false;
                }
                if (matrix[i, j] == 2)
                {
                    loop = i;
                }
                Console.WriteLine("--------");
                Console.WriteLine(firstIndex);
                Console.WriteLine(secondIndex);*/
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


    ////НЕОРІЄНТОВАНІ
    //// Метод для перевірки, чи є простий граф повним
    //static int IsUndirectedSimpleGraphComplete(int[,] matrix)
    //{
    //    int vertices = matrix.GetLength(0);
    //    int edges = matrix.GetLength(1);
    //    int expectedEdges = (vertices * (vertices - 1)) / 2;

    //    if (edges != expectedEdges)
    //    {
    //        return 0;
    //    }

    //    return 1;
    //}

    // //Метод для перевірки, чи є псевдограф повним
    //static bool IsUndirectedPseudographComplete(int[,] matrix)
    //{
    //    int vertices = matrix.GetLength(0);
    //    int edges = matrix.GetLength(1);
    //    int expectedEdges = (vertices * (vertices - 1)) / 2 + vertices; // Враховуємо петлі до кожної вершини

    //    if (edges < expectedEdges)
    //    {
    //        return false;
    //    }

    //    return true;
    //}

    ////Метод для перевірки, чи є мультиграф повним
    //static bool IsUndirectedMultigraphComplete(int[,] matrix)
    //{
    //    int vertices = matrix.GetLength(0);
    //    int edges = matrix.GetLength(1);
    //    int expectedEdges = (vertices * (vertices - 1)) / 2;

    //    if (edges < expectedEdges)
    //    {
    //        return false;
    //    }

    //    return true;
    //}

    ////Перевірка ейлеровості для простого графа

    //static bool IsEulerianUndirectedSimpleGraph(int[,] matrix)
    //{
    //    int vertices = matrix.GetLength(0);
    //    int[] degrees = new int[vertices];

    //    // Обчислюємо степінь кожної вершини
    //    for (int j = 0; j < matrix.GetLength(1); j++)
    //    {
    //        for (int i = 0; i < vertices; i++)
    //        {
    //            if (matrix[i, j] != 0)
    //            {
    //                degrees[i]++;
    //            }
    //        }
    //    }

    //    // Перевіряємо на парність степенів
    //    foreach (var degree in degrees)
    //    {
    //        if (degree % 2 != 0)
    //        {
    //            return false; // Якщо є непарна степінь, граф не є ейлеровим
    //        }
    //    }

    //    return true;
    //}

    //// Перевірка ейлеровості для псевдографа
    //static bool IsEulerianUndirectedPseudograph(int[,] matrix)
    //{
    //    int vertices = matrix.GetLength(0);
    //    int[] degrees = new int[vertices];

    //    // Обчислюємо степінь кожної вершини, враховуючи петлі
    //    for (int j = 0; j < matrix.GetLength(1); j++)
    //    {
    //        int loopCount = 0;
    //        for (int i = 0; i < vertices; i++)
    //        {
    //            if (matrix[i, j] == 2) // Петля додає два до степеня вершини
    //            {
    //                degrees[i] += 2;
    //                loopCount++;
    //            }
    //            else if (matrix[i, j] == 1)
    //            {
    //                degrees[i]++;
    //            }
    //        }

    //        if (loopCount > 1) // Якщо є більше ніж одна петля на одному ребрі
    //        {
    //            return false;
    //        }
    //    }

    //    // Перевіряємо на парність степенів
    //    foreach (var degree in degrees)
    //    {
    //        if (degree % 2 != 0)
    //        {
    //            return false;
    //        }
    //    }

    //    return true;
    //}

    //// Перевірка ейлеровості для мультиграфа
    //static bool IsEulerianUndirectedMultigraph(int[,] matrix)
    //{
    //    int vertices = matrix.GetLength(0);
    //    int[] degrees = new int[vertices];

    //    // Обчислюємо степінь кожної вершини, враховуючи кратні ребра
    //    for (int j = 0; j < matrix.GetLength(1); j++)
    //    {
    //        for (int i = 0; i < vertices; i++)
    //        {
    //            if (matrix[i, j] != 0)
    //            {
    //                degrees[i]++;
    //            }
    //        }
    //    }

    //    // Перевіряємо на парність степенів
    //    foreach (var degree in degrees)
    //    {
    //        if (degree % 2 != 0)
    //        {
    //            return false;
    //        }
    //    }

    //    return true;
    //}



    ////ОРІЄНТОВАНІ
    //// Метод для перевірки, чи є простий граф повним
    //static int IsDirectedSimpleGraphComplete(int[,] matrix)
    //{
    //    int vertices = matrix.GetLength(0);
    //    int edges = matrix.GetLength(1);
    //    int expectedEdges = vertices * (vertices - 1);

    //    if (edges != expectedEdges)
    //    {
    //        return 0;
    //    }

    //    //перевірка матриці???
    //    //for (int j = 0; j < edges; j++)
    //    //{
    //    //    int toVertex = 0;
    //    //    int fromVertex = 0;
    //    //    for (int i = 0; i < vertices; i++)
    //    //    {
    //    //        if (matrix[i, j] == 1) toVertex++;
    //    //        if (matrix[i, j] == -1) fromVertex++;
    //    //    }

    //    //    if (toVertex != 1 || fromVertex != 1)
    //    //    {
    //    //        return false;
    //    //    }
    //    //}

    //    return 1;
    //}

    ////// Метод для перевірки, чи є мультиграф повним
    ////static bool IsDirectedMultiraphComplete(int[,] matrix) { return false; }

    ////// Метод для перевірки, чи є псевдограф повним
    ////static bool IsDirectedPseudographComplete(int[,] matrix) { return false; }

    //// Перевірка ейлеровості для простого графа

    //static bool IsEulerianDirectedSimpleGraph(int[,] matrix)
    //{
    //    int vertices = matrix.GetLength(0);
    //    int[] inDegrees = new int[vertices];
    //    int[] outDegrees = new int[vertices];

    //    // Обчислюємо вхідні і вихідні степені для кожної вершини
    //    for (int j = 0; j < matrix.GetLength(1); j++)
    //    {
    //        for (int i = 0; i < vertices; i++)
    //        {
    //            if (matrix[i, j] == 1) outDegrees[i]++;
    //            if (matrix[i, j] == -1) inDegrees[i]++;
    //        }
    //    }

    //    // Перевіряємо чи для кожної вершини вхідні степені рівні вихідним
    //    for (int i = 0; i < vertices; i++)
    //    {
    //        if (inDegrees[i] != outDegrees[i])
    //        {
    //            return false;
    //        }
    //    }

    //    return true;
    //}

    ////Перевірка ейлеровості для мультиграфа
    //static bool IsEulerianDirectedMultigraph(int[,] matrix) { return false; }

    ////Перевірка ейлеровості для простого графа
    //static bool IsEulerianDirectedPseudograph(int[,] matrix) { return false; }
}