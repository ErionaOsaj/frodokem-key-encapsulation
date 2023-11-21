using System;
using System.Security.Cryptography;

public class Matrix
{
    public int Rows { get; private set; }
    public int Columns { get; private set; }
    private int[,] data;

    public Matrix(Matrix top, Matrix bottom)
    {
        if (top.Columns != bottom.Columns)
            throw new ArgumentException("Matrices must have the same number of columns to be combined.");

        this.Rows = top.Rows + bottom.Rows;
        this.Columns = top.Columns;
        this.data = new int[this.Rows, this.Columns];

        // Copy the top matrix
        for (int i = 0; i < top.Rows; i++)
        {
            for (int j = 0; j < top.Columns; j++)
            {
                this.data[i, j] = top[i, j];
            }
        }

        // Copy the bottom matrix
        for (int i = 0; i < bottom.Rows; i++)
        {
            for (int j = 0; j < bottom.Columns; j++)
            {
                this.data[i + top.Rows, j] = bottom[i, j];
            }
        }
    }

    public Matrix(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        data = new int[rows, columns];
    }

    public int this[int row, int col]
    {
        get { return data[row, col]; }
        set { data[row, col] = value; }
    }

    public Matrix Transpose()
    {
        throw new NotImplementedException();
    }

    public Matrix Subtract(Matrix other)
    {
        throw new NotImplementedException();
    }

    public Matrix Multiply(Matrix other)
    {
        throw new NotImplementedException();
    }

    public Matrix Add(Matrix other)
    {
        throw new NotImplementedException();
    }
}
