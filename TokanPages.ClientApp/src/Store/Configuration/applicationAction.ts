import { IApplicationState } from "./applicationState";

export interface IApplicationAction<TAction> 
{
    (dispatch: (action: TAction) => void, getState: () => IApplicationState): void;
}
