import Validate from "validate.js";
import { UNEXPECTED_ERROR, VALIDATION_ERRORS } from "../../../../Shared/constants";
import { IErrorDto } from "../../../../Api/Models";
import { IGetErrorMessage } from "./interface";

export const GetErrorMessage = (props: IGetErrorMessage): string =>
{
    console.error(props.errorObject);
    let result: string = UNEXPECTED_ERROR;

    if (props.errorObject?.response?.data)
    {
        const parsedJson: IErrorDto = props.errorObject.response.data as IErrorDto;
        result = Validate.isEmpty(parsedJson.ErrorMessage) 
            ? UNEXPECTED_ERROR 
            : parsedJson.ErrorMessage;

        if (parsedJson.ValidationErrors !== null) 
        {
            result = result + ", " + VALIDATION_ERRORS + ".";
        }
    }

    return result;
}
