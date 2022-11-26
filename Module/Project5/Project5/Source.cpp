#include<iostream>
#include<stack>
#include<string>
#include <algorithm> 
#include<vector>
#include<map>
#include<string>

#define Esi '#'
#define MM 1000000007

using namespace std;

class node {
public:
	char input;
	int to;
	node* next;
	bool deleted = false;
};

map<int, vector<string>> mappingState;
map<int, string> mappingClosure;
vector<vector<long>> matrixLong (100,vector<long>(100,0)) ;


void andd(vector<node*>& v, vector<vector<int>>& st);
void orr(vector<node*>& v, vector<vector<int> >& st);
void closure(vector<node*>& v, vector<vector<int> >& st);
int matrixExponent(vector<vector<long>> aMatrix,int realN, int k);

void printNode(vector<node*> v) {
	cout << "___________________________________________" << endl;
	cout << "| from state\t| input\t| tostates" << endl;
	for (int i = 0; i < v.size(); i++) {
		node* head = v[i];
		if (!head->deleted) {
			cout << "| " << i << "          \t|";
			cout << head->input;
			bool first = true;
			while (head != NULL) {
				if (first)
				{
					cout << "     \t|";
					first = false;
				}
				else {
					cout << "  ";
				}
				cout << head->to;
				head = head->next;
			}
			cout << endl;
		}
		// cout<<"\t\t\t\t\t\t|"<<endl;
	}
	cout << "___________________________________________" << endl;
}

node* makeNode(char in) {
	node* a = new node;
	a->input = in;
	a->to = -1;
	a->next = NULL;
	return a;
}

node* copyNode(node* copy) {
	node* a = new node;
	a->input = copy->input;
	a->to = -1;
	a->next = NULL;
	return a;
}

void addNodeToGraph(char str, vector<node*>& v, vector<vector<int>>& st) {
	node* tempA = makeNode(str);
	v.push_back(tempA);
	st.push_back({ (int)v.size() - 1,  (int)v.size() - 1 });
}

vector<int> splitString(string s , vector<node*>& v)
{
	vector<int> returnList(v.size(),0);

	string del = "*";
	int start, end = -1 * del.size();
	do {
		start = end + del.size();
		end = s.find(del, start);
		string index = s.substr(start, end - start);

		if (index == "") continue;

		int current = stoi(index);

		if (current < 0) continue;

		returnList[current] = 1;

	} while (end != -1);

	return returnList;
}

void buildNFA(string r, int& index, vector<node*>& v, vector<vector<int>>& st) {
	string getString = "";
	int countChar = 0;

	for (int i = index + 1; i < r.size(); i++) {
		if (r[i] == '(') buildNFA(r, i, v, st);
		else if (r[i] == ')') {
			index = i;
			break;
		}
		else if (r[i] == 'a' || r[i] == 'b') {
			countChar++;
			addNodeToGraph(r[i], v, st);
		}
		else getString += r[i];
	}

	if (getString == "") andd(v, st);
	else {
		if (getString == "|") orr(v, st);
		if (getString == "*") closure(v, st);
	}
}

string uniqueState(string v, vector<node*>& listNodes) {
	vector<int> listReturn = splitString(v, listNodes);

	string ss = "";
	for (int i = 0; i < listReturn.size(); i++) {
		if(listReturn[i] == 1)
	    	ss += ("*" + to_string(i));
	}
	return ss;
}

string getClosure(int index, vector<node*> v) {
	node* current = v[index];

	if (mappingClosure.find(index) == mappingClosure.end()) {
		mappingClosure[index] += ("*" + to_string(index));
	    if (current->input == Esi) {
	    		while (current->next != NULL) {
	    			mappingClosure[index] += ("*" + to_string(current->to));
	    			string result = getClosure(current->to, v);
	    			mappingClosure[index] += result;
	    			current = current->next;
	    		}
	    }
	}
	mappingClosure[index] = uniqueState(mappingClosure[index], v);
	return mappingClosure[index];
}

