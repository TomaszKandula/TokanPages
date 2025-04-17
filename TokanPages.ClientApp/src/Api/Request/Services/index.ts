import { ApplicationState } from "../../../Store/Configuration";
import { USER_DATA } from "../../../Shared/constants";
import { GetDataFromStorage } from "../../../Shared/Services/StorageServices";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { AuthenticateUserResultDto } from "../../Models";
import Validate from "validate.js";
import base64 from "base-64";
import utf8 from "utf8";

export interface Configuration {
    method: string;
    body?: object;
    headers?: Headers;
}

export interface StoreProps {
    dispatch: (action: any) => void;
    state: () => ApplicationState;
}

export interface PromiseResult {
    status: number | null;
    content: any | null;
    error: any | null;
}

export interface GetContentRequest extends StoreProps {
    request: string;
    receive: string;
    url: string;
}

export interface ExecuteActionRequest {
    url: string;
    configuration: Configuration;
}

export interface ExecuteRequest extends ExecuteActionRequest, StoreProps {
    optionalHandle?: string;
    responseType?: string | string[];
}

const IsSuccessStatusCode = (statusCode: number): boolean => {
    return statusCode >= 200 && statusCode <= 299;
};

const SetupHeaders = (): Headers => {
    const headers = new Headers();
    const timezoneOffset = new Date().getTimezoneOffset();
    const encoded = GetDataFromStorage({ key: USER_DATA }) as string;

    if (Validate.isEmpty(encoded)) {
        headers.append("UserTimezoneOffset", timezoneOffset.toString());
        headers.append("Content-Type", "application/json");
        return headers;
    }

    const decoded = base64.decode(encoded);
    const text = utf8.decode(decoded);
    const data = JSON.parse(text) as AuthenticateUserResultDto;
    const hasAuthorization = Validate.isObject(data) && !Validate.isEmpty(data.userToken);

    if (hasAuthorization) {
        headers.append("Authorization", `Bearer ${data.userToken}`);
        headers.append("UserTimezoneOffset", timezoneOffset.toString());
        headers.append("Content-Type", "application/json");
    } else {
        headers.append("UserTimezoneOffset", timezoneOffset.toString());
        headers.append("Content-Type", "application/json");
    }

    return headers;
};

export const GetContentAction = (props: GetContentRequest): void => {
    let url = props.url;
    if (props.state !== undefined) {
        const id = props.state().applicationLanguage.id as string;
        const queryParam = Validate.isEmpty(id) ? "" : `&language=${id}`;
        url = `${props.url}${queryParam}`;
    }

    props.dispatch({ type: props.request });

    const input: ExecuteRequest = {
        dispatch: props.dispatch,
        state: props.state,
        url: url,
        responseType: props.receive,
        configuration: {
            method: "GET",
            headers: SetupHeaders(),
        },
    };

    DispatchExecuteAction(input);
};

export const DispatchExecuteAction = async (props: ExecuteRequest): Promise<void> => {
    if (!props.dispatch || !props.state) {
        return;
    }

    const state = props.state();
    const components = state.contentPageData.components;
    const content = components.templates.templates.application;
    
    try {
        const optionalBody = props.configuration.body
        ? JSON.stringify(props.configuration.body)
        : null;

        const response = await fetch(props.url, {
            method: props.configuration.method,
            headers: SetupHeaders(),
            body: optionalBody,
        });

        if (IsSuccessStatusCode(response.status)){
            const data = await response.json();
            if (props.responseType !== undefined) {
                if (Array.isArray(props.responseType)) {
                    props.responseType.forEach(item => {
                        props.dispatch({ type: item, payload: data, handle: props.optionalHandle });
                    });
                } else {
                    props.dispatch({ type: props.responseType, payload: data, handle: props.optionalHandle });
                }
            } else {
                throw new Error(content.unexpectedError);
            }
        } else {
            const statusCode = response.status.toString();
            const statusText = content.unexpectedStatus.replace("{STATUS_CODE}", statusCode);
            throw new Error(statusText);
        }
    } catch (exception) {
        console.error(exception);
        RaiseError({
            dispatch: props.dispatch,
            errorObject: exception,
            content: content,
        });
    }
};

//rename: ExecuteAction
export const ExecuteAsync = async (props: ExecuteActionRequest): Promise<PromiseResult> => {
    let result: PromiseResult = { status: null, content: null, error: null };

    try {
        const optionalBody = props.configuration.body
        ? JSON.stringify(props.configuration.body)
        : null;

        const response = await fetch(props.url, {
            method: props.configuration.method,
            //headers: GetConfiguration(),
            body: optionalBody,
        });

        if (IsSuccessStatusCode(response.status)) {
            const data = await response.json();
            result = {
                status: response.status,
                content: data,
                error: null,
            };
        } else {
            throw new Error("Unexpected error!"); 
        }
    } catch (exception) {
        result = {
            status: null,
            content: null,
            error: exception,
        };
    }

    return result;
};
