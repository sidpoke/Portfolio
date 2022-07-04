#pragma once
#include <memory>

template <typename T>
class DynamicArray
{
public:
				DynamicArray();
				~DynamicArray();

	void		create( size_t initialCapacity = 0u );
	void		destroy();

	bool		isEmpty() const;
	bool		hasElements() const;
	size_t		getLength() const;
	size_t		getCapacity() const;

	void		clear();
	void		resize( size_t length );
	void		reserve( size_t length );

	T&			push_back();
	void		push_back( const T& value );
	T&			push_front();
	void		push_front(const T& value);

	void		pop_back();
	void		pop_back( T& target );
	void		pop_front();
	void		pop_front(T& target);

	void		removeSortedByIndex( size_t index);
	void		removeUnsortedByIndex( size_t index );

	T*			begin();
	T*			end();
	const T*	begin() const;
	const T*	end() const;

	T&			operator[]( size_t index );
	const T&	operator[]( size_t index ) const;

private:
	void*		m_pData;
	size_t		m_capacity = 0u;
	size_t		m_length = 0u;

	T*			get( size_t index );
	const T*	get( size_t index ) const;
};

#include "DynamicArray.inl"