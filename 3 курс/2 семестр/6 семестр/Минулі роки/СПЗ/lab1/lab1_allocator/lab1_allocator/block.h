#pragma once


class block
{
	public:
		block(void)		// constructor
		{
			start = 0;
			size = 0;
			used = false;
		}
		~block(void)	// destructor
		{
		}
		bool getUsed()
		{
			return used;
		}
		void setUsed(bool _used)
		{
			used = _used;
		}

		size_t getStart()
		{
			return start;
		}
		void setStart(size_t _start)
		{
			start = _start;
		}

		size_t getSize()
		{
			return size;
		}
		void setSize(size_t _size)
		{
			size = _size;
		}

	private:
		bool used;		// usage flag
		size_t start;	// block start
		size_t size;	// block size
};
