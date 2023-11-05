append([], [], []).

append([FirstListHead | FirstListTail], SecondList, [ResultHead | ResultTail]) :-
    ResultHead is FirstListHead,
    append(FirstListTail, SecondList, ResultTail).

append([], [SecondListHead | SecondListTail], [ResultHead | ResultTail]) :-
    ResultHead is SecondListHead,
    append([], SecondListTail, ResultTail).
