import { applyMiddleware, combineReducers, compose, createStore } from "redux";
import thunk from "redux-thunk";
import { connectRouter, routerMiddleware } from "connected-react-router";
import { History } from "history";
import { ApplicationReducer } from "./applicationReducer";
import { ApplicationDefault } from "./applicationDefault";
import { IApplicationState } from "./applicationState";

export const ConfigureStore = (history: History, initialState?: IApplicationState): any =>
{
    const initialAppState = initialState === undefined 
        ? ApplicationDefault 
        : initialState;
    
    const middleware = 
    [
        thunk,
        routerMiddleware(history)
    ];

    const rootReducer = combineReducers(
    {
        ...ApplicationReducer,
        router: connectRouter(history)
    });

    const enhancers = [];
    const windowIfDefined = typeof window === "undefined" ? null : window as any;
    
    if (windowIfDefined && windowIfDefined.__REDUX_DEVTOOLS_EXTENSION__) 
    {
        enhancers.push(windowIfDefined.__REDUX_DEVTOOLS_EXTENSION__());
    }

    return createStore(
        rootReducer,
        initialAppState,
        compose(applyMiddleware(...middleware), ...enhancers)
    );
}
