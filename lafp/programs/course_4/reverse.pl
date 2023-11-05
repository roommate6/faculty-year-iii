:- [append].

reverse([], []).

reverse([Head | Tail], Result) :-
    reverse(Tail, TailResult),
    append(TailResult, [Head], Result).