int nfaToDfa(string currentState,vector<node*> v, vector<vector<int>>& st) {
	if(currentState!=""){
	      string closureState = "";
	      
	      vector<int> listState = splitString(currentState, v);
	      
		  for (int index = 0; index < listState.size(); index++) {
			  if(listState[index] == 1)
			     closureState += getClosure(index, v);
		  }

		  closureState = uniqueState(closureState, v);

		  bool exist = false;
		  int currentIndex = -1;
		  for (pair<int, vector<string>> value : mappingState) {
			  if (value.second[0] == closureState)
              {
				 currentIndex = value.first;
                 exist = true;
				 break;
              }
		  } 
	   
		  if (!exist) {
			  currentIndex = mappingState.size();

			  mappingState[currentIndex] = vector<string>{ closureState ,"", "","","","" };

			  vector<int> listReturn = splitString(closureState, v);

			  for (int index = 0; index < listReturn.size(); index++) {
				  if (listReturn[st[0][1]] == 1)  mappingState[currentIndex][5] = "1";
				  if (listReturn[index] == 1)
				  {
					  node* current = v[index];
					  if (current->input == 'a') mappingState[currentIndex][1] += ("*" + to_string(current->to));
					  if (current->input == 'b') mappingState[currentIndex][2] += ("*" + to_string(current->to));
				  }
			  };

			  int aWay = nfaToDfa(mappingState[currentIndex][1], v , st);
			  mappingState[currentIndex][3] = to_string(aWay);
			  if(aWay > -1) matrixLong[currentIndex][aWay] = 1;

			  int bWay = nfaToDfa(mappingState[currentIndex][2], v, st);
			  mappingState[currentIndex][4] = to_string(bWay);
			  if (bWay > -1) matrixLong[currentIndex][bWay] = 1;

			  return currentIndex;
		  }
		  else return currentIndex;
	}
	return -1;
}

int main()
{
	string input;

	getline(cin, input);

	int amount = stoi(input);

	for (int i = 0; i < amount; i++) {
		getline(cin, input);
        
		bool regexCollect = true;
		int index = 0;

		string regex = "";
		string number = "";

		vector<node*> v;
		vector<vector<int>> st;

		mappingState.clear();
		mappingClosure.clear();
		matrixLong = vector<vector<long>>(1000, vector<long>(1000, 0));

		for (char c: input) {
			if (c == ' ') regexCollect = false;
			if (regexCollect) regex.push_back(c);
			else number.push_back(c);
		}

		buildNFA(regex, index, v, st);
		//printNode(v);
		if (v[st[0][1]]->input != Esi) {
			addNodeToGraph(Esi, v, st);
			andd(v, st);
		}

		nfaToDfa(("*" + to_string(st[0][0])), v, st);

		cout << matrixExponent(matrixLong, (int)mappingState.size(), stoi(number)) << endl;
	}
}

void orr(vector<node*>& v, vector<vector<int> >& st) {
	int x, y, x1, y1;
	x = st[st.size() - 2][0];
	y = st[st.size() - 1][0];

	x1 = st[st.size() - 2][1];
	y1 = st[st.size() - 1][1];

	node* start = makeNode(Esi);
	node* end = makeNode(Esi);

	v.push_back(start);
	int firstnode = v.size() - 1;
	v.push_back(end);
	int endnode = v.size() - 1;

	node* adlink = v[x1];
	node* adlink1 = v[y1];
	node* link = NULL;
	node* link1 = NULL;
	int linkIndex = -1;
	int link1Index = -1;

	if (adlink->input!='#') {
		link = makeNode(Esi);
		v.push_back(link);
		linkIndex = v.size() - 1;

		link->to = endnode;
		link->next = copyNode(end);
	}
	if (adlink1->input != '#') {
		link1 = makeNode(Esi);
		v.push_back(link1);
		link1Index = v.size() - 1;

		link1->to = endnode;
		link1->next = copyNode(end);
	}

	st.pop_back();
	st.pop_back();
	st.push_back({ firstnode,endnode });

	for (int i = 0; i < v.size() - 2; i++) {
		node* h = v[i];
		while (h->next != NULL) {
			if (h->to == x || h->to == y) h->to = firstnode;
			h = h->next;
		}
	}

	node* temp = copyNode(v[x]);
	node* temp1 = copyNode(v[y]);
	node* t = v[firstnode];

	while (t->next != NULL) t = t->next;

	t->to = x;
	t->next = temp;

	t->next->to = y;
	t->next->next = temp1;

	while (adlink->next != NULL) {
		adlink = adlink->next;
	}
	adlink->to = (linkIndex!=-1) ? linkIndex: endnode;
	adlink->next = (linkIndex != -1) ? copyNode(link): copyNode(end);

	//adlink->to = endnode;
	//adlink->next = copyNode(end);

	while (adlink1->next != NULL) {
		adlink1 = adlink1->next;
	}
	adlink1->to = (link1Index != -1) ? link1Index : endnode;
	adlink1->next = (link1Index != -1) ? copyNode(link1) : copyNode(end);
	//adlink1->to = endnode;
	//adlink1->next = copyNode(end);
}

