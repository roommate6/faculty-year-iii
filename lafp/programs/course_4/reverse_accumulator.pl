:- [last_element_of_list].
:- [pop_back].

reverse_accumulator([], []).

reverse_accumulator(List, [ResultHead | ResultTail]) :-
    last_element_of_list(List, ResultHead),
    pop_back(List, ListWithoutLastElement),
    reverse_accumulator(ListWithoutLastElement, ResultTail).
