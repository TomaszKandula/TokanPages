import { applyMiddleware, combineReducers, compose, createStore } from "redux";
import thunk from "redux-thunk";
import { connectRouter, routerMiddleware } from "connected-react-router";
import { History } from "history";
import { combinedReducers } from "./combinedReducers";
import { combinedDefaults } from "./Defaults/combinedDefaults";
import { IApplicationState } from "./applicationState";

export default function configureStore(history: History, initialState?: IApplicationState) 
{
    const initialAppState = initialState === undefined 
        ? combinedDefaults 
        : initialState;
    
    const middleware = 
    [
        thunk,
        routerMiddleware(history)
    ];

    const rootReducer = combineReducers(
    {
        ...combinedReducers,
        router: connectRouter(history)
    });

    const enhancers = [];
    const windowIfDefined = typeof window === 'undefined' ? null : window as any;
    
    if (windowIfDefined && windowIfDefined.__REDUX_DEVTOOLS_EXTENSION__) 
    {
        enhancers.push(windowIfDefined.__REDUX_DEVTOOLS_EXTENSION__());
    };

    return createStore(
        rootReducer,
        initialAppState,
        compose(applyMiddleware(...middleware), ...enhancers)
    );
}