void andd(vector<node*>& v, vector<vector<int>>& st) {
	int x, y;
	int first, last1;
	y = st[st.size() - 1][0];
	x = st[st.size() - 2][1];

	//node* end = makeNode(Esi);
	//v.push_back(end);
	//int endIndex = v.size() - 1;

	first = st[st.size() - 2][0];
	last1 = st[st.size() - 1][1];

	st.pop_back();
	st.pop_back();

	st.push_back({ first , last1 }); // endIndex

	node* last = copyNode(v[y]);
	node* lnode = v[x];
	
	if (lnode->input == '#') {
		lnode->deleted = true;

		for (int i = 0; i < v.size(); i++) {
			node* h = v[i];
			while (h->next != NULL) {
				if (h->to == x) {
					h->to = y;
					lnode->next = last;
				}
				h = h->next;
			}
		}
	}
	else {
		while (lnode->next != NULL) lnode = lnode->next;

		lnode->to = y;
		lnode->next = last;
	}
	//node* endLast = copyNode(v[endIndex]);
	//node* lNode = v[last1];

	//while (lNode->next != NULL) lNode = lNode->next;

	//lNode->to = endIndex;
	//lNode->next = endLast;
}

void closure(vector<node*>& v, vector<vector<int>>& st) {
	int x, x1;

	x = st[st.size() - 1][0];
	x1 = st[st.size() - 1][1];

	node* startNode = makeNode(Esi);
	node* endNode = makeNode(Esi);

	v.push_back(startNode);
	int firstNode = v.size() - 1;
	v.push_back(endNode);
	int finishNode = v.size() - 1;

	node* adlink = v[x1];
	int linkIndex = -1;

	if (adlink->input != '#') {
		node* link = makeNode(Esi);
		v.push_back(link);
		linkIndex = v.size() - 1;

		node* getLNode = v[x1];
		while (getLNode->next != NULL) getLNode = getLNode->next;
		getLNode->to = linkIndex;
		getLNode->next = copyNode(link);

		x1 = linkIndex;
	}

	st.pop_back();
	st.push_back({ firstNode, finishNode });

	for (int i = 0; i < v.size() - 2; i++) {
		node* h = v[i];
		while (h->next != NULL) {
			if (h->to == x) h->to = firstNode;
			h = h->next;
		}
	}

	node* temp = v[x];
	node* t = v[firstNode];

	node* getNodeX1 = v[x1];

	while (getNodeX1->next != NULL) getNodeX1 = getNodeX1->next;
	getNodeX1->to = finishNode;
	getNodeX1->next = copyNode(endNode);

	getNodeX1->next->to = x;
	getNodeX1->next->next = copyNode(temp);

	while (t->next != NULL) t = t->next;
	t->to = x;
	t->next = copyNode(temp);

	t->next->to = finishNode;
	t->next->next = copyNode(endNode);
}

int matrixExponent(vector<vector<long>> aMatrix,int realN, int k) {

	int n = realN;

	vector<vector<long>> X (n, vector<long>(n, 0));
	vector<vector<long>> Y (n, vector<long>(n, 0));
	vector<long> temp (n,0);
	vector<vector<long>> initalMatrix (n, vector<long>(n, 0));

	for (int i = 0; i < n; i++) {
		for (int j = 0; j < n; j++) {
			if (i == j) X[i][j] = 1;
			else X[i][j] = 0;
		}
	}

	int run = 1;

	while (2 * run <= k) run *= 2;

	while (run > 0) {
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < n; j++) {
				Y[i][j] = X[i][j];
			}
		}

		for (int i = 0; i < n; i++) {
			for (int j = 0; j < n; j++) {
				X[i][j] = 0;
				for (int l = 0; l < n; l++) {
					X[i][j] += Y[i][l] * Y[l][j];
					X[i][j] %= MM;
				}
			}
		}

		if (k >= run) {
			k -= run;
			for (int i = 0; i < n; i++) {
				for (int j = 0; j < n; j++) {
					temp[j] = X[i][j];
				}
				for (int j = 0; j < n; j++) {
					X[i][j] = 0;
					for (int l = 0; l < n; l++) {
						X[i][j] += temp[l] * aMatrix[l][j];
						X[i][j] %= MM;
					}
				}
			}
		}
		run /= 2;
	}

	int result = 0;
	for (int i = 0; i < X.size(); i++) {
		if(mappingState[i][5]=="1") result += X[0][i];
	}
	return result;
}

//