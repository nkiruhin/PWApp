export interface IState {
    isRefresh: boolean;
    balance: number;
}

interface IRefresh {
    type: "REFRESH";
    payload: IState
}

export type Actions = IRefresh;

export const initialState: IState = {
    isRefresh: false,
    balance: 0
};

export const reducer = (state: IState, action: Actions) => {
    switch (action.type) {
        case "REFRESH":
            return { ...state, balance: action.payload.balance, isRefresh: action.payload.isRefresh };
        default:
            return state
    }
};