import { LogMessageDto } from "../../../Api/Models";
import { TSeverity } from "../../../Shared/types";
import { UAParser } from "ua-parser-js";
import {
    ExecuteApiActionProps,
    ExecuteApiActionResultProps,
    GetProcessedBody,
    GetProcessedHeaders,
    GetProcessedResponse,
    IsSuccessStatusCode,
    LOG_MESSAGE,
} from "Api";

const ExecuteApiAction = async (props: ExecuteApiActionProps): Promise<ExecuteApiActionResultProps> => {
    let result: ExecuteApiActionResultProps = { };
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
    const executeApiAction = ExecuteApiAction;
    const executeLogAction = ExecuteLogAction;

    return {
        apiAction: executeApiAction,
        logAction: executeLogAction,
    }
}
