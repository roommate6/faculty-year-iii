last_element_of_list([Head], Head).

last_element_of_list([ _ | Tail], Result) :-
    last_element_of_list(Tail, Result).
