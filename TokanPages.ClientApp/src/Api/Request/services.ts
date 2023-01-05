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

export const EnrichConfiguration = (configuration: AxiosRequestConfig): AxiosRequestConfig => 
{
    const userData = GetDataFromStorage({ key: USER_DATA }) as IAuthenticateUserResultDto;
    const hasAuthorization = Validate.isObject(userData) && !Validate.isEmpty(userData.userToken);
    const timezoneOffset = new Date().getTimezoneOffset();

    const withAuthorization: any = 
    {
        Authorization: `Bearer ${userData.userToken}`,
        UserTimezoneOffset: timezoneOffset
    }

    const withoutAuthorization: any = 
    {
        UserTimezoneOffset: timezoneOffset
    }

    const withAuthorizationConfig = {...configuration, withCredentials: true, headers: withAuthorization};
    const withoutAuthorizationConfig = {...configuration, withCredentials: false, headers: withoutAuthorization};

    return hasAuthorization ? withAuthorizationConfig : withoutAuthorizationConfig;
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
