#include<iostream>
#include<string>
#include<vector>

using namespace std;

int minimumSubLength(int length,vector<int> inputArray, int k) {
	int n = inputArray.size();
	if (n < length-1) return 0;

	for (int i = 0; i < n - (length-1); i++) {
		int sum = 0;
		for (int j = 0; j < length; j++) {
			sum += inputArray[i+j];
		}
		if (sum >= k) return length;
	}

	int check = minimumSubLength(length + 1, inputArray, k);
	if (check > 0) return check;
	return 0;
}

int main() {
	int k = 7;
	vector<int> vectorInput{ 2,2,4 };

	cout << minimumSubLength(1, vectorInput, k) << endl;
	return 0;
}