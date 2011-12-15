#pragma once

generic<typename TFirst, typename TSecond>
public ref class Pair
{
    public:
	    Pair(TFirst first, TSecond second)
        {
	        First = first;
	        Second = second;
        };
	    property TFirst First;
	    property TSecond Second;
};