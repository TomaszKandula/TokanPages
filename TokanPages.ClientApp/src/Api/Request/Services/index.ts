import axios, { AxiosRequestConfig } from "axios";
import { NULL_RESPONSE_ERROR, USER_DATA } from "../../../Shared/constants";
import { GetDataFromStorage } from "../../../Shared/Services/StorageServices";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { AuthenticateUserResultDto } from "../../Models";
import Validate from "validate.js";

import { ExecuteContract, GetContentContract, PromiseResultContract, RequestContract } from "../Abstractions";

const IsSuccessStatusCode = (statusCode: number): boolean => {
    return statusCode >= 200 && statusCode <= 299;
};

export const GetConfiguration = (props: RequestContract): AxiosRequestConfig => {
    const timezoneOffset = new Date().getTimezoneOffset();
    const encoded = GetDataFromStorage({ key: USER_DATA }) as string;

    if (Validate.isEmpty(encoded)) {
        return {
            ...props.configuration,
            withCredentials: false,
            headers: {
                UserTimezoneOffset: timezoneOffset,
            },
        };
    }

    const data = JSON.parse(window.atob(encoded)) as AuthenticateUserResultDto;
    const hasAuthorization = Validate.isObject(data) && !Validate.isEmpty(data.userToken);

    const withAuthorization: any = {
        Authorization: `Bearer ${data.userToken}`,
        UserTimezoneOffset: timezoneOffset,
    };

    const withoutAuthorization: any = {
        UserTimezoneOffset: timezoneOffset,
    };

    const withAuthorizationConfig = { ...props.configuration, withCredentials: true, headers: withAuthorization };
    const withoutAuthorizationConfig = {
        ...props.configuration,
        withCredentials: false,
        headers: withoutAuthorization,
    };

    return hasAuthorization ? withAuthorizationConfig : withoutAuthorizationConfig;
};

export const GetContent = (props: GetContentContract) => {
    let url = props.url;
    if (props.state !== undefined) {
        const id = props.state().applicationLanguage.id as string;
        const queryParam = Validate.isEmpty(id) ? "" : `&language=${id}`;
        url = `${props.url}${queryParam}`;
    }

    props.dispatch({ type: props.request });

    const request: RequestContract = {
        configuration: {
            method: "GET",
            url: url,
            responseType: "json",
        },
    };

    const input: ExecuteContract = {
        configuration: GetConfiguration(request),
        dispatch: props.dispatch,
        responseType: props.receive,
    };

    Execute(input);
};

export const Execute = (props: ExecuteContract): void => {
    axios(props.configuration)
        .then(response => {
            if (!IsSuccessStatusCode(response.status)) {
                RaiseError({
                    dispatch: props.dispatch,
                    errorObject: GetTextStatusCode({ statusCode: response.status }),
                });

                return;
            }

            if (response.data === null) {
                RaiseError({ dispatch: props.dispatch, errorObject: NULL_RESPONSE_ERROR });
                return;
            }

            if (props.responseType !== undefined) {
                if (props.onSuccessCallback === undefined) {
                    props.dispatch({ type: props.responseType, payload: response.data });
                    return;
                }

                props.onSuccessCallback();
                return;
            }
        })
        .catch(error => {
            RaiseError({ dispatch: props.dispatch, errorObject: error });
        });
};

export const ExecuteAsync = async (configuration: AxiosRequestConfig): Promise<PromiseResultContract> => {
    let result: PromiseResultContract = { status: null, content: null, error: null };

    await axios(configuration)
        .then(response => {
            result = {
                status: response.status,
                content: response.data,
                error: null,
            };
        })
        .catch(error => {
            result = {
                status: null,
                content: null,
                error: error,
            };
        });

    return result;
};
