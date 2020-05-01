import React, { Dispatch, useReducer } from "react";
import { Actions, initialState, IState, reducer } from "./reducer";

interface IContextProps {
    state: IState;
    dispatch: Dispatch<Actions>;
}

export const AppContext = React.createContext({} as IContextProps);

export function AppContextProvider(props: any) {
    const [state, dispatch] = useReducer(reducer, initialState);

    const value = { state, dispatch };
    return (
        <AppContext.Provider value={value}>{props.children}</AppContext.Provider>
    );
}