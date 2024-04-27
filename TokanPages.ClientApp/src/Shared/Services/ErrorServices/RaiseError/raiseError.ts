import { RAISE } from "../../../../Store/Actions/Application/applicationError";
import { ErrorDto } from "../../../../Api/Models";
import Validate from "validate.js";

interface BaseErrorProps {
    errorObject: any;
    content: {
        unexpectedStatus: string;
        unexpectedError: string;
        validationError: string;
        nullError: string;
    };
}

export interface RaiseErrorProps extends BaseErrorProps {
    dispatch: any;
}

const GetErrorMessage = (props: BaseErrorProps): string => {
    let result: string = props.content.unexpectedError;
    let validationError: string = "";

    if (props.errorObject?.response?.data) {
        const parsed: ErrorDto = props.errorObject.response.data as ErrorDto;
        result = Validate.isEmpty(parsed.errorMessage) ? props.content.unexpectedError : parsed.errorMessage;

        if (parsed.validationErrors && parsed.validationErrors !== null && parsed.validationErrors.length > 0) {
            result = `${result}. ${props.content.validationError}:`;

            parsed.validationErrors!.forEach(item => {
                validationError = `${validationError} [${item.propertyName}] ${item.errorMessage.toLowerCase()};`;
            });

            result = result + validationError;
        }
    }

    return result;
};

export const RaiseError = (props: RaiseErrorProps): string => {
    let error: string = props.content.unexpectedError;
    const errorObjectType = typeof props.errorObject;

    if (errorObjectType === "object") {
        error = GetErrorMessage({
            errorObject: props.errorObject,
            content: props.content,
        });
    } else if (errorObjectType === "string") {
        error = props.errorObject;
    }

    props.dispatch({ type: RAISE, errorDetails: error });
    return error;
};
