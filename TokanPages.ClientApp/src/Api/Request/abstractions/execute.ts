import { AxiosRequestConfig } from "axios";

export interface IExecute
{
    configuration: AxiosRequestConfig;
    dispatch: any;
    responseType?: any;
    onSuccessCallback?: any;
}
