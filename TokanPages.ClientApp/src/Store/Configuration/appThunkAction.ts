import { IApplicationState } from "./applicationState";

export interface IAppThunkAction<TAction> 
{
    (dispatch: (action: TAction) => void, getState: () => IApplicationState): void;
}
