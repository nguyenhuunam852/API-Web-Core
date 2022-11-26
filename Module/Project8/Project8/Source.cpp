#include<iostream>
#include<string>
#include<vector>

using namespace std;

int main() {
	string input;
	vector<int> vectorInput;

	getline(cin, input);

	int n = stoi(input);

	for (int i = 0; i < n; i++) {
		getline(cin, input);
		vectorInput.push_back(stoi(input));
	}

	vector<vector<int>> matrixValue (n, vector<int>(2,-100001));

	matrixValue[0][1] = vectorInput[0];

	int sizeOfVector = vectorInput.size();
	
	for (int i = 1; i < n;i++) {
		if (matrixValue[i - 1][1] > vectorInput[i] && matrixValue[i - 1][0] > vectorInput[i]) {
			cout << "false" << endl;
			return 0;
		}

		if (matrixValue[i - 1][1] <= vectorInput[i]) {
			matrixValue[i][1] = vectorInput[i];
			matrixValue[i][0] = matrixValue[i-1][1];
		}
		else if (matrixValue[i - 1][0] <= vectorInput[i]) {
			matrixValue[i][0] = vectorInput[i];
			matrixValue[i][1] = matrixValue[i - 1][1];
		}
	}
	cout << "true" << endl;
	return 0;
}