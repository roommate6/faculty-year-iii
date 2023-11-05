list_length_accumulator([],Accumulator,Accumulator).

list_length_accumulator([_|Tail],Accumulator,TotalLength) :-
    TailAccumulator is Accumulator + 1,
    list_length_accumulator(Tail,TailAccumulator,TotalLength).

list_length_accumulator(List,TotalLength) :-
    list_length_accumulator(List, 0, TotalLength).
