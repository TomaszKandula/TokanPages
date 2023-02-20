import Validate from "validate.js";
import { UNEXPECTED_ERROR, VALIDATION_ERRORS } from "../../../../Shared/constants";
import { ErrorDto } from "../../../../Api/Models";

export interface Properties
{
    errorObject: any
}

export const GetErrorMessage = (props: Properties): string =>
{
    console.error(props.errorObject);
    let result: string = UNEXPECTED_ERROR;

    if (props.errorObject?.response?.data)
    {
        const parsedJson: ErrorDto = props.errorObject.response.data as ErrorDto;
        result = Validate.isEmpty(parsedJson.errorMessage) ? UNEXPECTED_ERROR : parsedJson.errorMessage;

        if (parsedJson.validationErrors !== null) 
        {
            result = `${result}. ${VALIDATION_ERRORS}.`;
        }
    }

    return result;
}
