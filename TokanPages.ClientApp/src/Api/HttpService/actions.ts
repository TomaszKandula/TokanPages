import { RaiseError } from "../../Shared/Services/ErrorServices";
import { LOG_MESSAGE } from "../../Api/Paths";
import { LogMessageDto } from "../Models";
import { TSeverity } from "../../Shared/types";
import {
    ExecuteApiActionProps,
    ExecuteStoreActionProps,
    ExecuteContentActionProps,
    GetProcessedHeaders,
    IsSuccessStatusCode,
    GetProcessedResponse,
    ExecuteApiActionResultProps,
    GetBaseHeaders,
    GetProcessedBody,
} from "./utils";
import { UAParser } from "ua-parser-js";
import Validate from "validate.js";

export const ExecuteLogAction = async (
    message: object,
    stackTrace: object,
    eventType: string,
    severity: TSeverity
): Promise<void> => {
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
};

export const ExecuteContentAction = (props: ExecuteContentActionProps): void => {
    let url = props.url;
    if (props.state !== undefined) {
        const id = props.state().applicationLanguage.id as string;
        const queryParam = Validate.isEmpty(id) ? "" : `&language=${id}`;
        url = `${props.url}${queryParam}`;
    }

    props.dispatch({ type: props.request });

    const headers = GetBaseHeaders();
    const input: ExecuteStoreActionProps = {
        dispatch: props.dispatch,
        state: props.state,
        url: url,
        responseType: props.receive,
        configuration: {
            method: "GET",
            headers: headers,
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
        const body = GetProcessedBody(props);
        const hasFormData = body instanceof FormData;
        const headers = GetProcessedHeaders(hasFormData, props.configuration.headers);
        const response = await fetch(props.url, {
            method: props.configuration.method,
            headers: headers,
            body: body,
        });

        if (IsSuccessStatusCode(response.status)) {
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
        const body = GetProcessedBody(props);
        const hasFormData = body instanceof FormData;
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
