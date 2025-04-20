import { ApplicationState } from "../../Store/Configuration";
import { USER_DATA } from "../../Shared/constants";
import { GetDataFromStorage } from "../../Shared/Services/StorageServices";
import { RaiseError } from "../../Shared/Services/ErrorServices";
import { LOG_MESSAGE } from "../../Api/Paths";
import { AuthenticateUserResultDto, LogMessageDto } from "../Models";
import { TSeverity } from "../../Shared/types";
import { UAParser } from "ua-parser-js";
import Validate from "validate.js";
import base64 from "base-64";
import utf8 from "utf8";

export interface HeadersProps {
    key: string;
    value: string;
}

export interface ConfigurationProps {
    method: string;
    body?: object;
    form?: FormData;
    headers?: HeadersProps[];
    hasJsonResponse: boolean;
}

export interface StoreProps {
    dispatch: (action: any) => void;
    state: () => ApplicationState;
}

export interface ExecuteApiActionResultProps {
    status: number | null;
    content: any | null;
    error: any | null;
}

export interface ExecuteContentActionProps extends StoreProps {
    request: string;
    receive: string;
    url: string;
}

export interface ExecuteApiActionProps {
    url: string;
    configuration: ConfigurationProps;
}

export interface ExecuteStoreActionProps extends ExecuteApiActionProps, StoreProps {
    optionalHandle?: string;
    responseType?: string | string[];
}

const IsSuccessStatusCode = (statusCode: number): boolean => {
    return statusCode >= 200 && statusCode <= 299;
};

const GetBaseHeaders = (): HeadersProps[] => {
    const timezoneOffset = new Date().getTimezoneOffset();
    const encoded = GetDataFromStorage({ key: USER_DATA }) as string;

    const headers: HeadersProps[] = [];
    headers.push({ key: "UserTimezoneOffset", value: timezoneOffset.toString() });
    headers.push({ key: "Content-Type", value: "application/json" });

    if (Validate.isEmpty(encoded)) {
        return headers;
    }

    const decoded = base64.decode(encoded);
    const text = utf8.decode(decoded);
    const data = JSON.parse(text) as AuthenticateUserResultDto;
    const hasAuthorization = Validate.isObject(data) && !Validate.isEmpty(data.userToken);

    if (hasAuthorization) {
        headers.push({ key: "Authorization", value: `Bearer ${data.userToken}`});
    }

    return headers;
};

const GetProcessedHeaders = (hasFormData: boolean, configurationHeaders?: HeadersProps[]): Headers => {
    const headers = new Headers();
    const baseHeaders = GetBaseHeaders();
    baseHeaders.forEach(header => {
        if (hasFormData && header.key === "Content-Type") {
            return;
        }

        headers.append(header.key, header.value);
    });

    if (Array.isArray(configurationHeaders)) {
        configurationHeaders.forEach(header => {
            if (header.key === "Content-Type" && headers.has("Content-Type")) {
                headers.delete("Content-Type");
            }

            headers.append(header.key, header.value);
        });
    }

    return headers;
}

const GetProcessedResponse = async (response: Response, isJson?: boolean): Promise<object | string> => {
    if (isJson) {
        return await response.json();
    } else {
        return await response.text();
    }
}

export const ExecuteLogAction = async (message: object, stackTrace: object, eventType: string, severity: TSeverity): Promise<void> => {
    const ua = UAParser(window.navigator.userAgent);

    const logMessage: LogMessageDto = {
        eventDateTime: new Date().toISOString(),
        eventType: eventType,
        severity: severity,
        message: JSON.stringify(message),
        stackTrace: JSON.stringify(stackTrace),
        pageUrl: window.location.href,
        userAgent: window.navigator.userAgent,
        clientData: {
            browser: {
                major: ua.browser.major,
                name: ua.browser.name,
                type: ua.browser.type,
                version: ua.browser.version,
            },
            cpu: {
                architecture: ua.cpu.architecture,
            },
            device: {
                model: ua.device.model,
                type: ua.device.type,
                vendor: ua.device.vendor,
            },
            engine: {
                name: ua.engine.name,
                version: ua.engine.version,
            },
            os: {
                name: ua.os.name,
                version: ua.os.version,
            },
        },
    };

    const request: ExecuteApiActionProps = {
        url: LOG_MESSAGE,
        configuration: {
            method: "POST",
            hasJsonResponse: true,
            body: logMessage,
        },
    };

    await ExecuteApiAction(request);
}

export const ExecuteContentAction = (props: ExecuteContentActionProps): void => {
    let url = props.url;
    if (props.state !== undefined) {
        const id = props.state().applicationLanguage.id as string;
        const queryParam = Validate.isEmpty(id) ? "" : `&language=${id}`;
        url = `${props.url}${queryParam}`;
    }

    props.dispatch({ type: props.request });

    const input: ExecuteStoreActionProps = {
        dispatch: props.dispatch,
        state: props.state,
        url: url,
        responseType: props.receive,
        configuration: {
            method: "GET",
            headers: GetBaseHeaders(),
            hasJsonResponse: true,
        },
    };

    ExecuteStoreAction(input);
};

export const ExecuteStoreAction = async (props: ExecuteStoreActionProps): Promise<void> => {
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

        const optionalFormData = props.configuration.form
        ? props.configuration.form
        : null;

        const body = optionalBody !== null 
        ? optionalBody 
        : optionalFormData !== null 
        ? optionalFormData 
        : null;

        const hasFormData = optionalFormData !== null;
        const headers = GetProcessedHeaders(hasFormData, props.configuration.headers);
        const response = await fetch(props.url, {
            method: props.configuration.method,
            headers: headers,
            body: body,
        });

        if (IsSuccessStatusCode(response.status)){
            const isJson = props.configuration.hasJsonResponse;
            const data = await GetProcessedResponse(response, isJson);

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

export const ExecuteApiAction = async (props: ExecuteApiActionProps): Promise<ExecuteApiActionResultProps> => {
    let result: ExecuteApiActionResultProps = { status: null, content: null, error: null };

    try {
        const optionalBody = props.configuration.body
        ? JSON.stringify(props.configuration.body)
        : null;

        const optionalFormData = props.configuration.form
        ? props.configuration.form
        : null;

        const body = optionalBody !== null 
        ? optionalBody 
        : optionalFormData !== null 
        ? optionalFormData 
        : null;

        const hasFormData = optionalFormData !== null;
        const headers = GetProcessedHeaders(hasFormData, props.configuration.headers);
        const response = await fetch(props.url, {
            method: props.configuration.method,
            headers: headers,
            body: body,
        });

        if (IsSuccessStatusCode(response.status)) {
            const isJson = props.configuration.hasJsonResponse;
            const data = await GetProcessedResponse(response, isJson);

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
