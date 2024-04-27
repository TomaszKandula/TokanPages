import { AxiosRequestConfig } from "axios";
import { ApplicationState } from "../../../../Store/Configuration";

export interface ExecuteContract {
    configuration: AxiosRequestConfig;
    dispatch: (action: any) => void;
    state: () => ApplicationState;
    responseType?: string;
    onSuccessCallback?: () => void;
}
