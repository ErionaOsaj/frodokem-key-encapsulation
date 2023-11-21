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
        Matrix transposed = new Matrix(this.Columns, this.Rows);

        for (int i = 0; i < this.Rows; i++)
        {
            for (int j = 0; j < this.Columns; j++)
            {
                transposed[j, i] = this[i, j];
            }
        }

        return transposed;
    }
    public Matrix Subtract(Matrix other)
    {
        if (this.Rows != other.Rows || this.Columns != other.Columns)
            throw new ArgumentException("Matrices must have the same dimensions for subtraction.");

        Matrix result = new Matrix(this.Rows, this.Columns);
        for (int i = 0; i < this.Rows; i++)
        {
            for (int j = 0; j < this.Columns; j++)
            {
                result[i, j] = this[i, j] - other[i, j];
            }
        }

        return result;
    }


    public Matrix Multiply(Matrix other)
    {
        if (this.Columns != other.Rows)
            throw new ArgumentException("The number of columns in the first matrix must equal the number of rows in the second matrix.");

        Matrix result = new Matrix(this.Rows, other.Columns);
        for (int i = 0; i < result.Rows; i++)
        {
            for (int j = 0; j < result.Columns; j++)
            {
                result[i, j] = 0;
                for (int k = 0; k < this.Columns; k++)
                {
                    result[i, j] += this[i, k] * other[k, j];
                }
            }
        }

        return result;
    }


    public Matrix Add(Matrix other)
    {
        if (this.Rows != other.Rows || this.Columns != other.Columns)
            throw new ArgumentException("Matrices must have the same dimensions to be added.");

        Matrix result = new Matrix(this.Rows, this.Columns);
        for (int i = 0; i < this.Rows; i++)
        {
            for (int j = 0; j < this.Columns; j++)
            {
                result[i, j] = this[i, j] + other[i, j];
            }
        }

        return result;
    }
}
