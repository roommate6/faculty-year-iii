list_length_stack([],0).

list_length_stack([_|Tail],TotalLength) :-
    list_length_stack(Tail,TailLength),
    TotalLength is TailLength + 1.
