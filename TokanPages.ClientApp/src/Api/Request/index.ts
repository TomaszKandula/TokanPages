import axios, { AxiosRequestConfig } from "axios";
import { USER_DATA } from "../../Shared/constants";
import { GetDataFromStorage } from "../../Shared/Services/StorageServices";
import { IAuthenticateUserResultDto } from "../Models";
import Validate from "validate.js";

interface IPromiseResult 
{
    status: number | null;
    content: any | null;
    error: any | null;
}

interface IHeaders 
{
    Authorization: string;
}

export const GetAuthorizationHeaders = (): IHeaders | undefined => 
{
    const userData = GetDataFromStorage({ key: USER_DATA }) as IAuthenticateUserResultDto;
    let headers = undefined;

    if (Validate.isObject(userData) && !Validate.isEmpty(userData.userToken))
    {
        headers = 
        {
            Authorization: `Bearer ${userData.userToken}`
        }
    }

    return headers;
}

export const EnrichConfiguration = (config: AxiosRequestConfig): AxiosRequestConfig => 
{
    const headers = GetAuthorizationHeaders();
    return headers === undefined
        ? {...config} 
        : {...config, headers: headers};
}

export const ApiCall = async (configuration: AxiosRequestConfig): Promise<IPromiseResult> =>
{
    let result: IPromiseResult = 
    {
        status: null,
        content: null,
        error: null
    };

    await axios(configuration)
    .then(response =>
    {
        result = 
        {
            status: response.status,
            content: response.data,
            error: null
        }
    })
    .catch(error =>
    {
        result = 
        { 
            status: null,
            content: null,
            error: error 
        };
    });

    return result;
}
