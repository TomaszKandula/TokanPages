import { UNEXPECTED_ERROR } from "../../../../Shared/constants";
import { RAISE_ERROR } from "../../../../Store/Actions/Application/applicationError";
import { GetErrorMessage } from "../GetErrorMessage/getErrorMessage";
import { IRaiseError } from "./interface";

export const RaiseError = (props: IRaiseError): string =>
{
    let error: string = UNEXPECTED_ERROR;
    const errorObjectType = typeof(props.errorObject);

    if (errorObjectType === "object")
    {
        error = GetErrorMessage({ errorObject: props.errorObject });
    }
    else if (errorObjectType === "string")
    {
        error = props.errorObject;
    }

    props.dispatch({ type: RAISE_ERROR, errorObject: error });
    return error;
}
