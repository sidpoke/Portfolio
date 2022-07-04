#include <iostream>
#include "DynamicArray.h"

template <typename T>
void printMyArray(DynamicArray<T>& theThing)
{
    for (auto& g : theThing)
    {
        std::cout << g << std::endl;
    }
}

int main()
{
    DynamicArray<int> da;

    da.push_back(int(42));
    da.push_back(int(1984));
    da.push_back(int(666));

    da.push_front(int(1234));
    da.push_front(int(123));
    da.push_front(int(12));
    da.push_front(int(1));

    printMyArray<int>(da);

    int i = 0;
    std::cin >> i;
}

