#include <iostream>
#include <fstream>
#include <iomanip>
#include <vector>
#include <math.h>
using namespace std;

#define ROWS 4
#define EPS 1e-4

// Loads matrix from file
vector<vector<double>> get_matrix(string file_name)
{
	vector<vector<double>> matrix;

	ifstream input_file(file_name);

	for (int i = 0; i < ROWS; i++)
	{
		vector<double> row;
		for (int j = 0; j < ROWS + 1; j++)
		{
			double x;
			input_file >> x;
			row.push_back(x);
		}
		matrix.push_back(row);
	}

	input_file.close();

	return matrix;
}

// Prints matrix
void print_matrix(vector<vector<double>> matrix)
{
	for (int i = 0; i < matrix.size(); i++)
	{
		for (int j = 0; j < matrix[i].size(); j++)
			cout << setw(15) << setprecision(2) << matrix[i][j];
		cout << endl;
	}
	cout << endl;
}




// Returns matrix m-norm
double m_norm(vector<vector<double>> matrix)
{
	double max;
	for (int j = 0; j < ROWS; j++)
	{
		double sum = 0;
		for (int i = 0; i < ROWS; i++)
			if (i != j)
				sum += abs(matrix[i][j]);
		sum /= abs(matrix[j][j]);

		if (!j || sum > max)
			max = sum;
	}

	return max;
}

// Returns "is the every xi - x_previ grosser then eps?"
bool grosser_then_eps(vector<vector<double>> matrix, vector<double> prev, vector<double> curr)
{
	double q = m_norm(matrix);
	for (int i = 0; i < ROWS; i++)
		if (abs(curr[i] - prev[i]) > (1 - q) * EPS / q)
			return true;
	return false;
}

// Copyes <from>-vector to <to>-vector
void copy_vector(vector<double> &from, vector<double> &to)
{
	for (int i = 0; i < from.size(); i++)
		to[i] = from[i];
}

// Adds one row to other
void add_row_to_row(vector<vector<double>> &matrix, int to, int what, double coef)
{
	for (int i = 0; i <= ROWS; i++)
		matrix[to][i] += coef * matrix[what][i];
}

// Swaps rows
void swap_rows(vector<vector<double>> &matrix, int first, int second)
{
	for (int i = 0; i <= ROWS; i++)
	{
		double buf = matrix[first][i];
		matrix[first][i] = matrix[second][i];
		matrix[second][i] = buf;
	}
}

// On paper maked changes to matrix
void make_iterative(vector<vector<double>> &matrix)
{
	swap_rows(matrix, 0, 3);

	add_row_to_row(matrix, 1, 0, -matrix[1][0] / matrix[0][0]);
	add_row_to_row(matrix, 2, 0, -matrix[2][0] / matrix[0][0]);
	add_row_to_row(matrix, 3, 0, -matrix[3][0] / matrix[0][0]);

	add_row_to_row(matrix, 0, 3, -matrix[0][3] / matrix[3][3]);
	add_row_to_row(matrix, 1, 3, -matrix[1][3] / matrix[3][3]);
	add_row_to_row(matrix, 2, 3, -matrix[2][3] / matrix[3][3]);

	add_row_to_row(matrix, 0, 2, -matrix[0][2] / matrix[2][2]);
}

// Prints roots
void print_roots(vector<double> roots)
{
	for (short i = 0; i < roots.size(); i++)
		cout << setprecision(15) << roots[i] << endl;
	cout << endl;
}

// Direct iteration (second task)
vector<double> direct_iteration(vector<vector<double>> matrix)
{
	// Roots for return
	vector<double> roots;
	vector<double> prev_roots(ROWS);

	make_iterative(matrix);

	// Roots initialisation 
	for (int i = 0; i < ROWS; i++)
	{
		double root = matrix[i][ROWS] / matrix[i][i];
		roots.push_back(root);
	}

	do
	{
		copy_vector(roots, prev_roots);
		for (int i = 0; i < ROWS; i++)
		{
			roots[i] = matrix[i][ROWS];
			for (int j = 0; j < ROWS; j++)
				if (i != j)
					roots[i] -= matrix[i][j] * prev_roots[j];
			roots[i] /= matrix[i][i];
		}
	}
	while (grosser_then_eps(matrix, prev_roots, roots));

	return roots;
}


// Complite pivoting (first task)
vector<double> gauss(vector<vector<double>> matrix)
{
	vector<double> roots(matrix.size(), 0);

	for (int j = 0; j < matrix.size() - 1; j++)
		for (int i = j + 1; i < matrix.size(); i++)
			add_row_to_row(matrix, i, j, -matrix[i][j] /(double) matrix[j][j]);

	for (int j = matrix.size() - 1; j > 0; j--)
		for (int i = j - 1; i >= 0; i--)
			add_row_to_row(matrix, i, j, -matrix[i][j] /(double) matrix[j][j]);

	for (int i = 0; i < matrix.size(); i++)
		roots[i] = matrix[i][matrix.size()] /(double) matrix[i][i];

	return roots;
}




int main()
{
	vector<vector<double>> matrix = get_matrix("Input.txt");

	vector<double> roots1 = gauss(matrix);
	cout << "Gauss" << endl;
	print_roots(roots1);

	vector<double> roots2 = direct_iteration(matrix);
	cout << endl << "Direct iteration" << endl;
	print_roots(roots2);
	system("pause");
	return 0;
 }