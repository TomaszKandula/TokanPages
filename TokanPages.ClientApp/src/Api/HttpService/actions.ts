import { RaiseError } from "../../Shared/Services/ErrorServices";
import {
    ExecuteStoreActionProps,
    GetProcessedHeaders,
    IsSuccessStatusCode,
    GetProcessedResponse,
    GetProcessedBody,
} from "./utils";

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
