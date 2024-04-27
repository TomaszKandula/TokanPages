import { ApplicationState } from "../../../../Store/Configuration";

export interface GetContentContract {
    dispatch: (action: any) => void;
    state: () => ApplicationState;
    request: string;
    receive: string;
    url: string;
}
