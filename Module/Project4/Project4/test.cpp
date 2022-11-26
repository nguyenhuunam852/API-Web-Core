#include<iostream>
#include<stack>
#include<string>
#include <algorithm> 
#include<vector>

#define Esi '#'
using namespace std;

class node {
   public:
       char input;
       int to;
       node* next;
};

void andd(vector<node*>& v, vector<vector<int>>& st);
void orr(vector<node*>& v, vector<vector<int> >& st);
void closure(vector<node*>& v, vector<vector<int> >& st);

void printNode(vector<node*> v) {
	cout << "___________________________________________" << endl;
	cout << "| from state\t| input\t| tostates" << endl;
	for (int i = 0; i < v.size(); i++) {
		cout << "| " << i << "          \t|";
		node* head = v[i];
		cout << head->input;
		bool first = true;
		while (head != NULL) {
			if (first)
			{
				cout << "     \t|";
				first = false;
			}
			else {
				cout << "     \t";
			}
			cout << head->to;
			head = head->next;
		}
		cout << endl;
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

void addNodeToGraph(char str, vector<node*> &v, vector<vector<int>> &st) {
    node* tempA = makeNode(str);
    v.push_back(tempA);
    st.push_back({ (int)v.size() - 1,  (int)v.size() - 1 });
}

void buildNFA(string r, int& index, vector<node*>& v, vector<vector<int>>& st) {
	string getString= "";
	//((((ab)|a)*)|(((aa)|(bb))*))
    for (int i = index + 1; i < r.size(); i++) {
        if (r[i] == '(') buildNFA(r, i, v, st);
		else if (r[i] == ')') {
			index = i;
			break;
		}
        else getString += r[i];
    }

    if (getString[0] == 'a' || getString[0] == 'b') addNodeToGraph(getString[0], v, st);
    if (getString[1] == 'a' || getString[1] == 'b') addNodeToGraph(getString[1], v, st);

    if (getString == "a" || getString == "b" ||getString == "aa" || getString == "ab" || getString == "bb" || getString == "ba") andd(v, st);
	if (getString[0] == '|' || getString[1] == '|') orr(v, st);
	if (getString == "*" || getString=="a*" || getString == "b*") closure(v, st);
}

int main()
{
	string input;
	int index = 0;

	vector<node*> v;
	vector<vector<int>> st;

	getline(cin, input);
	buildNFA(input, index, v, st);

	printNode(v);
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

	st.pop_back(); 
	st.pop_back();
	st.push_back({firstnode,endnode});

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

	node* adlink = v[x1];
	while (adlink->next != NULL) {
		adlink = adlink->next;
	}
	adlink->to = endnode;
	adlink->next = end;

	node* adlink1 = v[y1];
	while (adlink1->next != NULL) {
		adlink1 = adlink1->next;
	}
	adlink1->to = endnode;
	adlink1->next = end;
}

void andd(vector<node*>& v, vector<vector<int>>& st) {
    int x, y;
    int first, last1;
    y = st[st.size() - 1][0];
    x = st[st.size() - 2][1];

    first = st[st.size() - 2][0];
    last1 = st[st.size() - 1][1];

    st.pop_back();
    st.pop_back();

    st.push_back({ first ,last1});

    node* last = copyNode(v[y]);
    node* lnode = v[x];
    
    while (lnode->next != NULL) lnode = lnode->next;

	lnode->to = y;
    lnode->next = last;
}

void closure(vector<node*>& v, vector<vector<int> >& st) {
	int x, x1;

	x = st[st.size() - 1][0]; 
	x1 = st[st.size() - 1][1]; 

	node* s = makeNode(Esi);
	v.push_back(s);

	int firstnode = v.size() - 1; 
	st.pop_back();

	st.push_back({x,firstnode});

	for (int i = 0; i < v.size() - 2; i++) {
		node* h = v[i];
		while (h->next != NULL) {
			if (h->to == x) {
				h->to = firstnode;
			}
			h = h->next;
		}
	}

	node* t = v[x1];
	while (t->next != NULL) {
		t = t->next;
	}

	t->to = x;
	t->next = copyNode(t);

	t->next->to = firstnode;
	t->next->next = copyNode(s);
}