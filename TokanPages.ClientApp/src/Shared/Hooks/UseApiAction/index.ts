import { LogMessageDto } from "../../../Api/Models";
import { TSeverity } from "../../../Shared/types";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { UAParser } from "ua-parser-js";
import {
    ExecuteApiActionProps,
    ExecuteApiActionResultProps,
    ExecuteStoreActionProps,
    GetProcessedBody,
    GetProcessedHeaders,
    GetProcessedResponse,
    IsSuccessStatusCode,
    LOG_MESSAGE,
} from "../../../Api";

const ExecuteStoreAction = async (props: ExecuteStoreActionProps): Promise<void> => {
    if (!props.dispatch || !props.state) {
        return;
    }

    const state = props.state();
    const components = state.contentPageData.components;
    const content = components.templates.templates.application;

    try {
        const body = GetProcessedBody(props.configuration);
        const hasFormData = body instanceof FormData;
        const headers = GetProcessedHeaders(hasFormData, props.configuration.headers);
        const response = await fetch(props.url, {
            credentials: "include",
            method: props.configuration.method,
            headers: headers,
            body: body,
        });

        const isJson = props.configuration.hasJsonResponse;
        const data = await GetProcessedResponse(response, isJson);

        if (IsSuccessStatusCode(response.status)) {
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
            RaiseError({
                dispatch: props.dispatch,
                errorObject: data,
                content: content,
            });
        }
    } catch (exception) {
        RaiseError({
            dispatch: props.dispatch,
            errorObject: exception,
            content: content,
        });
    }
};

const ExecuteApiAction = async (props: ExecuteApiActionProps): Promise<ExecuteApiActionResultProps> => {
    let result: ExecuteApiActionResultProps = {};
    try {
        const body = GetProcessedBody(props.configuration);
        const hasFormData = body instanceof FormData;
        const headers = GetProcessedHeaders(hasFormData, props.configuration.headers);
        const response = await fetch(props.url, {
            credentials: "include",
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
            };
        } else {
            throw new Error("Unexpected error");
        }
    } catch (exception) {
        result = {
            error: exception,
        };
    }

    return result;
};

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

export const useApiAction = () => {
    return {
        storeAction: ExecuteStoreAction,
        apiAction: ExecuteApiAction,
        logAction: ExecuteLogAction,
    };
};
