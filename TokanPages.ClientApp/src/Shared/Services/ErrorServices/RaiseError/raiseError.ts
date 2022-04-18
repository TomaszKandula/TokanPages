import { RAISE_ERROR } from "../../../../Redux/Actions/raiseErrorAction";
import { GetErrorMessage } from "../GetErrorMessage/getErrorMessage";
import { IRaiseError } from "./interface";

export const RaiseError = (props: IRaiseError): string =>
{
    let error = typeof(props.errorObject) !== "string" ? GetErrorMessage(props.errorObject) : props.errorObject;
    props.dispatch({ type: RAISE_ERROR, errorObject: error });
    return "";
}
