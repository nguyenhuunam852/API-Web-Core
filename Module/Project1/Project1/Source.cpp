#include <cstdio>
#include <cstring>
#include <string>
#include <cmath>
#include <cstdlib>
#include <map>
#include <set>
#include <queue>
#include <iostream>
#include <vector>
#include <algorithm>
#include <numeric>
#include <iterator>
#include <functional>
#include <iostream>
#include <vector>
#include <fstream>
#include <iostream>
#include <fstream>
#include <string>
using namespace std;
string ltrim(const string&);
string rtrim(const string&);

vector<vector<int>> check;

int maxResult = 0;
int countTest = 0;


string morganAndString(string a, string b) {
    string greater = a.size() > b.size() ? a : b;
    string smaller = a.size() > b.size() ? b : a;

    int i = 0;
    int j = 0;

    string result = "";

    int saveI = i;
    int saveJ = j;
    bool collect = true;

    while (i < smaller.size() && j<greater.size()) {
        while (i < smaller.size() && smaller[i] < greater[j]) {
            result.push_back(smaller[i]);
            i++;
        }
        while (j <greater.size() && smaller[i]>greater[j]) {
            result.push_back(greater[j]);
            j++;
        }
        if (i >= smaller.size() || j >= greater.size()) break;

        if (smaller[i] == greater[j]) {
            if (i >= saveI || j >=saveJ) {
                saveI = i;
                saveJ = j;
                collect = true;
            }
            while (saveI<smaller.size() && saveJ<greater.size() && smaller[saveI] == greater[saveJ] && collect) {
                saveI++;
                saveJ++;
            }
            if (saveI >= smaller.size() && collect) {
                for (int i = smaller.size(); i <= saveI; i++) smaller[i] = (char)100;
            }
            if (saveJ >= greater.size() && collect) {
                for (int i = greater.size(); i <= saveJ; i++) greater[i] = (char)100;
            }
            if (smaller[saveI] < greater[saveJ]) {
                result.push_back(smaller[i]);
                i++;
            }
            else {
                result.push_back(greater[j]);
                j++;
            } 
            collect = false;
            if (smaller[saveI] == greater[saveJ]) {
                saveI = i;
                saveJ = j;
                collect = true;
            }
        }
    }
    for (int ri = i; ri < smaller.size(); ri++) result.push_back(smaller[ri]);
    for (int rj = j; rj < greater.size(); rj++) result.push_back(greater[rj]);

    return result;
}


int main()
{
    ifstream myfile("file.txt");

    string t_temp;
    getline(myfile, t_temp);

    int t = stoi(ltrim(rtrim(t_temp)));

    for (int t_itr = 0; t_itr < t; t_itr++) {
        string a;
        getline(myfile, a);

        string b;
        getline(myfile, b);

        string result = morganAndString(a, b);
        if (t_itr == 3) {
            cout << result << endl;
        }
    }

    myfile.close();

    return 0;
}

string ltrim(const string& str) {
    string s(str);

    s.erase(
        s.begin(),
        find_if(s.begin(), s.end(), not1(ptr_fun<int, int>(isspace)))
    );

    return s;
}

string rtrim(const string& str) {
    string s(str);

    s.erase(
        find_if(s.rbegin(), s.rend(), not1(ptr_fun<int, int>(isspace))).base(),
        s.end()
    );

    return s;
}

