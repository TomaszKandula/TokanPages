import { ApplicationState } from "./applicationState";

export interface ApplicationAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
