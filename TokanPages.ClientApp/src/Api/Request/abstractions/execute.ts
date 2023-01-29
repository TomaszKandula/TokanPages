import { AxiosRequestConfig } from "axios";

export interface ExecuteContract
{
    configuration: AxiosRequestConfig;
    dispatch: any;
    responseType?: any;
    onSuccessCallback?: any;
}
