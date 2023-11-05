pop_back([ _ ], []).

pop_back([FirstElement, SecondElement | Tail], [ResultHead | ResultTail]) :-
    ResultHead is FirstElement,
    pop_back([SecondElement | Tail], ResultTail).
