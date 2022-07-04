template <typename T>
DynamicArray<T>::DynamicArray()
{
	m_pData = nullptr;
}

template <typename T>
DynamicArray<T>::~DynamicArray() 
{
	destroy();
}

template <typename T> 
void DynamicArray<T>::clear()
{
	resize(0);
}

template <typename T>
void DynamicArray<T>::create(size_t initialCapacity)
{
	reserve(initialCapacity);
}

template <typename T>
void DynamicArray<T>::destroy()
{
	for (size_t i = 0; i < m_length; i++)
	{
		get(i)->~T();
	}

	free(m_pData);
}

template <typename T>
bool DynamicArray<T>::isEmpty() const
{
	return m_length == 0u;
}

template <typename T>
bool DynamicArray<T>::hasElements() const
{
	return m_length != 0u;
}

template <typename T>
size_t DynamicArray<T>::getLength() const
{
	return m_length;
}

template <typename T>
size_t DynamicArray<T>::getCapacity() const
{
	return m_capacity;
}

template <typename T>
void DynamicArray<T>::resize(size_t length)
{
	reserve(length);

	for (size_t i = length; i < m_length; ++i)
	{
		get(i)->~T();
	}

	for (size_t i = m_length; i < length; ++i)
	{
		new(get(i))T;
	}

	m_length = length;
}

template <typename T>
void DynamicArray<T>::reserve(size_t length)
{
	if (length <= m_capacity)
	{
		return;
	}

	void* newData = calloc(length, sizeof(T));								//create new void* with desired size with calloc
	memcpy_s(newData, length * sizeof(T), m_pData, m_length * sizeof(T));	//copy all old elements to new void* with memcpy
	free(m_pData);															//free old void*
	m_pData = newData;														//override old void* with new one

	m_capacity = length;	//set new capacity
}

template <typename T>
T& DynamicArray<T>::push_back()
{
	resize(m_length + 1);
	return *get(m_length - 1);
}

template <typename T>
void		DynamicArray<T>::push_back(const T& value)
{
	push_back() = value;
}

template <typename T>
T& DynamicArray<T>::push_front()
{
	reserve(m_length + 1);
	memcpy_s(get(1), m_length * sizeof(T), get(0u), m_length * sizeof(T));
	m_length++;

	T* pElement = get(0);
	new(pElement) T;
	return *pElement;
}

template <typename T>
void DynamicArray<T>::push_front(const T& value)
{
	push_front() = value;
}

template <typename T>
void DynamicArray<T>::pop_back()
{
	resize(m_length - 1);
}

template <typename T>
void DynamicArray<T>::pop_back(T& target) 
{
	target = *get(m_length - 1);
	pop_back();
}

template <typename T>
void DynamicArray<T>::pop_front()
{
	removeSortedByIndex(0);
}

template <typename T>
void DynamicArray<T>::pop_front(T& target)
{
	target = *get(0);
	pop_front();
}

template <typename T>
void DynamicArray<T>::removeSortedByIndex(size_t index)
{
	get(index)->~T();

	for (size_t i = index + 1; i < m_length; ++i)
	{
		memcpy_s(get(i - 1), sizeof(T), get(i), sizeof(T));
	}

	m_length--;
}
template <typename T>
void DynamicArray<T>::removeUnsortedByIndex(size_t index) 
{
	get(index)->~T();
	memcpy_s(get(index), sizeof(T), get(m_length - 1), sizeof(T));
	m_length--;
}

template <typename T>
T& DynamicArray<T>::operator[](size_t index) 
{
	return *get(index);
}

template <typename T>
const T& DynamicArray<T>::operator[](size_t index) const 
{
	return *get(index);
}

template <typename T>
T* DynamicArray<T>::begin()
{
	return get(0);
}

template <typename T>
const T* DynamicArray<T>::begin() const
{
	return get(0);
}

template <typename T>
T* DynamicArray<T>::end()
{
	return get(m_length);
}

template <typename T>
const T* DynamicArray<T>::end() const
{
	return get(m_length);
}

//PRIVATE

template <typename T>
T* DynamicArray<T>::get(size_t index)
{
	return (T*)m_pData + index;
}

template <typename T>
const T* DynamicArray<T>::get(size_t index) const
{
	return (T*)m_pData + index;
}