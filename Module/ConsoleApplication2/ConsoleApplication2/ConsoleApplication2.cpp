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

#include <chrono>
#include <iostream>
#include <fstream>
#include <string>
using namespace std::chrono;
using namespace std;

string ltrim(const string&);
string rtrim(const string&);
vector<string> split(const string&);

vector<vector<int>> saveValue(26, vector<int>(0, 0));

vector<unsigned long long> saveFact(50001, 0);

map<int, int> mymap;

/*
 * Complete the 'initialize' function below.
 *
 * The function accepts STRING s as parameter.
 */
unsigned long long power(unsigned long long x,
    unsigned long long y, int p)
{
    unsigned long long res = 1;

    x = x % p;

    while (y > 0)
    {
        if (y & 1)
            res = (res * x) % p;

        y = y >> 1;
        x = (x * x) % p;
    }
    return res;
}

unsigned long long modInverse(unsigned long long n, unsigned long long p)
{
    return power(n, p - 2, p);
}


unsigned long long nCrModPFermat(int n,
    int r, int p)
{
    if (n < r)
        return 0;
    if (r == 0)
        return 1;

    return (saveFact[n] * modInverse(saveFact[r], p) % p
        * modInverse(saveFact[n - r], p) % p)
        % p;
}

void initialize(string s) {

    saveFact[0] = 1;
    for (int i = 1; i <= saveFact.size()-1; i++)
        saveFact[i] = (saveFact[i - 1] * i) % 1000000007;

    for (int i = 0; i < s.size(); i++) {
        int getIndex = s[i] - 'a';
        saveValue[getIndex].push_back(i);
    }
}

/*
 * Complete the 'answerQuery' function below.
 *
 * The function is expected to return an INTEGER.
 * The function accepts following parameters:
 *  1. INTEGER l
 *  2. INTEGER r
 */

int answerQuery(int l, int r, string s) {
    // Return the answer for this query modulo 1000000007.
    auto start = high_resolution_clock::now();

    vector<int> saveQuery(26, 0);

    int length = 0;
    int isAllowOneOdd = true;
    int middleValue = 0;

    for (int i = 0; i < 26; i++) {
        if (saveValue[i].size() > 0 && saveQuery[i] == 0) {
            int nLength = saveValue[i].size();
            int countCharFirst = 0;
            int countCharLast = nLength - 1;
            int goFirst = l;
            int goRight = s.size()-r;
            int appearance = 0;

            if (goFirst + goRight < r - l) {
                while (countCharFirst<nLength - 1 && (l - 1)>saveValue[i][countCharFirst]) countCharFirst++;
                while (countCharLast > 0 && (r - 1) < saveValue[i][countCharLast]) countCharLast--;

                if (countCharFirst == countCharLast
                    && (saveValue[i][countCharFirst] > (r - 1) ||
                        saveValue[i][countCharLast] < (l - 1))) {
                    appearance = 0;
                }
                else {
                    countCharLast = nLength - 1 - countCharLast;
                    appearance = nLength - countCharFirst - countCharLast;
                }
            }
            else {
                for (int j = l - 1; j <= r - 1; j++) {
                    if ((int)(s[j] - 'a') == i) {
                        appearance++;
                    }
                }
            }

            int getSubLength = r - l;

            if (appearance % 2 != 0 && isAllowOneOdd) {
                length += 1;
                isAllowOneOdd = false;
            }

            if (appearance % 2 != 0) {
                middleValue += 1;
                appearance -= 1;
            }

            saveQuery[i] = appearance / 2;

            length += appearance;
        }
    }

    int halfLength = length / 2;
    long long int result = 1;

    for (int i = 0; i < saveQuery.size(); i++) {

        if (saveQuery[i] > 0) {
             /*int index =  halfLength*100000+saveQuery[i];
             int newResult = 0;
             if(mymap.count(index)){
                 newResult = mymap[index];
             }
             else{
                 newResult = nCrModPFermat(halfLength, saveQuery[i], 1000000007);
                 mymap.insert(pair<int, int>(index, newResult));            
             }*/

            int newResult = nCrModPFermat(halfLength, saveQuery[i], 1000000007);
            result *= newResult;
            result = result % 1000000007;
            halfLength -= saveQuery[i];
        }
    }

    auto stop = high_resolution_clock::now();

    auto duration = duration_cast<microseconds>(stop - start);

    cout << "Time taken by function: "
        << duration.count() << " microseconds" << endl;


    if (length % 2 != 0) return (result * middleValue) % 1000000007;
    return result % 1000000007;
}

int main()
{
    string line;
    ifstream myfile("file.txt");

    string s;
    getline(myfile, s);

    initialize(s);

    string q_temp;
    getline(myfile, q_temp);

    int q = stoi(ltrim(rtrim(q_temp)));
    cout << "s:" << s.size() << endl;

    for (int q_itr = 0; q_itr < q; q_itr++) {

        //cout << "i:" << q_itr << endl;

        string first_multiple_input_temp;
        getline(myfile, first_multiple_input_temp);

        vector<string> first_multiple_input = split(rtrim(first_multiple_input_temp));

        int l = stoi(first_multiple_input[0]);

        int r = stoi(first_multiple_input[1]);
        cout << "l:" << l<<" r:"<<r << endl;

        int result = answerQuery(l, r, s);

        //cout << result << endl;
        //fout << result << "\n";
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

vector<string> split(const string& str) {
    vector<string> tokens;

    string::size_type start = 0;
    string::size_type end = 0;

    while ((end = str.find(" ", start)) != string::npos) {
        tokens.push_back(str.substr(start, end - start));

        start = end + 1;
    }

    tokens.push_back(str.substr(start));

    return tokens;
}
